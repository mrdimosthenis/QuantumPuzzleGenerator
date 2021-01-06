module QuantumPuzzleGenerator.Selection

open System

open FSharpx.Collections

open QuantumPuzzleMechanics

let distinctQStates (numOfQubits: int)
                    (differenceThreshold: Number.Number)
                    (reachedQStates: Matrix.Matrix LazyList)
                    (candidateGate: Quantum.Gate.Gate)
                    : Matrix.Matrix LazyList option =
    let gateMatrix =
        Quantum.Gate.matrix numOfQubits candidateGate

    let newQStates =
        LazyList.map (Matrix.standardProduct gateMatrix) reachedQStates

    let allQStates =
        LazyList.append newQStates reachedQStates

    let areNewQStatesDistinct =
        Seq.forall (fun newQState ->
            allQStates
            |> LazyList.filter (Matrix.almostEqual differenceThreshold newQState)
            |> LazyList.length
            |> ((=) 1)) newQStates

    if areNewQStatesDistinct then Some allQStates else None

let rec selectNextGateAndQStates (random: Random)
                                 (maxGateType: Quantum.Gate.MaxGateType)
                                 (numOfQubits: int)
                                 (differenceThreshold: Number.Number)
                                 (reachedQStates: Matrix.Matrix LazyList)
                                 : Quantum.Gate.Gate * Matrix.Matrix LazyList =
    let candidateGate =
        match maxGateType with
        | Quantum.Gate.SingleQubit -> Generator.nextGate1 random numOfQubits ()
        | Quantum.Gate.DoubleQubit ->
            if random.NextDouble() < 0.5
            then Generator.nextGate1 random numOfQubits ()
            else Generator.nextGate2 random numOfQubits ()
        | Quantum.Gate.TripleQubit ->
            match random.NextDouble() with
            | x when x < 0.33 -> Generator.nextGate1 random numOfQubits ()
            | x when x < 0.66 -> Generator.nextGate2 random numOfQubits ()
            | _ -> Generator.nextGate3 random numOfQubits ()

    match distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate with
    | Some qStates -> (candidateGate, qStates)
    | None -> selectNextGateAndQStates random maxGateType numOfQubits differenceThreshold reachedQStates

let selectGatesAndQStates (random: Random)
                          (maxGateType: Quantum.Gate.MaxGateType)
                          (numOfGates: int)
                          (numOfQubits: int)
                          (isAbsoluteInitQState: bool)
                          (differenceThreshold: Number.Number)
                          : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =

    let rec go (selectedGates: Quantum.Gate.Gate LazyList)
               (selectedQStates: Matrix.Matrix LazyList)
               (n: int)
               : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =
        if n < numOfGates then
            let (nextGate, nextSelectedQStates) =
                selectNextGateAndQStates random maxGateType numOfQubits differenceThreshold selectedQStates

            let nextSelectedGates = LazyList.cons nextGate selectedGates
            n + 1 |> go nextSelectedGates nextSelectedQStates
        else
            (selectedGates, selectedQStates)

    let initSelectedGates = LazyList.empty

    let initSelectedQStates =
        if isAbsoluteInitQState
        then Generator.nextAbsolutQState random numOfQubits ()
        else Generator.nextQState random numOfQubits ()
        |> Seq.singleton
        |> LazyList.ofSeq

    go initSelectedGates initSelectedQStates 0

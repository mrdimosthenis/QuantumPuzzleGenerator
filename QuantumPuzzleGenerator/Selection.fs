module QuantumPuzzleGenerator.Selection

open System

open FSharpx.Collections

open QuantumPuzzleMechanics

let distinctQStates
        (numOfQubits: int)
        (differenceThreshold: Number.Number)
        (reachedQStates: Matrix.Matrix LazyList)
        (candidateGate: Quantum.Gate.Gate)
    : Matrix.Matrix LazyList option =
    let gateMatrix = Quantum.Gate.matrix numOfQubits candidateGate
    let newQstates = LazyList.map (Matrix.standardProduct gateMatrix) reachedQStates
    let allQstates = LazyList.append newQstates reachedQStates
    let areNewQstatesDistinct =
            Seq.forall
                    (fun newQState ->
                        allQstates
                        |> LazyList.filter (Matrix.almostEqual differenceThreshold newQState)
                        |> LazyList.length
                        |> ((=) 1)
                    )
                    newQstates
    if areNewQstatesDistinct
        then Some allQstates
        else None

let rec selectNextGateAndQStates
        (random: Random)
        (maxGateType: int)
        (numOfQubits: int)
        (differenceThreshold: Number.Number)
        (reachedQStates: Matrix.Matrix LazyList)
    : Quantum.Gate.Gate * Matrix.Matrix LazyList =
    let candidateGate =
            match maxGateType with
            | 1 -> Generator.nextGate1 random numOfQubits ()
            | 2 -> if random.NextDouble() < 0.5
                        then Generator.nextGate1 random numOfQubits ()
                        else Generator.nextGate2 random numOfQubits ()
            | _ -> match random.NextDouble() with
                   | x when x < 0.33 -> Generator.nextGate1 random numOfQubits ()
                   | x when x < 0.66 -> Generator.nextGate2 random numOfQubits ()
                   | _ -> Generator.nextGate3 random numOfQubits ()
    match distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate with
    | Some qStates -> (candidateGate, qStates)
    | None -> selectNextGateAndQStates random maxGateType numOfQubits differenceThreshold reachedQStates

let selectGatesAndQStates
        (random: Random)
        (maxGateType: int)
        (numOfGates: int)
        (numOfQubits: int)
        (differenceThreshold: Number.Number)
    : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =

    let rec go (selectedGates: Quantum.Gate.Gate LazyList)
               (selectedQStates: Matrix.Matrix LazyList)
               (n: int)
        : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =
        if n < numOfGates
            then let (nextGate, nextSelectedQStates) =
                        selectNextGateAndQStates
                                        random
                                        maxGateType
                                        numOfQubits
                                        differenceThreshold
                                        selectedQStates
                 let nextSelectedGates = LazyList.cons nextGate selectedGates
                 n + 1 |> go nextSelectedGates nextSelectedQStates
            else (selectedGates, selectedQStates)
    
    let initSelectedGates = LazyList.empty
    let initSelectedQStates =
            LazyList.ofList [ Generator.nextQState random numOfQubits () ]
    go initSelectedGates initSelectedQStates 0

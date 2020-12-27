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
        (numOfQubits: int)
        (differenceThreshold: Number.Number)
        (reachedQStates: Matrix.Matrix LazyList)
    : Quantum.Gate.Gate * Matrix.Matrix LazyList =
    let candidateGate = Generator.nextGate random numOfQubits ()
    match distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate with
    | Some qStates -> (candidateGate, qStates)
    | None -> selectNextGateAndQStates random numOfQubits differenceThreshold reachedQStates

let selectGatesAndQStates
        (random: Random)
        (numOfGates: int)
        (numOfQubits: int)
        (differenceThreshold: Number.Number)
    : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =

    let rec go (selectedGates: Quantum.Gate.Gate LazyList)
               (selectedQStates: Matrix.Matrix LazyList)
        : Quantum.Gate.Gate LazyList * Matrix.Matrix LazyList =
        if LazyList.length selectedGates = numOfGates
            then (selectedGates, selectedQStates)
            else let (nextGate, nextSelectedQStates) =
                        selectNextGateAndQStates random
                                                 numOfQubits
                                                 differenceThreshold
                                                 selectedQStates
                 let nextSelectedGates = LazyList.cons nextGate selectedGates
                 go nextSelectedGates nextSelectedQStates
    
    let initSelectedGates = LazyList.empty
    let initSelectedQStates =
            LazyList.ofList [ Generator.nextQState random numOfQubits () ]
    go initSelectedGates initSelectedQStates

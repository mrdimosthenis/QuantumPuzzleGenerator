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

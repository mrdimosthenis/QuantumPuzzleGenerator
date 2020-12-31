module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Msg = 
    | GateClick of int
    | Msg2

type Model = { Level: int
               TargetIndex: int
               TargetQStates: Matrix.Matrix list
               GateSelection: bool list
               Gates: Quantum.Gate.Gate list }

// update function

let update (msg: Msg) (model: Model) =
    match msg with
    | _ -> model, Cmd.none

// model functions

let numOfQubits (model: Model): int =
    match model.Level with
    | 1 -> 1
    | n when n < 5 -> 2
    | _ -> 3

let maxGateType (model: Model): int =
    match model.Level with
    | 1 -> 1
    | 2 -> 1
    | 3 -> 2
    | 4 -> 2
    | 5 -> 1
    | 6 -> 2
    | _ -> 3

let numOfGates (model: Model): int =
    match model.Level with
    | 1 -> 4
    | n when n < 4 -> 5
    | n when n < 8 -> 6
    | 8 -> 7
    | 9 -> 8
    | _ -> 9

let initModel (): Model =
    let random = System.Random()
    let (gateLazyList, qStates) =
            Selection.selectGatesAndQStates
                        random
                        1
                        4
                        1
                        0.2
    let targetQStates =
            Generator.nextDistinctGroup
                random
                (LazyList.length qStates)
                10
                ()
            |> List.map (fun i -> Seq.item i qStates)
    let gates = LazyList.toList gateLazyList
    { Level = 1
      TargetIndex = 0
      TargetQStates = targetQStates
      GateSelection = List.replicate gates.Length false
      Gates = gates }
 
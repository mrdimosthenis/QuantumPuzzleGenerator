module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Msg = GateClick of int

type Model =
    { Level: int
      InitQState: Matrix.Matrix
      TargetIndex: int
      TargetQStates: Matrix.Matrix list
      GateSelection: bool list
      Gates: Quantum.Gate.Gate list }

// update function

let update (msg: Msg) (model: Model) =
    match msg with
    | GateClick index ->
        let newGateSelection =
            model.GateSelection
            |> List.indexed
            |> List.map (fun (i, b) -> if i = index then not b else b)

        let newModel =
            { model with
                  GateSelection = newGateSelection }

        newModel, Cmd.none

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
    | 1 -> 3
    | n when n < 4 -> 5
    | n when n < 8 -> 6
    | 8 -> 7
    | 9 -> 8
    | _ -> 9

let initModel (): Model =
    let random = System.Random()

    let (gatesRev, qStates) =
        Selection.selectGatesAndQStates random Quantum.Gate.SingleQubit 3 1 0.2

    let (initQState, otherQStates) =
        qStates |> LazyList.rev |> LazyList.uncons

    let targetQStates =
        Generator.nextDistinctGroup random (LazyList.length otherQStates) 4 ()
        |> List.map (fun i -> Seq.item i otherQStates)

    let gates =
        gatesRev |> LazyList.rev |> LazyList.toList

    { Level = 1
      InitQState = initQState
      TargetIndex = 0
      TargetQStates = targetQStates
      GateSelection = List.replicate gates.Length false
      Gates = gates }

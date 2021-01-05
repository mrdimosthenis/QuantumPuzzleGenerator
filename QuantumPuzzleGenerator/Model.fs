module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Msg = GateClick of int

type Settings =
    { FontScale: float
      PlotScale: float
      CircuitScale: float }

type Model =
    { Level: Level.Level
      InitQState: Matrix.Matrix
      TargetIndex: int
      TargetQStates: Matrix.Matrix list
      GateSelection: bool list
      Gates: Quantum.Gate.Gate list
      Settings: Settings }

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

let initModel (): Model =
    
    //TODO: replace with 0
    let levelIndex = Generator.nextInt Constants.random Level.levels.Length ()

    let initLevel = List.item levelIndex Level.levels

    let (gatesRev, qStates) =
        Selection.selectGatesAndQStates
            Constants.random
            initLevel.MaxGateType
            initLevel.NumOfGates
            initLevel.NumOfQubits
            Constants.differenceThreshold

    let (initQState, otherQStates) =
        qStates |> LazyList.rev |> LazyList.uncons

    let otherQStatesLength = LazyList.length otherQStates

    let targetQStates =
        Generator.nextDistinctGroup Constants.random otherQStatesLength Constants.numOfPuzzlesPerLevel ()
        |> List.map (fun i -> Seq.item i otherQStates)

    let gates =
        gatesRev |> LazyList.rev |> LazyList.toList

    { Level = initLevel
      InitQState = initQState
      TargetIndex = 0
      TargetQStates = targetQStates
      GateSelection = List.replicate gates.Length false
      Gates = gates
      Settings =
          { FontScale = 1.0
            PlotScale = 1.0
            CircuitScale = 1.0 } }

module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Msg =
    | GateClick of int
    | ResetModel //TODO: remove

type Settings =
    { FontScale: float
      PlotScale: float
      CircuitScale: float }

type Puzzle =
    { InitQState: Matrix.Matrix
      TargetQState: Matrix.Matrix
      Gates: Quantum.Gate.Gate list }

type Model =
    { LevelIndex: int
      PuzzleIndex: int

      Level: Level.Level
      Puzzle: Puzzle
      
      GateSelection: bool list

      Settings: Settings }

// model initialization

let initModel (): Model =

    //TODO: replace with 0
    let levelIndex =
        Generator.nextInt Constants.random Level.levels.Length ()
        
    let puzzleIndex = 0

    let level = List.item levelIndex Level.levels

    let (gatesRev, qStates) =
        Selection.selectGatesAndQStates
            Constants.random
            level.MaxGateType
            level.NumOfGates
            level.NumOfQubits
            Constants.differenceThreshold

    let (initQState, otherQStates) =
        qStates |> LazyList.rev |> LazyList.uncons

    let otherQStatesLength = LazyList.length otherQStates

    let randomQStatesIndex =
        Generator.nextInt Constants.random otherQStatesLength ()

    let targetQState = Seq.item randomQStatesIndex otherQStates

    let gates =
        gatesRev |> LazyList.rev |> LazyList.toList

    let puzzle =
        { InitQState = initQState
          TargetQState = targetQState
          Gates = gates }
        
    let gateSelection = List.replicate gates.Length false

    { LevelIndex = levelIndex
      PuzzleIndex = puzzleIndex

      Level = level
      Puzzle = puzzle

      GateSelection = gateSelection
      
      Settings =
          { FontScale = 1.0
            PlotScale = 1.0
            CircuitScale = 1.0 } }

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

    | ResetModel -> initModel (), Cmd.none

module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Msg =
    | GateClick of int
    | Regenerate
    | NextPuzzle
    | NextLevel

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

// initialization

let initPuzzle (level: Level.Level): Puzzle =
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

    { InitQState = initQState
      TargetQState = targetQState
      Gates = gates }

let initModel (levelIndex: int) (puzzleIndex: int): Model =

    let level = List.item levelIndex Level.levels

    let puzzle = initPuzzle level

    let gateSelection = List.replicate puzzle.Gates.Length false

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

    | Regenerate -> initModel model.LevelIndex model.PuzzleIndex, Cmd.none

    | NextPuzzle ->
        let nextPuzzleIndex = model.PuzzleIndex + 1
        Preferences.setInt Preferences.puzzleIndexKey nextPuzzleIndex
        let newModel = initModel model.LevelIndex nextPuzzleIndex

        newModel, Cmd.none

    | NextLevel ->
        let newLevelIndex = model.LevelIndex + 1
        Preferences.setInt Preferences.levelIndexKey newLevelIndex

        initModel newLevelIndex 0, Cmd.none

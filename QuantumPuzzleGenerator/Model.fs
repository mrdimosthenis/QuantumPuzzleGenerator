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

type SelectedPage =
    | HomePage
    | PlayPage
    | LearnPage

type Settings =
    { FontScale: float
      PlotScale: float
      CircuitScale: float }

type Puzzle =
    { InitQState: Matrix.Matrix
      TargetQState: Matrix.Matrix
      Gates: Quantum.Gate.Gate list }

type Model =
    { SelectedPage: SelectedPage

      Level: Level.Level

      Puzzle: Puzzle
      PuzzleIndex: int

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
            level.IsAbsoluteInitQState
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

    { SelectedPage = HomePage

      Level = level

      Puzzle = puzzle
      PuzzleIndex = puzzleIndex

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

    | Regenerate ->
        let puzzle = initPuzzle model.Level
        let gateSelection = List.replicate puzzle.Gates.Length false

        let newModel =
            { model with
                  Puzzle = puzzle
                  GateSelection = gateSelection }

        newModel, Cmd.none

    | NextPuzzle ->
        let puzzle = initPuzzle model.Level
        let puzzleIndex = model.PuzzleIndex + 1
        let gateSelection = List.replicate puzzle.Gates.Length false

        Preferences.setInt Preferences.puzzleIndexKey puzzleIndex

        let newModel =
            { model with
                  Puzzle = puzzle
                  PuzzleIndex = puzzleIndex
                  GateSelection = gateSelection }

        newModel, Cmd.none

    | NextLevel ->
        let levelIndex = model.Level.Index + 1
        let level = List.item levelIndex Level.levels
        let puzzle = initPuzzle level
        let puzzleIndex = 0
        let gateSelection = List.replicate puzzle.Gates.Length false

        Preferences.setInt Preferences.levelIndexKey levelIndex
        Preferences.setInt Preferences.puzzleIndexKey puzzleIndex

        let newModel =
            { model with
                  Level = level
                  Puzzle = puzzle
                  PuzzleIndex = puzzleIndex
                  GateSelection = gateSelection }

        newModel, Cmd.none

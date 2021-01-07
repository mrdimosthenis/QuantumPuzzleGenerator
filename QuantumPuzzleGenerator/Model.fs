module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Page =
    | HomePage
    | PlayPage
    | LearnPage

type Settings =
    { FontScale: float
      PlotScale: float
      CircuitScale: float }

type Puzzle =
    { Level: Level.Level
      SolvedPuzzlesInLevel: int
      InitQState: Matrix.Matrix
      TargetQState: Matrix.Matrix
      Gates: Quantum.Gate.Gate list
      GateSelection: bool list }

type Model =
    { SelectedPage: Page
      Puzzle: Puzzle
      Settings: Settings }

type Msg =
    | SelectPage of Page
    | GateClick of int
    | Regenerate
    | NextPuzzle
    | NextLevel

// initialization

let initPuzzle (levelIndex: int) (solvedPuzzlesInLevel: int): Puzzle =
    let level = List.item levelIndex Level.levels

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

    let gateSelection = List.replicate gates.Length false

    { Level = level
      SolvedPuzzlesInLevel = solvedPuzzlesInLevel
      InitQState = initQState
      TargetQState = targetQState
      Gates = gates
      GateSelection = gateSelection }

let initModel (levelIndex: int) (solvedPuzzlesInLevel: int): Model =

    let puzzle =
        initPuzzle levelIndex solvedPuzzlesInLevel

    { SelectedPage = HomePage

      Puzzle = puzzle

      Settings =
          { FontScale = 1.0
            PlotScale = 1.0
            CircuitScale = 1.0 } }

// update function

let update (msg: Msg) (model: Model) =
    match msg with
    | SelectPage page ->
        let newModel = { model with SelectedPage = page }

        newModel, Cmd.none

    | GateClick index ->
        let gateSelection =
            model.Puzzle.GateSelection
            |> List.indexed
            |> List.map (fun (i, b) -> if i = index then not b else b)

        let puzzle =
            { model.Puzzle with
                  GateSelection = gateSelection }

        { model with Puzzle = puzzle }, Cmd.none

    | Regenerate ->
        let puzzle =
            initPuzzle model.Puzzle.Level.Index model.Puzzle.SolvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

    | NextPuzzle ->
        let solvedPuzzlesInLevel = model.Puzzle.SolvedPuzzlesInLevel + 1

        Preferences.setInt Preferences.solvedPuzzlesInLevelKey solvedPuzzlesInLevel

        let puzzle =
            initPuzzle model.Puzzle.Level.Index solvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

    | NextLevel ->
        let levelIndex = model.Puzzle.Level.Index + 1
        let solvedPuzzlesInLevel = 0

        Preferences.setInt Preferences.levelIndexKey levelIndex
        Preferences.setInt Preferences.solvedPuzzlesInLevelKey solvedPuzzlesInLevel

        let puzzle =
            initPuzzle levelIndex solvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

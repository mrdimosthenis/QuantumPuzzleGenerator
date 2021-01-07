module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

// types

type Page =
    | HomePage
    | LessonCategoriesPage
    | LearnPage
    | PlayPage

type Settings =
    { FontScale: float
      PlotScale: float
      CircuitScale: float }

type Model =
    { SelectedPage: Page
      Lesson: Lesson.Lesson
      Puzzle: Puzzle.Puzzle
      Settings: Settings }

type Msg =
    | SelectPage of Page
    | SelectLesson of int
    | LessonGateClick of int
    | PuzzleGateClick of int
    | RegeneratePuzzle
    | NextPuzzle
    | NextLevel

// constructor

let initModel (levelIndex: int) (solvedPuzzlesInLevel: int): Model =
    let lesson = Lesson.initLesson 0

    let puzzle =
        Puzzle.initPuzzle levelIndex solvedPuzzlesInLevel

    { SelectedPage = HomePage
      Lesson = lesson
      Puzzle = puzzle
      Settings =
          { FontScale = 1.0
            PlotScale = 1.0
            CircuitScale = 1.0 } }

// update function

let update (msg: Msg) (model: Model) =
    match msg with
    | SelectPage page ->

        { model with SelectedPage = page }, Cmd.none
        
    | SelectLesson index ->
        let lesson = Lesson.initLesson index

        { model with
            Lesson = lesson
            SelectedPage = LearnPage }, Cmd.none

    | LessonGateClick index ->
        let gateSelection =
            model.Lesson.GateSelection
            |> List.indexed
            |> List.map (fun (i, b) -> if i = index then not b else b)

        let lesson =
            { model.Lesson with
                  GateSelection = gateSelection }

        { model with Lesson = lesson }, Cmd.none

    | PuzzleGateClick index ->
        let gateSelection =
            model.Puzzle.GateSelection
            |> List.indexed
            |> List.map (fun (i, b) -> if i = index then not b else b)

        let puzzle =
            { model.Puzzle with
                  GateSelection = gateSelection }

        { model with Puzzle = puzzle }, Cmd.none

    | RegeneratePuzzle ->
        let puzzle =
            Puzzle.initPuzzle model.Puzzle.Level.Index model.Puzzle.SolvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

    | NextPuzzle ->
        let solvedPuzzlesInLevel = model.Puzzle.SolvedPuzzlesInLevel + 1

        Preferences.setInt Preferences.solvedPuzzlesInLevelKey solvedPuzzlesInLevel

        let puzzle =
            Puzzle.initPuzzle model.Puzzle.Level.Index solvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

    | NextLevel ->
        let levelIndex = model.Puzzle.Level.Index + 1
        let solvedPuzzlesInLevel = 0

        Preferences.setInt Preferences.levelIndexKey levelIndex
        Preferences.setInt Preferences.solvedPuzzlesInLevelKey solvedPuzzlesInLevel

        let puzzle =
            Puzzle.initPuzzle levelIndex solvedPuzzlesInLevel

        { model with Puzzle = puzzle }, Cmd.none

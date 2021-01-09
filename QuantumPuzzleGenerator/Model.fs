module QuantumPuzzleGenerator.Model

open Fabulous
open FSharpx.Collections

// types

type Page =
    | HomePage
    | LessonCategoriesPage
    | LearnPage
    | PlayPage

type Model =
    { SelectedPage: Page
      Lesson: Lesson.Lesson
      Puzzle: Puzzle.Puzzle
      Settings: Settings.Settings }

type Msg =
    | SelectPage of Page
    | SelectLesson of int
    | LessonGateClick of int
    | PuzzleGateClick of int
    | RegeneratePuzzle
    | NextPuzzle
    | NextLevel
    | IncreaseScale of Settings.ScaleElement
    | DecreaseScale of Settings.ScaleElement

// constructor

let initModel (levelIndex: int) (solvedPuzzlesInLevel: int): Model =
    let lesson = Lesson.initLesson 0

    let puzzle =
        Puzzle.initPuzzle levelIndex solvedPuzzlesInLevel

    let settings = Settings.initSettings ()

    { SelectedPage = HomePage
      Lesson = lesson
      Puzzle = puzzle
      Settings = settings }

// update function

let update (msg: Msg) (model: Model) =
    match msg with
    | SelectPage page ->

        { model with SelectedPage = page }, Cmd.none

    | SelectLesson index ->
        let lesson = Lesson.initLesson index

        let newModel =
            { model with
                  Lesson = lesson
                  SelectedPage = LearnPage }

        newModel, Cmd.none

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

    | IncreaseScale scaleElement ->
        let settings =
            match scaleElement with
            | Settings.Plot ->
                { model.Settings with
                      PlotScale = Settings.increasedScaleValue model.Settings.PlotScale }
            | Settings.Circuit ->
                { model.Settings with
                      CircuitScale = Settings.increasedScaleValue model.Settings.CircuitScale }
            | Settings.ColorCircle ->
                { model.Settings with
                      ColorCircleScale = Settings.increasedScaleValue model.Settings.ColorCircleScale }

        { model with Settings = settings }, Cmd.none

    | DecreaseScale scaleElement ->
        let settings =
            match scaleElement with
            | Settings.Plot ->
                { model.Settings with
                      PlotScale = Settings.decreasedScaleValue model.Settings.PlotScale }
            | Settings.Circuit ->
                { model.Settings with
                      CircuitScale = Settings.decreasedScaleValue model.Settings.CircuitScale }
            | Settings.ColorCircle ->
                { model.Settings with
                      ColorCircleScale = Settings.decreasedScaleValue model.Settings.ColorCircleScale }

        { model with Settings = settings }, Cmd.none

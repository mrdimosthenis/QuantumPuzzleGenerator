module QuantumPuzzleGenerator.Model

open System
open FSharpx.Collections

open Fabulous
open Xamarin.Essentials

// types

type Page =
    | HomePage
    | LessonCategoriesPage
    | LearnPage
    | PlayPage
    | CreditsPage

type Model =
    { SelectedPage: Page
      Lesson: Lesson.Lesson
      Puzzle: Puzzle.Puzzle
      Settings: Settings.Settings
      AreAnalyticsEnabled: bool
      UserId: string
      SessionId: string }

type ExternalUrl =
    | GitHub
    | LinkedIn
    | GooglePlay
    | AppleStore
    | PrivacyPolicy

type SharableUrl =
    | AppOnGooglePlay
    | AppOnAppleStore

type Msg =
    | BackClick
    | SelectPage of Page
    | SelectLesson of int
    | LessonGateClick of int
    | PuzzleGateClick of int
    | RegeneratePuzzle
    | NextPuzzle
    | NextLevel
    | IncreaseScale of Settings.ScaleElement
    | DecreaseScale of Settings.ScaleElement
    | UrlClick of ExternalUrl
    | UrlShare of SharableUrl
    | SwitchAnalytics

// constructor

let initModel (): Model =
    let levelIndex =
        Preferences.levelIndexKey
        |> Preferences.tryGetInt
        |> Option.defaultValue 0

    let solvedPuzzlesInLevel =
        Preferences.solvedPuzzlesInLevelKey
        |> Preferences.tryGetInt
        |> Option.defaultValue 0

    let lesson = Lesson.initLesson 0

    let puzzle =
        Puzzle.initPuzzle levelIndex solvedPuzzlesInLevel

    let settings = Settings.initSettings ()

    let areAnalyticsEnabled =
        Preferences.areAnalyticsEnabledKey
        |> Preferences.tryGetBool
        |> Option.defaultValue false

    let userId =
        match Preferences.tryGetString Preferences.userIdKey with
        | Some str -> str
        | None ->
            let newUserId = Tracking.randomAlphanumeric ()
            Preferences.setString Preferences.userIdKey newUserId
            newUserId

    let sessionId = Tracking.randomAlphanumeric ()

    { SelectedPage = HomePage
      Lesson = lesson
      Puzzle = puzzle
      Settings = settings
      AreAnalyticsEnabled = areAnalyticsEnabled
      UserId = userId
      SessionId = sessionId }

// analytics

let modelTrackingSubMap (model: Model): Map<string, string> =
    [ ("lastLessonCategoryIndex", string model.Lesson.LessonCategory.Index)
      ("lastPuzzleLevelIndex", string model.Puzzle.Level.Index)
      ("plotScaleSetting", string model.Settings.PlotScale)
      ("circuitScaleSetting", string model.Settings.CircuitScale)
      ("colorCircleScaleSetting", string model.Settings.ColorCircleScale)
      ("userId", model.UserId)
      ("sessionId", model.SessionId) ]
    |> Map.ofList

let maybeTrackEvent (msg: Msg) (model: Model): unit =
    match msg with
    | SelectPage _
    | SelectLesson _
    | RegeneratePuzzle
    | NextPuzzle
    | NextLevel
    | UrlClick _
    | UrlShare _
    | SwitchAnalytics ->
        let modelSubMap = modelTrackingSubMap model

        let eventJsonString =
            Newtonsoft.Json.JsonConvert.SerializeObject msg

        Tracking.track modelSubMap eventJsonString
    | _ -> ()

// workaround for problematic initial rendering in web-views
let rerenderWebViews: Cmd<Msg> =
    [ IncreaseScale Settings.Plot
      DecreaseScale Settings.Plot
      IncreaseScale Settings.ColorCircle
      DecreaseScale Settings.ColorCircle ]
    |> List.map Cmd.ofMsg
    |> Cmd.batch

// update function

let update (msg: Msg) (model: Model): Model * Cmd<Msg> =
    maybeTrackEvent msg model

    match msg with
    | BackClick ->
        let cmd =
            match model.SelectedPage with
            | HomePage ->
                System
                    .Diagnostics
                    .Process
                    .GetCurrentProcess()
                    .CloseMainWindow()
                |> ignore

                HomePage
            | LearnPage -> LessonCategoriesPage
            | _ -> HomePage
            |> SelectPage
            |> Cmd.ofMsg

        model, cmd

    | SelectPage page ->

        { model with SelectedPage = page }, rerenderWebViews

    | SelectLesson index ->
        let lesson = Lesson.initLesson index

        let newModel =
            { model with
                  Lesson = lesson
                  SelectedPage = LearnPage }

        newModel, rerenderWebViews

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
                let increasedScaleValue =
                    Settings.increasedScaleValue model.Settings.PlotScale

                Preferences.setFloat Preferences.plotScaleKey increasedScaleValue

                { model.Settings with
                      PlotScale = increasedScaleValue }
            | Settings.Circuit ->
                let increasedScaleValue =
                    Settings.increasedScaleValue model.Settings.CircuitScale

                Preferences.setFloat Preferences.circuitScaleKey increasedScaleValue

                { model.Settings with
                      CircuitScale = increasedScaleValue }
            | Settings.ColorCircle ->
                let increasedScaleValue =
                    Settings.increasedScaleValue model.Settings.ColorCircleScale

                Preferences.setFloat Preferences.colorCircleScaleKey increasedScaleValue

                { model.Settings with
                      ColorCircleScale = increasedScaleValue }

        { model with Settings = settings }, Cmd.none

    | DecreaseScale scaleElement ->
        let settings =
            match scaleElement with
            | Settings.Plot ->
                let decreasedScaleValue =
                    Settings.decreasedScaleValue model.Settings.PlotScale

                Preferences.setFloat Preferences.plotScaleKey decreasedScaleValue

                { model.Settings with
                      PlotScale = decreasedScaleValue }
            | Settings.Circuit ->
                let decreasedScaleValue =
                    Settings.decreasedScaleValue model.Settings.CircuitScale

                Preferences.setFloat Preferences.circuitScaleKey decreasedScaleValue

                { model.Settings with
                      CircuitScale = decreasedScaleValue }
            | Settings.ColorCircle ->
                let decreasedScaleValue =
                    Settings.decreasedScaleValue model.Settings.ColorCircleScale

                Preferences.setFloat Preferences.colorCircleScaleKey decreasedScaleValue

                { model.Settings with
                      ColorCircleScale = decreasedScaleValue }

        { model with Settings = settings }, Cmd.none

    | UrlClick externalUrl ->
        match externalUrl with
        | GitHub -> Constants.gitHubUrl
        | LinkedIn -> Constants.linkedInUrl
        | GooglePlay -> Constants.googlePlayUrl
        | AppleStore -> Constants.appleStoreUrl
        | PrivacyPolicy -> Constants.privacyPolicyUrl
        |> Uri
        |> Launcher.OpenAsync
        |> Async.AwaitTask
        |> Async.StartImmediate

        model, Cmd.none

    | UrlShare sharableUrl ->
        match sharableUrl with
        | AppOnGooglePlay -> Constants.googlePlayUrl
        | AppOnAppleStore -> Constants.appleStoreUrl
        |> Share.RequestAsync
        |> Async.AwaitTask
        |> Async.StartImmediate

        model, Cmd.none

    | SwitchAnalytics ->
        let areAnalyticsEnabled = not model.AreAnalyticsEnabled
        Preferences.setBool Preferences.areAnalyticsEnabledKey areAnalyticsEnabled

        if areAnalyticsEnabled then Tracking.initialize () else Tracking.stop ()

        { model with
              AreAnalyticsEnabled = areAnalyticsEnabled },
        Cmd.none

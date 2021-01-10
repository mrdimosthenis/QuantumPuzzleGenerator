module QuantumPuzzleGenerator.Pages.Play

open Fabulous

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let levelLbl (model: Model.Model): ViewElement =
    model.Puzzle.Level.Index
    |> (+) 1
    |> sprintf "Level: %i"
    |> UIComponents.label UIComponents.Title

let puzzleLbl (model: Model.Model): ViewElement =
    let displayedPuzzle = model.Puzzle.SolvedPuzzlesInLevel + 1

    let text =
        if model.Puzzle.Level.Index < Level.levels.Length - 1
        then sprintf "Puzzle: %i/%i" displayedPuzzle Constants.numOfPuzzlesPerLevel
        else sprintf "Puzzle: %i" displayedPuzzle

    UIComponents.label UIComponents.Title text

let regeneratePuzzleOrLevelBtn (currentQState: Matrix.Matrix)
                               (targetQState: Matrix.Matrix)
                               (levelIndex: int)
                               (puzzleIndex: int)
                               (dispatch: Model.Msg -> unit)
                               : ViewElement =
    let regenerateBtn =
        let imageNameOpt = Some "icons.refresh"

        fun () -> dispatch Model.RegeneratePuzzle
        |> UIComponents.button "Regenerate" imageNameOpt

    let nextPuzzleBtn =
        let imageNameOpt = Some "icons.play_light"

        fun () -> dispatch Model.NextPuzzle
        |> UIComponents.button "Next Puzzle" imageNameOpt

    let nextLevelBtn =
        let imageNameOpt = Some "icons.play_dark"

        fun () -> dispatch Model.NextLevel
        |> UIComponents.button "Next Level" imageNameOpt

    match Matrix.almostEqual Constants.differenceThreshold currentQState targetQState with
    | false -> regenerateBtn
    | true when puzzleIndex >= Constants.numOfPuzzlesPerLevel - 1
                && levelIndex >= Level.levels.Length - 1 -> regenerateBtn
    | true when puzzleIndex >= Constants.numOfPuzzlesPerLevel - 1 -> nextLevelBtn
    | true -> nextPuzzleBtn

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.home"

    fun () -> dispatch Model.BackClick
    |> UIComponents.button "Back" imageNameOpt

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let goalQStatePlot =
        QStatePlotting.webView
            "Target State"
            model.Settings.PlotScale
            model.Puzzle.Level.NumOfQubits
            model.Puzzle.TargetQState
            dispatch

    let currentQState =
        List.zip model.Puzzle.Gates model.Puzzle.GateSelection
        |> List.filter snd
        |> List.map fst
        |> List.fold (fun qState gate ->
            let gateMatrix =
                Quantum.Gate.matrix model.Puzzle.Level.NumOfQubits gate

            Matrix.standardProduct gateMatrix qState) model.Puzzle.InitQState

    let circuit =
        CircuitDrawing.stackLayout
            model.Puzzle.Level.NumOfQubits
            model.Puzzle.Gates
            model.Puzzle.GateSelection
            model.Settings.CircuitScale
            Model.Msg.PuzzleGateClick
            dispatch

    let currentQStatePlot =
        QStatePlotting.webView
            "Current State"
            model.Settings.PlotScale
            model.Puzzle.Level.NumOfQubits
            currentQState
            dispatch

    let regeneratePuzzleOrLevel =
        regeneratePuzzleOrLevelBtn
            currentQState
            model.Puzzle.TargetQState
            model.Puzzle.Level.Index
            model.Puzzle.SolvedPuzzlesInLevel
            dispatch

    [ levelLbl model
      puzzleLbl model
      goalQStatePlot
      circuit
      UIComponents.emptySpaceElem ()
      currentQStatePlot
      UIComponents.horizontalStackLayout [ backBtn dispatch
                                           regeneratePuzzleOrLevel ] ]
    |> UIComponents.stackLayout

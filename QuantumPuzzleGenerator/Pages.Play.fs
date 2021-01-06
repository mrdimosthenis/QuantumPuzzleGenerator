module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let levelLbl (model: Model.Model): ViewElement =
    let displayedLevel = model.Level.Index + 1

    View.Label
        (text = sprintf "Level: %i" displayedLevel,
         horizontalTextAlignment = TextAlignment.Center,
         verticalTextAlignment = TextAlignment.Center,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center)

let puzzleLbl (model: Model.Model): ViewElement =
    let displayedPuzzle = model.PuzzleIndex + 1

    let text =
        if model.Level.Index < Level.levels.Length - 1
        then sprintf "Puzzle: %i/%i" displayedPuzzle Constants.numOfPuzzlesPerLevel
        else sprintf "Puzzle: %i" displayedPuzzle

    View.Label
        (text = text,
         horizontalTextAlignment = TextAlignment.Center,
         verticalTextAlignment = TextAlignment.Center,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center)

let regeneratePuzzleOrLevelBtn (currentQState: Matrix.Matrix)
                               (targetQState: Matrix.Matrix)
                               (levelIndex: int)
                               (puzzleIndex: int)
                               (dispatch: Model.Msg -> unit)
                               : ViewElement =
    let regenerateBtn =
        View.Button(text = "Regenerate", command = fun () -> dispatch Model.Regenerate)

    let nextPuzzleBtn =
        View.Button(text = "Next Puzzle", command = fun () -> dispatch Model.NextPuzzle)

    let nextLevelBtn =
        View.Button(text = "Next Level", command = fun () -> dispatch Model.NextLevel)

    match Matrix.almostEqual Constants.differenceThreshold currentQState targetQState with
    | false -> regenerateBtn
    | true when puzzleIndex >= Constants.numOfPuzzlesPerLevel - 1
                && levelIndex >= Level.levels.Length - 1 -> regenerateBtn
    | true when puzzleIndex >= Constants.numOfPuzzlesPerLevel - 1 -> nextLevelBtn
    | true -> nextPuzzleBtn

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let goalQStatePlot =
        model.Puzzle.TargetQState
        |> QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits

    let currentQState =
        List.zip model.Puzzle.Gates model.GateSelection
        |> List.filter snd
        |> List.map fst
        |> List.fold (fun qState gate ->
            let gateMatrix =
                Quantum.Gate.matrix model.Level.NumOfQubits gate

            Matrix.standardProduct gateMatrix qState) model.Puzzle.InitQState

    let currentQStatePlot =
        QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits currentQState

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ levelLbl model
               puzzleLbl model
               goalQStatePlot
               CircuitDrawing.stackLayout model dispatch
               currentQStatePlot
               regeneratePuzzleOrLevelBtn
                   currentQState
                   model.Puzzle.TargetQState
                   model.Level.Index
                   model.PuzzleIndex
                   dispatch ])

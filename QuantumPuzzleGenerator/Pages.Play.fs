module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator


let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =

    let displayedLevel = model.Level.Index + 1
    let displayedPuzzle = model.PuzzleIndex + 1

    let levelLbl =
        View.Label
            (text = sprintf "Level: %i" displayedLevel,
             horizontalTextAlignment = TextAlignment.Center,
             verticalTextAlignment = TextAlignment.Center,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)

    let puzzleLbl =
        View.Label
            (text = sprintf "Puzzle: %i/%i" displayedPuzzle Constants.numOfPuzzlesPerLevel,
             horizontalTextAlignment = TextAlignment.Center,
             verticalTextAlignment = TextAlignment.Center,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)

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

    let regenerateBtn =
        View.Button(text = "Regenerate", command = fun () -> dispatch Model.Regenerate)

    let nextPuzzleBtn =
        View.Button(text = "Next Puzzle", command = fun () -> dispatch Model.NextPuzzle)

    let nextLevelBtn =
        View.Button(text = "Next Level", command = fun () -> dispatch Model.NextLevel)

    let regeneratePuzzleOrLevelBtn =
        match Matrix.almostEqual Constants.differenceThreshold currentQState model.Puzzle.TargetQState with
        | false -> regenerateBtn
        | true when model.PuzzleIndex = Constants.numOfPuzzlesPerLevel - 1
                    && model.Level.Index = Level.levels.Length - 1 -> regenerateBtn
        | true when model.PuzzleIndex = Constants.numOfPuzzlesPerLevel - 1 -> nextLevelBtn
        | true -> nextPuzzleBtn

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ levelLbl
               puzzleLbl
               goalQStatePlot
               CircuitDrawing.stackLayout model dispatch
               currentQStatePlot
               regeneratePuzzleOrLevelBtn ])

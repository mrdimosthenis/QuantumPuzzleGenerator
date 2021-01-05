module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator


let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =

    let goalQStatePlot =
        model.Puzzle.TargetQState
        |> QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits

    let currentQStatePlot =
        List.zip model.Puzzle.Gates model.GateSelection
        |> List.filter snd
        |> List.map fst
        |> List.fold (fun qState gate ->
            let gateMatrix =
                Quantum.Gate.matrix model.Level.NumOfQubits gate

            Matrix.standardProduct gateMatrix qState) model.Puzzle.InitQState
        |> QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits

    //TODO: remove
    let resetModelBtn =
        View.Button(text = "reset model", command = fun () -> dispatch Model.ResetModel)

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ goalQStatePlot
               CircuitDrawing.stackLayout model dispatch
               currentQStatePlot
               resetModelBtn ]) //TODO: remove

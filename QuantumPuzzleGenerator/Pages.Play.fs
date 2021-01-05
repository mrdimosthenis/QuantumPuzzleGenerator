module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator


let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =

    let goalQStatePlot =
        model.TargetQStates.[model.TargetIndex]
        |> QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits

    let currentQStatePlot =
        List.zip model.Gates model.GateSelection
        |> List.filter snd
        |> List.map fst
        |> List.fold (fun qState gate ->
            let gateMatrix =
                Quantum.Gate.matrix model.Level.NumOfQubits gate

            Matrix.standardProduct gateMatrix qState) model.InitQState
        |> QStatePlotting.webView model.Settings.PlotScale model.Level.NumOfQubits

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ goalQStatePlot
               CircuitDrawing.stackLayout model dispatch
               currentQStatePlot ])

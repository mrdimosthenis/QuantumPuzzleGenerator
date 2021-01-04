module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let gateButtons (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let gateWidth =
        model.Level.NumOfGates
        |> float
        |> (/) Constants.deviceWidth
        |> (*) model.Settings.CircuitScale

    let gateHeight =
        model.Level.NumOfQubits |> float |> (*) gateWidth

    let children =
        List.zip model.Gates model.GateSelection
        |> List.indexed
        |> List.map (fun (i, (g, b)) ->
            View.ImageButton
                (source = Resources.gateImage g model.Level.NumOfQubits,
                 width = gateWidth,
                 height = gateHeight,
                 backgroundColor = (if b then Color.LightGreen else Color.LightBlue),
                 command = fun () -> i |> Model.GateClick |> dispatch))

    View.StackLayout
        (orientation = StackOrientation.Horizontal,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         spacing = 0.0,
         children = children)

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
               gateButtons model dispatch
               currentQStatePlot ])

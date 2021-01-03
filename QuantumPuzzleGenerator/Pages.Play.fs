module QuantumPuzzleGenerator.Pages.Play

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let gateButtons (model: Model.Model) (dispatch: Model.Msg -> unit) : ViewElement list =
    List.zip model.Gates model.GateSelection
    |> List.indexed
    |> List.map
        (fun (i, (g, b)) ->
            View.ImageButton(
                source = (model |> Model.numOfQubits |> Resources.gateImage g),
                backgroundColor = (if b then Color.LightGreen else Color.Transparent),
                command = fun () -> i |> Model.GateClick |> dispatch
            )
        )

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit) : ViewElement =
    let numOfQubits = Model.numOfQubits model
    let goalQStatePlot =
            model.TargetQStates.[model.TargetIndex]
            |> QStatePlotting.webView numOfQubits
    let currentQStatePlot =
            List.zip model.Gates model.GateSelection
            |> List.filter snd
            |> List.map fst
            |> List.fold
                 (fun qState gate ->
                         let gateMatrix = Quantum.Gate.matrix numOfQubits gate
                         Matrix.standardProduct gateMatrix qState
                 )
                 model.InitQState
            |> QStatePlotting.webView numOfQubits
    let children = List.concat [ [ goalQStatePlot ]
                                 gateButtons model dispatch
                                 [ currentQStatePlot ] ]
    View.StackLayout(
        horizontalOptions = LayoutOptions.Center,
        verticalOptions = LayoutOptions.Center,
        children = children
    )

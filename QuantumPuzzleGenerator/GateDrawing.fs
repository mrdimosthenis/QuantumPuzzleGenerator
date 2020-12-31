module QuantumPuzzleGenerator.GateDrawing

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit) : ViewElement =
    let imageButtons =
        List.zip model.Gates model.GateSelection
        |> List.indexed
        |> List.map
            (fun (i, (g, b)) ->
                View.ImageButton(
                    source = (model |> Model.numOfGates |> Resources.gateImage g),
                    backgroundColor = (if b then Color.LightGreen else Color.Transparent),
                    command = fun () -> i |> Model.GateClick |> dispatch
                )
            )
    View.StackLayout(
        horizontalOptions = LayoutOptions.Center,
        verticalOptions = LayoutOptions.Center,
        children = imageButtons
    )

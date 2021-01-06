module QuantumPuzzleGenerator.Pages.Learn

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator


let backBtn (dispatch: Model.Msg -> unit) =
    View.Button
        (text = "Back",
         command =
             fun () ->
                 Model.Page.HomePage
                 |> Model.SelectPage
                 |> dispatch)

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ backBtn dispatch ])

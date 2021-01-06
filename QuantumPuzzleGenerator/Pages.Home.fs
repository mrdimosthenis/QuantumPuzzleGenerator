module QuantumPuzzleGenerator.Pages.Home

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleGenerator

let titleLbl: ViewElement =
    View.Label
        (text = "Quantum Puzzle Generator",
         horizontalTextAlignment = TextAlignment.Center,
         verticalTextAlignment = TextAlignment.Center,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center)

let learnBtn (dispatch: Model.Msg -> unit) =
    View.Button
        (text = "Interactive Learning",
         command =
             fun () ->
                 Model.Page.LearnPage
                 |> Model.SelectPage
                 |> dispatch)

let playBtn (dispatch: Model.Msg -> unit) =
    View.Button
        (text = "Puzzle Solving",
         command =
             fun () ->
                 Model.Page.PlayPage
                 |> Model.SelectPage
                 |> dispatch)

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children =
             [ titleLbl
               learnBtn dispatch
               playBtn dispatch ])

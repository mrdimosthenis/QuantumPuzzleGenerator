module QuantumPuzzleGenerator.Pages.Intro

open System

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open Xamarin.Essentials

open QuantumPuzzleGenerator

let stackLayout (_: Model.Model) (_: Model.Msg -> unit): ViewElement =
    let btnCommand =
        fun () ->
            Uri("https://github.com/mrdimosthenis/mobile-apps")
            |> Launcher.OpenAsync
            |> Async.AwaitTask
            |> Async.StartImmediate

    let imgBtn =
        View.ImageButton
            (backgroundColor = Color.Transparent,
             borderColor = Color.Black,
             borderWidth = 1.0,
             command = btnCommand,
             source = Resources.image "icons.list")

    View.StackLayout
        (verticalOptions = LayoutOptions.Center,
         horizontalOptions = LayoutOptions.Center,
         children =
             [ UIComponents.label UIComponents.SimpleLabel "...please wait a few seconds..."
               imgBtn
               View.ActivityIndicator(isRunning = true) ])

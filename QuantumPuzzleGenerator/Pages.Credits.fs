module QuantumPuzzleGenerator.Pages.Credits

open System
open Fabulous
open Xamarin.Essentials

open QuantumPuzzleGenerator

let openUrl (url: string): unit =
    url
    |> Uri
    |> Launcher.OpenAsync
    |> Async.AwaitTask
    |> Async.StartImmediate

let authorDescriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (): ViewElement =
    let imageNameOpt = Some "icons.code"

    let command =
        fun () -> openUrl "https://github.com/mrdimosthenis/QuantumPuzzleGenerator"

    UIComponents.button "Code on GitHub" imageNameOpt false command

let devBtn (): ViewElement =
    let imageNameOpt = Some "icons.profile"

    let command =
        fun () -> openUrl "https://www.linkedin.com/in/mrdimosthenis/"

    UIComponents.button "Developer on LinkedIn" imageNameOpt false command

let appStoreDescriptionLbl (): ViewElement =
    "Please rate this app"
    |> UIComponents.label UIComponents.Paragraph

let googleAppStoreBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.play_dark"

    let isHighlighted =
        not model.DidVisitAppStore && not Constants.isIOS

    let command =
        fun () ->
            openUrl "https://play.google.com/store/apps/details?id=com.github.mrdimosthenis.quantumpuzzlegenerator"
            if not Constants.isIOS then dispatch Model.VisitAppStore else ()

    UIComponents.button "App on Google Play" imageNameOpt isHighlighted command

let appleAppStoreBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.info"

    let isHighlighted =
        not model.DidVisitAppStore && Constants.isIOS

    let command =
        fun () ->
            //TODO replace the zeros with the actual app id
            openUrl "https://apps.apple.com/us/app/apple-store/id000000000"
            if Constants.isIOS then dispatch Model.VisitAppStore else ()

    UIComponents.button "App on Apple Store" imageNameOpt isHighlighted command

let versionLbl (): ViewElement =
    Constants.version
    |> sprintf "Version: %s"
    |> UIComponents.label UIComponents.SimpleLabel

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.home"

    fun () -> dispatch Model.BackClick
    |> UIComponents.button "Back" imageNameOpt false

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    UIComponents.stackLayout [ UIComponents.label UIComponents.Title "Credits"
                               authorDescriptionLbl ()
                               codeBtn ()
                               devBtn ()
                               UIComponents.emptySpaceElem ()
                               appStoreDescriptionLbl ()
                               googleAppStoreBtn model dispatch
                               appleAppStoreBtn model dispatch
                               UIComponents.emptySpaceElem ()
                               versionLbl ()
                               backBtn dispatch ]

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

let openGitHub (): unit =
    openUrl "https://github.com/mrdimosthenis/QuantumPuzzleGenerator"

let openLinkedIn (): unit =
    openUrl "https://www.linkedin.com/in/mrdimosthenis/"

let openGoogleAppStore (): unit =
    openUrl "https://play.google.com/store/apps/details?id=com.github.mrdimosthenis.quantumpuzzlegenerator"

//TODO replace the zeros with the actual app id
let openAppleAppStore (): unit =
    openUrl "https://apps.apple.com/us/app/apple-store/id000000000"

let authorDescriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (): ViewElement =
    let imageNameOpt = Some "icons.code"

    UIComponents.button "Code on GitHub" imageNameOpt openGitHub

let devBtn (): ViewElement =
    let imageNameOpt = Some "icons.profile"

    UIComponents.button "Developer on LinkedIn" imageNameOpt openLinkedIn

let appStoreDescriptionLbl (): ViewElement =
    "Please rate this app"
    |> UIComponents.label UIComponents.Paragraph

let googleAppStoreBtn (): ViewElement =
    let imageNameOpt = Some "icons.play_dark"

    UIComponents.button "App on Google Play" imageNameOpt openGoogleAppStore

let appleAppStoreBtn (): ViewElement =
    let imageNameOpt = Some "icons.info"

    UIComponents.button "App on Apple Store" imageNameOpt openAppleAppStore

let versionLbl (): ViewElement =
    Constants.version
    |> sprintf "Version: %s"
    |> UIComponents.label UIComponents.SimpleLabel

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.home"

    fun () -> dispatch Model.BackClick
    |> UIComponents.button "Back" imageNameOpt

let stackLayout (_: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    UIComponents.stackLayout [ UIComponents.label UIComponents.Title "Credits"
                               authorDescriptionLbl ()
                               codeBtn ()
                               devBtn ()
                               UIComponents.emptySpaceElem ()
                               appStoreDescriptionLbl ()
                               googleAppStoreBtn ()
                               appleAppStoreBtn ()
                               UIComponents.emptySpaceElem ()
                               versionLbl ()
                               backBtn dispatch ]

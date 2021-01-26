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

let descriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (): ViewElement =
    let imageNameOpt = Some "icons.code"

    UIComponents.button "Code on GitHub" imageNameOpt openGitHub

let devBtn (): ViewElement =
    let imageNameOpt = Some "icons.profile"

    UIComponents.button "Developer on LinkedIn" imageNameOpt openLinkedIn

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
                               descriptionLbl ()
                               codeBtn ()
                               devBtn ()
                               versionLbl ()
                               backBtn dispatch ]

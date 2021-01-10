module QuantumPuzzleGenerator.Pages.Credits

open System
open Fabulous
open Xamarin.Essentials

open QuantumPuzzleGenerator

let openGitHub () =
    Uri("https://github.com/mrdimosthenis/QuantumPuzzleGenerator")
    |> Launcher.OpenAsync
    |> Async.AwaitTask
    |> Async.StartImmediate

let openLinkedIn () =
    Uri("https://www.linkedin.com/in/mrdimosthenis/")
    |> Launcher.OpenAsync
    |> Async.AwaitTask
    |> Async.StartImmediate

let descriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (): ViewElement =
    let imageNameOpt = Some "icons.code"

    UIComponents.button "Source Code" imageNameOpt openGitHub

let devBtn (): ViewElement =
    let imageNameOpt = Some "icons.profile"

    UIComponents.button "Developer" imageNameOpt openLinkedIn

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

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

let shareUrl (url: string): unit =
    ShareTextRequest(Uri = url)
    |> Share.RequestAsync
    |> Async.AwaitTask
    |> Async.StartImmediate

let authorDescriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (): ViewElement =
    let imageNameOpt = Some "icons.code"

    let command =
        fun () ->
            Tracking.codeOnGitHubClicked ()
            openUrl "https://github.com/mrdimosthenis/QuantumPuzzleGenerator"

    UIComponents.button "Code on GitHub" imageNameOpt command

let devBtn (): ViewElement =
    let imageNameOpt = Some "icons.profile"

    let command =
        fun () ->
            Tracking.developerOnLinkedInClicked ()
            openUrl "https://www.linkedin.com/in/mrdimosthenis/"

    UIComponents.button "Developer on LinkedIn" imageNameOpt command

let googleAppStoreHorizontalLayout (): ViewElement =
    let url =
        "https://play.google.com/store/apps/details?id=com.github.mrdimosthenis.quantumpuzzlegenerator"

    let storeBtnImageNameOpt = Some "icons.play_dark"
    let shareBtnImageNameOpt = Some "icons.share"

    let storeBtn =
        fun () ->
            Tracking.appOnGooglePlayVisited ()
            openUrl url
        |> UIComponents.button "App on Google Play" storeBtnImageNameOpt

    let shareBtn =
        fun () ->
            Tracking.appOnGooglePlayShared ()
            shareUrl url
        |> UIComponents.button "" shareBtnImageNameOpt

    UIComponents.horizontalStackLayout [ storeBtn
                                         shareBtn ]


let appleAppStoreHorizontalLayout (): ViewElement =
    //TODO replace the zeros with the actual app id
    let url =
        "https://apps.apple.com/us/app/apple-store/id000000000"

    let storeBtnImageNameOpt = Some "icons.info"
    let shareBtnImageNameOpt = Some "icons.share"

    let storeBtn =
        fun () ->
            Tracking.appOnAppleStoreVisited ()
            openUrl url
        |> UIComponents.button "App on Apple Store" storeBtnImageNameOpt

    let shareBtn =
        fun () ->
            Tracking.appOnAppleStoreShared ()
            shareUrl url
        |> UIComponents.button "" shareBtnImageNameOpt

    UIComponents.horizontalStackLayout [ storeBtn
                                         shareBtn ]

let privacyPolicyBtn (): ViewElement =
    let imageNameOpt = Some "icons.text_document"

    let command =
        fun () ->
            Tracking.privacyPolicyClicked ()
            openUrl "https://github.com/mrdimosthenis/QuantumPuzzleGenerator/blob/master/privacy_policy.md"

    UIComponents.button "Privacy Policy" imageNameOpt command

let analyticsHorizontalLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let shortDescriptionLbl =
        "Send anonymous usage statistics"
        |> UIComponents.label UIComponents.Paragraph

    let checkBox =
        fun _ -> dispatch Model.SwitchAnalytics
        |> UIComponents.checkBox model.AreAnalyticsEnabled

    UIComponents.horizontalStackLayout [ shortDescriptionLbl
                                         checkBox ]

let versionLbl (): ViewElement =
    Constants.version
    |> sprintf "Version: %s"
    |> UIComponents.label UIComponents.SimpleLabel

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.home"

    fun () -> dispatch Model.BackClick
    |> UIComponents.button "Back" imageNameOpt

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    [ UIComponents.label UIComponents.Title "Credits"
      authorDescriptionLbl ()
      codeBtn ()
      devBtn ()
      UIComponents.emptySpaceElem ()
      googleAppStoreHorizontalLayout ()
      appleAppStoreHorizontalLayout ()
      UIComponents.emptySpaceElem ()
      privacyPolicyBtn ()
      analyticsHorizontalLayout model dispatch
      UIComponents.emptySpaceElem ()
      versionLbl ()
      backBtn dispatch ]
    |> UIComponents.stackLayout

module QuantumPuzzleGenerator.Pages.Credits

open Fabulous

open QuantumPuzzleGenerator

let header (dispatch: Model.Msg -> unit): ViewElement =
    fun () -> dispatch Model.BackClick
    |> UIComponents.header "Credits" "icons.identity" "icons.inverted_play_dark"

let authorDescriptionLbl (): ViewElement =
    "Quantum Puzzle Generator was created by Dimosthenis Michailidis"
    |> UIComponents.label UIComponents.Paragraph

let codeBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.code"

    let command =
        fun () -> Model.GitHub |> Model.UrlClick |> dispatch

    UIComponents.button "Code on GitHub" imageNameOpt command

let devBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.profile"

    let command =
        fun () -> Model.LinkedIn |> Model.UrlClick |> dispatch

    UIComponents.button "Developer on LinkedIn" imageNameOpt command

let googleAppStoreHorizontalLayout (dispatch: Model.Msg -> unit): ViewElement =
    let storeBtnImageNameOpt = Some "icons.play_dark"
    let shareBtnImageNameOpt = Some "icons.share"

    let storeBtn =
        fun () -> Model.GooglePlay |> Model.UrlClick |> dispatch
        |> UIComponents.button "App on Google Play" storeBtnImageNameOpt

    let shareBtn =
        fun () ->
            Model.AppOnGooglePlay
            |> Model.UrlShare
            |> dispatch
        |> UIComponents.button "" shareBtnImageNameOpt

    UIComponents.horizontalStackLayout [ storeBtn
                                         shareBtn ]


//let appleAppStoreHorizontalLayout (dispatch: Model.Msg -> unit): ViewElement =
//    let storeBtnImageNameOpt = Some "icons.info"
//    let shareBtnImageNameOpt = Some "icons.share"
//
//    let storeBtn =
//        fun () -> Model.AppleStore |> Model.UrlClick |> dispatch
//        |> UIComponents.button "App on Apple Store" storeBtnImageNameOpt
//
//    let shareBtn =
//        fun () ->
//            Model.AppOnAppleStore
//            |> Model.UrlShare
//            |> dispatch
//        |> UIComponents.button "" shareBtnImageNameOpt
//
//    UIComponents.horizontalStackLayout [ storeBtn
//                                         shareBtn ]

let privacyPolicyBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.text_document"

    fun () -> Model.PrivacyPolicy |> Model.UrlClick |> dispatch
    |> UIComponents.button "Privacy Policy" imageNameOpt

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
    [ header dispatch
      UIComponents.emptySpaceElem ()
      authorDescriptionLbl ()
      codeBtn dispatch
      devBtn dispatch
      UIComponents.emptySpaceElem ()
      googleAppStoreHorizontalLayout dispatch
      //appleAppStoreHorizontalLayout dispatch
      UIComponents.emptySpaceElem ()
      privacyPolicyBtn dispatch
      analyticsHorizontalLayout model dispatch
      UIComponents.emptySpaceElem ()
      versionLbl () ]
    |> UIComponents.stackLayout

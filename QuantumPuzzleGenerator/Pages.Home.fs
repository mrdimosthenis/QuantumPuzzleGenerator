module QuantumPuzzleGenerator.Pages.Home

open Fabulous
open Fabulous.XamarinForms

open QuantumPuzzleGenerator

let learnBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.library"

    fun () ->
        Model.Page.LessonCategoriesPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Interactive Learning" imageNameOpt false

let playBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.puzzle"

    fun () ->
        Model.Page.PlayPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Puzzle Solving" imageNameOpt false

let creditsBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.identity"

    let isHighlighted =
        not model.DidVisitAppStore
        || not model.AreAnalyticsEnabled
        || not model.AreAdsEnabled

    fun () ->
        Model.Page.CreditsPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Credits" imageNameOpt isHighlighted

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    UIComponents.stackLayout [ UIComponents.label UIComponents.Title "Quantum Puzzle Generator"
                               View.Image(source = Resources.image "logo")
                               learnBtn dispatch
                               playBtn dispatch
                               creditsBtn model dispatch ]

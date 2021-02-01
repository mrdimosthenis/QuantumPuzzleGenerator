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
    |> UIComponents.button "Interactive Learning" imageNameOpt

let playBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.puzzle"

    fun () ->
        Model.Page.PlayPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Puzzle Solving" imageNameOpt

let creditsBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.identity"

    fun () ->
        Model.Page.CreditsPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Credits" imageNameOpt

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    UIComponents.stackLayout [ UIComponents.label UIComponents.Title "Quantum Puzzle Generator"
                               View.Image(source = Resources.image "logo")
                               learnBtn dispatch
                               playBtn dispatch
                               creditsBtn dispatch ]

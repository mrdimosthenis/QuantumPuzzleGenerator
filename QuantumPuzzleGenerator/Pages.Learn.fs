module QuantumPuzzleGenerator.Pages.Learn

open FSharpx.Collections
open Fabulous

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let qStatePlot (model: Model.Model): ViewElement =
    List.zip model.Lesson.LessonCategory.Gates model.Lesson.GateSelection
    |> List.filter snd
    |> List.map fst
    |> List.fold (fun qState gate ->
        let gateMatrix =
            Quantum.Gate.matrix model.Lesson.LessonCategory.NumOfQubits gate

        Matrix.standardProduct gateMatrix qState) model.Lesson.QState
    |> QStatePlotting.webView model.Settings.PlotScale model.Lesson.LessonCategory.NumOfQubits

let circuit (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    CircuitDrawing.stackLayout model.Lesson.LessonCategory.NumOfQubits model.Lesson.LessonCategory.Gates
        model.Lesson.GateSelection model.Settings.CircuitScale (fun i -> i |> Model.Msg.LessonGateClick |> dispatch)

let prevLessonBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.left"

    fun () ->
        if model.Lesson.LessonCategory.Index <= 0 then
            ()
        else
            model.Lesson.LessonCategory.Index - 1
            |> Model.SelectLesson
            |> dispatch
    |> UIComponents.button "" imageNameOpt

let nextLessonBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.right"

    fun () ->
        if model.Lesson.LessonCategory.Index
           >= LessonCategory.lessonCategories.Length - 1 then
            ()
        else
            model.Lesson.LessonCategory.Index + 1
            |> Model.SelectLesson
            |> dispatch
    |> UIComponents.button "" imageNameOpt

let regenerateBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.refresh"

    fun () ->
        model.Lesson.LessonCategory.Index
        |> Model.SelectLesson
        |> dispatch
    |> UIComponents.button "Regenerate" imageNameOpt

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.library"

    fun () ->
        Model.Page.LessonCategoriesPage
        |> Model.SelectPage
        |> dispatch
    |> UIComponents.button "Back" imageNameOpt

let colorCircle (model: Model.Model): ViewElement list =
    match model.Lesson.LessonCategory.IsHueDisplayedOpt with
    | Some true ->
        List.zip model.Lesson.LessonCategory.Gates model.Lesson.GateSelection
        |> List.filter snd
        |> List.map fst
        |> List.fold (fun qState gate ->
            let gateMatrix =
                Quantum.Gate.matrix model.Lesson.LessonCategory.NumOfQubits gate

            Matrix.standardProduct gateMatrix qState) model.Lesson.QState
        |> Some
        |> ColorCircle.webView model.Settings.ColorCircleScale
        |> List.singleton
    | Some false -> [ ColorCircle.webView model.Settings.ColorCircleScale None ]
    | None -> []

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    [ [ UIComponents.horizontalStackLayout [ prevLessonBtn model dispatch
                                             UIComponents.label UIComponents.Title model.Lesson.LessonCategory.Title
                                             nextLessonBtn model dispatch ] ]
      [ UIComponents.label UIComponents.Paragraph model.Lesson.LessonCategory.Description ]
      [ qStatePlot model ]
      if model.Lesson.LessonCategory.Gates.IsEmpty
      then []
      else [ circuit model dispatch ]
      colorCircle model
      [ UIComponents.horizontalStackLayout [ backBtn dispatch
                                             regenerateBtn model dispatch ] ] ]
    |> List.concat
    |> UIComponents.stackLayout

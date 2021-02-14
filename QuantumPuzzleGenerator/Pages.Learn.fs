module QuantumPuzzleGenerator.Pages.Learn

open FSharpx.Collections
open Fabulous

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let quantumState (model: Model.Model): Matrix.Matrix =
    List.zip model.Lesson.LessonCategory.Gates model.Lesson.GateSelection
    |> List.filter snd
    |> List.map fst
    |> List.fold (fun qState gate ->
        let gateMatrix =
            Quantum.Gate.matrix model.Lesson.LessonCategory.NumOfQubits gate

        Matrix.standardProduct gateMatrix qState) model.Lesson.QState

let qStatePlot (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let qState = quantumState model
    QStatePlotting.webView "State" model.Settings.PlotScale model.Lesson.LessonCategory.NumOfQubits qState dispatch

let circuit (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    CircuitDrawing.stackLayout
        model.Lesson.LessonCategory.NumOfQubits
        model.Lesson.LessonCategory.Gates
        model.Lesson.GateSelection
        model.Settings.CircuitScale
        Model.Msg.LessonGateClick
        dispatch

let header (dispatch: Model.Msg -> unit): ViewElement =
    fun () -> dispatch Model.BackClick
    |> UIComponents.header "Interactive Learning" "icons.library" "icons.inverted_play_dark"

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

let colorCircle (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement list =
    match model.Lesson.LessonCategory.IsHueDisplayedOpt with
    | Some true ->
        let qStateOpt = model |> quantumState |> Some

        [ UIComponents.emptySpaceElem ()
          ColorCircle.webView model.Settings.ColorCircleScale qStateOpt dispatch ]
    | Some false ->
        [ UIComponents.emptySpaceElem ()
          ColorCircle.webView model.Settings.ColorCircleScale None dispatch ]
    | None -> []

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    [ [ header dispatch
        UIComponents.emptySpaceElem () ]
      [ UIComponents.horizontalStackLayout [ prevLessonBtn model dispatch
                                             UIComponents.label UIComponents.Title model.Lesson.LessonCategory.Title
                                             nextLessonBtn model dispatch ] ]
      [ UIComponents.label UIComponents.Paragraph model.Lesson.LessonCategory.Description
        UIComponents.emptySpaceElem ()
        qStatePlot model dispatch ]
      if model.Lesson.LessonCategory.Gates.IsEmpty
      then []
      else [ circuit model dispatch ]
      colorCircle model dispatch
      [ UIComponents.emptySpaceElem ()
        regenerateBtn model dispatch ] ]
    |> List.concat
    |> UIComponents.stackLayout

module QuantumPuzzleGenerator.Pages.Learn

open FSharpx.Collections
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator

let titleLbl (model: Model.Model): ViewElement =
    View.Label
        (text = model.Lesson.LessonCategory.Title,
         horizontalTextAlignment = TextAlignment.Center,
         verticalTextAlignment = TextAlignment.Center,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center)
        
let descriptionLbl (model: Model.Model): ViewElement =
    View.Label
        (text = model.Lesson.LessonCategory.Description,
         verticalTextAlignment = TextAlignment.Center,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center)

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

let regenerateBtn (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    View.Button
        (text = "Regenerate",
         image = Resources.image "icons.refresh",
         command =
             fun () ->
                 model.Lesson.LessonCategory.Index
                 |> Model.SelectLesson
                 |> dispatch)

let backBtn (dispatch: Model.Msg -> unit) =
    View.Button
        (text = "Back",
         command =
             fun () ->
                 Model.Page.LessonCategoriesPage
                 |> Model.SelectPage
                 |> dispatch)

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let colorCircle =
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

    let children =
        [ [ titleLbl model ]
          [ descriptionLbl model ]
          [ qStatePlot model ]
          if model.Lesson.LessonCategory.Gates.IsEmpty
          then []
          else [ circuit model dispatch ]
          colorCircle
          [ regenerateBtn model dispatch ]
          [ backBtn dispatch ] ]
        |> List.concat

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center, verticalOptions = LayoutOptions.Center, children = children)

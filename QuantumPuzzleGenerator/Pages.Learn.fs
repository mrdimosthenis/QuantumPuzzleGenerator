﻿module QuantumPuzzleGenerator.Pages.Learn

open FSharpx.Collections
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator
open Xamarin.Forms.Shapes

let lessonLbl (model: Model.Model): ViewElement =
    View.Label
        (text = model.Lesson.LessonCategory.Title,
         horizontalTextAlignment = TextAlignment.Center,
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

let backBtn (dispatch: Model.Msg -> unit) =
    View.Button
        (text = "Back",
         command =
             fun () ->
                 Model.Page.LessonCategoriesPage
                 |> Model.SelectPage
                 |> dispatch)

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let qStateForColorCircle =
        if model.Lesson.LessonCategory.NumOfQubits = 1
            then List.zip model.Lesson.LessonCategory.Gates model.Lesson.GateSelection
                 |> List.filter snd
                 |> List.map fst
                 |> List.fold (fun qState gate ->
                     let gateMatrix =
                         Quantum.Gate.matrix model.Lesson.LessonCategory.NumOfQubits gate
         
                     Matrix.standardProduct gateMatrix qState) model.Lesson.QState
            else Matrix.zero 1 2

    let children =
        [ [ lessonLbl model ]
          [ qStatePlot model ]
          if model.Lesson.LessonCategory.Gates.IsEmpty
          then []
          else [ circuit model dispatch ]
          if model.Lesson.LessonCategory.IsHueDisplayed
          then [ ColorCircle.webView model.Settings.ColorCircleScale qStateForColorCircle ]
          else []
          [ backBtn dispatch ] ]
        |> List.concat

    View.StackLayout
        (horizontalOptions = LayoutOptions.Center, verticalOptions = LayoutOptions.Center, children = children)

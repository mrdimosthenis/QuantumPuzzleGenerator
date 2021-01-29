﻿module QuantumPuzzleGenerator.Pages.LessonCategories

open Fabulous

open QuantumPuzzleGenerator

let lessonButtons (dispatch: Model.Msg -> unit): ViewElement list =
    List.map (fun (category: LessonCategory.LessonCategory) ->
        fun () -> category.Index |> Model.SelectLesson |> dispatch
        |> UIComponents.button category.Title None false) LessonCategory.lessonCategories

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    let imageNameOpt = Some "icons.home"

    fun () -> dispatch Model.BackClick
    |> UIComponents.button "Back" imageNameOpt false

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    [ [ UIComponents.label UIComponents.Title "Interactive Learning" ]
      lessonButtons dispatch
      [ backBtn dispatch ] ]
    |> List.concat
    |> UIComponents.stackLayout

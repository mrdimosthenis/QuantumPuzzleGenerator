module QuantumPuzzleGenerator.Pages.LessonCategories

open Fabulous

open QuantumPuzzleGenerator

let header (dispatch: Model.Msg -> unit): ViewElement =
    fun () -> dispatch Model.BackClick
    |> UIComponents.header "Interactive Learning" "icons.library" "icons.inverted_play_dark"

let lessonButtons (dispatch: Model.Msg -> unit): ViewElement list =
    List.map (fun (category: LessonCategory.LessonCategory) ->
        fun () -> category.Index |> Model.SelectLesson |> dispatch
        |> UIComponents.button category.Title None) LessonCategory.lessonCategories

let stackLayout (_: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    [ [ header dispatch
        UIComponents.emptySpaceElem () ]
      lessonButtons dispatch ]
    |> List.concat
    |> UIComponents.stackLayout

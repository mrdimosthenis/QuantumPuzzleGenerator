module QuantumPuzzleGenerator.Pages.LessonCategories

open FSharpx.Collections
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleGenerator

let lessonButtons (dispatch: Model.Msg -> unit): ViewElement list =
        List.map (fun (category: LessonCategory.LessonCategory) ->
            View.Button
                (text = category.Title,
                 command =
                     fun () ->
                         category.Index
                         |> Model.SelectLesson
                         |> dispatch)) LessonCategory.lessonCategories

let backBtn (dispatch: Model.Msg -> unit): ViewElement =
    View.Button
        (text = "Back",
         command =
             fun () ->
                 Model.Page.HomePage
                 |> Model.SelectPage
                 |> dispatch)

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let children = List.append (lessonButtons dispatch) [ backBtn dispatch ]
    View.StackLayout
        (horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children = children )

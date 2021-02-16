module QuantumPuzzleGenerator.UIComponents

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

// types

type LabelType =
    | Title
    | SimpleLabel
    | Paragraph

// functions

let emptySpaceElem (): ViewElement =
    View.BoxView(color = Color.Transparent, height = 5.0)

let label (labelType: LabelType) (text: string): ViewElement =
    match labelType with
    | Title ->
        View.Label
            (text = text,
             horizontalTextAlignment = TextAlignment.Center,
             verticalTextAlignment = TextAlignment.Center,
             fontAttributes = FontAttributes.Bold,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)
    | SimpleLabel ->
        View.Label
            (text = text,
             horizontalTextAlignment = TextAlignment.Center,
             verticalTextAlignment = TextAlignment.Center,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)
    | Paragraph ->
        View.Label
            (text = text,
             verticalTextAlignment = TextAlignment.Center,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)

let button (text: string) (imageNameOpt: string option) (command: unit -> unit): ViewElement =
    match (text, imageNameOpt) with
    | ("", Some imageName) ->
        View.ImageButton
            (backgroundColor = Color.Transparent,
             borderColor = Color.Black,
             borderWidth = 1.0,
             width = Constants.imageSize,
             height = Constants.imageSize,
             command = command,
             source = Resources.image imageName)
    | (_, Some imageName) ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = Color.Transparent,
             borderColor = Color.Black,
             borderWidth = 1.0,
             command = command,
             image = Resources.image imageName)
    | (_, None) ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = Color.Transparent,
             borderColor = Color.Black,
             borderWidth = 1.0,
             command = command)

let checkBox (isChecked: bool) (checkedChanged: unit -> unit): ViewElement =
    View.CheckBox(color = Color.Black, isChecked = isChecked, checkedChanged = (fun _ -> checkedChanged ()))

let horizontalStackLayout (children: ViewElement list): ViewElement =
    View.StackLayout
        (orientation = StackOrientation.Horizontal,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children = children)

let header (text: string) (pageImageName: string) (backImageName: string) (backCommand: unit -> unit): ViewElement =
    let backBtn =
        let imageNameOpt = Some backImageName
        button "" imageNameOpt backCommand

    let lbl = label Title text

    let pageImg =
        View.ImageButton
            (backgroundColor = Color.Transparent,
             width = Constants.imageSize,
             height = Constants.imageSize,
             source = Resources.image pageImageName)

    [ backBtn; lbl; pageImg ] |> horizontalStackLayout

let stackLayout (children: ViewElement list): ViewElement =
    View.StackLayout(horizontalOptions = LayoutOptions.Center, children = children)

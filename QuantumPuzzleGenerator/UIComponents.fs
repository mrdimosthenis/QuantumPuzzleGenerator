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

let button (text: string) (imageNameOpt: string option) (isHighlighted: bool) (command: unit -> unit): ViewElement =
    let backgroundColor = if isHighlighted then Constants.colorC else Color.Transparent
    match (text, imageNameOpt) with
    | ("", Some imageName) ->
        View.ImageButton
            (backgroundColor = backgroundColor,
             width = 30.0,
             height = 30.0,
             command = command,
             source = Resources.image imageName)
    | (_, Some imageName) ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = backgroundColor,
             borderWidth = 1.0,
             borderColor = Color.Black,
             command = command,
             image = Resources.image imageName)
    | (_, None) ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = backgroundColor,
             borderColor = Color.Black,
             borderWidth = 1.0,
             command = command)
            
let checkBox (isChecked: bool) (checkedChanged: unit -> unit) =
    View.CheckBox(
        color = Color.Black,
        isChecked = isChecked,
        checkedChanged = (fun _ -> checkedChanged ())
    )

let stackLayout (children: ViewElement list): ViewElement =
    View.StackLayout(horizontalOptions = LayoutOptions.Center, children = children)

let horizontalStackLayout (isHighlighted: bool) (children: ViewElement list): ViewElement =
    let backgroundColor = if isHighlighted then Constants.colorC else Color.Transparent
    View.StackLayout
        (backgroundColor = backgroundColor,
         orientation = StackOrientation.Horizontal,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         children = children)

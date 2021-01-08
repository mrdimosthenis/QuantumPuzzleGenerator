module QuantumPuzzleGenerator.UIComponents

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

// types

type LabelType =
    | Title
    | Paragraph

// functions

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
    | Paragraph ->
        View.Label
            (text = text,
             verticalTextAlignment = TextAlignment.Center,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center)

let button (text: string) (imageNameOpt: string option) (command: unit -> unit): ViewElement =
    match imageNameOpt with
    | Some imageName ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = Color.Transparent,
             borderWidth = 1.0,
             borderColor = Color.Black,
             command = command,
             image = Resources.image imageName)
    | None ->
        View.Button
            (text = text,
             textTransform = TextTransform.None,
             backgroundColor = Color.Transparent,
             borderColor = Color.Black,
             borderWidth = 1.0,
             command = command)

let stackLayout (children: ViewElement list): ViewElement =
    View.StackLayout
        (horizontalOptions = LayoutOptions.Center, verticalOptions = LayoutOptions.Center, children = children)

﻿module QuantumPuzzleGenerator.CircuitDrawing

open FSharpx.Collections
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics

let invertedIndex (numOfQubits: int) (i: int): int = numOfQubits - i - 1

let gateImage (numOfQubits: int) (gate: Quantum.Gate.Gate): Image.Value =
    let invertedGate =
        match gate with
        | Quantum.Gate.XGate index -> Quantum.Gate.XGate(invertedIndex numOfQubits index)
        | Quantum.Gate.YGate index -> Quantum.Gate.YGate(invertedIndex numOfQubits index)
        | Quantum.Gate.ZGate index -> Quantum.Gate.ZGate(invertedIndex numOfQubits index)
        | Quantum.Gate.HGate index -> Quantum.Gate.HGate(invertedIndex numOfQubits index)
        | Quantum.Gate.SwapGate (indexA, indexB) ->
            Quantum.Gate.SwapGate(invertedIndex numOfQubits indexA, invertedIndex numOfQubits indexB)
        | Quantum.Gate.CXGate (indexA, indexB) ->
            Quantum.Gate.CXGate(invertedIndex numOfQubits indexA, invertedIndex numOfQubits indexB)
        | Quantum.Gate.CZGate (indexA, indexB) ->
            Quantum.Gate.CZGate(invertedIndex numOfQubits indexA, invertedIndex numOfQubits indexB)
        | Quantum.Gate.CCXGate (indexA, indexB, indexC) ->
            Quantum.Gate.CCXGate
                (invertedIndex numOfQubits indexA, invertedIndex numOfQubits indexB, invertedIndex numOfQubits indexC)
        | Quantum.Gate.CSwapGate (indexA, indexB, indexC) ->
            Quantum.Gate.CSwapGate
                (invertedIndex numOfQubits indexA, invertedIndex numOfQubits indexB, invertedIndex numOfQubits indexC)

    Resources.gateImage invertedGate numOfQubits

let headerHorizontalLayout (dispatch: Model.Msg -> unit): ViewElement =
    let title =
        UIComponents.label UIComponents.SimpleLabel "Gates"

    let decreaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_out"

        fun () ->
            Settings.Circuit
            |> Model.DecreaseScale
            |> dispatch
        |> UIComponents.button "" imageNameOpt

    let increaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_in"

        fun () ->
            Settings.Circuit
            |> Model.IncreaseScale
            |> dispatch
        |> UIComponents.button "" imageNameOpt

    UIComponents.horizontalStackLayout [ decreaseScaleBtn
                                         title
                                         increaseScaleBtn ]

let stackLayout (numOfQubits: int)
                (gates: Quantum.Gate.Gate list)
                (gateSelection: bool list)
                (circuitScaleSetting: float)
                (gateClickCommand: int -> Model.Msg)
                (dispatch: Model.Msg -> unit)
                : ViewElement =
    let headerLayout = headerHorizontalLayout dispatch

    let gateWidth =
        gates.Length
        |> float
        |> (/) Constants.deviceWidth
        |> (*) 0.5
        |> (*) circuitScaleSetting

    let gateHeight = numOfQubits |> float |> (*) gateWidth

    let children =
        List.zip gates gateSelection
        |> List.indexed
        |> List.map (fun (i, (g, b)) ->
            View.ImageButton
                (source = gateImage numOfQubits g,
                 width = gateWidth,
                 height = gateHeight,
                 backgroundColor = (if b then Constants.colorA else Constants.colorB),
                 command = fun () -> i |> gateClickCommand |> dispatch))

    let circuitStack =
        View.StackLayout
            (orientation = StackOrientation.Horizontal,
             horizontalOptions = LayoutOptions.Center,
             verticalOptions = LayoutOptions.Center,
             spacing = 0.0,
             children = children)

    UIComponents.stackLayout [ headerLayout
                               circuitStack ]

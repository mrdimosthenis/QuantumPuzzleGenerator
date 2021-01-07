module QuantumPuzzleGenerator.CircuitDrawing

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

let stackLayout
    (numOfQubits: int)
    (gates: Quantum.Gate.Gate list)
    (gateSelection: bool list)
    (circuitScaleSetting: float)
    (gateClickCommand: int -> unit)
    : ViewElement =
    let gateWidth =
        gates.Length
        |> float
        |> (/) Constants.deviceWidth
        |> (*) 0.8
        |> (*) circuitScaleSetting

    let gateHeight =
        numOfQubits |> float |> (*) gateWidth

    let children =
        List.zip gates gateSelection
        |> List.indexed
        |> List.map (fun (i, (g, b)) ->
            View.ImageButton
                (source = gateImage numOfQubits g,
                 width = gateWidth,
                 height = gateHeight,
                 backgroundColor = (if b then Color.YellowGreen else Color.LightBlue),
                 command = fun () -> gateClickCommand i))

    View.StackLayout
        (orientation = StackOrientation.Horizontal,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         spacing = 0.0,
         children = children)

module QuantumPuzzleGenerator.CircuitDrawing

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

let stackLayout (model: Model.Model) (dispatch: Model.Msg -> unit): ViewElement =
    let gateWidth =
        model.Level.NumOfGates
        |> float
        |> (/) Constants.deviceWidth
        |> (*) model.Settings.CircuitScale

    let gateHeight =
        model.Level.NumOfQubits |> float |> (*) gateWidth

    let children =
        List.zip model.Puzzle.Gates model.GateSelection
        |> List.indexed
        |> List.map (fun (i, (g, b)) ->
            View.ImageButton
                (source = gateImage model.Level.NumOfQubits g,
                 width = gateWidth,
                 height = gateHeight,
                 backgroundColor = (if b then Color.YellowGreen else Color.LightBlue),
                 command = fun () -> i |> Model.GateClick |> dispatch))

    View.StackLayout
        (orientation = StackOrientation.Horizontal,
         horizontalOptions = LayoutOptions.Center,
         verticalOptions = LayoutOptions.Center,
         spacing = 0.0,
         children = children)

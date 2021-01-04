module QuantumPuzzleGenerator.QStatePlotting

open System
open Fable
open Fable.React
open Fable.React.Props
open Fabulous
open Fabulous.XamarinForms
open Xamarin
open FSharpx.Collections

open QuantumPuzzleMechanics
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

// types

type SpaceCoordinates = float * float * float

type Style = { Fill: string; Stroke: string }

type Row =
    { X: float
      Y: float
      Z: float
      Style: Style }

type Data = Row list

type GeneratedByQuantumPuzzles =
    { DotSizeRatioScale: float
      Data: Data }

// exceptions

exception InvalidNumOfQubits

// constants

let coordsForOneQubit: SpaceCoordinates LazyList =
    [ (0.25, 0.5, 0.5); (0.75, 0.5, 0.5) ]
    |> LazyList.ofList

let coordsForTwoQubits: SpaceCoordinates LazyList =
    [ (0.25, 0.25, 0.5)
      (0.75, 0.25, 0.5)
      (0.25, 0.75, 0.5)
      (0.75, 0.75, 0.5) ]
    |> LazyList.ofList

let coordsForThreeQubits: SpaceCoordinates LazyList =
    [ (0.25, 0.25, 0.25)
      (0.75, 0.25, 0.25)
      (0.25, 0.75, 0.25)
      (0.75, 0.75, 0.25)
      (0.25, 0.25, 0.75)
      (0.75, 0.25, 0.75)
      (0.25, 0.75, 0.75)
      (0.75, 0.75, 0.75) ]
    |> LazyList.ofList

let coordsForFourQubits: SpaceCoordinates LazyList =
    [ (0.1, 0.4, 0.4)
      (0.3, 0.4, 0.4)
      (0.1, 0.6, 0.4)
      (0.3, 0.6, 0.4)
      (0.1, 0.4, 0.6)
      (0.3, 0.4, 0.6)
      (0.1, 0.6, 0.6)
      (0.3, 0.6, 0.6)
      (0.7, 0.4, 0.4)
      (0.9, 0.4, 0.4)
      (0.7, 0.6, 0.4)
      (0.9, 0.6, 0.4)
      (0.7, 0.4, 0.6)
      (0.9, 0.4, 0.6)
      (0.7, 0.6, 0.6)
      (0.9, 0.6, 0.6) ]
    |> LazyList.ofList

// functions

let coordsForQubits (numOfQubits: int): SpaceCoordinates LazyList =
    match numOfQubits with
    | 1 -> coordsForOneQubit
    | 2 -> coordsForTwoQubits
    | 3 -> coordsForThreeQubits
    | 4 -> coordsForFourQubits
    | _ -> raise InvalidNumOfQubits

let fillColor (c: Complex.Complex): Forms.Color =
    let v =
        c
        |> Complex.toPolar
        |> fst
        |> (*) -256.0
        |> (+) 256.0
        |> Math.Floor
        |> int

    Forms.Color.FromRgb(v, v, v)

let strokeColor (c: Complex.Complex): Forms.Color =
    if c = Complex.zero then
        Forms.Color.White
    else
        let h =
            c
            |> Complex.toPolar
            |> snd
            |> (+) Math.PI
            |> (*) (0.5 / Math.PI)

        Forms.Color.FromHsv(h, 1.0, 1.0)

let data (numOfQubits: int) (qState: Matrix.Matrix): Data =
    qState
    |> LazyList.concat
    |> LazyList.zip (coordsForQubits numOfQubits)
    |> LazyList.map (fun ((x, y, z), complex) ->
        { X = x
          Y = y
          Z = z
          Style =
              { Fill = complex |> fillColor |> Graphics.Elems.rgb
                Stroke = complex |> strokeColor |> Graphics.Elems.rgb } })
    |> LazyList.toList

let generatedByQuantumPuzzles (numOfQubits: int) (qState: Matrix.Matrix): string =
    let dotSizeRatioScale =
        match numOfQubits with
        | 1 -> 4.0
        | 2 -> 3.0
        | 3 -> 2.0
        | 4 -> 1.0
        | _ -> raise InvalidNumOfQubits

    let content =
        { DotSizeRatioScale = dotSizeRatioScale
          Data = data numOfQubits qState }

    let serializerSettings = JsonSerializerSettings()
    serializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()
    JsonConvert.SerializeObject(content, serializerSettings)

let singlePlotHtmlString (numOfQubits: int) (qState: Matrix.Matrix): string =
    let visJs = Resources.text "vis_graph3d_601.js"

    let dataJs =
        generatedByQuantumPuzzles numOfQubits qState
        |> sprintf "var generatedByQuantumPuzzles = %s;"

    let drawJs = Resources.text "single_plot.js"

    html [] [
        head [] [
            meta [ Name "viewport"
                   HTMLAttr.Custom("content", "width=device-width, initial-scale=1") ]
            Standard.style [] [
                RawText "* { margin: 0; }"
            ]
            script [ Type "text/javascript" ] [
                RawText visJs
            ]
            script [ Type "text/javascript" ] [
                RawText dataJs
            ]
            script [ Type "text/javascript" ] [
                RawText drawJs
            ]
        ]
        body [ HTMLAttr.Custom("onload", "drawVisualization();") ] [
            div [ Id "mygraph" ] []
        ]
    ]
    |> ReactServer.renderToString

let webView (plotScale: float) (numOfQubits: int) (qState: Matrix.Matrix): ViewElement =
    let size =
        Constants.deviceWidth |> (*) 0.75 |> (*) plotScale

    let htmlString = singlePlotHtmlString numOfQubits qState

    View.WebView
        (source = Xamarin.Forms.HtmlWebViewSource(Html = htmlString),
         width = size,
         height = size,
         horizontalOptions = Forms.LayoutOptions.Center,
         verticalOptions = Forms.LayoutOptions.Center)

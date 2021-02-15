module QuantumPuzzleGenerator.QStatePlotting

open System

open FSharpx.Collections
open Giraffe
open GiraffeViewEngine
open Fabulous
open Fabulous.XamarinForms
open Xamarin
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

open QuantumPuzzleMechanics

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

// functions

let coordsForQubits (numOfQubits: int): SpaceCoordinates LazyList =
    match numOfQubits with
    | 1 -> coordsForOneQubit
    | 2 -> coordsForTwoQubits
    | 3 -> coordsForThreeQubits
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
        Forms.Color.Black
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
            meta [ attr "name" "viewport"
                   attr "content" "width=device-width, initial-scale=1" ]
            GiraffeViewEngine.style [] [
                rawText "* { margin: 0; }"
            ]
            script [ attr "type" "text/javascript" ] [
                rawText visJs
            ]
            script [ attr "type" "text/javascript" ] [
                rawText dataJs
            ]
            script [ attr "type" "text/javascript" ] [
                rawText drawJs
            ]
        ]
        body [ attr "onload" "drawVisualization();" ] [
            div [ _id "mygraph" ] []
        ]
    ]
    |> renderHtmlNode

let headerHorizontalLayout (title: string) (dispatch: Model.Msg -> unit): ViewElement =
    let title =
        UIComponents.label UIComponents.SimpleLabel title

    let decreaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_out"

        fun () -> Settings.Plot |> Model.DecreaseScale |> dispatch
        |> UIComponents.button "" imageNameOpt

    let increaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_in"

        fun () -> Settings.Plot |> Model.IncreaseScale |> dispatch
        |> UIComponents.button "" imageNameOpt

    UIComponents.horizontalStackLayout [ decreaseScaleBtn
                                         title
                                         increaseScaleBtn ]

let webView (title: string)
            (plotScale: float)
            (numOfQubits: int)
            (qState: Matrix.Matrix)
            (dispatch: Model.Msg -> unit)
            : ViewElement =
    let headerLayout = headerHorizontalLayout title dispatch

    let size =
        Constants.deviceWidth |> (*) 0.5 |> (*) plotScale

    let htmlString = singlePlotHtmlString numOfQubits qState

    let plotWebView =
        View.WebView
            (source = Xamarin.Forms.HtmlWebViewSource(Html = htmlString),
             width = size,
             height = size,
             horizontalOptions = Forms.LayoutOptions.Center,
             verticalOptions = Forms.LayoutOptions.Center)

    UIComponents.stackLayout [ headerLayout
                               plotWebView ]

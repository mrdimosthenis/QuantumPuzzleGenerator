module QuantumPuzzleGenerator.ColorCircle


open System
open Fable
open Fable.React
open Fable.React.Props
open Fabulous
open Fabulous.XamarinForms
open Xamarin
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

open QuantumPuzzleMechanics

// types

type GeneratedByQuantumPuzzles =
    { X1: float
      Y1: float
      X2: float
      Y2: float }

// functions

let generatedByQuantumPuzzles (qState: Matrix.Matrix): string =
    let c1 = qState.Head.Head
    let c2 = qState.Tail.Head.Head

    let content =
        { X1 = c1.Re
          Y1 = c1.Im
          X2 = c2.Re
          Y2 = c2.Im }

    let serializerSettings = JsonSerializerSettings()
    serializerSettings.ContractResolver <- CamelCasePropertyNamesContractResolver()
    JsonConvert.SerializeObject(content, serializerSettings)

let singlePlotHtmlString (size: float) (qState: Matrix.Matrix): string =
    let canvasSize = size |> Math.Round |> int

    let dataJs =
        qState
        |> generatedByQuantumPuzzles
        |> sprintf "var generatedByQuantumPuzzles = %s;"

    let circleJs = Resources.text "color_circle.js"

    html [] [
        head [] [
            meta [ Name "viewport"
                   HTMLAttr.Custom("content", "width=device-width, initial-scale=1") ]
            Standard.style [] [
                RawText "* { margin: 0; }"
            ]
            script [ Type "text/javascript" ] [
                RawText dataJs
            ]
        ]
        body [] [
            canvas [ Id "myCanvas"
                     HTMLAttr.Width canvasSize
                     HTMLAttr.Height canvasSize ] []
            script [ Type "text/javascript" ] [
                RawText circleJs
            ]
        ]
    ]
    |> ReactServer.renderToString

let webView (colorCircleScale: float) (qState: Matrix.Matrix): ViewElement =
    let size =
        Constants.deviceWidth
        |> (*) 0.75
        |> (*) colorCircleScale

    let htmlString = singlePlotHtmlString size qState

    View.WebView
        (source = Xamarin.Forms.HtmlWebViewSource(Html = htmlString),
         width = size,
         height = size,
         horizontalOptions = Forms.LayoutOptions.Center,
         verticalOptions = Forms.LayoutOptions.Center)

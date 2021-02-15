module QuantumPuzzleGenerator.ColorCircle


open System
open Giraffe
open GiraffeViewEngine
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

let singlePlotHtmlString (size: float) (qStateOpt: Matrix.Matrix option): string =
    let canvasSize = size |> Math.Round |> int

    let jsScripts =
        match qStateOpt with
        | Some qState ->
            [ qState
              |> generatedByQuantumPuzzles
              |> sprintf "var generatedByQuantumPuzzles = %s;"
              Resources.text "color_circle_main.js"
              Resources.text "color_circle_lines.js" ]
        | None -> [ Resources.text "color_circle_main.js" ]
        |> List.map (fun jsString ->
            script [ attr "type" "text/javascript" ] [
                rawText jsString
            ])

    let htmlBody =
        List.append
            [ canvas [ _id "myCanvas"
                       canvasSize |> string |> attr "width"
                       canvasSize |> string |> attr "height" ] [] ]
            jsScripts


    html [] [
        head [] [
            meta [ attr "name" "viewport"
                   attr "content" "width=device-width, initial-scale=1" ]
            GiraffeViewEngine.style [] [
                rawText "* { margin: 0; }"
            ]
        ]
        body [] htmlBody
    ]
    |> renderHtmlNode

let headerHorizontalLayout (dispatch: Model.Msg -> unit): ViewElement =
    let title =
        UIComponents.label UIComponents.SimpleLabel "Circle of Colors"

    let decreaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_out"

        fun () ->
            Settings.ColorCircle
            |> Model.DecreaseScale
            |> dispatch
        |> UIComponents.button "" imageNameOpt

    let increaseScaleBtn =
        let imageNameOpt = Some "icons.zoom_in"

        fun () ->
            Settings.ColorCircle
            |> Model.IncreaseScale
            |> dispatch
        |> UIComponents.button "" imageNameOpt

    UIComponents.horizontalStackLayout [ decreaseScaleBtn
                                         title
                                         increaseScaleBtn ]

let webView (colorCircleScale: float) (qStateOpt: Matrix.Matrix option) (dispatch: Model.Msg -> unit): ViewElement =
    let headerLayout = headerHorizontalLayout dispatch

    let size =
        Constants.deviceWidth
        |> (*) 0.5
        |> (*) colorCircleScale

    let htmlString = singlePlotHtmlString size qStateOpt

    let circleColorWebView =
        View.WebView
            (source = Xamarin.Forms.HtmlWebViewSource(Html = htmlString),
             width = size,
             height = size,
             horizontalOptions = Forms.LayoutOptions.Center,
             verticalOptions = Forms.LayoutOptions.Center)

    UIComponents.stackLayout [ headerLayout
                               circleColorWebView ]

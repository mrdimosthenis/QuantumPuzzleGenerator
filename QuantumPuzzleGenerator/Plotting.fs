module QuantumPuzzleGenerator.Plotting

open System
open Fable
open Fable.React
open Fable.React.Props
open Fabulous
open Fabulous.XamarinForms
open Xamarin
open XPlot.Plotly
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type SpaceCoordinates = float * float * float

// functions

let rec coordinates (i: int): SpaceCoordinates =
    match i with
    | 0 -> (0.0, 0.0, 0.0)
    | 1 -> (1.0, 0.0, 0.0)
    | 2 -> (0.0, 1.0, 0.0)
    | 3 -> (1.0, 1.0, 0.0)
    | 4 -> (0.0, 0.0, 1.0)
    | 5 -> (1.0, 0.0, 1.0)
    | 6 -> (0.0, 1.0, 1.0)
    | 7 -> (1.0, 1.0, 1.0)
    | _ when i < 16 ->
            let (x, y, z) = coordinates (i - 8)
            (x + 4.0, y, z)
    | _ when i < 32 ->
        let (x, y, z) = coordinates (i - 16)
        (x, y + 4.0, z)
    | _ ->
        let (x, y, z) = coordinates (i - 32)
        (x, y, z + 4.0)

let hue (c: Complex.Complex): float =
    if c = Complex.zero
        then 0.0
        else c
             |> Complex.toPolar
             |> snd
             |> (+) Math.PI
             |> (*) (0.5 / Math.PI)

let layout (coordinateLazyList: SpaceCoordinates LazyList): Layout =
    let (hd, tl) = LazyList.uncons coordinateLazyList
    let (initX, initY, initZ) = hd
    let (maxX, maxY, maxZ, minX, minY, minZ) =
            LazyList.fold
                (fun (accMaxX, accMaxY, accMaxZ, accMinX, accMinY, accMinZ) (x, y, z) ->
                    (
                        max accMaxX x, max accMaxY y, max accMaxZ z,
                        min accMinX x, min accMinY y, min accMinZ z
                    )
                )
                (initX, initY, initZ, initX, initY, initZ)
                tl
    let maxHalfDistance = maxX - minX
                          |> max (maxY - minY)
                          |> max (maxZ - minZ)
                          |> (*) 0.5
    let middleX = (minX + maxX) / 2.0
    let middleY = (minY + maxY) / 2.0
    let middleZ = (minZ + maxZ) / 2.0
    let rangeX = [ middleX - maxHalfDistance - 0.5; middleX + maxHalfDistance + 0.5]
    let rangeY = [ middleY - maxHalfDistance - 0.5; middleY + maxHalfDistance + 0.5]
    let rangeZ = [ middleZ - maxHalfDistance - 0.5; middleZ + maxHalfDistance + 0.5]
    Layout(
        scene=Scene(
            xaxis=Xaxis(
                title="",
                range=rangeX,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            yaxis=Yaxis(
                title="",
                range=rangeY,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            zaxis=Zaxis(
                title="",
                range=rangeZ,
                ticks="",
                showticklabels=false,
                zeroline=false
            )
        ),
        margin=Margin(l=0.0,r=0.0,t=0.0,b=0.0)
    )

let trace (coords: float * float * float)
          (size: float)
          (color: Forms.Color)
          (symbol: string)
    :Scatter3d =
    let (x, y, z) = coords
    Scatter3d(
        x = [x],
        y = [y],
        z = [z],
        hoverinfo = "none",
        mode = "markers",
        showlegend = false,
        marker = Marker(
            color = Graphics.Elems.rgb color,
            size = size,
            symbol = symbol
        )
    )

let jsScriptPair (qState: Matrix.Matrix): string * string =
   let indexedQState = qState
                       |> LazyList.concat
                       |> Seq.indexed
                       |> LazyList.ofSeq
   let coordinateLazyList = LazyList.map
                               (fun (i, _) -> coordinates i)
                               indexedQState
   let lyt = layout coordinateLazyList
   let plot = LazyList.zip indexedQState coordinateLazyList
              |> LazyList.map
                      (fun ((i, c), coords) ->
                          let size = c
                                     |> Complex.toPolar
                                     |> fst
                                     |> (*) 50.0
                          let h = hue c
                          let color = Forms.Color.FromHsv(h, 1.0, 1.0)
                          let symbol = if i = 0 then "diamond" else "circle"
                          trace coords size color symbol
                      )
              |> LazyList.toList
              |> Chart.Plot
              |> Chart.WithLayout lyt
   let scriptLines = plot.GetInlineJS().Split('\n')
   (scriptLines.[1].Trim(), scriptLines.[2].Trim())

let htmlContent (divId: string) (jsPair: string * string): ReactElement =
    let (js1, js2) = jsPair
    let plotConfig = "{responsive:true,displayModeBar:false}"
    let js3 = sprintf "Plotly.newPlot('%s', data, layout, %s);" divId plotConfig
    let rawJsString = String.Join("\n", [ js1; js2; js3 ])
    html []
         [ head [] [ meta [ CharSet "UTF-8" ]
                     script [ Src "plotly_latest_min.js" ] [] ]
           body [] [ div [ Id divId ] []
                     script [] [ RawText rawJsString ] ] ]

let webView (qState: Matrix.Matrix): ViewElement =
    let divId = Guid.NewGuid().ToString()
    let htmlString = qState
                  |> jsScriptPair
                  |> htmlContent divId
                  |> ReactServer.renderToString
    View.WebView(source=Xamarin.Forms.HtmlWebViewSource(Html=htmlString))

// TODO: delete
let sampleQstate =
    QuantumPuzzleMechanics.Generator.nextQState (System.Random()) 3 ()

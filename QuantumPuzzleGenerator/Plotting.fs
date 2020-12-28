module QuantumPuzzleGenerator.Plotting

open System

open FSharpx.Collections

open XPlot.Plotly
open Fabulous.XamarinForms
open Xamarin

open QuantumPuzzleMechanics

let rec coordinates (i: int): float * float * float =
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
            (x + 2.0, y, z)
    | _ when i < 32 ->
        let (x, y, z) = coordinates (i - 16)
        (x, y + 2.0, z)
    | _ ->
        let (x, y, z) = coordinates (i - 32)
        (x, y, z + 2.0)

let hue (c: Complex.Complex): float =
    if c = Complex.zero
        then 0.0
        else c
             |> Complex.toPolar
             |> snd
             |> (+) Math.PI
             |> (*) (0.5 / Math.PI)

let layout: Layout =
    Layout(
        scene=Scene(
            xaxis=Xaxis(
                title="",
                autorange=true,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            yaxis=Yaxis(
                title="",
                autorange=true,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            zaxis=Zaxis(
                title="",
                autorange=true,
                ticks="",
                showticklabels=false,
                zeroline=false
            )
        )
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

let representationHtml (qState: Matrix.Matrix): string =
    let plot = qState
               |> LazyList.concat
               |> Seq.indexed
               |> LazyList.ofSeq
               |> LazyList.map
                       (fun (i, c) ->
                           let coords = coordinates i
                           let size = c
                                      |> Complex.toPolar
                                      |> fst
                                      |> (*) 100.0
                           let h = hue c
                           let color = Forms.Color.FromHsv(h, 1.0, 1.0)
                           let symbol = if i = 0 then "diamond" else "circle"
                           trace coords size color symbol
                       )
               |> LazyList.toList
               |> Chart.Plot
               |> Chart.WithLayout layout
    let oldScriptLines = plot.GetInlineJS().Split('\n')
    let guid = Guid.NewGuid().ToString()
    let config = "{responsive:true,displayModeBar:false}"
    let newScriptLines =
            [ "<!DOCTYPE html>"
              "<html>"
              "<head>"
              """<meta charset="UTF-8" />"""
              """<script src="plotly_latest_min.js"></script>"""
              "</head>"
              "<body>"
              sprintf """<div id="%s"></div>""" guid
              "<script>"
              oldScriptLines.[1]
              oldScriptLines.[2]
              sprintf """Plotly.newPlot('%s', data, layout, %s);""" guid config
              "</script>"
              "</body>"
              "</html>" ]
            |> List.map (fun s -> s.Trim())
    String.Join("\n", newScriptLines)

let sampleQstate =
    QuantumPuzzleMechanics.Generator.nextQState (System.Random()) 4 ()

let quantumState(qState: Matrix.Matrix) =
    View.WebView(source=Xamarin.Forms.HtmlWebViewSource(Html=representationHtml qState))

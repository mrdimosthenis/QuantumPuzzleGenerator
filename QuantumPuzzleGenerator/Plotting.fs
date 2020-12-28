module QuantumPuzzleGenerator.Plotting

open System

open XPlot.Plotly
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
            (x + 3.0, y, z)
    | _ when i < 32 ->
        let (x, y, z) = coordinates (i - 16)
        (x, y + 4.0, z)
    | _ ->
        let (x, y, z) = coordinates (i - 32)
        (x, y, z + 5.0)

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
                showgrid=false,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            yaxis=Yaxis(
                title="",
                autorange=true,
                showgrid=false,
                ticks="",
                showticklabels=false,
                zeroline=false
            ),
            zaxis=Zaxis(
                title="",
                autorange=true,
                showgrid=false,
                ticks="",
                showticklabels=false,
                zeroline=false
            )
        )
    )

let trace (coords: float * float * float)
          (opacity: float)
          (size: float)
          (color: Forms.Color)
    :Scatter3d =
    let (x, y, z) = coords
    Scatter3d(
        x = [x],
        y = [y],
        z = [z],
        hoverinfo = "none",
        mode = "markers",
        showlegend = false,
        marker =
            Marker(
                color = Graphics.Elems.rgb color,
                size = size,
                opacity = opacity
            )
    )

let plot () =
    let p = []
            |> Chart.Plot
            |> Chart.WithLayout layout
    p.GetHtml()

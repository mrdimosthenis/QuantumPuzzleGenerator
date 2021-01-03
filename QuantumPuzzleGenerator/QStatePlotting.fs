module QuantumPuzzleGenerator.QStatePlotting

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
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

// types

type SpaceCoordinates = float * float * float

type Style = {
    Fill: string
    Stroke: string
}

type Row = {
    X: float
    Y: float
    Z: float
    Style: Style
}

type Data = Row list

type GeneratedByQuantumPuzzles = {
    Width: string
    Height: string
    Data: Data
}

// exceptions

exception InvalidNumOfQubits

// constants

let coordsForOneQubit: SpaceCoordinates LazyList =
    [ (0.25, 0.5, 0.5)
      (0.75, 0.5, 0.5) ]
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
    let v = c
            |> Complex.toPolar
            |> fst
    Forms.Color.FromRgba(v, v, v, 1.0)

let strokeColor (c: Complex.Complex): Forms.Color =
    if c = Complex.zero
        then Forms.Color.White
        else let h = c
                     |> Complex.toPolar
                     |> snd
                     |> (+) Math.PI
                     |> (*) (0.5 / Math.PI)
             Forms.Color.FromHsv(h, 1.0, 1.0)

let data (numOfQubits: int) (qState: Matrix.Matrix): Data =
    qState
    |> LazyList.concat
    |> LazyList.zip (coordsForQubits numOfQubits)
    |> LazyList.map
            (fun ((x, y, z), complex) -> {
                    X = x
                    Y = y
                    Z = z
                    Style = {
                        Fill = (fillColor complex).ToHex()
                        Stroke = (strokeColor complex).ToHex()
                    }
                }
            )
    |> LazyList.toList

let generatedByQuantumPuzzles
        (width: int)
        (height: int)
        (numOfQubits: int)
        (qState: Matrix.Matrix)
    : string =
    let content = {
        Width = sprintf "%ipx" width
        Height = sprintf "%ipx" height
        Data = data numOfQubits qState
    }
    let serializerSettings = new JsonSerializerSettings()
    serializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()
    JsonConvert.SerializeObject(content, serializerSettings)

module QuantumPuzzleMechanics.Graphics.Elems

open System

open Giraffe
open GiraffeViewEngine

open Xamarin.Forms

let lineWidth (size: float): string = size |> (*) 0.05 |> string

let rgbVal (v: float): string =
    v |> (*) 255.0 |> Math.Round |> int |> string

let rgb (color: Color): string =
    sprintf "rgb(%s, %s, %s)" (rgbVal color.R) (rgbVal color.G) (rgbVal color.B)

let transform (scale: float) (size: float): string =
    sprintf "translate(%s,%s)" (size |> (*) scale |> string) (size |> (*) scale |> string)

let letterBox (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag
        "rect"
        [ fillColor |> rgb |> attr "fill"
          strokeColor |> rgb |> attr "stroke"
          size |> (*) 0.5 |> string |> attr "width"
          size |> (*) 0.5 |> string |> attr "height"
          size
          |> (*) 0.0025
          |> string
          |> attr "stroke-width" ]
        []
    |> List.singleton
    |> tag "g" [ transform 0.25 size |> attr "transform" ]

let lineElem (x1: float) (y1: float) (x2: float) (y2: float) (color: Color) (strokeWidth: string) (size: float): XmlNode =
    let stroke = rgb color

    tag
        "line"
        [ attr "stroke" stroke
          size |> (*) x1 |> string |> attr "x1"
          size |> (*) y1 |> string |> attr "y1"
          size |> (*) x2 |> string |> attr "x2"
          size |> (*) y2 |> string |> attr "y2"
          attr "stroke-width" strokeWidth
          attr "stroke-linecap" "round" ]
        []

let letter (aX1Point: float)
           (aY1Point: float)
           (aX2Point: float)
           (aY2Point: float)
           (bX1Point: float)
           (bY1Point: float)
           (bX2Point: float)
           (bY2Point: float)
           (cX1Point: float)
           (cY1Point: float)
           (cX2Point: float)
           (cY2Point: float)
           (color: Color)
           (size: float)
           : XmlNode =
    let strokeWidth = lineWidth size

    let lineA =
        lineElem aX1Point aY1Point aX2Point aY2Point color strokeWidth size

    let lineB =
        lineElem bX1Point bY1Point bX2Point bY2Point color strokeWidth size

    let lineC =
        lineElem cX1Point cY1Point cX2Point cY2Point color strokeWidth size

    tag "g" [] [ lineA; lineB; lineC ]

let letterY (color: Color) (size: float): XmlNode =
    letter 0.4 0.4 0.5 0.5 0.6 0.4 0.5 0.5 0.5 0.5 0.5 0.6 color size

let letterZ (color: Color) (size: float): XmlNode =
    letter 0.4 0.4 0.6 0.4 0.6 0.4 0.4 0.6 0.4 0.6 0.6 0.6 color size

let letterH (color: Color) (size: float): XmlNode =
    letter 0.4 0.4 0.4 0.6 0.6 0.4 0.6 0.6 0.4 0.5 0.6 0.5 color size

// wires

let horizWire (color: Color) (size: float): XmlNode =
    let strokeWidth = lineWidth size
    lineElem 0.0 0.5 1.0 0.5 color strokeWidth size

let vertWire (color: Color) (size: float): XmlNode =
    let strokeWidth = lineWidth size
    lineElem 0.5 0.0 0.5 1.0 color strokeWidth size

let topVertWire (color: Color) (size: float): XmlNode =
    let strokeWidth = lineWidth size
    lineElem 0.5 0.0 0.5 0.5 color strokeWidth size

let bottomVertWire (color: Color) (size: float): XmlNode =
    let strokeWidth = lineWidth size
    lineElem 0.5 0.5 0.5 1.0 color strokeWidth size

// symbols

let controlSymbol (color: Color) (size: float): XmlNode =
    tag
        "circle"
        [ color |> rgb |> attr "fill"
          size |> (*) 0.5 |> string |> attr "cx"
          size |> (*) 0.5 |> string |> attr "cy"
          size |> (*) 0.15 |> string |> attr "r" ]
        []

let notSymbol (color: Color) (size: float): XmlNode =
    let innerSize = 0.2 * size
    let strokeWidth = lineWidth size

    let innerHorizWire =
        lineElem -1.0 0.0 1.0 0.0 color strokeWidth innerSize

    let innerVertWire =
        lineElem 0.0 -1.0 0.0 1.0 color strokeWidth innerSize

    let innerCircle =
        tag
            "circle"
            [ color |> rgb |> attr "stroke"
              attr "cx" "0"
              attr "cy" "0"
              innerSize |> string |> attr "r"
              attr "stroke-width" strokeWidth
              attr "fill-opacity" "0.0" ]
            []

    tag
        "g"
        [ transform 0.5 size |> attr "transform" ]
        [ innerHorizWire
          innerVertWire
          innerCircle ]

let swapSymbol (color: Color) (size: float): XmlNode =
    let strokeWidth = lineWidth size

    let lineA =
        lineElem 0.3 0.3 0.7 0.7 color strokeWidth size

    let lineB =
        lineElem 0.3 0.7 0.7 0.3 color strokeWidth size

    tag "g" [] [ lineA; lineB ]

let ySymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag
        "g"
        []
        [ letterBox fillColor strokeColor size
          letterY strokeColor size ]

let zSymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag
        "g"
        []
        [ letterBox fillColor strokeColor size
          letterZ strokeColor size ]

let hSymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag
        "g"
        []
        [ letterBox fillColor strokeColor size
          letterH strokeColor size ]

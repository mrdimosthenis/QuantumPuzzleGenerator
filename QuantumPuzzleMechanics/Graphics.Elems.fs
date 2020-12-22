module QuantumPuzzleMechanics.Graphics.Elems

open System

open Giraffe.ViewEngine
open Xamarin.Forms

let lineWidth (size: float): string =
    size
    |> (*) 0.05
    |> string

let rgbVal (v: float): string =
    v
    |> (*) 100.0
    |> Math.Round
    |> int
    |> string

let rgb(color: Color): string =
    sprintf "rgb(%s, %s, %s)"
            (rgbVal color.R)
            (rgbVal color.G)
            (rgbVal color.B)

let transform (scale: float) (size: float): string =
        sprintf "translate(%s,%s)"
                (size |> (*) scale |> string)
                (size |> (*) scale |> string)

let horizWire (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let x1 = string 0
    let y1 = size |> (*) 0.5 |> string
    let x2 = string size
    let y2 = size |> (*) 0.5 |> string
    let strokeWidth = lineWidth size
    tag "line"
        [ attr "stroke" stroke
          attr "x1" x1
          attr "y1" y1
          attr "x2" x2
          attr "y2" y2
          attr "stroke-width" strokeWidth ]
        []

let vertWire (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let x1 = size |> (*) 0.5 |> string
    let y1 = string 0
    let x2 = size |> (*) 0.5 |> string
    let y2 = string size
    let strokeWidth = lineWidth size
    tag "line"
        [ attr "stroke" stroke
          attr "x1" x1
          attr "y1" y1
          attr "x2" x2
          attr "y2" y2
          attr "stroke-width" strokeWidth ]
        []

let topVertWire (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let x1 = size |> (*) 0.5 |> string
    let y1 = string 0
    let x2 = size |> (*) 0.5 |> string
    let y2 = size |> (*) 0.5 |> string
    let strokeWidth = lineWidth size
    tag "line"
        [ attr "stroke" stroke
          attr "x1" x1
          attr "y1" y1
          attr "x2" x2
          attr "y2" y2
          attr "stroke-width" strokeWidth ]
        []

let bottomVertWire (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let x1 = size |> (*) 0.5 |> string
    let y1 = size |> (*) 0.5 |> string
    let x2 = size |> (*) 0.5 |> string
    let y2 = string size
    let strokeWidth = lineWidth size
    tag "line"
        [ attr "stroke" stroke
          attr "x1" x1
          attr "y1" y1
          attr "x2" x2
          attr "y2" y2
          attr "stroke-width" strokeWidth ]
        []

let controlSymbol (color: Color) (size: float): XmlNode =
    let fill = rgb color
    let cx = size |> (*) 0.5 |> string
    let cy = size |> (*) 0.5 |> string
    let r = size |> (*) 0.15 |> string
    tag "circle"
        [ attr "fill" fill
          attr "cx" cx
          attr "cy" cy
          attr "r" r ]
        []

let notSymbol (color: Color) (size: float): XmlNode =
    let innerSize = 0.2 * size
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let innerHorizWire =
            let x1 = innerSize |> (*) -1.0 |> string
            let y1 = string 0
            let x2 = string innerSize
            let y2 = string 0
            tag "line"
                [ attr "stroke" stroke
                  attr "x1" x1
                  attr "y1" y1
                  attr "x2" x2
                  attr "y2" y2
                  attr "stroke-width" strokeWidth ]
                []
    let innerVertWire =
            let x1 = string 0
            let y1 = innerSize |> (*) -1.0 |> string
            let x2 = string 0
            let y2 = string innerSize
            tag "line"
                [ attr "stroke" stroke
                  attr "x1" x1
                  attr "y1" y1
                  attr "x2" x2
                  attr "y2" y2
                  attr "stroke-width" strokeWidth ]
                []
    let innerCircle =
            let cx = string 0
            let cy = string 0
            let r = string innerSize
            tag "circle"
                [ attr "stroke" stroke
                  attr "cx" cx
                  attr "cy" cy
                  attr "r" r
                  attr "stroke-width" strokeWidth
                  attr "fill-opacity" "0.0" ]
                []
    let transformVal = transform 0.5 size
    tag "g"
        [ attr "transform" transformVal ]
        [ innerHorizWire
          innerVertWire
          innerCircle ]

let swapSymbol (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA = let x1 = size |> (*) 0.3 |> string
                let y1 = size |> (*) 0.3 |> string
                let x2 = size |> (*) 0.7 |> string
                let y2 = size |> (*) 0.7 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineB = let x1 = size |> (*) 0.3 |> string
                let y1 = size |> (*) 0.7 |> string
                let x2 = size |> (*) 0.7 |> string
                let y2 = size |> (*) 0.3 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    tag "g" [] [ lineA; lineB ]

let letterBox (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    let rect = let fill =  rgb fillColor
               let stroke = rgb strokeColor
               let width = size |> (*) 0.5 |> string
               let strokeWidth = size |> (*) 0.0025 |> string
               tag "rect"
                   [ attr "fill" fill
                     attr "stroke" stroke
                     attr "width" width
                     attr "height" width
                     attr "stroke-width" strokeWidth ]
                   []
    let transformVal = transform 0.25 size
    tag "g" [ attr "transform" transformVal ] [ rect ]

let letterY (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA = let x1 = size |> (*) 0.35 |> string
                let y1 = size |> (*) 0.35 |> string
                let x2 = size |> (*) 0.5 |> string
                let y2 = size |> (*) 0.5 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineB = let x1 = size |> (*) 0.65 |> string
                let y1 = size |> (*) 0.35 |> string
                let x2 = size |> (*) 0.5 |> string
                let y2 = size |> (*) 0.5 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineC = let x1 = size |> (*) 0.5 |> string
                let y1 = size |> (*) 0.5 |> string
                let x2 = size |> (*) 0.5 |> string
                let y2 = size |> (*) 0.65 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    tag "g" [] [ lineA; lineB; lineC ]

let letterZ (color: Color) (size: float): XmlNode =
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA = let x1 = size |> (*) 0.35 |> string
                let y1 = size |> (*) 0.35 |> string
                let x2 = size |> (*) 0.65 |> string
                let y2 = size |> (*) 0.35 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineB = let x1 = size |> (*) 0.65 |> string
                let y1 = size |> (*) 0.35 |> string
                let x2 = size |> (*) 0.35 |> string
                let y2 = size |> (*) 0.65 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineC = let x1 = size |> (*) 0.35 |> string
                let y1 = size |> (*) 0.65 |> string
                let x2 = size |> (*) 0.65 |> string
                let y2 = size |> (*) 0.65 |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    tag "g" [] [ lineA; lineB; lineC ]

let ySymbol (fillColor: Color) (color: Color) (size: float): XmlNode =
    tag "g" [] [ letterBox fillColor color size; letterY color size ]

let zSymbol (fillColor: Color) (color: Color) (size: float): XmlNode =
    tag "g" [] [ letterBox fillColor color size; letterZ color size ]

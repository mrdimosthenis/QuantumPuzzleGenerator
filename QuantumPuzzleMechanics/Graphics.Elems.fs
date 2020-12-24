﻿module QuantumPuzzleMechanics.Graphics.Elems

open System

open Giraffe.ViewEngine
open Xamarin.Forms

let lineWidth (size: float): string =
    size
    |> (*) 0.05
    |> string

let rgbVal (v: float): string =
    v
    |> (*) 255.0
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
           (size: float): XmlNode =
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA = let x1 = size |> (*) aX1Point |> string
                let y1 = size |> (*) aY1Point |> string
                let x2 = size |> (*) aX2Point |> string
                let y2 = size |> (*) aY2Point |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineB = let x1 = size |> (*) bX1Point |> string
                let y1 = size |> (*) bY1Point |> string
                let x2 = size |> (*) bX2Point |> string
                let y2 = size |> (*) bY2Point |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    let lineC = let x1 = size |> (*) cX1Point |> string
                let y1 = size |> (*) cY1Point |> string
                let x2 = size |> (*) cX2Point |> string
                let y2 = size |> (*) cY2Point |> string
                tag "line"
                    [ attr "stroke" stroke
                      attr "x1" x1
                      attr "y1" y1
                      attr "x2" x2
                      attr "y2" y2
                      attr "stroke-width" strokeWidth ]
                    []
    tag "g" [] [ lineA; lineB; lineC ]

let letterY (color: Color) (size: float): XmlNode =
    letter 0.4
           0.4
           0.5
           0.5
           0.6
           0.4
           0.5
           0.5
           0.5
           0.5
           0.5
           0.6
           color
           size

let letterZ (color: Color) (size: float): XmlNode =
    letter 0.4
           0.4
           0.6
           0.4
           0.6
           0.4
           0.4
           0.6
           0.4
           0.6
           0.6
           0.6
           color
           size

let letterH (color: Color) (size: float): XmlNode =
    letter 0.4
           0.4
           0.4
           0.6
           0.6
           0.4
           0.6
           0.6
           0.4
           0.5
           0.6
           0.5
           color
           size

// wires

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

// symbols

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

let ySymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag "g" [] [ letterBox fillColor strokeColor size; letterY strokeColor size ]

let zSymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag "g" [] [ letterBox fillColor strokeColor size; letterZ strokeColor size ]
    
let hSymbol (fillColor: Color) (strokeColor: Color) (size: float): XmlNode =
    tag "g" [] [ letterBox fillColor strokeColor size; letterH strokeColor size ]

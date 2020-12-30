module QuantumPuzzleMechanics.Graphics.Elems

open System

open Fable.React
open Fable.React.Props

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

let letterBox (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    rect [ fillColor |> rgb |> SVGAttr.Fill
           strokeColor |> rgb |> SVGAttr.Stroke
           SVGAttr.Custom("width", size |> (*) 0.5 |> string)
           SVGAttr.Custom("height", size |> (*) 0.5 |> string)
           SVGAttr.Custom("stroke-width", size |> (*) 0.0025 |> string) ]
         []
    |> List.singleton
    |> g [ transform 0.25 size |> SVGAttr.Transform ]

let lineElem (x1: float)
       (y1: float)
       (x2: float)
       (y2: float)
       (color: Color)
       (strokeWidth: string)
       (size: float)
    : ReactElement =
    let stroke = rgb color
    line [ SVGAttr.Stroke stroke
           SVGAttr.Custom("x1", size |> (*) x1 |> string)
           SVGAttr.Custom("y1", size |> (*) y1 |> string)
           SVGAttr.Custom("x2", size |> (*) x2 |> string)
           SVGAttr.Custom("y2", size |> (*) y2 |> string)
           SVGAttr.Custom("stroke-width", strokeWidth)
           SVGAttr.StrokeLinecap "round" ]
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
    : ReactElement =
    let strokeWidth = lineWidth size
    let lineA = lineElem aX1Point aY1Point aX2Point aY2Point color strokeWidth size
    let lineB = lineElem bX1Point bY1Point bX2Point bY2Point color strokeWidth size
    let lineC = lineElem cX1Point cY1Point cX2Point cY2Point color strokeWidth size
    g [] [ lineA; lineB; lineC ]

let letterY (color: Color) (size: float): ReactElement =
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

let letterZ (color: Color) (size: float): ReactElement =
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

let letterH (color: Color) (size: float): ReactElement =
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

let letterT (color: Color) (size: float): ReactElement =
    letter 0.4
           0.4
           0.5
           0.4
           0.5
           0.4
           0.6
           0.4
           0.5
           0.4
           0.5
           0.6
           color
           size

// wires

let horizWire (color: Color) (size: float): ReactElement =
    let strokeWidth = lineWidth size
    lineElem 0.0 0.5 1.0 0.5 color strokeWidth size

let vertWire (color: Color) (size: float): ReactElement =
    let strokeWidth = lineWidth size
    lineElem 0.5 0.0 0.5 1.0 color strokeWidth size

let leftHorizWire (color: Color) (size: float): ReactElement =
    let strokeWidth = lineWidth size
    lineElem 0.0 0.5 0.5 0.5 color strokeWidth size

let rightHorizWire (color: Color) (size: float): ReactElement =
    let strokeWidth = lineWidth size
    lineElem 0.5 0.5 1.0 0.5 color strokeWidth size

// symbols

let controlSymbol (color: Color) (size: float): ReactElement =
    circle [ color |> rgb |> SVGAttr.Fill
             SVGAttr.Custom("cx", size |> (*) 0.5 |> string)
             SVGAttr.Custom("cy", size |> (*) 0.5 |> string)
             SVGAttr.Custom("r", size |> (*) 0.15 |> string) ]
           []

let notSymbol (color: Color) (size: float): ReactElement =
    let innerSize = 0.2 * size
    let strokeWidth = lineWidth size
    let innerHorizWire = lineElem -1.0 0.0 1.0 0.0 color strokeWidth innerSize
    let innerVertWire = lineElem 0.0 -1.0 0.0 1.0 color strokeWidth innerSize
    let innerCircle =
        circle [ color |> rgb |> SVGAttr.Stroke
                 SVGAttr.Custom("cx", string 0)
                 SVGAttr.Custom("cy", string 0)
                 SVGAttr.Custom("r", string innerSize)
                 SVGAttr.Custom("stroke-width", strokeWidth)
                 SVGAttr.Custom("fill-opacity", "0.0") ]
               []
    g [ transform 0.5 size |> SVGAttr.Transform ]
      [ innerHorizWire
        innerVertWire
        innerCircle ]

let swapSymbol (color: Color) (size: float): ReactElement =
    let strokeWidth = lineWidth size
    let lineA = lineElem 0.3 0.3 0.7 0.7 color strokeWidth size
    let lineB = lineElem 0.3 0.7 0.7 0.3 color strokeWidth size
    g [] [ lineA; lineB ]

let ySymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterY strokeColor size ]

let zSymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterZ strokeColor size ]
    
let hSymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterH strokeColor size ]

let tSymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterT strokeColor size ]

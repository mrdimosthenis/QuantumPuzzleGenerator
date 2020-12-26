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
           (size: float): ReactElement =
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA = line [ SVGAttr.Stroke stroke
                       SVGAttr.Custom("x1", size |> (*) aX1Point |> string)
                       SVGAttr.Custom("y1", size |> (*) aY1Point |> string)
                       SVGAttr.Custom("x2", size |> (*) aX2Point |> string)
                       SVGAttr.Custom("y2", size |> (*) aY2Point |> string)
                       SVGAttr.Custom("stroke-width", strokeWidth) ]
                     []
    let lineB = line [ SVGAttr.Stroke stroke
                       SVGAttr.Custom("x1", size |> (*) bX1Point |> string)
                       SVGAttr.Custom("y1", size |> (*) bY1Point |> string)
                       SVGAttr.Custom("x2", size |> (*) bX2Point |> string)
                       SVGAttr.Custom("y2", size |> (*) bY2Point |> string)
                       SVGAttr.Custom("stroke-width", strokeWidth) ]
                     []
    let lineC = line [ SVGAttr.Stroke stroke
                       SVGAttr.Custom("x1", size |> (*) cX1Point |> string)
                       SVGAttr.Custom("y1", size |> (*) cY1Point |> string)
                       SVGAttr.Custom("x2", size |> (*) cX2Point |> string)
                       SVGAttr.Custom("y2", size |> (*) cY2Point |> string)
                       SVGAttr.Custom("stroke-width", strokeWidth) ]
                     []
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

// wires

let horizWire (color: Color) (size: float): ReactElement =
    line [ color |> rgb |> SVGAttr.Stroke
           SVGAttr.Custom("x1", string 0)
           SVGAttr.Custom("y1", size |> (*) 0.5 |> string)
           SVGAttr.Custom("x2", string size)
           SVGAttr.Custom("y2", size |> (*) 0.5 |> string)
           SVGAttr.Custom("stroke-width", lineWidth size) ]
         []

let vertWire (color: Color) (size: float): ReactElement =
    line [ color |> rgb |> SVGAttr.Stroke
           SVGAttr.Custom("x1", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y1", string 0)
           SVGAttr.Custom("x2", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y2", string size)
           SVGAttr.Custom("stroke-width", lineWidth size) ]
         []

let topVertWire (color: Color) (size: float): ReactElement =
    line [ color |> rgb |> SVGAttr.Stroke
           SVGAttr.Custom("x1", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y1", string 0)
           SVGAttr.Custom("x2", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y2", size |> (*) 0.5 |> string)
           SVGAttr.Custom("stroke-width", lineWidth size) ]
         []

let bottomVertWire (color: Color) (size: float): ReactElement =
    line [ color |> rgb |> SVGAttr.Stroke
           SVGAttr.Custom("x1", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y1", size |> (*) 0.5 |> string)
           SVGAttr.Custom("x2", size |> (*) 0.5 |> string)
           SVGAttr.Custom("y2", string size)
           SVGAttr.Custom("stroke-width", lineWidth size) ]
         []

// symbols

let controlSymbol (color: Color) (size: float): ReactElement =
    circle [ color |> rgb |> SVGAttr.Fill
             SVGAttr.Custom("cx", size |> (*) 0.5 |> string)
             SVGAttr.Custom("cy", size |> (*) 0.5 |> string)
             SVGAttr.Custom("r", size |> (*) 0.15 |> string) ]
           []

let notSymbol (color: Color) (size: float): ReactElement =
    let innerSize = 0.2 * size
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let innerHorizWire =
            line [ SVGAttr.Stroke stroke
                   SVGAttr.Custom("x1", innerSize |> (*) -1.0 |> string)
                   SVGAttr.Custom("y1", string 0)
                   SVGAttr.Custom("x2", string innerSize)
                   SVGAttr.Custom("y2", string 0)
                   SVGAttr.Custom("stroke-width", strokeWidth) ]
                 []
    let innerVertWire =
            line [ SVGAttr.Stroke stroke
                   SVGAttr.Custom("x1", string 0)
                   SVGAttr.Custom("y1", innerSize |> (*) -1.0 |> string)
                   SVGAttr.Custom("x2", string 0)
                   SVGAttr.Custom("y2", string innerSize)
                   SVGAttr.Custom("stroke-width", strokeWidth) ]
                 []
    let innerCircle =
        circle [ SVGAttr.Stroke stroke
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
    let stroke = rgb color
    let strokeWidth = lineWidth size
    let lineA =
        line [ SVGAttr.Stroke stroke
               SVGAttr.Custom("x1", size |> (*) 0.3 |> string)
               SVGAttr.Custom("y1", size |> (*) 0.3 |> string)
               SVGAttr.Custom("x2", size |> (*) 0.7 |> string)
               SVGAttr.Custom("y2", size |> (*) 0.7 |> string)
               SVGAttr.Custom("stroke-width", strokeWidth) ]
             []
    let lineB =
        line [ SVGAttr.Stroke stroke
               SVGAttr.Custom("x1", size |> (*) 0.3 |> string)
               SVGAttr.Custom("y1", size |> (*) 0.7 |> string)
               SVGAttr.Custom("x2", size |> (*) 0.7 |> string)
               SVGAttr.Custom("y2", size |> (*) 0.3 |> string)
               SVGAttr.Custom("stroke-width", strokeWidth) ]
             []
    g [] [ lineA; lineB ]

let ySymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterY strokeColor size ]

let zSymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterZ strokeColor size ]
    
let hSymbol (fillColor: Color) (strokeColor: Color) (size: float): ReactElement =
    g [] [ letterBox fillColor strokeColor size; letterH strokeColor size ]

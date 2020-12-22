module QuantumPuzzleMechanics.Graphics.ElemsTest

open Xunit
open FsUnit.Xunit

open Giraffe.ViewEngine
open Xamarin.Forms

open QuantumPuzzleMechanics.Graphics.Elems

[<Fact>]
let ``Horizontal wire`` () =
    horizWire Color.Gray 100.0
    |> RenderView.AsString.xmlNode
    |> should equal """<line stroke="rgb(50, 50, 50)" x1="0" y1="50" x2="100" y2="50" stroke-width="5"></line>"""

[<Fact>]
let ``Vertical wire`` () =
    vertWire Color.Red 100.0
    |> RenderView.AsString.xmlNode
    |> should equal """<line stroke="rgb(100, 0, 0)" x1="50" y1="0" x2="50" y2="100" stroke-width="5"></line>"""

[<Fact>]
let ``Control symbol`` () =
    controlSymbol Color.Green 100.0
    |> RenderView.AsString.xmlNode
    |> should equal """<circle fill="rgb(0, 50, 0)" cx="50" cy="50" r="15"></circle>"""

[<Fact>]
let ``Not symbol`` () =
    notSymbol Color.Green 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g transform="translate(50,50)">"""
                   """<line stroke="rgb(0, 50, 0)" x1="-20" y1="0" x2="20" y2="0" stroke-width="5"></line>"""
                   """<line stroke="rgb(0, 50, 0)" x1="0" y1="-20" x2="0" y2="20" stroke-width="5"></line>"""
                   """<circle stroke="rgb(0, 50, 0)" cx="0" cy="0" r="20" stroke-width="5" fill-opacity="0.0"></circle>"""
                   """</g>""" |]

[<Fact>]
let ``Swap symbol`` () =
    swapSymbol Color.Brown 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g>"""
                   """<line stroke="rgb(65, 16, 16)" x1="30" y1="30" x2="70" y2="70" stroke-width="5"></line>"""
                   """<line stroke="rgb(65, 16, 16)" x1="30" y1="70" x2="70" y2="30" stroke-width="5"></line>"""
                   """</g>""" |]

[<Fact>]
let ``Letter box`` () =
    letterBox Color.Cyan Color.Khaki 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g transform="translate(25,25)">"""
                   """<rect fill="rgb(0, 100, 100)" stroke="rgb(94, 90, 55)" width="50" height="50" stroke-width="0.25"></rect>"""
                   """</g>""" |]

[<Fact>]
let ``Letter Y`` () =
    letterY Color.LightGray 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g>"""
                   """<line stroke="rgb(83, 83, 83)" x1="35" y1="35" x2="50" y2="50" stroke-width="5"></line>"""
                   """<line stroke="rgb(83, 83, 83)" x1="65" y1="35" x2="50" y2="50" stroke-width="5"></line>"""
                   """<line stroke="rgb(83, 83, 83)" x1="50" y1="50" x2="50" y2="65" stroke-width="5"></line>"""
                   """</g>""" |]

[<Fact>]
let ``Letter Z`` () =
    letterZ Color.LightGray 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g>"""
                   """<line stroke="rgb(83, 83, 83)" x1="35" y1="35" x2="65" y2="35" stroke-width="5"></line>"""
                   """<line stroke="rgb(83, 83, 83)" x1="65" y1="35" x2="35" y2="65" stroke-width="5"></line>"""
                   """<line stroke="rgb(83, 83, 83)" x1="35" y1="65" x2="65" y2="65" stroke-width="5"></line>"""
                   """</g>""" |]

[<Fact>]
let ``Y symbol`` () =
    ySymbol Color.AntiqueWhite Color.Blue 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g>"""
                   """<g transform="translate(25,25)">"""
                   """<rect fill="rgb(98, 92, 84)" stroke="rgb(0, 0, 100)" width="50" height="50" stroke-width="0.25"></rect>"""
                   """</g>"""
                   """<g>"""
                   """<line stroke="rgb(0, 0, 100)" x1="35" y1="35" x2="50" y2="50" stroke-width="5"></line>"""
                   """<line stroke="rgb(0, 0, 100)" x1="65" y1="35" x2="50" y2="50" stroke-width="5"></line>"""
                   """<line stroke="rgb(0, 0, 100)" x1="50" y1="50" x2="50" y2="65" stroke-width="5"></line>"""
                   """</g>"""
                   """</g>""" |]

[<Fact>]
let ``Z symbol`` () =
    zSymbol Color.Blue Color.AntiqueWhite 100.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g>"""
                   """<g transform="translate(25,25)">"""
                   """<rect fill="rgb(0, 0, 100)" stroke="rgb(98, 92, 84)" width="50" height="50" stroke-width="0.25"></rect>"""
                   """</g>"""
                   """<g>"""
                   """<line stroke="rgb(98, 92, 84)" x1="35" y1="35" x2="65" y2="35" stroke-width="5"></line>"""
                   """<line stroke="rgb(98, 92, 84)" x1="65" y1="35" x2="35" y2="65" stroke-width="5"></line>"""
                   """<line stroke="rgb(98, 92, 84)" x1="35" y1="65" x2="65" y2="65" stroke-width="5"></line>"""
                   """</g>"""
                   """</g>""" |]

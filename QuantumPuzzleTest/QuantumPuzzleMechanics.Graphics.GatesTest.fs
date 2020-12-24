module QuantumPuzzleMechanics.Graphics.GatesTest

open Xunit
open FsUnit.Xunit

open Giraffe.ViewEngine
open Xamarin.Forms

open QuantumPuzzleMechanics.Graphics.Gates

[<Fact>]
let ``H gate1 graphics on a single qubit`` () =
    gate1Graphics 1 0 HSymbol Color.LightGreen 200.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g><g transform="translate(0,0)"><g>"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """<g><g transform="translate(50,50)">"""
                   """<rect fill="rgb(255, 255, 255)" stroke="rgb(144, 238, 144)" width="100" height="100" stroke-width="0.5"></rect>"""
                   """</g><g>"""
                   """<line stroke="rgb(144, 238, 144)" x1="80" y1="80" x2="80" y2="120" stroke-width="10"></line>"""
                   """<line stroke="rgb(144, 238, 144)" x1="120" y1="80" x2="120" y2="120" stroke-width="10"></line>"""
                   """<line stroke="rgb(144, 238, 144)" x1="80" y1="100" x2="120" y2="100" stroke-width="10"></line>"""
                   """</g></g></g></g></g>""" |]

[<Fact>]
let ``X gate1 graphics on the last of two qubits`` () =
    gate1Graphics 2 1 NotSymbol Color.DarkRed 200.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g><g transform="translate(0,0)">"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """</g><g transform="translate(0,200)"><g>"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """<g transform="translate(100,100)">"""
                   """<line stroke="rgb(139, 0, 0)" x1="-40" y1="0" x2="40" y2="0" stroke-width="10"></line>"""
                   """<line stroke="rgb(139, 0, 0)" x1="0" y1="-40" x2="0" y2="40" stroke-width="10"></line>"""
                   """<circle stroke="rgb(139, 0, 0)" cx="0" cy="0" r="40" stroke-width="10" fill-opacity="0.0"></circle>"""
                   """</g></g></g></g>""" |]

[<Fact>]
let ``Z gate1 graphics on the middle of three qubits`` () =
    gate1Graphics 3 1 ZSymbol Color.LightGreen 200.0
    |> RenderView.AsString.xmlNode
    |> should equal
    <| String.concat
                ""
                [| """<g><g transform="translate(0,0)">"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """</g><g transform="translate(0,200)"><g>"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """<g><g transform="translate(50,50)">"""
                   """<rect fill="rgb(255, 255, 255)" stroke="rgb(144, 238, 144)" width="100" height="100" stroke-width="0.5"></rect>"""
                   """</g><g>"""
                   """<line stroke="rgb(144, 238, 144)" x1="80" y1="80" x2="120" y2="80" stroke-width="10"></line>"""
                   """<line stroke="rgb(144, 238, 144)" x1="120" y1="80" x2="80" y2="120" stroke-width="10"></line>"""
                   """<line stroke="rgb(144, 238, 144)" x1="80" y1="120" x2="120" y2="120" stroke-width="10"></line>"""
                   """</g></g></g></g><g transform="translate(0,400)">"""
                   """<line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10"></line>"""
                   """</g></g>""" |]

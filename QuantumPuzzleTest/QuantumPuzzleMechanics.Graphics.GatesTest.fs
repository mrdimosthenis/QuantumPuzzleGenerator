﻿module QuantumPuzzleMechanics.Graphics.GatesTest

open Xunit
open FsUnit.Xunit

open Giraffe
open GiraffeViewEngine
open Xamarin.Forms

open QuantumPuzzleMechanics
open QuantumPuzzleMechanics.Graphics.Gates

[<Fact>]
let ``H gate1 graphics on a single qubit`` () =
    gate1Graphics 1 0 HSymbol Color.LightGreen 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><g><g transform="translate(50,50)"><rect fill="rgb(255, 255, 255)" stroke="rgb(144, 238, 144)" width="100" height="100" stroke-width="0.5"></rect></g><g><line stroke="rgb(144, 238, 144)" x1="80" y1="80" x2="80" y2="120" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(144, 238, 144)" x1="120" y1="80" x2="120" y2="120" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(144, 238, 144)" x1="80" y1="100" x2="120" y2="100" stroke-width="10" stroke-linecap="round"></line></g></g></g></g></g>"""

[<Fact>]
let ``X gate1 graphics on the last of two qubits`` () =
    gate1Graphics 2 1 NotSymbol Color.DarkRed 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><g transform="translate(100,100)"><line stroke="rgb(139, 0, 0)" x1="-40" y1="0" x2="40" y2="0" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(139, 0, 0)" x1="0" y1="-40" x2="0" y2="40" stroke-width="10" stroke-linecap="round"></line><circle stroke="rgb(139, 0, 0)" cx="0" cy="0" r="40" stroke-width="10" fill-opacity="0.0"></circle></g></g></g></g>"""

[<Fact>]
let ``Z gate1 graphics on the middle of three qubits`` () =
    gate1Graphics 3 1 ZSymbol Color.LightGreen 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><g><g transform="translate(50,50)"><rect fill="rgb(255, 255, 255)" stroke="rgb(144, 238, 144)" width="100" height="100" stroke-width="0.5"></rect></g><g><line stroke="rgb(144, 238, 144)" x1="80" y1="80" x2="120" y2="80" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(144, 238, 144)" x1="120" y1="80" x2="80" y2="120" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(144, 238, 144)" x1="80" y1="120" x2="120" y2="120" stroke-width="10" stroke-linecap="round"></line></g></g></g></g><g transform="translate(0,400)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g></g>"""

[<Fact>]
let ``CY gate2 graphics on two qubits`` () =
    gate2Graphics 2 0 1 ControlSymbol YSymbol Color.Gray 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 128, 128)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(128, 128, 128)" cx="100" cy="100" r="30"></circle></g></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 128, 128)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><g><g transform="translate(50,50)"><rect fill="rgb(255, 255, 255)" stroke="rgb(128, 128, 128)" width="100" height="100" stroke-width="0.5"></rect></g><g><line stroke="rgb(128, 128, 128)" x1="80" y1="80" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 128, 128)" x1="120" y1="80" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 128, 128)" x1="100" y1="100" x2="100" y2="120" stroke-width="10" stroke-linecap="round"></line></g></g></g></g></g>"""

[<Fact>]
let ``Inverted CX gate2 graphics on the first and last of three qubits`` () =
    gate2Graphics 3 2 0 ControlSymbol NotSymbol Color.MediumSeaGreen 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(60, 179, 113)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><g transform="translate(100,100)"><line stroke="rgb(60, 179, 113)" x1="-40" y1="0" x2="40" y2="0" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(60, 179, 113)" x1="0" y1="-40" x2="0" y2="40" stroke-width="10" stroke-linecap="round"></line><circle stroke="rgb(60, 179, 113)" cx="0" cy="0" r="40" stroke-width="10" fill-opacity="0.0"></circle></g></g></g><g transform="translate(0,200)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(60, 179, 113)" x1="100" y1="0" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,400)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(60, 179, 113)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(60, 179, 113)" cx="100" cy="100" r="30"></circle></g></g></g>"""

[<Fact>]
let ``Swap gate2 graphics on the middles of four qubits`` () =
    gate2Graphics 4 1 2 SwapSymbol SwapSymbol Color.Purple 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 0, 128)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><g><line stroke="rgb(128, 0, 128)" x1="60" y1="60" x2="140" y2="140" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 0, 128)" x1="60" y1="140" x2="140" y2="60" stroke-width="10" stroke-linecap="round"></line></g></g></g><g transform="translate(0,400)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 0, 128)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><g><line stroke="rgb(128, 0, 128)" x1="60" y1="60" x2="140" y2="140" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(128, 0, 128)" x1="60" y1="140" x2="140" y2="60" stroke-width="10" stroke-linecap="round"></line></g></g></g><g transform="translate(0,600)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g></g>"""

[<Fact>]
let ``Inverted CCX gate3 graphics on three qubits`` () =
    gate3Graphics 3 2 0 1 ControlSymbol NotSymbol ControlSymbol Color.Moccasin 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(255, 228, 181)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><g transform="translate(100,100)"><line stroke="rgb(255, 228, 181)" x1="-40" y1="0" x2="40" y2="0" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(255, 228, 181)" x1="0" y1="-40" x2="0" y2="40" stroke-width="10" stroke-linecap="round"></line><circle stroke="rgb(255, 228, 181)" cx="0" cy="0" r="40" stroke-width="10" fill-opacity="0.0"></circle></g></g></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(255, 228, 181)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(255, 228, 181)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(255, 228, 181)" cx="100" cy="100" r="30"></circle></g></g><g transform="translate(0,400)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(255, 228, 181)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(255, 228, 181)" cx="100" cy="100" r="30"></circle></g></g></g>"""

[<Fact>]
let ``Shuffled CSwap gate3 graphics on five qubits`` () =
    gate3Graphics 5 4 2 1 SwapSymbol ControlSymbol SwapSymbol Color.SpringGreen 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><g><line stroke="rgb(0, 255, 127)" x1="60" y1="60" x2="140" y2="140" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="60" y1="140" x2="140" y2="60" stroke-width="10" stroke-linecap="round"></line></g></g></g><g transform="translate(0,400)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(0, 255, 127)" cx="100" cy="100" r="30"></circle></g></g><g transform="translate(0,600)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="100" y1="0" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,800)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><g><line stroke="rgb(0, 255, 127)" x1="60" y1="60" x2="140" y2="140" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(0, 255, 127)" x1="60" y1="140" x2="140" y2="60" stroke-width="10" stroke-linecap="round"></line></g></g></g></g>"""

[<Fact>]
let ``Inverted CZ gate graphics on the second and fourth of five qubits`` () =
    let gate = Quantum.Gate.CZGate (3, 1)
    gateGraphics 5 gate Color.Plum 200.0
    |> renderXmlNode
    |> should equal """<g><g transform="translate(0,0)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,200)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(221, 160, 221)" x1="100" y1="100" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line><g><g transform="translate(50,50)"><rect fill="rgb(255, 255, 255)" stroke="rgb(221, 160, 221)" width="100" height="100" stroke-width="0.5"></rect></g><g><line stroke="rgb(221, 160, 221)" x1="80" y1="80" x2="120" y2="80" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(221, 160, 221)" x1="120" y1="80" x2="80" y2="120" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(221, 160, 221)" x1="80" y1="120" x2="120" y2="120" stroke-width="10" stroke-linecap="round"></line></g></g></g></g><g transform="translate(0,400)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(221, 160, 221)" x1="100" y1="0" x2="100" y2="200" stroke-width="10" stroke-linecap="round"></line></g><g transform="translate(0,600)"><g><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line><line stroke="rgb(221, 160, 221)" x1="100" y1="0" x2="100" y2="100" stroke-width="10" stroke-linecap="round"></line><circle fill="rgb(221, 160, 221)" cx="100" cy="100" r="30"></circle></g></g><g transform="translate(0,800)"><line stroke="rgb(0, 0, 0)" x1="0" y1="100" x2="200" y2="100" stroke-width="10" stroke-linecap="round"></line></g></g>"""

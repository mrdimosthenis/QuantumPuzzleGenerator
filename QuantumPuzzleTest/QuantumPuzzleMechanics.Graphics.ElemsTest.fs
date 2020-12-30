module QuantumPuzzleMechanics.Graphics.ElemsTest

open Xunit
open FsUnit.Xunit

open Fable

open Xamarin.Forms

open QuantumPuzzleMechanics.Graphics.Elems

[<Fact>]
let ``Horizontal wire`` () =
    horizWire Color.Gray 100.0
    |> ReactServer.renderToString
    |> should equal """<line data-reactroot="" stroke="rgb(128, 128, 128)" x1="0" y1="50" x2="100" y2="50" stroke-width="5" stroke-linecap="round"></line>"""

[<Fact>]
let ``Vertical wire`` () =
    vertWire Color.Red 100.0
    |> ReactServer.renderToString
    |> should equal """<line data-reactroot="" stroke="rgb(255, 0, 0)" x1="50" y1="0" x2="50" y2="100" stroke-width="5" stroke-linecap="round"></line>"""

[<Fact>]
let ``Control symbol`` () =
    controlSymbol Color.Green 100.0
    |> ReactServer.renderToString
    |> should equal """<circle data-reactroot="" fill="rgb(0, 128, 0)" cx="50" cy="50" r="15"></circle>"""

[<Fact>]
let ``Not symbol`` () =
    notSymbol Color.Green 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot="" transform="translate(50,50)"><line stroke="rgb(0, 128, 0)" x1="-20" y1="0" x2="20" y2="0" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(0, 128, 0)" x1="0" y1="-20" x2="0" y2="20" stroke-width="5" stroke-linecap="round"></line><circle stroke="rgb(0, 128, 0)" cx="0" cy="0" r="20" stroke-width="5" fill-opacity="0.0"></circle></g>"""

[<Fact>]
let ``Swap symbol`` () =
    swapSymbol Color.Brown 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><line stroke="rgb(165, 42, 42)" x1="30" y1="30" x2="70" y2="70" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(165, 42, 42)" x1="30" y1="70" x2="70" y2="30" stroke-width="5" stroke-linecap="round"></line></g>"""

[<Fact>]
let ``Letter box`` () =
    letterBox Color.Cyan Color.Khaki 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot="" transform="translate(25,25)"><rect fill="rgb(0, 255, 255)" stroke="rgb(240, 230, 140)" width="50" height="50" stroke-width="0.25"></rect></g>"""

[<Fact>]
let ``Letter Y`` () =
    letterY Color.LightGray 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><line stroke="rgb(211, 211, 211)" x1="40" y1="40" x2="50" y2="50" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(211, 211, 211)" x1="60" y1="40" x2="50" y2="50" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(211, 211, 211)" x1="50" y1="50" x2="50" y2="60" stroke-width="5" stroke-linecap="round"></line></g>"""

[<Fact>]
let ``Letter Z`` () =
    letterZ Color.LightGray 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><line stroke="rgb(211, 211, 211)" x1="40" y1="40" x2="60" y2="40" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(211, 211, 211)" x1="60" y1="40" x2="40" y2="60" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(211, 211, 211)" x1="40" y1="60" x2="60" y2="60" stroke-width="5" stroke-linecap="round"></line></g>"""

[<Fact>]
let ``Y symbol`` () =
    ySymbol Color.AntiqueWhite Color.Blue 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><g transform="translate(25,25)"><rect fill="rgb(250, 235, 215)" stroke="rgb(0, 0, 255)" width="50" height="50" stroke-width="0.25"></rect></g><g><line stroke="rgb(0, 0, 255)" x1="40" y1="40" x2="50" y2="50" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(0, 0, 255)" x1="60" y1="40" x2="50" y2="50" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(0, 0, 255)" x1="50" y1="50" x2="50" y2="60" stroke-width="5" stroke-linecap="round"></line></g></g>"""

[<Fact>]
let ``Z symbol`` () =
    zSymbol Color.Blue Color.AntiqueWhite 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><g transform="translate(25,25)"><rect fill="rgb(0, 0, 255)" stroke="rgb(250, 235, 215)" width="50" height="50" stroke-width="0.25"></rect></g><g><line stroke="rgb(250, 235, 215)" x1="40" y1="40" x2="60" y2="40" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(250, 235, 215)" x1="60" y1="40" x2="40" y2="60" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(250, 235, 215)" x1="40" y1="60" x2="60" y2="60" stroke-width="5" stroke-linecap="round"></line></g></g>"""

[<Fact>]
let ``H symbol`` () =
    hSymbol Color.LightPink Color.Gold 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><g transform="translate(25,25)"><rect fill="rgb(255, 182, 193)" stroke="rgb(255, 215, 0)" width="50" height="50" stroke-width="0.25"></rect></g><g><line stroke="rgb(255, 215, 0)" x1="40" y1="40" x2="40" y2="60" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(255, 215, 0)" x1="60" y1="40" x2="60" y2="60" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(255, 215, 0)" x1="40" y1="50" x2="60" y2="50" stroke-width="5" stroke-linecap="round"></line></g></g>"""

[<Fact>]
let ``T symbol`` () =
    tSymbol Color.LavenderBlush Color.DarkGray 100.0
    |> ReactServer.renderToString
    |> should equal """<g data-reactroot=""><g transform="translate(25,25)"><rect fill="rgb(255, 240, 245)" stroke="rgb(169, 169, 169)" width="50" height="50" stroke-width="0.25"></rect></g><g><line stroke="rgb(169, 169, 169)" x1="40" y1="40" x2="50" y2="40" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(169, 169, 169)" x1="50" y1="40" x2="60" y2="40" stroke-width="5" stroke-linecap="round"></line><line stroke="rgb(169, 169, 169)" x1="50" y1="40" x2="50" y2="60" stroke-width="5" stroke-linecap="round"></line></g></g>"""

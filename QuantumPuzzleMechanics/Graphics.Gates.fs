module QuantumPuzzleMechanics.Graphics.Gates

open Giraffe.ViewEngine
open Xamarin.Forms

// types

type Place = Single
           | Top
           | Middle
           | Bottom

type Symbol = ControlSymbol
            | NotSymbol
            | SwapSymbol
            | YSymbol
            | ZSymbol
            | HSymbol

type Gate = XGate of int
          | YGate of int
          | ZGate of int
          | HGate of int
          | SwapGate of int * int
          | CXGate of int * int
          | CZGate of int * int
          | CCXGate of int * int * int
          | CSwapGate of int * int * int

// functions

let part (place: Place)
         (symbol: Symbol)
         (strokeColor: Color)
         (size: float)
    : XmlNode =
    let horizWire = Elems.horizWire Color.Black size
    let vertWires =
            match place with
            | Single ->
                []
            | Top ->
                [ Elems.bottomVertWire strokeColor size ]
            | Middle ->
                [ Elems.topVertWire strokeColor size
                  Elems.bottomVertWire strokeColor size ]
            | Bottom ->
                [ Elems.topVertWire strokeColor size ]
    let symbolElem =
            match symbol with
            | ControlSymbol ->
                Elems.controlSymbol strokeColor size
            | NotSymbol ->
                Elems.notSymbol strokeColor size
            | SwapSymbol ->
                Elems.swapSymbol strokeColor size
            | YSymbol ->
                Elems.ySymbol Color.White strokeColor size
            | ZSymbol ->
                Elems.zSymbol Color.White strokeColor size
            | HSymbol ->
                Elems.hSymbol Color.White strokeColor size
    [ [ horizWire ]
      vertWires
      [ symbolElem ] ]
    |> List.concat
    |> tag "g" []

let transform (i: int) (size: float): string =
    i
    |> float
    |> (*) size
    |> string
    |> sprintf "translate(0,%s)"

let gate1Graphics
        (numOfQubits: int)
        (index: int)
        (symbol: Symbol)
        (strokeColor: Color)
        (size: float)
    : XmlNode =
    fun i -> 
        let transformVal = transform i size
        if i = index
                then [ part Single symbol strokeColor size ]
                else [ Elems.horizWire Color.Black size ]
        |> tag "g" [ attr "transform" transformVal ]
    |> List.init numOfQubits
    |> tag "g" []

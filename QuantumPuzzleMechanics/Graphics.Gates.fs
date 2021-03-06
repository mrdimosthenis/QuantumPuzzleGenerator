﻿module QuantumPuzzleMechanics.Graphics.Gates

open Xamarin.Forms

open Giraffe
open GiraffeViewEngine

open QuantumPuzzleMechanics

// types

type Place =
    | Single
    | Top
    | Middle
    | Bottom

type Symbol =
    | ControlSymbol
    | NotSymbol
    | SwapSymbol
    | YSymbol
    | ZSymbol
    | HSymbol

// functions

let part (place: Place) (symbol: Symbol) (strokeColor: Color) (size: float): XmlNode =
    let horizWire = Elems.horizWire Color.Black size

    let vertWires =
        match place with
        | Single -> []
        | Top -> [ Elems.bottomVertWire strokeColor size ]
        | Middle ->
            [ Elems.topVertWire strokeColor size
              Elems.bottomVertWire strokeColor size ]
        | Bottom -> [ Elems.topVertWire strokeColor size ]

    let symbolElem =
        match symbol with
        | ControlSymbol -> Elems.controlSymbol strokeColor size
        | NotSymbol -> Elems.notSymbol strokeColor size
        | SwapSymbol -> Elems.swapSymbol strokeColor size
        | YSymbol -> Elems.ySymbol Color.White strokeColor size
        | ZSymbol -> Elems.zSymbol Color.White strokeColor size
        | HSymbol -> Elems.hSymbol Color.White strokeColor size

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

let gate1Graphics (numOfQubits: int) (index: int) (symbol: Symbol) (strokeColor: Color) (size: float): XmlNode =
    fun i ->
        if i = index then [ part Single symbol strokeColor size ] else [ Elems.horizWire Color.Black size ]
        |> tag "g" [ transform i size |> attr "transform" ]
    |> List.init numOfQubits
    |> tag "g" []

let gate2Graphics (numOfQubits: int)
                  (indexA: int)
                  (indexB: int)
                  (symbolA: Symbol)
                  (symbolB: Symbol)
                  (strokeColor: Color)
                  (size: float)
                  : XmlNode =
    let minIndex = min indexA indexB
    let maxIndex = max indexA indexB

    let (minSymbol, maxSymbol) =
        if minIndex = indexA then (symbolA, symbolB) else (symbolB, symbolA)

    fun i ->
        match i with
        | _ when i = minIndex -> [ part Top minSymbol strokeColor size ]
        | _ when i = maxIndex -> [ part Bottom maxSymbol strokeColor size ]
        | _ when i < minIndex || i > maxIndex -> [ Elems.horizWire Color.Black size ]
        | _ ->
            [ Elems.horizWire Color.Black size
              Elems.vertWire strokeColor size ]
        |> tag "g" [ transform i size |> attr "transform" ]
    |> List.init numOfQubits
    |> tag "g" []

let gate3Graphics (numOfQubits: int)
                  (indexA: int)
                  (indexB: int)
                  (indexC: int)
                  (symbolA: Symbol)
                  (symbolB: Symbol)
                  (symbolZ: Symbol)
                  (strokeColor: Color)
                  (size: float)
                  : XmlNode =
    let sortedIndexWithSymbols =
        [ symbolA; symbolB; symbolZ ]
        |> List.zip [ indexA; indexB; indexC ]
        |> List.sortBy fst

    match sortedIndexWithSymbols with
    | [ (minIndex, minSymbol); (middleIndex, middleSymbol); (maxIndex, maxSymbol) ] ->
        fun i ->
            match i with
            | _ when i = minIndex -> [ part Top minSymbol strokeColor size ]
            | _ when i = middleIndex -> [ part Middle middleSymbol strokeColor size ]
            | _ when i = maxIndex -> [ part Bottom maxSymbol strokeColor size ]
            | _ when i < minIndex || i > maxIndex -> [ Elems.horizWire Color.Black size ]
            | _ ->
                [ Elems.horizWire Color.Black size
                  Elems.vertWire strokeColor size ]
            |> tag "g" [ transform i size |> attr "transform" ]
        |> List.init numOfQubits
        |> tag "g" []
    | _ -> raise Quantum.Gate3.InvalidIndexOrder

let gateGraphics (numOfQubits: int) (gate: Quantum.Gate.Gate) (strokeColor: Color) (size: float): XmlNode =
    match gate with
    | Quantum.Gate.XGate index -> gate1Graphics numOfQubits index NotSymbol strokeColor size
    | Quantum.Gate.YGate index -> gate1Graphics numOfQubits index YSymbol strokeColor size
    | Quantum.Gate.ZGate index -> gate1Graphics numOfQubits index ZSymbol strokeColor size
    | Quantum.Gate.HGate index -> gate1Graphics numOfQubits index HSymbol strokeColor size
    | Quantum.Gate.SwapGate (indexA, indexB) ->
        gate2Graphics numOfQubits indexA indexB SwapSymbol SwapSymbol strokeColor size
    | Quantum.Gate.CXGate (indexA, indexB) ->
        gate2Graphics numOfQubits indexA indexB ControlSymbol NotSymbol strokeColor size
    | Quantum.Gate.CZGate (indexA, indexB) ->
        gate2Graphics numOfQubits indexA indexB ControlSymbol ZSymbol strokeColor size
    | Quantum.Gate.CCXGate (indexA, indexB, indexC) ->
        gate3Graphics numOfQubits indexA indexB indexC ControlSymbol ControlSymbol NotSymbol strokeColor size
    | Quantum.Gate.CSwapGate (indexA, indexB, indexC) ->
        gate3Graphics numOfQubits indexA indexB indexC ControlSymbol SwapSymbol SwapSymbol strokeColor size

module QuantumPuzzleMechanics.Graphics.Gates

open Xamarin.Forms

open Fable.React
open Fable.React.Props

open QuantumPuzzleMechanics

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
            | TSymbol

// functions

let part (place: Place)
         (symbol: Symbol)
         (strokeColor: Color)
         (size: float)
    : ReactElement =
    let vertWire = Elems.vertWire Color.Black size
    let horizWires =
            match place with
            | Single ->
                []
            | Top ->
                [ Elems.rightHorizWire strokeColor size ]
            | Middle ->
                [ Elems.leftHorizWire strokeColor size
                  Elems.rightHorizWire strokeColor size ]
            | Bottom ->
                [ Elems.leftHorizWire strokeColor size ]
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
            | TSymbol ->
                Elems.tSymbol Color.White strokeColor size
    [ [ vertWire ]
      horizWires
      [ symbolElem ] ]
    |> List.concat
    |> g []

let transform (i: int) (size: float): string =
    i
    |> float
    |> (*) size
    |> string
    |> sprintf "translate(%s,0)"

let gate1Graphics
        (numOfQubits: int)
        (index: int)
        (symbol: Symbol)
        (strokeColor: Color)
        (size: float)
    : ReactElement =
    fun i ->
        if i = index
                then [ part Single symbol strokeColor size ]
                else [ Elems.vertWire Color.Black size ]
        |> g [ transform i size |> SVGAttr.Transform ]
    |> List.init numOfQubits
    |> g []

let gate2Graphics
        (numOfQubits: int)
        (indexA: int)
        (indexB: int)
        (symbolA: Symbol)
        (symbolB: Symbol)
        (strokeColor: Color)
        (size: float)
    : ReactElement =
    let minIndex = min indexA indexB
    let maxIndex = max indexA indexB
    let (minSymbol, maxSymbol) =
            if minIndex = indexA
                then (symbolA, symbolB)
                else (symbolB, symbolA)
    fun i ->
        match i with
        | _ when i = minIndex ->
            [ part Top minSymbol strokeColor size ]
        | _ when i = maxIndex ->
            [ part Bottom maxSymbol strokeColor size ]
        | _ when i < minIndex || i > maxIndex ->
            [ Elems.vertWire Color.Black size ]
        | _ ->
            [ Elems.vertWire Color.Black size
              Elems.horizWire strokeColor size ]
        |> g [ transform i size |> SVGAttr.Transform ]
    |> List.init numOfQubits
    |> g []

let gate3Graphics
        (numOfQubits: int)
        (indexA: int)
        (indexB: int)
        (indexC: int)
        (symbolA: Symbol)
        (symbolB: Symbol)
        (symbolZ: Symbol)
        (strokeColor: Color)
        (size: float)
    : ReactElement =
    let sortedIndexWithSymbols =
            [ symbolA; symbolB; symbolZ ]
            |> List.zip [ indexA; indexB; indexC ]
            |> List.sortBy fst
    match sortedIndexWithSymbols with
    | [ (minIndex, minSymbol)
        (middleIndex, middleSymbol)
        (maxIndex, maxSymbol) ] ->
            fun i -> 
                match i with
                | _ when i = minIndex ->
                    [ part Top minSymbol strokeColor size ]
                | _ when i = middleIndex ->
                    [ part Middle middleSymbol strokeColor size ]
                | _ when i = maxIndex ->
                    [ part Bottom maxSymbol strokeColor size ]
                | _ when i < minIndex || i > maxIndex ->
                    [ Elems.vertWire Color.Black size ]
                | _ ->
                    [ Elems.vertWire Color.Black size
                      Elems.horizWire strokeColor size ]
                |> g [ transform i size |> SVGAttr.Transform ]
            |> List.init numOfQubits
            |> g []
    | _ -> raise Quantum.Gate3.InvalidIndexOrder

let gateGraphics
        (numOfQubits: int)
        (gate: Quantum.Gate.Gate)
        (strokeColor: Color)
        (size: float)
    : ReactElement =
    match gate with
    | Quantum.Gate.XGate index ->
        gate1Graphics numOfQubits
                      index
                      NotSymbol
                      strokeColor
                      size
    | Quantum.Gate.YGate index ->
        gate1Graphics numOfQubits
                      index
                      YSymbol
                      strokeColor
                      size
    | Quantum.Gate.ZGate index ->
        gate1Graphics numOfQubits
                      index
                      ZSymbol
                      strokeColor
                      size
    | Quantum.Gate.HGate index ->
        gate1Graphics numOfQubits
                      index
                      HSymbol
                      strokeColor
                      size
    | Quantum.Gate.TGate index ->
        gate1Graphics numOfQubits
                      index
                      TSymbol
                      strokeColor
                      size
    | Quantum.Gate.SwapGate (indexA, indexB) ->
        gate2Graphics numOfQubits
                      indexA
                      indexB
                      SwapSymbol
                      SwapSymbol
                      strokeColor
                      size
    | Quantum.Gate.CXGate (indexA, indexB) ->
        gate2Graphics numOfQubits
                      indexA
                      indexB
                      ControlSymbol
                      NotSymbol
                      strokeColor
                      size
    | Quantum.Gate.CZGate (indexA, indexB) ->
        gate2Graphics numOfQubits
                      indexA
                      indexB
                      ControlSymbol
                      ZSymbol
                      strokeColor
                      size
    | Quantum.Gate.CCXGate (indexA, indexB, indexC) ->
        gate3Graphics numOfQubits
                      indexA
                      indexB
                      indexC
                      ControlSymbol
                      ControlSymbol
                      NotSymbol
                      strokeColor
                      size
    | Quantum.Gate.CSwapGate (indexA, indexB, indexC) ->
        gate3Graphics numOfQubits
                      indexA
                      indexB
                      indexC
                      ControlSymbol
                      SwapSymbol
                      SwapSymbol
                      strokeColor
                      size

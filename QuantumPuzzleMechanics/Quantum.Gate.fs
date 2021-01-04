module QuantumPuzzleMechanics.Quantum.Gate

open QuantumPuzzleMechanics
open QuantumPuzzleMechanics.Quantum

// types

type Gate =
    | XGate of int
    | YGate of int
    | ZGate of int
    | HGate of int
    | SwapGate of int * int
    | CXGate of int * int
    | CZGate of int * int
    | CCXGate of int * int * int
    | CSwapGate of int * int * int

// functions

let matrix (numOfQubits: int) (gate: Gate): Matrix.Matrix =
    match gate with
    | XGate index -> Gate1.matrix numOfQubits index Gate1.X
    | YGate index -> Gate1.matrix numOfQubits index Gate1.Y
    | ZGate index -> Gate1.matrix numOfQubits index Gate1.Z
    | HGate index -> Gate1.matrix numOfQubits index Gate1.H
    | SwapGate (indexA, indexB) -> Gate2.matrix numOfQubits indexA indexB Gate2.SWAP
    | CXGate (indexA, indexB) -> Gate2.matrix numOfQubits indexA indexB Gate2.CX
    | CZGate (indexA, indexB) -> Gate2.matrix numOfQubits indexA indexB Gate2.CZ
    | CCXGate (indexA, indexB, indexC) -> Gate3.matrix numOfQubits indexA indexB indexC Gate3.CCX
    | CSwapGate (indexA, indexB, indexC) -> Gate3.matrix numOfQubits indexA indexB indexC Gate3.CSWAP

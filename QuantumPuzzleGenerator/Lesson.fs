module QuantumPuzzleGenerator.Lesson

open QuantumPuzzleMechanics

// types

type Lesson =
    { Index: int
      Title: string
      Description: string
      NumOfQubits: int
      GateOpt: Quantum.Gate.Gate option
      IsHueDisplayed: bool }

// constructor

let lesson (index: int)
           (title: string)
           (description: string)
           (numOfQubits: int)
           (gateOpt: Quantum.Gate.Gate option)
           (isHueDisplayed: bool)
           : Lesson =
    { Index = index
      Title = title
      Description = description
      NumOfQubits = numOfQubits
      GateOpt = gateOpt
      IsHueDisplayed = isHueDisplayed }

// constants

let descriptions: string list =
    [ ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      ""
      "" ]

let lessons: Lesson list =
    // Title NumOfQubits GateOpt IsHueDisplayed
    [ ("", 1, None, false)
      ("", 2, None, false)
      ("", 3, None, false)
      ("", 1, 0 |> Quantum.Gate.XGate |> Some, false)
      ("", 2, 1 |> Quantum.Gate.XGate |> Some, false)
      ("", 2, 0 |> Quantum.Gate.XGate |> Some, false)
      ("", 3, 2 |> Quantum.Gate.XGate |> Some, false)
      ("", 3, 1 |> Quantum.Gate.XGate |> Some, false)
      ("", 3, 0 |> Quantum.Gate.XGate |> Some, false)
      ("", 1, 0 |> Quantum.Gate.ZGate |> Some, true)
      ("", 2, 1 |> Quantum.Gate.ZGate |> Some, true)
      ("", 2, 0 |> Quantum.Gate.ZGate |> Some, true)
      ("", 3, 2 |> Quantum.Gate.ZGate |> Some, true)
      ("", 3, 1 |> Quantum.Gate.ZGate |> Some, true)
      ("", 3, 0 |> Quantum.Gate.ZGate |> Some, true)
      ("", 1, 0 |> Quantum.Gate.YGate |> Some, true)
      ("", 2, 1 |> Quantum.Gate.YGate |> Some, true)
      ("", 2, 0 |> Quantum.Gate.YGate |> Some, true)
      ("", 3, 2 |> Quantum.Gate.YGate |> Some, true)
      ("", 3, 1 |> Quantum.Gate.YGate |> Some, true)
      ("", 3, 0 |> Quantum.Gate.YGate |> Some, true)
      ("", 1, 0 |> Quantum.Gate.HGate |> Some, true)
      ("", 2, 1 |> Quantum.Gate.HGate |> Some, true)
      ("", 2, 0 |> Quantum.Gate.HGate |> Some, true)
      ("", 3, 2 |> Quantum.Gate.HGate |> Some, true)
      ("", 3, 1 |> Quantum.Gate.HGate |> Some, true)
      ("", 3, 0 |> Quantum.Gate.HGate |> Some, true)
      ("", 2, (1,0) |> Quantum.Gate.CXGate |> Some, false)
      ("", 2, (0,1) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (1,0) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (0,1) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (2,0) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (0,2) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (2,1) |> Quantum.Gate.CXGate |> Some, false)
      ("", 3, (1,2) |> Quantum.Gate.CXGate |> Some, false)
      ("", 2, (1,0) |> Quantum.Gate.SwapGate |> Some, false)
      ("", 3, (1,0) |> Quantum.Gate.SwapGate |> Some, false)
      ("", 3, (2,0) |> Quantum.Gate.SwapGate |> Some, false)
      ("", 3, (2,1) |> Quantum.Gate.SwapGate |> Some, false) ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gateOpt, isHueDisplayed)) ->
        lesson i title description numOfQubits gateOpt isHueDisplayed) descriptions

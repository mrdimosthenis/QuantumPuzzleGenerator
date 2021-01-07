module QuantumPuzzleGenerator.Lesson

open QuantumPuzzleMechanics

// types

type Category =
    { Index: int
      Title: string
      Description: string
      NumOfQubits: int
      Gates: Quantum.Gate.Gate list
      IsInAbsoluteQState: bool
      IsHueDisplayed: bool }

type Lesson =
    { Category: Category
      QState: Matrix.Matrix
      GateSelection: bool list }

// constructor

let category (index: int)
             (title: string)
             (description: string)
             (numOfQubits: int)
             (gates: Quantum.Gate.Gate list)
             (isInAbsoluteQState: bool)
             (isHueDisplayed: bool)
             : Category =
    { Index = index
      Title = title
      Description = description
      NumOfQubits = numOfQubits
      Gates = gates
      IsInAbsoluteQState = isInAbsoluteQState
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

let categories: Category list =
    // Title NumOfQubits Gates IsInAbsoluteQState IsHueDisplayed
    [ ("", 1, [], true, false)
      
      
      
      
      
      
       ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gates, isInAbsoluteQState, isHueDisplayed)) ->
        category i title description numOfQubits gates isInAbsoluteQState isHueDisplayed) descriptions

module QuantumPuzzleGenerator.LessonCategory

open QuantumPuzzleMechanics

// types

type LessonCategory =
    { Index: int
      Title: string
      Description: string
      NumOfQubits: int
      Gates: Quantum.Gate.Gate list
      IsInAbsoluteQState: bool
      IsHueDisplayed: bool }

// constructor

let lessonCategory (index: int)
                   (title: string)
                   (description: string)
                   (numOfQubits: int)
                   (gates: Quantum.Gate.Gate list)
                   (isInAbsoluteQState: bool)
                   (isHueDisplayed: bool)
                   : LessonCategory =
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

let lessonCategories: LessonCategory list =
    // Title NumOfQubits Gates IsInAbsoluteQState IsHueDisplayed
    [ ("", 1, [], true, false) ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gates, isInAbsoluteQState, isHueDisplayed)) ->
        lessonCategory i title description numOfQubits gates isInAbsoluteQState isHueDisplayed) descriptions

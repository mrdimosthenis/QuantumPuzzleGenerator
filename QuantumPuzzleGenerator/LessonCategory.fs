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
      "" ]

let lessonCategories: LessonCategory list =
    // Title NumOfQubits Gates IsInAbsoluteQState IsHueDisplayed
    [ ("Absolute Quantum State", 1, [], true, false)
      ("Random Quantum State", 1, [], false, false)
      ("Two Qubits", 2, [], false, false)
      ("Three Qubits", 3, [], false, false)

      ("⊕ for One Qubit", 1, [ Quantum.Gate.XGate 0 ], false, false)
      ("⊕ for One of Two Qubits",
       2,
       [ Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       false)
      ("⊕ for One of Three Qubits",
       3,
       [ Quantum.Gate.XGate 2
         Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       false)

      ("Z for One Qubit", 1, [ Quantum.Gate.ZGate 0 ], false, true)
      ("Z for One of Two Qubits",
       2,
       [ Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       true)
      ("Z for One of Three Qubits",
       3,
       [ Quantum.Gate.ZGate 2
         Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       true)

      ("Y for One Qubit", 1, [ Quantum.Gate.YGate 0 ], false, true)
      ("Y for One of Two Qubits",
       2,
       [ Quantum.Gate.YGate 1
         Quantum.Gate.YGate 0 ],
       false,
       true)
      ("Y for One of Three Qubits",
       3,
       [ Quantum.Gate.YGate 2
         Quantum.Gate.YGate 1
         Quantum.Gate.YGate 0 ],
       false,
       true)

      ("H for One Qubit", 1, [ Quantum.Gate.HGate 0 ], false, true)
      ("H for One of Two Qubits",
       2,
       [ Quantum.Gate.HGate 1
         Quantum.Gate.HGate 0 ],
       false,
       true)
      ("H for One of Three Qubits",
       3,
       [ Quantum.Gate.HGate 2
         Quantum.Gate.HGate 1
         Quantum.Gate.HGate 0 ],
       false,
       true)

      ("Swap for Two Qubits", 2, [ Quantum.Gate.SwapGate(1, 0) ], false, false)
      ("Swap for Two of Three Qubits",
       3,
       [ Quantum.Gate.SwapGate(1, 2)
         Quantum.Gate.SwapGate(0, 2)
         Quantum.Gate.SwapGate(0, 1) ],
       false,
       false)

      ("Controlled ⊕ for Two Qubits",
       2,
       [ Quantum.Gate.CXGate(1, 0)
         Quantum.Gate.CXGate(0, 1) ],
       false,
       false)
      ("Controlled ⊕ for Two of Three Qubits",
       3,
       [ Quantum.Gate.CXGate(2, 1)
         Quantum.Gate.CXGate(1, 2)
         Quantum.Gate.CXGate(2, 0)
         Quantum.Gate.CXGate(0, 2)
         Quantum.Gate.CXGate(1, 0)
         Quantum.Gate.CXGate(0, 1) ],
       false,
       false) ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gates, isInAbsoluteQState, isHueDisplayed)) ->
        lessonCategory i title description numOfQubits gates isInAbsoluteQState isHueDisplayed) descriptions

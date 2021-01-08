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
      IsHueDisplayedOpt: bool option }

// constructor

let lessonCategory (index: int)
                   (title: string)
                   (description: string)
                   (numOfQubits: int)
                   (gates: Quantum.Gate.Gate list)
                   (isInAbsoluteQState: bool)
                   (isHueDisplayedOpt: bool option)
                   : LessonCategory =
    { Index = index
      Title = title
      Description = description
      NumOfQubits = numOfQubits
      Gates = gates
      IsInAbsoluteQState = isInAbsoluteQState
      IsHueDisplayedOpt = isHueDisplayedOpt }

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
    // Title NumOfQubits Gates IsInAbsoluteQState IsHueDisplayedOpt
    [ ("Absolute Quantum State", 1, [], true, None)
      ("Random Quantum State", 1, [], false, None)
      ("Two Qubits", 2, [], false, None)
      ("Three Qubits", 3, [], false, None)

      ("⊕ for One Qubit", 1, [ Quantum.Gate.XGate 0 ], false, None)
      ("⊕ for One of Two Qubits",
       2,
       [ Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       None)
      ("⊕ for One of Three Qubits",
       3,
       [ Quantum.Gate.XGate 2
         Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       None)

      ("Z for One Qubit", 1, [ Quantum.Gate.ZGate 0 ], false, Some true)
      ("Z for One of Two Qubits",
       2,
       [ Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       Some false)
      ("Z for One of Three Qubits",
       3,
       [ Quantum.Gate.ZGate 2
         Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       Some false)

      ("Y for One Qubit", 1, [ Quantum.Gate.YGate 0 ], false, Some true)
      ("Y for One of Two Qubits",
       2,
       [ Quantum.Gate.YGate 1
         Quantum.Gate.YGate 0 ],
       false,
       Some false)
      ("Y for One of Three Qubits",
       3,
       [ Quantum.Gate.YGate 2
         Quantum.Gate.YGate 1
         Quantum.Gate.YGate 0 ],
       false,
       Some false)

      ("H for One Qubit", 1, [ Quantum.Gate.HGate 0 ], false, Some true)
      ("H for One of Two Qubits",
       2,
       [ Quantum.Gate.HGate 1
         Quantum.Gate.HGate 0 ],
       false,
       Some false)
      ("H for One of Three Qubits",
       3,
       [ Quantum.Gate.HGate 2
         Quantum.Gate.HGate 1
         Quantum.Gate.HGate 0 ],
       false,
       Some false)

      ("Swap for Two Qubits", 2, [ Quantum.Gate.SwapGate(1, 0) ], false, None)
      ("Swap for Two of Three Qubits",
       3,
       [ Quantum.Gate.SwapGate(1, 2)
         Quantum.Gate.SwapGate(0, 2)
         Quantum.Gate.SwapGate(0, 1) ],
       false,
       None)

      ("Controlled ⊕ for Two Qubits",
       2,
       [ Quantum.Gate.CXGate(1, 0)
         Quantum.Gate.CXGate(0, 1) ],
       false,
       None)
      ("Controlled ⊕ for Two of Three Qubits",
       3,
       [ Quantum.Gate.CXGate(2, 1)
         Quantum.Gate.CXGate(1, 2)
         Quantum.Gate.CXGate(2, 0)
         Quantum.Gate.CXGate(0, 2)
         Quantum.Gate.CXGate(1, 0)
         Quantum.Gate.CXGate(0, 1) ],
       false,
       None) ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gates, isInAbsoluteQState, isHueDisplayedOpt)) ->
        lessonCategory i title description numOfQubits gates isInAbsoluteQState isHueDisplayedOpt) descriptions

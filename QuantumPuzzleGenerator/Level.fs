module QuantumPuzzleGenerator.Level

open QuantumPuzzleMechanics

// types

type Level =
    { Index: int
      NumOfQubits: int
      MaxGateType: Quantum.Gate.MaxGateType
      NumOfGates: int
      IsAbsoluteInitQState: bool }

// constructor

let level (index: int)
          (numOfQubits: int)
          (maxGateType: Quantum.Gate.MaxGateType)
          (numOfGates: int)
          (isAbsoluteInitQState: bool)
          : Level =
    { Index = index
      NumOfQubits = numOfQubits
      MaxGateType = maxGateType
      NumOfGates = numOfGates
      IsAbsoluteInitQState = isAbsoluteInitQState }

// constants

let levels: Level list =
    // NumOfQubits MaxGateType NumOfGates IsAbsoluteInitQState
    [ (1, Quantum.Gate.SingleQubit, 2, true)
      (1, Quantum.Gate.SingleQubit, 2, false)
      
      (1, Quantum.Gate.SingleQubit, 3, false)
      
      (2, Quantum.Gate.SingleQubit, 3, false)

      (2, Quantum.Gate.SingleQubit, 4, true)
      (2, Quantum.Gate.SingleQubit, 4, false)

      (3, Quantum.Gate.SingleQubit, 3, true)
      (3, Quantum.Gate.SingleQubit, 3, false)
      (3, Quantum.Gate.DoubleQubit, 3, true)
      (3, Quantum.Gate.DoubleQubit, 3, false)
      (3, Quantum.Gate.TripleQubit, 3, true)
      (3, Quantum.Gate.TripleQubit, 3, false)

      (3, Quantum.Gate.TripleQubit, 4, true)
      (3, Quantum.Gate.TripleQubit, 4, false)
      (3, Quantum.Gate.TripleQubit, 5, true)
      (3, Quantum.Gate.TripleQubit, 5, false)
      (3, Quantum.Gate.TripleQubit, 6, true)
      (3, Quantum.Gate.TripleQubit, 6, false)
      (3, Quantum.Gate.TripleQubit, 7, true)
      (3, Quantum.Gate.TripleQubit, 7, false) ]
    |> List.indexed
    |> List.map (fun (i, (numOfQubits, maxGateType, numOfGates, isAbsoluteInitQState)) ->
        level i numOfQubits maxGateType numOfGates isAbsoluteInitQState)

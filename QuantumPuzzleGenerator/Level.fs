module QuantumPuzzleGenerator.Level

open QuantumPuzzleMechanics

// types

type Level =
    { Index: int
      NumOfQubits: int
      MaxGateType: Quantum.Gate.MaxGateType
      NumOfGates: int }

// constructor

let level (index: int) (numOfQubits: int) (maxGateType: Quantum.Gate.MaxGateType) (numOfGates: int): Level =
    { Index = index
      NumOfQubits = numOfQubits
      MaxGateType = maxGateType
      NumOfGates = numOfGates }

// constants

let levels: Level list =
    // NumOfQubits MaxGateType NumOfGates
    [ (1, Quantum.Gate.SingleQubit, 3)

      (2, Quantum.Gate.SingleQubit, 4)
      (2, Quantum.Gate.DoubleQubit, 4)

      (2, Quantum.Gate.DoubleQubit, 5)

      (3, Quantum.Gate.SingleQubit, 4)
      (3, Quantum.Gate.DoubleQubit, 4)
      (3, Quantum.Gate.TripleQubit, 4)

      (3, Quantum.Gate.TripleQubit, 5)
      (3, Quantum.Gate.TripleQubit, 6)
      (3, Quantum.Gate.TripleQubit, 7) ]
    |> List.indexed
    |> List.map (fun (i, (numOfQubits, maxGateType, numOfGates)) -> level i numOfQubits maxGateType numOfGates)

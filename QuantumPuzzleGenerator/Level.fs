module QuantumPuzzleGenerator.Level

open QuantumPuzzleMechanics

// types

type Level =
    { NumOfQubits: int
      MaxGateType: Quantum.Gate.MaxGateType
      NumOfGates: int }

// constructor

let level (numOfQubits: int) (maxGateType: Quantum.Gate.MaxGateType) (numOfGates: int): Level =
    { NumOfQubits = numOfQubits
      MaxGateType = maxGateType
      NumOfGates = numOfGates }

// constants

let levels: Level list =
    // NumOfQubits MaxGateType NumOfGates
    [ level 2 Quantum.Gate.SingleQubit 4 //     level 1
      level 2 Quantum.Gate.DoubleQubit 4 //     level 2

      level 2 Quantum.Gate.DoubleQubit 5 //     level 3
      level 2 Quantum.Gate.DoubleQubit 6 //     level 4

      level 3 Quantum.Gate.SingleQubit 4 //     level 5
      level 3 Quantum.Gate.DoubleQubit 4 //     level 6
      level 3 Quantum.Gate.TripleQubit 4 //     level 7

      level 3 Quantum.Gate.TripleQubit 5 //     level 8
      level 3 Quantum.Gate.TripleQubit 6 //     level 9
      level 3 Quantum.Gate.TripleQubit 7 //     level 10
      level 3 Quantum.Gate.TripleQubit 8 //     level 11
      level 3 Quantum.Gate.TripleQubit 9 //     level 12
      level 3 Quantum.Gate.TripleQubit 10 //    level 13

      level 4 Quantum.Gate.SingleQubit 4 //     level 14
      level 4 Quantum.Gate.DoubleQubit 4 //     level 15
      level 4 Quantum.Gate.TripleQubit 4 //     level 16

      level 4 Quantum.Gate.TripleQubit 5 //     level 17
      level 4 Quantum.Gate.TripleQubit 6 //     level 18
      level 4 Quantum.Gate.TripleQubit 7 //     level 19
      level 4 Quantum.Gate.TripleQubit 8 ]

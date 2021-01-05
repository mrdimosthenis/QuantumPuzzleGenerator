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
    [ level 1 Quantum.Gate.SingleQubit 3 // level 1
      
      level 2 Quantum.Gate.SingleQubit 4 // level 2
      level 2 Quantum.Gate.DoubleQubit 4 // level 3

      level 2 Quantum.Gate.DoubleQubit 5 // level 4

      level 3 Quantum.Gate.SingleQubit 4 // level 5
      level 3 Quantum.Gate.DoubleQubit 4 // level 6
      level 3 Quantum.Gate.TripleQubit 4 // level 7

      level 3 Quantum.Gate.TripleQubit 5 // level 8
      level 3 Quantum.Gate.TripleQubit 6 // level 9
      level 3 Quantum.Gate.TripleQubit 7  ]

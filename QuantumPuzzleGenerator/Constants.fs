module QuantumPuzzleGenerator.Constants

open Xamarin.Forms

open QuantumPuzzleMechanics

// types

type Level =
    { NumOfQubits: int
      MaxGateType: Quantum.Gate.MaxGateType
      NumOfGates: int }

// constants

let numOfPuzzlesPerLevel: int = 10

let deviceWidth: float =
    Device.info.PixelScreenSize.Width
    |> min Device.info.PixelScreenSize.Height
    |> (*) 0.5

let levels: Level list =
    [ { NumOfQubits = 2
        MaxGateType = Quantum.Gate.SingleQubit
        NumOfGates = 6 }
      { NumOfQubits = 2
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 6 }
      { NumOfQubits = 2
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 7 }
      { NumOfQubits = 2
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 8 }
      { NumOfQubits = 2
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 9 }
      { NumOfQubits = 2
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 10 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.SingleQubit
        NumOfGates = 6 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 6 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 6 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 7 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 8 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 9 }
      { NumOfQubits = 3
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 10 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.SingleQubit
        NumOfGates = 6 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.DoubleQubit
        NumOfGates = 6 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 6 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 7 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 8 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 9 }
      { NumOfQubits = 4
        MaxGateType = Quantum.Gate.TripleQubit
        NumOfGates = 10 } ]

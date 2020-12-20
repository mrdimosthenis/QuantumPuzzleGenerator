module QuantumPuzzleMechanics.Quantum.Gate2

open System
open FSharpx.Collections

open QuantumPuzzleMechanics

// constants

let SWAP: Matrix.Matrix =
    [ [ Complex.ofNum 1.0; Complex.zero;      Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.zero;      Complex.ofNum 1.0; Complex.zero ]
      [ Complex.zero;      Complex.ofNum 1.0; Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.zero;      Complex.zero;      Complex.ofNum 1.0 ] ]
    |> Utils.ofListOfLists

let CX: Matrix.Matrix =
    [ [ Complex.ofNum 1.0; Complex.zero;      Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.ofNum 1.0; Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.zero;      Complex.zero;      Complex.ofNum 1.0 ]
      [ Complex.zero;      Complex.zero;      Complex.ofNum 1.0; Complex.zero ] ] 
    |> Utils.ofListOfLists

let CZ: Matrix.Matrix =
    [ [ Complex.ofNum 1.0; Complex.zero;      Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.ofNum 1.0; Complex.zero;      Complex.zero ]
      [ Complex.zero;      Complex.zero;      Complex.ofNum 1.0; Complex.zero ]
      [ Complex.zero;      Complex.zero;      Complex.zero;      Complex.ofNum -1.0] ] 
    |> Utils.ofListOfLists

// for states of more qubits

let identityMatrix (numOfQubits: int): Matrix.Matrix =
    Math.Pow(2.0, float numOfQubits)
    |> Math.Round
    |> int
    |> Matrix.identity

let matrixOfConsecutiveIndices(numOfQubits: int) (baseIndex: int) (gate: Matrix.Matrix): Matrix.Matrix =
    fun i -> match i - baseIndex with
             | 0 -> gate
             | 1 -> Matrix.identity 1
             | _ -> Matrix.identity 2
    |> Seq.init numOfQubits
    |> Seq.fold Matrix.tensorProduct (Matrix.identity 1)

let matrixOfGettingSideBySide (numOfQubits: int) (baseIndex: int) (distance: int): Matrix.Matrix =
    fun i -> matrixOfConsecutiveIndices numOfQubits (baseIndex + i + 1) SWAP
    |> Seq.init (distance - 1)
    |> Seq.fold Matrix.standardProduct (identityMatrix numOfQubits)

let matrixOfOrderedIndices (numOfQubits: int) (minIndex: int) (maxIndex: int) (gate: Matrix.Matrix): Matrix.Matrix =
    let distance = maxIndex - minIndex
    let sideBySideMatrix = matrixOfGettingSideBySide numOfQubits minIndex distance
    sideBySideMatrix
    |> Matrix.standardProduct (matrixOfConsecutiveIndices numOfQubits minIndex gate)
    |> Matrix.standardProduct (Matrix.transjugate sideBySideMatrix)

let matrix (numOfQubits: int) (indexA: int) (indexB: int) (gate: Matrix.Matrix): Matrix.Matrix =
    let minIndex = min indexA indexB
    let maxIndex = max indexA indexB
    let gateMatrix = matrixOfOrderedIndices numOfQubits minIndex maxIndex gate
    if minIndex = indexA
        then gateMatrix
        else let swapMatrix = matrixOfOrderedIndices numOfQubits minIndex maxIndex SWAP
             swapMatrix
             |> Matrix.standardProduct gateMatrix
             |> Matrix.standardProduct swapMatrix

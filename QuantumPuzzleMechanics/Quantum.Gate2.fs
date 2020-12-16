module QuantumPuzzleMechanics.Quantum.Gate2

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
    |> Seq.fold Matrix.standardProduct (Matrix.identity numOfQubits)

let matrixOfMovingApart (numOfQubits: int) (baseIndex: int) (distance: int): Matrix.Matrix =
    fun i -> matrixOfConsecutiveIndices numOfQubits (baseIndex + i + 1) SWAP
    |> Seq.init (distance - 1)
    |> Seq.rev
    |> Seq.fold Matrix.standardProduct (Matrix.identity numOfQubits)

let matrix (numOfQubits: int) (indexA: int) (indexB: int) (gate: Matrix.Matrix): Matrix.Matrix =
    let minIndex = min indexA indexB
    let maxIndex = max indexA indexB
    let distance = maxIndex - minIndex
    let innerMatrix =
            if minIndex = indexA
                then Matrix.identity numOfQubits
                else matrixOfConsecutiveIndices numOfQubits minIndex SWAP
            |> Matrix.standardProduct (matrixOfConsecutiveIndices numOfQubits indexA gate)
    if distance = 1
        then innerMatrix
        else matrixOfMovingApart numOfQubits minIndex distance
             |> Matrix.standardProduct innerMatrix
             |> Matrix.standardProduct (matrixOfGettingSideBySide numOfQubits minIndex distance)

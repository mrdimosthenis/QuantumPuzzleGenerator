module QuantumPuzzleMechanics.Quantum.Gate3

open FSharpx.Collections

open QuantumPuzzleMechanics

// exceptions

exception InvalidIndexOrder

// constants

let CCX: Matrix.Matrix =
    let (first6Rows, last2Rows) =
            8
            |> Matrix.identity
            |> Seq.splitAt 6
    last2Rows
    |> Seq.rev
    |> Seq.append first6Rows
    |> LazyList.ofSeq

let CSWAP: Matrix.Matrix =
    let (first5Rows, last3Rows) =
            8
            |> Matrix.identity
            |> Seq.splitAt 5
    let (lastRow, reversedButLastRows) =
            last3Rows
            |> Seq.rev
            |> Seq.splitAt 1
    [ first5Rows
      reversedButLastRows
      lastRow ]
    |> Seq.ofList
    |> Seq.concat
    |> LazyList.ofSeq

// for states of more qubits

let matrixOfConsecutiveIndices(numOfQubits: int) (baseIndex: int) (q3Gate: Matrix.Matrix): Matrix.Matrix =
    fun i -> match i - baseIndex with
             | 0 -> q3Gate
             | 1 | 2 -> Matrix.identity 1
             | _ -> Matrix.identity 2
    |> Seq.init numOfQubits
    |> Seq.fold Matrix.tensorProduct (Matrix.identity 1)

let matrixOfOrderedIndices
        (numOfQubits: int)
        (minIndex: int)
        (middleIndex: int)
        (maxIndex: int)
        (q3Gate: Matrix.Matrix)
    : Matrix.Matrix =
    let distanceA = middleIndex - minIndex
    let distanceB = maxIndex - minIndex
    let sideBySideMatrixA = Gate2.matrixOfGettingSideBySide numOfQubits minIndex distanceA
    let sideBySideMatrixB = Gate2.matrixOfGettingSideBySide
                                    numOfQubits
                                    (minIndex + 1)
                                    (distanceB - 1)
    let sideBySideMatrix = Matrix.standardProduct sideBySideMatrixB sideBySideMatrixA
    sideBySideMatrix
    |> Matrix.standardProduct (matrixOfConsecutiveIndices numOfQubits minIndex q3Gate)
    |> Matrix.standardProduct (Matrix.transjugate sideBySideMatrix)

let matrix (numOfQubits: int)
           (indexA: int)
           (indexB: int)
           (indexC: int)
           (q3Gate: Matrix.Matrix)
    : Matrix.Matrix =
    let indexedIndices = [ indexA
                           indexB
                           indexC ]
                         |> List.indexed
                         |> List.sortBy snd
    match List.map snd indexedIndices with
    | [ minIndex; middleIndex; maxIndex ] ->
            let swapMatrix =
                    match List.map fst indexedIndices with
                     | [ 0; 1; 2 ] -> Gate2.identityMatrix numOfQubits
                     | [ 0; 2; 1 ] -> Gate2.matrixOfOrderedIndices numOfQubits middleIndex maxIndex Gate2.SWAP
                     | [ 1; 0; 2 ] -> Gate2.matrixOfOrderedIndices numOfQubits minIndex middleIndex Gate2.SWAP
                     | [ 1; 2; 0 ] ->
                             Gate2.matrixOfOrderedIndices numOfQubits middleIndex maxIndex Gate2.SWAP
                             |> Matrix.standardProduct (Gate2.matrixOfOrderedIndices numOfQubits minIndex middleIndex Gate2.SWAP)
                     | [ 2; 0; 1 ] ->
                             Gate2.matrixOfOrderedIndices numOfQubits minIndex middleIndex Gate2.SWAP
                             |> Matrix.standardProduct (Gate2.matrixOfOrderedIndices numOfQubits middleIndex maxIndex Gate2.SWAP)
                     | [ 2; 1; 0 ] -> Gate2.matrixOfOrderedIndices numOfQubits minIndex maxIndex Gate2.SWAP
                     | _ -> raise InvalidIndexOrder
            swapMatrix
            |> Matrix.standardProduct (matrixOfOrderedIndices numOfQubits minIndex middleIndex maxIndex q3Gate)
            |> Matrix.standardProduct (Matrix.transjugate swapMatrix)
    | _ -> raise InvalidIndexOrder

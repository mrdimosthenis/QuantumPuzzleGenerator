module QuantumPuzzleMechanics.Quantum.Gate1

open FSharpx.Collections

open QuantumPuzzleMechanics

// constants

let X: Matrix.Matrix =
    [ [ Complex.zero; Complex.ofNum 1.0 ]
      [ Complex.ofNum 1.0; Complex.zero ] ]
    |> Utils.ofListOfLists

let Y: Matrix.Matrix =
    [ [ Complex.zero; Complex.ofNumbers 0.0 -1.0 ]
      [ Complex.ofNumbers 0.0 1.0; Complex.zero ] ]
    |> Utils.ofListOfLists

let Z: Matrix.Matrix =
    [ [ Complex.ofNum 1.0; Complex.zero ]
      [ Complex.zero; Complex.ofNum -1.0 ] ]
    |> Utils.ofListOfLists

let H: Matrix.Matrix =
    [ [ Complex.ofNum 1.0; Complex.ofNum 1.0 ]
      [ Complex.ofNum 1.0; Complex.ofNum -1.0 ] ]
    |> Utils.ofListOfLists
    |> Matrix.timesNumber (1.0 / sqrt 2.0)

// for states of multiple qubits

let matrix (numOfQubits: int) (index: int) (gate: Matrix.Matrix): Matrix.Matrix =
    fun i -> if i = index then gate else Matrix.identity 2
    |> Seq.init numOfQubits
    |> Seq.fold Matrix.tensorProduct (Matrix.identity 1)

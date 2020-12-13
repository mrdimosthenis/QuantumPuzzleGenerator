module QuantumPuzzleMechanics.Generator

open System

open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Generator = Random

// functions

let nextNumber (random: Random) (): Number.Number =
    if random.NextDouble() < 0.5
        then 1.0
        else -1.0
    |> (*) (random.NextDouble())
    |> (*) 1000.0

let nextComplex (random: Random) (): Complex.Complex =
    Complex.ofNumbers
        (nextNumber random ())
        (nextNumber random ())

let nextVector (random: Random) (n: int) (): Vector.Vector =
    n
    |> Vector.zero
    |> LazyList.map
        (fun _ ->
            nextComplex (random) ()
        )

let nextMatrix (random: Random) (m: int) (n: int) (): Matrix.Matrix =
    Matrix.zero m n
    |> LazyList.map
        (fun _ ->
            nextVector random m ()
        )

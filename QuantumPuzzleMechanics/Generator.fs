module QuantumPuzzleMechanics.Generator

open System

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

module QuantumPuzzleMechanics.Generator

open System

open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Generator = Random

// functions

let nextInt (random: Random) (maxVal: int) (): int =
    random.NextDouble()
    |> (*) (float maxVal)
    |> floor
    |> int

let rec nextDiffInt (random: Random) (maxVal: int) (excludedVals: int list) (): int =
    let n = nextInt random maxVal ()
    if (Seq.forall ((<>) n) excludedVals)
        then n
        else nextDiffInt random maxVal excludedVals ()

let nextPosInt (random: Random) (maxVal: int) (): int =
    nextInt random maxVal ()
    |> (+) 1

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

let rec nextQState (random: Random) (numOfQubits: int) (): Matrix.Matrix =
    let n = Math.Pow(2.0, float numOfQubits)
            |> Math.Round
            |> int
    let v = nextVector random n ()
    if (Seq.exists ((<>) Complex.zero) v)
        then v
             |> Vector.unary
             |> Option.get
             |> Seq.singleton
             |> Seq.transpose
             |> Utils.ofSeqOfSeqs
        else nextQState random numOfQubits ()

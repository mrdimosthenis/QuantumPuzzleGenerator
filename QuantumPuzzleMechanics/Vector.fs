module QuantumPuzzleMechanics.Vector

open System
open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Vector = Complex.Complex LazyList

// exceptions

exception DifferentDimension

let maybeThrowException (v1: Vector) (v2: Vector): unit =
    if LazyList.length v1 <> LazyList.length v2
        then raise DifferentDimension
        else ()

// basic converters

let toString (v: Vector): string =
    let componentString =
        LazyList.map Complex.toString v
    String.Join(", ", componentString)
    |> sprintf "[%s]"

// constants

let zero (n: int): Vector =
    Complex.zero
    |> LazyList.repeat
    |> LazyList.take n
    
// methods

let timesNumber (x: Number.Number) (v: Vector): Vector =
    LazyList.map (Complex.timesNumber x) v

let timesComplex (c: Complex.Complex) (v: Vector): Vector =
    LazyList.map (Complex.product c) v

let dim (v: Vector): int =
    LazyList.length v

let norm (v: Vector): Number.Number =
    v
    |> LazyList.map
        (fun z ->
            Complex.absVal z * Complex.absVal z
        )
    |> LazyList.fold (+) 0.0
    |> sqrt

let opposite (v: Vector): Vector =
    LazyList.map Complex.opposite v

let unary (v: Vector): Vector option =
    let nrm = norm v
    if nrm = 0.0
        then None
        else timesNumber (1.0 / nrm) v
             |> Some

// operators

let sum (v1: Vector) (v2: Vector): Vector =
    maybeThrowException v1 v2
    LazyList.map2 Complex.sum v1 v2

let difference (v1: Vector) (v2: Vector): Vector =
    maybeThrowException v1 v2
    v2
    |> opposite
    |> sum v1

let innerProduct (v1: Vector) (v2: Vector): Complex.Complex =
    maybeThrowException v1 v2
    LazyList.zip v1 v2
    |> LazyList.map
        ( fun (c, z) ->
            z
            |> Complex.conjugate
            |> Complex.product c
        )
    |> LazyList.fold Complex.sum Complex.zero

// comparison

let almostEqual (error: Number.Number) (v1: Vector) (v2: Vector): bool =
    maybeThrowException v1 v2
    Seq.forall2 (Complex.almostEqual error) v1 v2

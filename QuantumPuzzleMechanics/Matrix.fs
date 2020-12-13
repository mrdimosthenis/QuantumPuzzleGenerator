module QuantumPuzzleMechanics.Matrix

open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Matrix = Vector.Vector LazyList

// exceptions

exception NonMatchingDims

exception DifferentDims

let maybeThrowException (a: Matrix) (b: Matrix): unit =
    let v1 = LazyList.head a
    let v2 = LazyList.head b
    let diffN = LazyList.length a <> LazyList.length b
    let diffM = Vector.dim v1 <> Vector.dim v2
    if diffN || diffM
        then raise DifferentDims
        else ()

// basic converters

let ofVec (v: Vector.Vector): Matrix =
    LazyList.map
        ( fun c ->
            c
            |> Seq.singleton
            |> LazyList.ofSeq
        )
        v

let ofVectors (v1: Vector.Vector) (v2: Vector.Vector): Matrix =
    LazyList.map
        ( fun c ->
            LazyList.map
                ( fun z ->
                    z
                    |> Complex.conjugate
                    |> Complex.times c
                )
                v2
        )
        v1

// constants

let zero (m: int) (n: int): Matrix =
    m
    |> Vector.zero
    |> LazyList.repeat
    |> LazyList.take n

let identity (n: int): Matrix =
    zero n n
    |> Seq.indexed
    |> LazyList.ofSeq
    |> LazyList.map
            ( fun (i, v) ->
                v
                |> Seq.indexed
                |> LazyList.ofSeq
                |> LazyList.map
                        ( fun (j, c) ->
                            if i = j
                                then Complex.ofNum 1.0
                                else c
                        )
            )

// methods

let timesNumber (x: Number.Number) (a: Matrix): Matrix =
    LazyList.map (Vector.timesNumber x) a

let timesComplex (c: Complex.Complex) (a: Matrix): Matrix =
    LazyList.map (Vector.timesComplex c) a

let vectorExpansion (a: Matrix): Vector.Vector =
    LazyList.concat a

let dims (a: Matrix): int * int =
    let m = a
            |> LazyList.head
            |> Vector.dim
    let n = LazyList.length a
    (m, n)

let opposite (a: Matrix): Matrix =
    LazyList.map Vector.opposite a

let transposed (a: Matrix): Matrix =
    a
    |> Seq.transpose
    |> LazyList.ofSeq
    |> LazyList.map LazyList.ofSeq

let transjugate (a: Matrix): Matrix =
    a
    |> LazyList.map
            ( fun v ->
                LazyList.map Complex.conjugate v
            )
    |> transposed

// operators

let plus (a: Matrix) (b: Matrix): Matrix =
    maybeThrowException a b
    LazyList.map2 Vector.plus a b

let minus (a: Matrix) (b: Matrix): Matrix =
    maybeThrowException a b
    LazyList.map2 Vector.minus a b

let times (a: Matrix) (b: Matrix): Matrix =
    let (m1, _) = dims a
    let (_, n2) = dims b
    if m1 <> n2
        then raise NonMatchingDims
        else ()
    LazyList.map
            ( fun v1 ->
                b
                |> transjugate
                |> LazyList.map (Vector.times v1)
            )
            a

let tensorProduct (a: Matrix) (b: Matrix): Matrix =
    let (m1, n1) = dims a
    let (m2, n2) = dims b
    let toNestedArray matrix =
        matrix
        |> LazyList.map LazyList.toArray
        |> LazyList.toArray
    let arrayA = toNestedArray a
    let arrayB = toNestedArray b
    let resultArray = Array2D.zeroCreate
                        (n1 * n2)
                        (m1 * m2)
    for i = 0 to n1 do
        for j = 0 to m1 do
            for k = 0 to n2 do
                for l = 0 to m2 do
                    resultArray.[n2 * i + k, m2 * j + l] <-
                        Complex.times
                            arrayA.[i].[j]
                            arrayB.[k].[l]
    seq {
        for x in 0 .. Array2D.length1 resultArray - 1 do
            yield seq {
                for y in 0 .. Array2D.length2 resultArray - 1 ->
                    resultArray.[x, y] 
            }
    }
    |> LazyList.ofSeq
    |> LazyList.map LazyList.ofSeq

// comparison

let almostEqual (error: Number.Number) (a: Matrix) (b: Matrix): bool =
    maybeThrowException a b
    Seq.forall2 (Vector.almostEqual error) a b

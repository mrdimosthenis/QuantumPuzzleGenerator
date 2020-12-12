﻿module QuantumPuzzleMechanics.Complex

open QuantumPuzzleMechanics

// types

type Complex = { Re: Number.Number; Im: Number.Number }

// basic converters

let ofNumbers (re: Number.Number) (im: Number.Number): Complex =
    { Re = re; Im = im }

let ofNum (x: Number.Number): Complex =
    ofNumbers x 0.0

let toString (c: Complex): string =
    sprintf
        "%s %s %s*i"
        (string c.Re)
        (if c.Im < 0.0 then "-" else "+")
        (c.Im |> abs |> string)

// constants

let zero: Complex =
    ofNumbers 0.0 0.0

// methods

let timesNumber (x: Number.Number) (c: Complex): Complex =
    ofNumbers (x * c.Re) (x * c.Im)

let conjugate (c: Complex): Complex =
    ofNumbers c.Re -c.Im

let absVal (c: Complex): Number.Number =
    c.Re*c.Re + c.Im*c.Im
    |> sqrt

let opposite (c: Complex): Complex =
    ofNumbers -c.Re -c.Im

let inverse (c: Complex): Complex option =
    if c = zero
        then None
        else let absSqr = absVal c * absVal c
             let invRe = c.Re / absSqr
             let invIm = -c.Im / absSqr
             ofNumbers invRe invIm
             |> Some

// advanced converters

let toPolar (c: Complex): Number.Number * Number.Number =
    let r = absVal c
    let theta = atan (c.Im / c.Re)
    (r, theta)

let ofPolar (r: Number.Number, theta: Number.Number): Complex =
    let re = r * cos theta
    let im = r * sin theta
    ofNumbers re im

// operators

let plus (c: Complex) (z: Complex): Complex =
    ofNumbers (c.Re + z.Re) (c.Im + z.Im)

let minus (c: Complex) (z: Complex): Complex =
    z
    |> opposite
    |> plus c

let times (c: Complex) (z: Complex): Complex =
    let re = c.Re * z.Re - c.Im * z.Im
    let im = c.Re * z.Im + c.Im * z.Re
    ofNumbers re im

let divide (c: Complex) (z: Complex): Complex option =
    z
    |> inverse
    |> Option.map (times c)

// comparison

let almostEqual (error: Number.Number) (c: Complex) (z: Complex): bool =
    let b1 = Number.almostEqual error c.Re z.Re
    let b2 = Number.almostEqual error c.Im z.Im
    b1 && b2
    
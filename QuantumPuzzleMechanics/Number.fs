module QuantumPuzzleMechanics.Number

open System

// types

type Number = float

// converters

let toRadians (angle: Number): Number = (Math.PI / 180.0) * angle

// comparison

let almostEqual (error: Number) (x: Number) (y: Number): bool = abs (x - y) < error

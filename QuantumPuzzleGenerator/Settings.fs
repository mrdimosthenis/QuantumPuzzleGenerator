module QuantumPuzzleGenerator.Settings

// types

type ScaleElement =
    | Plot
    | Circuit
    | ColorCircle

type Settings =
    { PlotScale: float
      CircuitScale: float
      ColorCircleScale: float }

// constants

let stepScaleValue: float = 0.2

let minScaleValue: float = 0.2

let maxScaleValue: float = 6.0

// constructor

let initSettings (): Settings =
    { PlotScale = 1.0
      CircuitScale = 1.0
      ColorCircleScale = 1.0 }

// functions

let increasedScaleValue (oldScaleValue: float): float =
    stepScaleValue
    |> (+) oldScaleValue
    |> min maxScaleValue

let decreasedScaleValue (oldScaleValue: float): float =
    stepScaleValue
    |> (-) oldScaleValue
    |> max minScaleValue

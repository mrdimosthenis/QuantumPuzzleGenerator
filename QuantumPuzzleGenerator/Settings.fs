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
    let plotScale =
        Preferences.plotScaleKey
        |> Preferences.tryGetFloat
        |> Option.defaultValue 1.0

    let circuitScale =
        Preferences.circuitScaleKey
        |> Preferences.tryGetFloat
        |> Option.defaultValue 1.0

    let colorCircleScale =
        Preferences.colorCircleScaleKey
        |> Preferences.tryGetFloat
        |> Option.defaultValue 1.0

    { PlotScale = plotScale
      CircuitScale = circuitScale
      ColorCircleScale = colorCircleScale }

// functions

let increasedScaleValue (oldScaleValue: float): float =
    stepScaleValue
    |> (+) oldScaleValue
    |> min maxScaleValue

let decreasedScaleValue (oldScaleValue: float): float =
    stepScaleValue
    |> (-) oldScaleValue
    |> max minScaleValue

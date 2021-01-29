module QuantumPuzzleGenerator.Preferences

open Xamarin.Essentials

// keys

let levelIndexKey: string = "levelIndex"
let solvedPuzzlesInLevelKey: string = "solvedPuzzlesInLevel"
let plotScaleKey: string = "plotScale"
let circuitScaleKey: string = "circuitScale"
let colorCircleScaleKey: string = "colorCircleScale"

// functions

let clear (): unit = Preferences.Clear()

let removeIfExists (k: string): unit =
    if Preferences.ContainsKey(k) then Preferences.Remove(k)

let setBool (k: string) (v: bool): unit = Preferences.Set(k, v)

let setInt (k: string) (v: int): unit = Preferences.Set(k, v)

let setFloat (k: string) (v: float): unit = Preferences.Set(k, v)

let setString (k: string) (v: string): unit = Preferences.Set(k, v)

let tryGetBool (k: string): bool option =
    if Preferences.ContainsKey(k) then Some(Preferences.Get(k, false)) else None

let tryGetInt (k: string): int option =
    if Preferences.ContainsKey(k) then Some(Preferences.Get(k, 0)) else None

let tryGetFloat (k: string): float option =
    if Preferences.ContainsKey(k) then Some(Preferences.Get(k, 0.0)) else None

let tryGetString (k: string): string option =
    if Preferences.ContainsKey(k) then Some(Preferences.Get(k, "")) else None

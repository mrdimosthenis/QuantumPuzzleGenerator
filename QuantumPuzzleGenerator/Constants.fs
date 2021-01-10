module QuantumPuzzleGenerator.Constants

open System
open Xamarin.Forms

open QuantumPuzzleMechanics

let random: Random = Random()

let differenceThreshold:Number.Number = 0.2

let numOfPuzzlesPerLevel: int = 20

let deviceWidth: float =
    Device.info.PixelScreenSize.Width
    |> min Device.info.PixelScreenSize.Height
    |> (*) 0.5
    
let backgroundColor = Color.White
   
let version = "1.0.0"
    
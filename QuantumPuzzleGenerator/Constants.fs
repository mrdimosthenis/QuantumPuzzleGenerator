module QuantumPuzzleGenerator.Constants

open System
open Xamarin.Essentials
open Xamarin.Forms

open QuantumPuzzleMechanics

VersionTracking.Track()

let version: string = VersionTracking.CurrentVersion

let isIOS: bool = DeviceInfo.Platform = DevicePlatform.iOS

let random: Random = Random()

let differenceThreshold: Number.Number = 0.2

let numOfPuzzlesPerLevel: int = 20

let deviceWidth: float =
    Device.info.PixelScreenSize.Width
    |> min Device.info.PixelScreenSize.Height
    |> (*) 0.5

let backgroundColor: Color = Color.White
let colorA: Color = Color.FromHex "#ffc46d" // selected gate
let colorB: Color = Color.FromHex "#fffbf6" // unselected gate

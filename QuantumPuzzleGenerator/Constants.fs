module QuantumPuzzleGenerator.Constants

open System
open Xamarin.Essentials
open Xamarin.Forms

open QuantumPuzzleMechanics

VersionTracking.Track()

let version: string = VersionTracking.CurrentVersion

let random: Random = Random()

let differenceThreshold: Number.Number = 0.2

let numOfPuzzlesPerLevel: int = 20

// device info

let isIOS: bool = DeviceInfo.Platform = DevicePlatform.iOS

let deviceWidth: float =
    Device.info.PixelScreenSize.Width
    |> min Device.info.PixelScreenSize.Height
    |> (*) 0.5

// color palette

let backgroundColor: Color = Color.White
let colorA: Color = Color.FromHex "#ffc46d" // selected gate
let colorB: Color = Color.FromHex "#fffbf6" // unselected gate
let colorC: Color = Color.LightGray // highlighted button

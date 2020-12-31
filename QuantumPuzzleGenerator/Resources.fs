module QuantumPuzzleGenerator.Resources

open Fabulous.XamarinForms
open Xamarin.Forms

let image (name: string): Image.Value = 
    name
    |> sprintf "QuantumPuzzleGenerator.resources.images.%s.png"
    |> ImageSource.FromResource
    |> Image.fromImageSource

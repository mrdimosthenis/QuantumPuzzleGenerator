module QuantumPuzzleGenerator.Resources

open System.IO
open System.Reflection

open Fabulous.XamarinForms
open Xamarin.Forms

open QuantumPuzzleMechanics

// text files

let text (fullName: string): string =
    use stream =
        fullName
        |> sprintf "QuantumPuzzleGenerator.resources.text_files.%s"
        |> Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream

    use reader = new StreamReader(stream)
    reader.ReadToEnd()

// images

let image (name: string): Image.Value =
    name
    |> sprintf "QuantumPuzzleGenerator.resources.images.%s.png"
    |> ImageSource.FromResource
    |> Image.fromImageSource

let gateName (numOfQubits: int) (gate: Quantum.Gate.Gate): string =
    match gate with
    | Quantum.Gate.XGate i -> sprintf "x%i%i" numOfQubits i
    | Quantum.Gate.YGate i -> sprintf "y%i%i" numOfQubits i
    | Quantum.Gate.ZGate i -> sprintf "z%i%i" numOfQubits i
    | Quantum.Gate.HGate i -> sprintf "h%i%i" numOfQubits i
    | Quantum.Gate.SwapGate (ia, ib) -> sprintf "sw%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CXGate (ia, ib) -> sprintf "cx%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CZGate (ia, ib) -> sprintf "cz%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CCXGate (ia, ib, ic) -> sprintf "ccx%i%i%i%i" numOfQubits ia ib ic
    | Quantum.Gate.CSwapGate (ia, ib, ic) -> sprintf "csw%i%i%i%i" numOfQubits ia ib ic

let gateImage (gate: Quantum.Gate.Gate) (numOfQubits: int): Image.Value =
    gateName numOfQubits gate
    |> sprintf "gates.%s"
    |> image

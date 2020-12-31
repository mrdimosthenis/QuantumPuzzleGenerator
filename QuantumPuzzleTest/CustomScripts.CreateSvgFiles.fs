module CustomScripts.CreateSvgFiles

open System
open System.IO

open Xunit
open FsUnit.Xunit

open Fable
open Fable.React
open Fable.React.Props
open Xamarin.Forms

open QuantumPuzzleMechanics

let dirPath: string = """C:\Users\MrDIM\Desktop\svgGates"""

let maxNumOfQubits: int = 4

let size: float = 256.0
let strokeColor: Color = Color.Gray

let svgContent (numOfQubits: int) (gate: Quantum.Gate.Gate): string =
    let width = numOfQubits
                |> float
                |> (*) size
                |> int
                |> string
    let height = size
                 |> int
                 |> string
    svg [ Version "1.1"
          SVGAttr.Custom("baseProfile", "full")
          SVGAttr.Custom("width", width)
          SVGAttr.Custom("height", height)
          SVGAttr.Custom("xmlns", "http://www.w3.org/2000/svg") ]
        [ Graphics.Gates.gateGraphics numOfQubits gate strokeColor size ]
    |> ReactServer.renderToString

let gateName (numOfQubits: int) (gate: Quantum.Gate.Gate): string =
    match gate with
    | Quantum.Gate.XGate i -> sprintf "x%i%i" numOfQubits i
    | Quantum.Gate.YGate i -> sprintf "y%i%i" numOfQubits i
    | Quantum.Gate.ZGate i -> sprintf "z%i%i" numOfQubits i
    | Quantum.Gate.HGate i -> sprintf "h%i%i" numOfQubits i
    | Quantum.Gate.TGate i -> sprintf "t%i%i" numOfQubits i
    | Quantum.Gate.SwapGate (ia, ib) -> sprintf "sw%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CXGate (ia, ib) -> sprintf "cx%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CZGate (ia, ib) -> sprintf "cz%i%i%i" numOfQubits ia ib
    | Quantum.Gate.CCXGate (ia, ib, ic) -> sprintf "ccx%i%i%i%i" numOfQubits ia ib ic
    | Quantum.Gate.CSwapGate (ia, ib, ic) -> sprintf "csw%i%i%i%i" numOfQubits ia ib ic

let saveGate (numOfQubits: int) (gate: Quantum.Gate.Gate): unit =
    let filePath = gateName numOfQubits gate
                   |> sprintf "%s\%s.svg" dirPath
    let content = svgContent numOfQubits gate
    File.WriteAllLines (filePath, [content])

let saveAllSingeQubitGates(): unit =
    for numOfQubits = 1 to maxNumOfQubits do
        for g in [ Quantum.Gate.XGate; Quantum.Gate.YGate; Quantum.Gate.ZGate; Quantum.Gate.HGate; Quantum.Gate.TGate ] do
           for i = 0 to (numOfQubits-1) do
                saveGate numOfQubits (g i)

let saveAllDoubleQubitGates(): unit =
    for numOfQubits = 2 to maxNumOfQubits do
        for g in [ Quantum.Gate.SwapGate; Quantum.Gate.CXGate; Quantum.Gate.CZGate ] do
           for ia = 0 to (numOfQubits-1) do
                for ib = 0 to (numOfQubits-1) do
                    if ib <> ia
                        then saveGate numOfQubits (g (ia, ib))
                        else ()

let saveAllTripleQubitGates(): unit =
    for numOfQubits = 3 to maxNumOfQubits do
        for g in [ Quantum.Gate.CCXGate; Quantum.Gate.CSwapGate ] do
           for ia = 0 to (numOfQubits-1) do
                for ib = 0 to (numOfQubits-1) do
                    if ib <> ia
                        then for ic = 0 to (numOfQubits-1) do
                                if ic <> ia && ic <> ib
                                    then saveGate numOfQubits (g (ia, ib, ic))
                                    else ()
                        else ()

//[<Fact>]
//let ``save all qubit gates`` () =
//    saveAllSingeQubitGates()
//    saveAllDoubleQubitGates()
//    saveAllTripleQubitGates()
//    should equal () ()

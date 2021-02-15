module CustomScripts.CreateSvgFiles

open System.IO

open Xunit
open FsUnit.Xunit

open Giraffe
open GiraffeViewEngine
open Xamarin.Forms

open QuantumPuzzleMechanics

let dirPath: string = """C:\Users\MrDIM\Desktop\svgGates"""

let maxNumOfQubits: int = 4

let size: float = 256.0
let strokeColor: Color = Color.Gray

let svgContent (numOfQubits: int) (gate: Quantum.Gate.Gate): string =
    let width = size |> int |> string

    let height =
        numOfQubits |> float |> (*) size |> int |> string

    tag
        "svg"
        [ attr "version" "1.1"
          attr "baseProfile" "full"
          attr "width" width
          attr "height" height
          attr "xmlns" "http://www.w3.org/2000/svg" ]
        [ Graphics.Gates.gateGraphics numOfQubits gate strokeColor size ]
    |> renderXmlNode

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

let saveGate (numOfQubits: int) (gate: Quantum.Gate.Gate): unit =
    let filePath =
        gateName numOfQubits gate
        |> sprintf "%s\%s.svg" dirPath

    let content = svgContent numOfQubits gate
    File.WriteAllLines(filePath, [ content ])

let saveAllSingeQubitGates (): unit =
    for numOfQubits = 1 to maxNumOfQubits do
        for g in [ Quantum.Gate.XGate
                   Quantum.Gate.YGate
                   Quantum.Gate.ZGate
                   Quantum.Gate.HGate ] do
            for i = 0 to (numOfQubits - 1) do
                saveGate numOfQubits (g i)

let saveAllDoubleQubitGates (): unit =
    for numOfQubits = 2 to maxNumOfQubits do
        for g in [ Quantum.Gate.SwapGate
                   Quantum.Gate.CXGate
                   Quantum.Gate.CZGate ] do
            for ia = 0 to (numOfQubits - 1) do
                for ib = 0 to (numOfQubits - 1) do
                    if ib <> ia then saveGate numOfQubits (g (ia, ib)) else ()

let saveAllTripleQubitGates (): unit =
    for numOfQubits = 3 to maxNumOfQubits do
        for g in [ Quantum.Gate.CCXGate
                   Quantum.Gate.CSwapGate ] do
            for ia = 0 to (numOfQubits - 1) do
                for ib = 0 to (numOfQubits - 1) do
                    if ib <> ia then
                        for ic = 0 to (numOfQubits - 1) do
                            if ic <> ia && ic <> ib then saveGate numOfQubits (g (ia, ib, ic)) else ()
                    else
                        ()

//[<Fact>]
//let ``save all qubit gates`` () =
//    saveAllSingeQubitGates ()
//    saveAllDoubleQubitGates ()
//    saveAllTripleQubitGates ()
//    should equal () ()

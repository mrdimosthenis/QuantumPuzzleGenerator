module QuantumPuzzleMechanics.Generator

open System

open FSharpx.Collections

open QuantumPuzzleMechanics

// types

type Generator = Random

// exceptions

exception InvalidGateSize

// functions

let nextInt (random: Random) (maxVal: int) (): int =
    random.NextDouble()
    |> (*) (float maxVal)
    |> floor
    |> int

let nextListItem<'a> (random: Random) (ls: 'a list) (): 'a =
    let i = nextInt random ls.Length ()
    List.item i ls

let rec nextDiffInt (random: Random) (maxVal: int) (excludedValues: int list) (): int =
    let n = nextInt random maxVal ()

    if (Seq.forall ((<>) n) excludedValues)
    then n
    else nextDiffInt random maxVal excludedValues ()

let nextDistinctGroup (random: Random) (maxVal: int) (groupSize: int) (): int list =

    let rec go (acc: int list) (n: int): int list =
        if n < groupSize then
            let hd = nextDiffInt random maxVal acc ()
            let nextAcc = List.cons hd acc
            n + 1 |> go nextAcc
        else
            acc

    go [] 0

let nextPosInt (random: Random) (maxVal: int) (): int = nextInt random maxVal () |> (+) 1

let nextNumber (random: Random) (): Number.Number =
    if random.NextDouble() < 0.5 then 1.0 else -1.0
    |> (*) (random.NextDouble())
    |> (*) 1000.0

let nextComplex (random: Random) (): Complex.Complex =
    Complex.ofNumbers (nextNumber random ()) (nextNumber random ())

let nextVector (random: Random) (n: int) (): Vector.Vector =
    n
    |> Vector.zero
    |> LazyList.map (fun _ -> nextComplex (random) ())

let nextMatrix (random: Random) (m: int) (n: int) (): Matrix.Matrix =
    Matrix.zero m n
    |> LazyList.map (fun _ -> nextVector random m ())
    
let qStateSize (numOfQubits: int): int =
    Math.Pow(2.0, float numOfQubits)
        |> Math.Round
        |> int

let rec nextQState (random: Random) (numOfQubits: int) (): Matrix.Matrix =
    let n = qStateSize numOfQubits
    let v = nextVector random n ()

    if (Seq.exists ((<>) Complex.zero) v) then
        v
        |> Vector.unary
        |> Option.get
        |> Seq.singleton
        |> Seq.transpose
        |> Utils.ofSeqOfSeqs
    else
        nextQState random numOfQubits ()
        
let rec nextAbsolutQState (random: Random) (numOfQubits: int) (): Matrix.Matrix =
    let n = qStateSize numOfQubits
    let nonZeroIndex = nextInt random n ()
    
    fun i -> if i = nonZeroIndex
                then seq [ Complex.ofNum 1.0 ]
                else seq [ Complex.zero ]
    |> Seq.init n
    |> Utils.ofSeqOfSeqs

let nextGate1 (random: Random) (numOfQubits: int) (): Quantum.Gate.Gate =
    let selectedGate =
        nextListItem
            random
            [ Quantum.Gate.XGate
              Quantum.Gate.YGate
              Quantum.Gate.ZGate
              Quantum.Gate.HGate ]
            ()

    let index = nextInt random numOfQubits ()
    selectedGate index

let nextGate2 (random: Random) (numOfQubits: int) (): Quantum.Gate.Gate =
    let selectedGate =
        nextListItem
            random
            [ Quantum.Gate.SwapGate
              Quantum.Gate.CXGate
              Quantum.Gate.CZGate ]
            ()

    let indexA = nextInt random numOfQubits ()

    let indexB =
        nextDiffInt random numOfQubits [ indexA ] ()

    selectedGate (indexA, indexB)

let nextGate3 (random: Random) (numOfQubits: int) (): Quantum.Gate.Gate =
    let selectedGate =
        nextListItem
            random
            [ Quantum.Gate.CCXGate
              Quantum.Gate.CSwapGate ]
            ()

    let indexA = nextInt random numOfQubits ()

    let indexB =
        nextDiffInt random numOfQubits [ indexA ] ()

    let indexC =
        nextDiffInt random numOfQubits [ indexA; indexB ] ()

    selectedGate (indexA, indexB, indexC)

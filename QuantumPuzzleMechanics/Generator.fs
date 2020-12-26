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

let rec nextDiffInt (random: Random) (maxVal: int) (excludedVals: int list) (): int =
    let n = nextInt random maxVal ()
    if (Seq.forall ((<>) n) excludedVals)
        then n
        else nextDiffInt random maxVal excludedVals ()

let nextPosInt (random: Random) (maxVal: int) (): int =
    nextInt random maxVal ()
    |> (+) 1

let nextNumber (random: Random) (): Number.Number =
    if random.NextDouble() < 0.5
        then 1.0
        else -1.0
    |> (*) (random.NextDouble())
    |> (*) 1000.0

let nextComplex (random: Random) (): Complex.Complex =
    Complex.ofNumbers
        (nextNumber random ())
        (nextNumber random ())

let nextVector (random: Random) (n: int) (): Vector.Vector =
    n
    |> Vector.zero
    |> LazyList.map
        (fun _ ->
            nextComplex (random) ()
        )

let nextMatrix (random: Random) (m: int) (n: int) (): Matrix.Matrix =
    Matrix.zero m n
    |> LazyList.map
        (fun _ ->
            nextVector random m ()
        )

let rec nextQState (random: Random) (numOfQubits: int) (): Matrix.Matrix =
    let n = Math.Pow(2.0, float numOfQubits)
            |> Math.Round
            |> int
    let v = nextVector random n ()
    if (Seq.exists ((<>) Complex.zero) v)
        then v
             |> Vector.unary
             |> Option.get
             |> Seq.singleton
             |> Seq.transpose
             |> Utils.ofSeqOfSeqs
        else nextQState random numOfQubits ()

let nextGate (random: Random) (numOfQubits: int) (): Quantum.Gate.Gate =
    let gateSize =
            nextPosInt random 3 ()
            |> min numOfQubits
    match gateSize with
    | 1 -> let selectedGate =
                    nextListItem random
                                 [ Quantum.Gate.XGate
                                   Quantum.Gate.YGate
                                   Quantum.Gate.ZGate
                                   Quantum.Gate.HGate ]
                                 ()
           let index = nextInt random numOfQubits ()
           selectedGate index
    | 2 -> let selectedGate =
                    nextListItem random
                                    [ Quantum.Gate.SwapGate
                                      Quantum.Gate.CXGate
                                      Quantum.Gate.CZGate ]
                                    ()
           let indexA = nextInt random numOfQubits ()
           let indexB = nextDiffInt random numOfQubits [indexA] ()
           selectedGate (indexA, indexB)
    | 3 -> let selectedGate =
                    nextListItem random
                                    [ Quantum.Gate.CCXGate
                                      Quantum.Gate.CSwapGate ]
                                    ()
           let indexA = nextInt random numOfQubits ()
           let indexB = nextDiffInt random numOfQubits [indexA] ()
           let indexC = nextDiffInt random numOfQubits [indexA; indexB] ()
           selectedGate (indexA, indexB, indexC)
    | _ -> raise InvalidGateSize

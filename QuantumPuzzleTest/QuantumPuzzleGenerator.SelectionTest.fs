module QuantumPuzzleGenerator.SelectionTest

open Xunit
open FsUnit.Xunit

open FSharpx.Collections

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator.Selection

let differenceThreshold = 0.001

let random = System.Random()

let numOfQubits = Generator.nextInt random 3 ()
                  |> (+) 3

let initQState = Generator.nextQState random numOfQubits ()

let indexA = Generator.nextInt random numOfQubits ()
let indexB = Generator.nextDiffInt random numOfQubits [indexA] ()

[<Fact>]
let ``X selected as first gate results distinct quantum states`` () =
    let reachedQStates = LazyList.ofList [initQState]
    let candidateGate = Quantum.Gate.XGate indexA
    distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate
    |> Option.isSome
    |> should equal true

[<Fact>]
let ``H selected as first and second gate does not result distinct quantum states`` () =
    let initReachedQStates = LazyList.ofList [initQState]
    let candidateGate = Quantum.Gate.HGate indexA
    let reachedQStates =
            distinctQStates numOfQubits differenceThreshold initReachedQStates candidateGate
            |> Option.get
    distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate
    |> Option.isNone
    |> should equal true

[<Fact>]
let ``Swap selected as first gate results distinct quantum states`` () =
    let reachedQStates = LazyList.ofList [initQState]
    let candidateGate = Quantum.Gate.SwapGate (indexA, indexB)
    distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate
    |> Option.isSome
    |> should equal true

[<Fact>]
let ``Swap selected as first and second gate does not result distinct quantum states`` () =
    let initReachedQStates = LazyList.ofList [initQState]
    let candidateGate = Quantum.Gate.SwapGate (indexA, indexB)
    let reachedQStates =
            distinctQStates numOfQubits differenceThreshold initReachedQStates candidateGate
            |> Option.get
    distinctQStates numOfQubits differenceThreshold reachedQStates candidateGate
    |> Option.isNone
    |> should equal true

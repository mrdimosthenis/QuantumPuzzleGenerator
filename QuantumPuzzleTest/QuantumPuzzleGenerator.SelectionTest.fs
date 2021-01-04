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

[<Fact>]
let ``Select three gates and the quantum states of one qubit with  0_2 difference threshold`` () =
    let (selectedGates, selectedQStates) = selectGatesAndQStates random Quantum.Gate.SingleQubit 3 1 0.2
    (LazyList.length selectedGates, LazyList.length selectedQStates)
    |> should equal (3, 8)

[<Fact>]
let ``Select one gate and the quantum states of three qubits with  0_01 difference threshold`` () =
    let (selectedGates, selectedQStates) = selectGatesAndQStates random Quantum.Gate.DoubleQubit 1 3 0.01
    (LazyList.length selectedGates, LazyList.length selectedQStates)
    |> should equal (1, 2)

[<Fact>]
let ``Select two gates and the quantum states of four qubits with  0_1 difference threshold`` () =
    let (selectedGates, selectedQStates) = selectGatesAndQStates random Quantum.Gate.TripleQubit 2 4 0.1
    (LazyList.length selectedGates, LazyList.length selectedQStates)
    |> should equal (2, 4)

[<Fact>]
let ``Select three gates and the quantum states of five qubits with  0_3 difference threshold`` () =
    let (selectedGates, selectedQStates) = selectGatesAndQStates random Quantum.Gate.SingleQubit 3 5 0.3
    (LazyList.length selectedGates, LazyList.length selectedQStates)
    |> should equal (3, 8)

[<Fact>]
let ``Select three single-qubit-gates for one qubit with 0_02 difference threshold`` () =
    let randomInstance = System.Random(1000)
    selectGatesAndQStates randomInstance Quantum.Gate.SingleQubit 3 1 0.02
    |> fst
    |> LazyList.toList
    |> should equal
        [ Quantum.Gate.HGate 0
          Quantum.Gate.XGate 0
          Quantum.Gate.ZGate 0 ]

[<Fact>]
let ``Select four single-or-double-qubit-gates for five qubits with 0_2 difference threshold`` () =
    let randomInstance = System.Random(1000)
    selectGatesAndQStates randomInstance Quantum.Gate.DoubleQubit 4 5 0.2
    |> fst
    |> LazyList.toList
    |> should equal
         [ Quantum.Gate.CZGate (0, 1)
           Quantum.Gate.YGate 0
           Quantum.Gate.ZGate 3
           Quantum.Gate.CZGate (4, 3) ]

[<Fact>]
let ``Select 10 single-qubit-gates for four qubits with 0_2 difference threshold`` () =
    let randomInstance = System.Random(1000)
    selectGatesAndQStates randomInstance Quantum.Gate.SingleQubit 10 4 0.2
    |> fst
    |> LazyList.toList
    |> should equal
        [ Quantum.Gate.ZGate 3
          Quantum.Gate.YGate 1
          Quantum.Gate.XGate 2
          Quantum.Gate.HGate 2
          Quantum.Gate.YGate 0
          Quantum.Gate.YGate 3
          Quantum.Gate.XGate 1
          Quantum.Gate.HGate 3
          Quantum.Gate.HGate 0
          Quantum.Gate.ZGate 0 ]

[<Fact>]
let ``Select 10 single-or-double-or-triple-qubit-gates for three qubits with 0_2 difference threshold`` () =
    let randomInstance = System.Random(1000)
    selectGatesAndQStates randomInstance Quantum.Gate.TripleQubit 10 3 0.2
    |> fst
    |> LazyList.toList
    |> should equal
        [ Quantum.Gate.XGate 0
          Quantum.Gate.ZGate 2
          Quantum.Gate.XGate 2
          Quantum.Gate.CSwapGate (0, 1, 2)
          Quantum.Gate.CSwapGate (2, 1, 0)
          Quantum.Gate.YGate 0
          Quantum.Gate.SwapGate (1, 0)
          Quantum.Gate.ZGate 0
          Quantum.Gate.CZGate (1, 2)
          Quantum.Gate.SwapGate (2, 0) ]

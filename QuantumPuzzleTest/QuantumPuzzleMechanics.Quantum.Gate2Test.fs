module QuantumPuzzleMechanics.Quantum.Gate2Test

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics
open QuantumPuzzleMechanics.Quantum.Gate2

let error = 0.001
let almostEq = Matrix.almostEqual error

let sampleQState2 =
    [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
      [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
      [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
      [ Complex.ofNumbers 0.5864646142 0.01865244057 ] ]
    |> Utils.ofListOfLists

let sampleQState3 =
    [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
      [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
      [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
      [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
      [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
      [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
      [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
      [ Complex.ofNumbers -0.08383917706 0.1880713765 ] ]
    |> Utils.ofListOfLists

[<Fact>]
let ``Sample QState of two qubits gets through SWAP`` () =
    sampleQState2
    |> Matrix.standardProduct SWAP
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
              [ Complex.ofNumbers 0.5864646142 0.01865244057 ] ]

[<Fact>]
let ``Sample QState of two qubits gets through CX`` () =
    sampleQState2
    |> Matrix.standardProduct CX
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
              [ Complex.ofNumbers 0.5864646142 0.01865244057 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ] ]

[<Fact>]
let ``Sample QState of two qubits gets through inverted CX`` () =
    let m = matrix 2 1 0 CX
    sampleQState2
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
              [ Complex.ofNumbers 0.5864646142 0.01865244057 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ] ]

[<Fact>]
let ``Sample QState of two qubits gets through CZ`` () =
    sampleQState2
    |> Matrix.standardProduct CZ
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
              [ Complex.ofNumbers -0.5864646142 -0.01865244057 ] ]

[<Fact>]
let ``Sample QState of two qubits gets through inverted CZ`` () =
    let m = matrix 2 1 0 CZ
    sampleQState2
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
              [ Complex.ofNumbers -0.5864646142 -0.01865244057 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through SWAP with indexA = 0 and indexB = 1`` () =
    let m = matrix 3 0 1 SWAP
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through SWAP with indexA = 0 and indexB = 2`` () =
    let m = matrix 3 0 2 SWAP
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through SWAP with indexA = 2 and indexB = 0`` () =
    let m = matrix 3 2 0 SWAP
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through CX with indexA = 1 and indexB = 0`` () =
    let m = matrix 3 1 0 CX
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through CX with indexA = 0 and indexB = 2`` () =
    let m = matrix 3 0 2 CX
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through CX with indexA = 2 and indexB = 0`` () =
    let m = matrix 3 2 0 CX
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.08383917706 0.1880713765 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through CZ with indexA = 0 and indexB = 2`` () =
    let m = matrix 3 0 2 CZ
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers 0.5277142989 -0.1005585398 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers -0.2262718247 0.1957497824 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers 0.08383917706 -0.1880713765 ] ]

[<Fact>]
let ``Sample QState of three qubits gets through CZ with indexA = 2 and indexB = 1`` () =
    let m = matrix 3 2 1 CZ
    sampleQState3
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2759361607 -0.02947730478 ]
              [ Complex.ofNumbers -0.2184314665 0.09638925639 ]
              [ Complex.ofNumbers -0.03952082926 -0.08227234484 ]
              [ Complex.ofNumbers -0.5277142989 0.1005585398 ]
              [ Complex.ofNumbers -0.3554997165 -0.5563152553 ]
              [ Complex.ofNumbers 0.03515688061 0.006527585667 ]
              [ Complex.ofNumbers 0.2262718247 -0.1957497824 ]
              [ Complex.ofNumbers 0.08383917706 -0.1880713765 ] ]

let random = System.Random()

[<Fact>]
let ``3xCNOT equals SWAP property`` () =
    let numOfQubits = Generator.nextInt random 4 ()
                      |> (+) 2
    let indexA = Generator.nextInt random numOfQubits ()
    let indexB = Generator.nextDiffInt random numOfQubits [indexA] ()
    let qState = Generator.nextQState random numOfQubits ()
    let cx = matrix numOfQubits indexA indexB CX
    let xc = matrix numOfQubits indexB indexA CX
    let sw = matrix numOfQubits indexA indexB SWAP
    qState
    |> Matrix.standardProduct cx

    |> Matrix.standardProduct sw
    |> Matrix.standardProduct cx

    |> Matrix.standardProduct sw
    |> Matrix.standardProduct cx
    |> almostEq (Matrix.standardProduct sw qState)
    |> should equal true

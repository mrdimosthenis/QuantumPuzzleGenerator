module QuantumPuzzleMechanics.Quantum.Gate1Test

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics
open QuantumPuzzleMechanics.Quantum.Gate1

let error = 0.001
let almostEq = Matrix.almostEqual error

let zeroQState1 =
    [ [ Complex.ofNum 1.0 ]
      [ Complex.zero ] ]
    |> Utils.ofListOfLists

let oneQState1 =
    [ [ Complex.zero ]
      [ Complex.ofNum 1.0 ] ]
    |> Utils.ofListOfLists

let sampleQState1 =
    [ [ Complex.ofNumbers 0.3293618281 -0.8101559664 ]
      [ Complex.ofNumbers 0.2247344625 -0.429723769 ] ]
    |> Utils.ofListOfLists

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
let ``|0> gets through X`` () =
    zeroQState1
    |> Matrix.standardProduct X
    |> Utils.toListOfLists
    |> should equal
            (Utils.toListOfLists oneQState1)

[<Fact>]
let ``|1> gets through X`` () =
    oneQState1
    |> Matrix.standardProduct X
    |> Utils.toListOfLists
    |> should equal
            (Utils.toListOfLists zeroQState1)

[<Fact>]
let ``Sample QState of a single qubit gets through X`` () =
    sampleQState1
    |> Matrix.standardProduct X
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.2247344625 -0.429723769 ]
              [ Complex.ofNumbers 0.3293618281 -0.8101559664 ] ]

[<Fact>]
let ``|0> gets through Y`` () =
    zeroQState1
    |> Matrix.standardProduct Y
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.zero ]
              [ Complex.ofNumbers 0.0 1.0 ] ]

[<Fact>]
let ``|1> gets through Y`` () =
    oneQState1
    |> Matrix.standardProduct Y
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.0 -1.0 ]
              [ Complex.zero ] ]

[<Fact>]
let ``Sample QState of a single qubit gets through Y`` () =
    sampleQState1
    |> Matrix.standardProduct Y
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers -0.429723769 -0.2247344625 ]
              [ Complex.ofNumbers 0.8101559664 0.3293618281 ] ]

[<Fact>]
let ``|0> gets through Z`` () =
    zeroQState1
    |> Matrix.standardProduct Z
    |> Utils.toListOfLists
    |> should equal
            (Utils.toListOfLists zeroQState1)

[<Fact>]
let ``|1> gets through Z`` () =
    oneQState1
    |> Matrix.standardProduct Z
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.zero ]
              [ Complex.ofNum -1.0 ] ]

[<Fact>]
let ``Sample QState of a single qubit gets through Z`` () =
    sampleQState1
    |> Matrix.standardProduct Z
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.3293618281 -0.8101559664 ]
              [ Complex.ofNumbers -0.2247344625 0.429723769 ] ]

[<Fact>]
let ``|0> gets through H`` () =
    zeroQState1
    |> Matrix.standardProduct H
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNum (1.0 / sqrt 2.0) ]
              [ Complex.ofNum (1.0 / sqrt 2.0) ] ]

[<Fact>]
let ``|1> gets through H`` () =
    oneQState1
    |> Matrix.standardProduct H
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNum (1.0 / sqrt 2.0) ]
              [ Complex.ofNum -(1.0 / sqrt 2.0) ] ]

[<Fact>]
let ``Sample QState of a single qubit gets through H`` () =
    sampleQState1
    |> Matrix.standardProduct H
    |> almostEq
            (Utils.ofListOfLists
                [ [ Complex.ofNumbers 0.3918052445 -0.8767273688 ]
                  [ Complex.ofNumbers 0.07398271971 -0.2690061866 ] ])
    |> should equal true

[<Fact>]
let ``The first qubit out of two gets through X`` () =
    sampleQState2
    |> Matrix.standardProduct (matrix 2 0 X)
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1615844534 0.1296435844 ]
              [ Complex.ofNumbers 0.5864646142 0.01865244057 ]
              [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.5620387438 0.5325288344 ] ]

[<Fact>]
let ``The second qubit out of two gets through X`` () =
    sampleQState2
    |> Matrix.standardProduct (matrix 2 1 X)
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.5620387438 0.5325288344 ]
              [ Complex.ofNumbers 0.1109863929 0.03165216845 ]
              [ Complex.ofNumbers 0.5864646142 0.01865244057 ]
              [ Complex.ofNumbers 0.1615844534 0.1296435844 ] ]

[<Fact>]
let ``The first qubit out of three gets through Y`` () =
    sampleQState3
    |> Matrix.standardProduct (matrix 3 0 Y)
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers -0.5563152553 0.3554997165 ]
              [ Complex.ofNumbers -0.1957497824 -0.2262718247 ]
              [ Complex.ofNumbers 0.006527585667 -0.03515688061 ]
              [ Complex.ofNumbers 0.1880713765 0.08383917706 ]
              [ Complex.ofNumbers 0.02947730478 0.2759361607 ]
              [ Complex.ofNumbers 0.08227234484 -0.03952082926 ]
              [ Complex.ofNumbers -0.09638925639 -0.2184314665 ]
              [ Complex.ofNumbers 0.1005585398 0.5277142989 ] ]

[<Fact>]
let ``The second qubit out of three gets through Y`` () =
    sampleQState3
    |> Matrix.standardProduct (matrix 3 1 Y)
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.09638925639 0.2184314665 ]
              [ Complex.ofNumbers -0.1005585398 -0.5277142989 ]
              [ Complex.ofNumbers 0.02947730478 0.2759361607 ]
              [ Complex.ofNumbers 0.08227234484 -0.03952082926 ]
              [ Complex.ofNumbers 0.006527585667 -0.03515688061 ]
              [ Complex.ofNumbers 0.1880713765 0.08383917706 ]
              [ Complex.ofNumbers 0.5563152553 -0.3554997165 ]
              [ Complex.ofNumbers 0.1957497824 0.2262718247 ] ]

[<Fact>]
let ``The third qubit out of three gets through Y`` () =
    sampleQState3
    |> Matrix.standardProduct (matrix 3 2 Y)
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers -0.08227234484 0.03952082926 ]
              [ Complex.ofNumbers 0.02947730478 0.2759361607 ]
              [ Complex.ofNumbers -0.1005585398 -0.5277142989 ]
              [ Complex.ofNumbers -0.09638925639 -0.2184314665 ]
              [ Complex.ofNumbers -0.1957497824 -0.2262718247 ]
              [ Complex.ofNumbers 0.5563152553 -0.3554997165 ]
              [ Complex.ofNumbers 0.1880713765 0.08383917706 ]
              [ Complex.ofNumbers -0.006527585667 0.03515688061 ] ]

let random = System.Random()
let numOfQubits = Generator.nextPosInt random 3 ()
let index = Generator.nextPosInt random (numOfQubits - 1) ()
let qState = Generator.nextQState random numOfQubits ()

[<Fact>]
let ``NOT is its own inverse property`` () =
    let m = matrix numOfQubits index X
    qState
    |> Matrix.standardProduct m
    |> Matrix.standardProduct m
    |> almostEq qState
    |> should equal true

[<Fact>]
let ``HAD is its own inverse property`` () =
    let m = matrix numOfQubits index H
    qState
    |> Matrix.standardProduct m
    |> Matrix.standardProduct m
    |> almostEq qState
    |> should equal true

[<Fact>]
let ``Z is its own inverse property`` () =
    let m = matrix numOfQubits index Z
    qState
    |> Matrix.standardProduct m
    |> Matrix.standardProduct m
    |> almostEq qState
    |> should equal true

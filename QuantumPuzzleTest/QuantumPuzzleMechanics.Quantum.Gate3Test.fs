module QuantumPuzzleMechanics.Quantum.Gate3Test

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics
open QuantumPuzzleMechanics.Quantum.Gate3

let error = 0.001
let almostEq = Matrix.almostEqual error

let sampleQState4 =
    [ [ Complex.ofNumbers 0.1689816124 -0.2397928746 ]
      [ Complex.ofNumbers 0.1460380625 0.1911942581 ]
      [ Complex.ofNumbers 0.1348747574 -0.01360647846 ]
      [ Complex.ofNumbers -0.1560892409 0.162675552 ]
      [ Complex.ofNumbers -0.2566864164 -0.209643826 ]
      [ Complex.ofNumbers 0.1064029632 -0.3203841137 ]
      [ Complex.ofNumbers -0.0112908212 0.1241081746 ]
      [ Complex.ofNumbers 0.2291828741 0.1085204133 ]
      [ Complex.ofNumbers -0.2694558194 0.116226412 ]
      [ Complex.ofNumbers 0.2056576183 -0.1374952252 ]
      [ Complex.ofNumbers 0.0239330094 0.07006200091 ]
      [ Complex.ofNumbers -0.1360830893 0.2760781599 ]
      [ Complex.ofNumbers -0.1172951558 -0.1075498039 ]
      [ Complex.ofNumbers 0.07358113317 0.2974524401 ]
      [ Complex.ofNumbers -0.3081663118 0.06972884607 ]
      [ Complex.ofNumbers 0.03056354898 0.1253263191 ] ]
    |> Utils.ofListOfLists

[<Fact>]
let ``Sample QState of four qubits gets through CCX with indexA = 0, indexB = 1 and indexC = 2`` () =
    let m = matrix 4 0 1 2 CCX
    sampleQState4
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1689816124 -0.2397928746 ]
              [ Complex.ofNumbers 0.1460380625 0.1911942581 ]
              [ Complex.ofNumbers 0.1348747574 -0.01360647846 ]
              [ Complex.ofNumbers -0.1560892409 0.162675552 ]
              [ Complex.ofNumbers -0.2566864164 -0.209643826 ]
              [ Complex.ofNumbers 0.1064029632 -0.3203841137 ]
              [ Complex.ofNumbers -0.0112908212 0.1241081746 ]
              [ Complex.ofNumbers 0.2291828741 0.1085204133 ]
              [ Complex.ofNumbers -0.2694558194 0.116226412 ]
              [ Complex.ofNumbers 0.2056576183 -0.1374952252 ]
              [ Complex.ofNumbers 0.0239330094 0.07006200091 ]
              [ Complex.ofNumbers -0.1360830893 0.2760781599 ]
              [ Complex.ofNumbers -0.3081663118 0.06972884607 ]
              [ Complex.ofNumbers 0.03056354898 0.1253263191 ]
              [ Complex.ofNumbers -0.1172951558 -0.1075498039 ]
              [ Complex.ofNumbers 0.07358113317 0.2974524401 ] ]

[<Fact>]
let ``Sample QState of four qubits gets through CCX with indexA = 1, indexB = 2 and indexC = 3`` () =
    let m = matrix 4 1 2 3 CCX
    sampleQState4
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1689816124 -0.2397928746 ]
              [ Complex.ofNumbers 0.1460380625 0.1911942581 ]
              [ Complex.ofNumbers 0.1348747574 -0.01360647846 ]
              [ Complex.ofNumbers -0.1560892409 0.162675552 ]
              [ Complex.ofNumbers -0.2566864164 -0.209643826 ]
              [ Complex.ofNumbers 0.1064029632 -0.3203841137 ]
              [ Complex.ofNumbers 0.2291828741 0.1085204133 ]
              [ Complex.ofNumbers -0.0112908212 0.1241081746 ]
              [ Complex.ofNumbers -0.2694558194 0.116226412 ]
              [ Complex.ofNumbers 0.2056576183 -0.1374952252 ]
              [ Complex.ofNumbers 0.0239330094 0.07006200091 ]
              [ Complex.ofNumbers -0.1360830893 0.2760781599 ]
              [ Complex.ofNumbers -0.1172951558 -0.1075498039 ]
              [ Complex.ofNumbers 0.07358113317 0.2974524401 ]
              [ Complex.ofNumbers 0.03056354898 0.1253263191 ]
              [ Complex.ofNumbers -0.3081663118 0.06972884607 ] ]

[<Fact>]
let ``Sample QState of four qubits gets through CCX with indexA = 0, indexB = 1 and indexC = 3`` () =
    let m = matrix 4 0 1 3 CCX
    sampleQState4
    |> Matrix.standardProduct m
    |> Utils.toListOfLists
    |> should equal
            [ [ Complex.ofNumbers 0.1689816124 -0.2397928746 ]
              [ Complex.ofNumbers 0.1460380625 0.1911942581 ]
              [ Complex.ofNumbers 0.1348747574 -0.01360647846 ]
              [ Complex.ofNumbers -0.1560892409 0.162675552 ]
              [ Complex.ofNumbers -0.2566864164 -0.209643826 ]
              [ Complex.ofNumbers 0.1064029632 -0.3203841137 ]
              [ Complex.ofNumbers -0.0112908212 0.1241081746 ]
              [ Complex.ofNumbers 0.2291828741 0.1085204133 ]
              [ Complex.ofNumbers -0.2694558194 0.116226412 ]
              [ Complex.ofNumbers 0.2056576183 -0.1374952252 ]
              [ Complex.ofNumbers 0.0239330094 0.07006200091 ]
              [ Complex.ofNumbers -0.1360830893 0.2760781599 ]
              [ Complex.ofNumbers 0.07358113317 0.2974524401 ]
              [ Complex.ofNumbers -0.1172951558 -0.1075498039 ]
              [ Complex.ofNumbers 0.03056354898 0.1253263191 ]
              [ Complex.ofNumbers -0.3081663118 0.06972884607 ] ]
            
            
let random = System.Random()
let numOfQubits = Generator.nextInt random 3 ()
                  |> (+) 3

let indexA = Generator.nextInt random numOfQubits ()
let indexB = Generator.nextDiffInt random numOfQubits [indexA] ()
let indexC = Generator.nextDiffInt random numOfQubits [indexA; indexB] ()

let qState = Generator.nextQState random numOfQubits ()

[<Fact>]
let ``3xCCNOT equals CSWAP property`` () =
    let ccx = matrix numOfQubits indexA indexB indexC CCX
    let cxc = matrix numOfQubits indexA indexC indexB CCX
    let csw = matrix numOfQubits indexA indexB indexC CSWAP
    qState
    |> Matrix.standardProduct ccx
    |> Matrix.standardProduct cxc
    |> Matrix.standardProduct ccx
    |> almostEq (Matrix.standardProduct csw qState)
    |> should equal true

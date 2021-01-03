﻿module QuantumPuzzleGenerator.QStatePlottingTest

open Xunit
open FsUnit.Xunit

open FSharpx.Collections

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator.QStatePlotting

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
let ``Generated by quantum puzzles`` () =
    generatedByQuantumPuzzles 600 600 4 sampleQState4
    |> should equal ""
        

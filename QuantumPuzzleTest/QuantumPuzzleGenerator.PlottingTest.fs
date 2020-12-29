﻿module QuantumPuzzleGenerator.PlottingTest

open Xunit
open FsUnit.Xunit

open FSharpx.Collections

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator.Plotting

[<Fact>]
let ``Coordinates`` () =
    List.init 64 id
    |> List.map coordinates
    |> should equal
        [ (0.0, 0.0, 0.0); (1.0, 0.0, 0.0); (0.0, 1.0, 0.0); (1.0, 1.0, 0.0)
          (0.0, 0.0, 1.0); (1.0, 0.0, 1.0); (0.0, 1.0, 1.0); (1.0, 1.0, 1.0)
          (2.0, 0.0, 0.0); (3.0, 0.0, 0.0); (2.0, 1.0, 0.0); (3.0, 1.0, 0.0)
          (2.0, 0.0, 1.0); (3.0, 0.0, 1.0); (2.0, 1.0, 1.0); (3.0, 1.0, 1.0)
          (0.0, 2.0, 0.0); (1.0, 2.0, 0.0); (0.0, 3.0, 0.0); (1.0, 3.0, 0.0)
          (0.0, 2.0, 1.0); (1.0, 2.0, 1.0); (0.0, 3.0, 1.0); (1.0, 3.0, 1.0)
          (2.0, 2.0, 0.0); (3.0, 2.0, 0.0); (2.0, 3.0, 0.0); (3.0, 3.0, 0.0)
          (2.0, 2.0, 1.0); (3.0, 2.0, 1.0); (2.0, 3.0, 1.0); (3.0, 3.0, 1.0)
          (0.0, 0.0, 2.0); (1.0, 0.0, 2.0); (0.0, 1.0, 2.0); (1.0, 1.0, 2.0)
          (0.0, 0.0, 3.0); (1.0, 0.0, 3.0); (0.0, 1.0, 3.0); (1.0, 1.0, 3.0)
          (2.0, 0.0, 2.0); (3.0, 0.0, 2.0); (2.0, 1.0, 2.0); (3.0, 1.0, 2.0)
          (2.0, 0.0, 3.0); (3.0, 0.0, 3.0); (2.0, 1.0, 3.0); (3.0, 1.0, 3.0)
          (0.0, 2.0, 2.0); (1.0, 2.0, 2.0); (0.0, 3.0, 2.0); (1.0, 3.0, 2.0)
          (0.0, 2.0, 3.0); (1.0, 2.0, 3.0); (0.0, 3.0, 3.0); (1.0, 3.0, 3.0)
          (2.0, 2.0, 2.0); (3.0, 2.0, 2.0); (2.0, 3.0, 2.0); (3.0, 3.0, 2.0)
          (2.0, 2.0, 3.0); (3.0, 2.0, 3.0); (2.0, 3.0, 3.0); (3.0, 3.0, 3.0) ]

[<Fact>]
let ``Color by hsv`` () =
    [ Complex.zero
      Complex.ofNum 1.0
      Complex.ofNum 0.5
      Complex.ofNum -1.0
      Complex.ofNum -0.5
      Complex.ofNumbers 0.0 1.0
      Complex.ofNumbers 0.0 0.5
      Complex.ofNumbers 0.0 -1.0
      Complex.ofNumbers 0.0 -0.5
      Complex.ofNumbers 0.2 0.4
      Complex.ofNumbers -0.2 -0.4
      Complex.ofNumbers -0.2 0.4
      Complex.ofNumbers 0.2 -0.4 ]
    |> List.map hue
    |> should equal
        [ 0.0
          0.5
          0.5
          1.0
          1.0
          0.75
          0.75
          0.25
          0.25
          0.6762081911747834
          0.17620819117478337
          0.8237918088252166
          0.32379180882521663 ]

[<Fact>]
let ``sdfsdf`` () =
    Generator.nextQState (System.Random()) 4 ()
    |> representationHtml
    |> should equal ""
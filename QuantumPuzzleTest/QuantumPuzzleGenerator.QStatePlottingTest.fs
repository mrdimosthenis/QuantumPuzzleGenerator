﻿module QuantumPuzzleGenerator.QStatePlottingTest

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator.QStatePlotting

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
let ``Generated by quantum puzzles`` () =
    generatedByQuantumPuzzles 3 sampleQState3
    |> should equal """{"dotSizeRatioScale":2.0,"data":[{"x":0.25,"y":0.25,"z":0.25,"style":{"fill":"rgb(184, 184, 184)","stroke":"rgb(0, 255, 229)"}},{"x":0.75,"y":0.25,"z":0.25,"style":{"fill":"rgb(232, 232, 232)","stroke":"rgb(237, 255, 0)"}},{"x":0.25,"y":0.75,"z":0.25,"style":{"fill":"rgb(194, 194, 194)","stroke":"rgb(255, 0, 101)"}},{"x":0.75,"y":0.75,"z":0.25,"style":{"fill":"rgb(118, 118, 118)","stroke":"rgb(0, 255, 209)"}},{"x":0.25,"y":0.25,"z":0.75,"style":{"fill":"rgb(86, 86, 86)","stroke":"rgb(255, 244, 0)"}},{"x":0.75,"y":0.25,"z":0.75,"style":{"fill":"rgb(179, 179, 179)","stroke":"rgb(0, 255, 81)"}},{"x":0.25,"y":0.75,"z":0.75,"style":{"fill":"rgb(246, 246, 246)","stroke":"rgb(0, 210, 255)"}},{"x":0.75,"y":0.75,"z":0.75,"style":{"fill":"rgb(203, 203, 203)","stroke":"rgb(230, 0, 255)"}}]}"""
    
[<Fact>]
let ``Single plot html string`` () =
    singlePlotHtmlString 3 sampleQState3
    |> String.length
    |> should equal 172946

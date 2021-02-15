module QuantumPuzzleGenerator.ColorCircleTest

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics
open QuantumPuzzleGenerator.ColorCircle

let sampleQState1 =
    [ [ Complex.ofNumbers 0.3293618281 -0.8101559664 ]
      [ Complex.ofNumbers 0.2247344625 -0.429723769 ] ]
    |> Utils.ofListOfLists

[<Fact>]
let ``Single plot html string`` () =
    sampleQState1
    |> Some
    |> singlePlotHtmlString 600.0
    |> String.length
    |> should equal 1655

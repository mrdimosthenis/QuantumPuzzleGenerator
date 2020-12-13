module QuantumPuzzleMechanics.VectorTest

open System

open Xunit
open FsUnit.Xunit

open FSharpx.Collections

open QuantumPuzzleMechanics.Vector

[<Fact>]
let ``Norm of v1`` () =
    [ Complex.ofNumbers 2.0 1.0
      Complex.zero
      Complex.ofNumbers 4.0 -5.0 ]
    |> LazyList.ofList
    |> norm
    |> should equal (sqrt 46.0)

[<Fact>]
let ``Norm of v2`` () =
    [ Complex.ofNumbers 1.0 1.0
      Complex.ofNumbers 2.0 1.0
      Complex.zero ]
    |> LazyList.ofList
    |> norm
    |> should equal (sqrt 7.0)

[<Fact>]
let ``Addition`` () =
    let v1 = [ Complex.ofNumbers 1.0 2.0
               Complex.ofNumbers 3.0 -1.0 ]
             |> LazyList.ofList
    let v2 = [ Complex.ofNumbers -2.0 1.0
               Complex.ofNumbers 4.0 0.0 ]
             |> LazyList.ofList
    plus v1 v2
    |> toString
    |> should equal "[-1 + 3*i, 7 - 1*i]"

[<Fact>]
let ``Multiplication`` () =
    let z = Complex.ofNumbers 2.0 1.0
    let v = [ Complex.ofNumbers 1.0 2.0
              Complex.ofNumbers 3.0 -1.0 ]
            |> LazyList.ofList
    timesComplex z v
    |> toString
    |> should equal "[0 + 5*i, 7 + 1*i]"

[<Fact>]
let ``Inner product`` () =
    let v1 = [ Complex.ofNumbers 2.0 1.0
               Complex.zero
               Complex.ofNumbers 4.0 -5.0 ]
             |> LazyList.ofList
    let v2 = [ Complex.ofNumbers 1.0 1.0
               Complex.ofNumbers 2.0 1.0
               Complex.zero ]
             |> LazyList.ofList
    times v1 v2
    |> Complex.toString
    |> should equal "3 - 1*i"

[<Fact>]
let ``Combination of basic operations`` () =
    let z = Complex.ofNumbers 5.0 -1.0
    let v1 = [ Complex.ofNumbers 1.0 2.0
               Complex.ofNumbers 3.0 -1.0 ]
             |> LazyList.ofList
    let v2 = [ Complex.ofNumbers -2.0 1.0
               Complex.ofNumbers 4.0 0.0 ]
             |> LazyList.ofList
    minus
        (timesNumber 3.0 v1)
        (timesComplex z v2)
    |> toString
    |> should equal "[12 - 1*i, -11 + 1*i]"

let random = System.Random()

[<Fact>]
let ``Basic representation`` () =
    let z1 = Generator.nextComplex random ()
    let z2 = Generator.nextComplex random ()
    let z3 = Generator.nextComplex random ()
    let one = Complex.ofNum 1.0
    let v1 = [ one; Complex.zero; Complex.zero ]
             |> LazyList.ofList
    let v2 = [ Complex.zero; one; Complex.zero ]
             |> LazyList.ofList
    let v3 = [ Complex.zero; Complex.zero; one ]
             |> LazyList.ofList
    timesComplex z1 v1
    |> plus (timesComplex z2 v2)
    |> plus (timesComplex z3 v3)
    |> LazyList.toList
    |> should equal [ z1; z2; z3 ]

let z = Generator.nextComplex random ()
let n = random.NextDouble()
        |> (*) 10.0
        |> ceil
        |> int
let v1 = Generator.nextVector random n ()
let v2 = Generator.nextVector random n ()
let v3 = Generator.nextVector random n ()

[<Fact>]
let ``Inner product property A`` () =
    let res1 = times v1 v2
    let res2 = times v2 v1
               |> Complex.conjugate
    ComplexTest.almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Inner product property B`` () =
    let res1 = times (plus v1 v2) v3
    let res2 = Complex.plus
                    (times v1 v3)
                    (times v2 v3)
    ComplexTest.almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Inner product property C`` () =
    let res1 = times (timesComplex z v1) v2
    let res2 = Complex.times z (times v1 v2)
    ComplexTest.almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Inner product property D`` () =
    let res1 = timesComplex z v2
               |> times v1
    let res2 = Complex.times
                    (Complex.conjugate z)
                    (times v1 v2)
    ComplexTest.almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Inner product property E`` () =
    (times v1 v1).Re >= 0.0
    |> should equal true

[<Fact>]
let ``Inner product property F`` () =
    ComplexTest.almostEqNum (times v1 v1).Im 0.0
    |> should equal true

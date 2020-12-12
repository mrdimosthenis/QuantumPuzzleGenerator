module QuantumPuzzleMechanics.ComplexTest

open System

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics.Complex

let error = 0.04
let almostEqNum = Number.almostEqual error
let almostEq = almostEqual error

[<Fact>]
let ``(0.0, 0.0) To string`` () =
    ofNumbers 0.0 0.0
    |> toString
    |> should equal "0 + 0*i"

[<Fact>]
let ``(0.5, 0.0) To string`` () =
    ofNumbers 0.5 0.0
    |> toString
    |> should equal "0.5 + 0*i"

[<Fact>]
let ``(0.0, -0.2) To string`` () =
    ofNumbers 0.0 -0.2
    |> toString
    |> should equal "0 - 0.2*i"

[<Fact>]
let ``(0.5, -0.2) To string`` () =
    ofNumbers 0.5 -0.2
    |> toString
    |> should equal "0.5 - 0.2*i"

[<Fact>]
let ``(-0.5, 0.2) To string`` () =
    ofNumbers -0.5 0.2
    |> toString
    |> should equal "-0.5 + 0.2*i"

[<Fact>]
let ``Of polar`` () =
    (3.0, Number.toRadians 40.0)
    |> ofPolar
    |> almostEq (ofNumbers 2.3 1.9)
    |> should equal true

[<Fact>]
let ``To polar`` () =
    let (r, theta) = ofNumbers -1.0 2.0
                     |> toPolar
    -63.4
    |> Number.toRadians
    |> almostEqNum theta
    |> (&&) (almostEqNum r 2.2)
    |> should equal true

[<Fact>]
let ``Addition`` () =
    ofNumbers 5.0 2.0
    |> plus (ofNumbers -4.0 7.0)
    |> should equal (ofNumbers 1.0 9.0)

[<Fact>]
let ``Multiplication`` () =
    ofNumbers 5.0 2.0
    |> times (ofNumbers -4.0 3.0)
    |> should equal (ofNumbers -26.0 7.0)


[<Fact>]
let ``Rationalisation`` () =
    ofNumbers 5.0 2.0
    |> inverse
    |> Option.get
    |> almostEq (ofNumbers (5.0 / 29.0) (-2.0 / 29.0))
    |> should equal true

[<Fact>]
let ``Division of complex numbers`` () =
    ofNumbers 0.0 2.0
    |> divide (ofNumbers 3.0 2.0)
    |> Option.get
    |> almostEq (ofNumbers 1.0 (-3.0 / 2.0))
    |> should equal true

let random = System.Random()
let z1 = Generator.nextComplex random ()
let z2 = Generator.nextComplex random ()

[<Fact>]
let ``Commutativity of addition property`` () =
    plus z1 z2
    |> should equal (plus z2 z1)

[<Fact>]
let ``Commutativity of multiplication property`` () =
    times z1 z2
    |> should equal (times z2 z1)

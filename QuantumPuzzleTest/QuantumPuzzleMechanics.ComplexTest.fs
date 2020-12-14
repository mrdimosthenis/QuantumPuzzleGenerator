module QuantumPuzzleMechanics.ComplexTest

open Xunit
open FsUnit.Xunit

open QuantumPuzzleMechanics.Complex

let error = 0.04
let almostEqNum = Number.almostEqual error
let almostEq = almostEqual error

[<Fact>]
let ``To string`` () =
    ofNumbers 0.5 -0.2
    |> toString
    |> should equal "0.5 - 0.2*i"

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
let ``Sum`` () =
    ofNumbers 5.0 2.0
    |> sum (ofNumbers -4.0 7.0)
    |> toString
    |> should equal "1 + 9*i"

[<Fact>]
let ``Product`` () =
    ofNumbers 5.0 2.0
    |> product (ofNumbers -4.0 3.0)
    |> toString
    |> should equal "-26 + 7*i"


[<Fact>]
let ``Rationalisation`` () =
    ofNumbers 5.0 2.0
    |> inverse
    |> Option.get
    |> almostEq (ofNumbers (5.0 / 29.0) (-2.0 / 29.0))
    |> should equal true

[<Fact>]
let ``Quotient of complex numbers`` () =
    ofNumbers 0.0 2.0
    |> quotient (ofNumbers 3.0 2.0)
    |> Option.get
    |> almostEq (ofNumbers 1.0 (-3.0 / 2.0))
    |> should equal true

let random = System.Random()
let z1 = Generator.nextComplex random ()
let z2 = Generator.nextComplex random ()

[<Fact>]
let ``Commutativity of sum property`` () =
    sum z1 z2
    |> should equal (sum z2 z1)

[<Fact>]
let ``Commutativity of product property`` () =
    product z1 z2
    |> should equal (product z2 z1)

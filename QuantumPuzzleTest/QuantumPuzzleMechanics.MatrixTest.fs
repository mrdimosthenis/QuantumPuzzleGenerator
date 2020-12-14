module QuantumPuzzleMechanics.MatrixTest

open Xunit
open FsUnit.Xunit

open FSharpx.Collections

open QuantumPuzzleMechanics.Matrix

let error = 0.001
let almostEq = almostEqual error

[<Fact>]
let ``Outer product of vectors`` () =
    let v1 = [ Complex.ofNum 1.0
               Complex.ofNum 2.0
               Complex.ofNum 3.0 ]
             |> LazyList.ofList
    let v2 = [ Complex.ofNum 4.0
               Complex.ofNum 5.0 ]
             |> LazyList.ofList
    let res = [ [ Complex.ofNum 4.0; Complex.ofNum 5.0 ]
                [ Complex.ofNum 8.0; Complex.ofNum 10.0 ]
                [ Complex.ofNum 12.0; Complex.ofNum 15.0 ] ]
    outerProductOfVectors v1 v2
    |> LazyList.map LazyList.toList
    |> LazyList.toList
    |> should equal res

[<Fact>]
let ``Conjugate transpose`` () =
    let a = [ [ Complex.ofNumbers 3.0 7.0; Complex.zero ]
              [ Complex.ofNumbers 0.0 2.0; Complex.ofNumbers 4.0 -1.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let res = [ [ Complex.ofNumbers 3.0 -7.0; Complex.ofNumbers 0.0 -2.0 ]
                [ Complex.zero; Complex.ofNumbers 4.0 1.0 ] ]
    a
    |> transjugate
    |> LazyList.map LazyList.toList
    |> LazyList.toList
    |> should equal res

[<Fact>]
let ``Sum`` () =
    let a = [ [ Complex.ofNumbers 0.0 1.0; Complex.ofNumbers 1.0 1.0 ]
              [ Complex.ofNumbers 2.0 -3.0; Complex.ofNum 4.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let b = [ [ Complex.ofNumbers 0.0 2.0; Complex.zero ]
              [ Complex.ofNumbers 0.0 1.0; Complex.ofNumbers 1.0 2.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let res = [ [ Complex.ofNumbers 0.0 3.0; Complex.ofNumbers 1.0 1.0 ]
                [ Complex.ofNumbers 2.0 -2.0; Complex.ofNumbers 5.0 2.0 ] ]
    sum a b
    |> LazyList.map LazyList.toList
    |> LazyList.toList
    |> should equal res

[<Fact>]
let ``Standard product`` () =
    let a = [ [ Complex.ofNumbers 0.0 2.0; Complex.zero ]
              [ Complex.ofNumbers 0.0 1.0; Complex.ofNumbers 1.0 2.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let b = [ [ Complex.ofNumbers 0.0 1.0; Complex.ofNumbers 1.0 1.0 ]
              [ Complex.ofNumbers 2.0 -3.0; Complex.ofNum 4.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let res = [ [ Complex.ofNum -2.0; Complex.ofNumbers -2.0 2.0 ]
                [ Complex.ofNumbers 7.0 1.0; Complex.ofNumbers 3.0 9.0 ] ]
    standardProduct a b
    |> LazyList.map LazyList.toList
    |> LazyList.toList
    |> should equal res

[<Fact>]
let ``Tensor product`` () =
    let a = [ [ Complex.ofNum 1.0; Complex.ofNum 2.0 ]
              [ Complex.ofNum 3.0; Complex.ofNum 4.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let b = [ [ Complex.zero; Complex.ofNum 5.0 ]
              [ Complex.ofNum 6.0; Complex.ofNum 7.0 ] ]
             |> LazyList.ofList
             |> LazyList.map LazyList.ofList
    let res = [ [ Complex.zero;       Complex.ofNum 5.0;  Complex.zero;       Complex.ofNum 10.0 ]
                [ Complex.ofNum 6.0;  Complex.ofNum 7.0;  Complex.ofNum 12.0; Complex.ofNum 14.0 ]
                [ Complex.zero;       Complex.ofNum 15.0; Complex.zero;       Complex.ofNum 20.0 ]
                [ Complex.ofNum 18.0; Complex.ofNum 21.0; Complex.ofNum 24.0; Complex.ofNum 28.0 ] ]
    tensorProduct a b
    |> LazyList.map LazyList.toList
    |> LazyList.toList
    |> should equal res

let random = System.Random()
let z = Generator.nextComplex random ()
let n = Generator.nextPosInt random 10 ()
let a = Generator.nextMatrix random n n ()
let b = Generator.nextMatrix random n n ()

[<Fact>]
let ``Conjugate transpose property A of square matrices`` () =
    a
    |> transjugate
    |> transjugate
    |> almostEq a
    |> should equal true

[<Fact>]
let ``Conjugate transpose property B of square matrices`` () =
    sum (transjugate a) (transjugate b)
    |> almostEq (sum a b |> transjugate)
    |> should equal true

[<Fact>]
let ``Conjugate transpose property C of square matrices`` () =
    timesComplex (Complex.conjugate z) (transjugate a)
    |> almostEq (timesComplex z a |> transjugate)
    |> should equal true

[<Fact>]
let ``Conjugate transpose property D of square matrices`` () =
    standardProduct (transjugate a) (transjugate b)
    |> almostEq (standardProduct b a |> transjugate)
    |> should equal true

let m1 = Generator.nextPosInt random 10 ()
let n1 = Generator.nextPosInt random 10 ()
let m2 = Generator.nextPosInt random 10 ()
let n2 = Generator.nextPosInt random 10 ()
let a1 = Generator.nextMatrix random m1 n1 ()
let a2 = Generator.nextMatrix random m2 n2 ()
let a3 = Generator.nextMatrix random m1 n1 ()
let a4 = Generator.nextMatrix random m2 n2 ()

[<Fact>]
let ``Tensor product property A`` () =
    tensorProduct (transposed a1) (transposed a2)
    |> almostEq (tensorProduct a1 a2 |> transposed)
    |> should equal true

[<Fact>]
let ``Tensor product property B`` () =
    tensorProduct (transjugate a1) (transjugate a2)
    |> almostEq (tensorProduct a1 a2 |> transjugate)
    |> should equal true

[<Fact>]
let ``Tensor product property C`` () =
    let res1 = timesComplex z (tensorProduct a1 a2)
    let res2 = tensorProduct (timesComplex z a1) a2
    almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Tensor product property D`` () =
    let res1 = timesComplex z (tensorProduct a1 a2)
    let res2 = tensorProduct a1 (timesComplex z a2)
    almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Tensor product property E`` () =
    let res1 = tensorProduct (sum a1 a3) a2
    let res2 = sum (tensorProduct a1 a2) (tensorProduct a3 a2)
    almostEq res1 res2
    |> should equal true

[<Fact>]
let ``Tensor product property F`` () =
    let res1 = tensorProduct a1 (sum a2 a4)
    let res2 = sum (tensorProduct a1 a2) (tensorProduct a1 a4)
    almostEq res1 res2
    |> should equal true

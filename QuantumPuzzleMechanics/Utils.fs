module QuantumPuzzleMechanics.Utils

open FSharpx.Collections

let ofListOfLists<'a> (ls: 'a list list): 'a LazyList LazyList =
    ls
    |> LazyList.ofList
    |> LazyList.map LazyList.ofList

let ofSeqOfSeqs<'a> (sq: 'a seq seq): 'a LazyList LazyList =
    sq
    |> LazyList.ofSeq
    |> LazyList.map LazyList.ofSeq

let toListOfLists<'a> (lzls: 'a LazyList LazyList): 'a List List =
    lzls
    |> LazyList.map LazyList.toList
    |> LazyList.toList

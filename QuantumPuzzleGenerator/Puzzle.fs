module QuantumPuzzleGenerator.Puzzle

open QuantumPuzzleMechanics

open FSharpx.Collections

// types

type Puzzle =
    { Level: Level.Level
      SolvedPuzzlesInLevel: int
      InitQState: Matrix.Matrix
      TargetQState: Matrix.Matrix
      Gates: Quantum.Gate.Gate list
      GateSelection: bool list }
    
// constructor

let initPuzzle (levelIndex: int) (solvedPuzzlesInLevel: int): Puzzle =
    let level = List.item levelIndex Level.levels

    let (gatesRev, qStates) =
        Selection.selectGatesAndQStates
            Constants.random
            level.MaxGateType
            level.NumOfGates
            level.NumOfQubits
            level.IsAbsoluteInitQState
            Constants.differenceThreshold

    let (initQState, otherQStates) =
        qStates |> LazyList.rev |> LazyList.uncons

    let otherQStatesLength = LazyList.length otherQStates

    let randomQStatesIndex =
        Generator.nextInt Constants.random otherQStatesLength ()

    let targetQState = Seq.item randomQStatesIndex otherQStates

    let gates =
        gatesRev |> LazyList.rev |> LazyList.toList

    let gateSelection = List.replicate gates.Length false

    { Level = level
      SolvedPuzzlesInLevel = solvedPuzzlesInLevel
      InitQState = initQState
      TargetQState = targetQState
      Gates = gates
      GateSelection = gateSelection }

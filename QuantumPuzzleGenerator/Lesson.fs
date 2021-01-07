module QuantumPuzzleGenerator.Lesson

open QuantumPuzzleMechanics

// types

type Lesson =
    { LessonCategory: LessonCategory.LessonCategory
      QState: Matrix.Matrix
      GateSelection: bool list }

// constructor

let initLesson (categoryIndex: int): Lesson =
    let category =
        List.item categoryIndex LessonCategory.lessonCategories

    let qState =
        if category.IsInAbsoluteQState
        then Generator.nextAbsolutQState Constants.random category.NumOfQubits ()
        else Generator.nextQState Constants.random category.NumOfQubits ()

    let gateSelection =
        List.replicate category.Gates.Length false

    { LessonCategory = category
      QState = qState
      GateSelection = gateSelection }

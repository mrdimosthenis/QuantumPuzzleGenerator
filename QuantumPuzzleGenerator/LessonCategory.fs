module QuantumPuzzleGenerator.LessonCategory

open QuantumPuzzleMechanics

// types

type LessonCategory =
    { Index: int
      Title: string
      Description: string
      NumOfQubits: int
      Gates: Quantum.Gate.Gate list
      IsInAbsoluteQState: bool
      IsHueDisplayedOpt: bool option }

// constructor

let lessonCategory (index: int)
                   (title: string)
                   (description: string)
                   (numOfQubits: int)
                   (gates: Quantum.Gate.Gate list)
                   (isInAbsoluteQState: bool)
                   (isHueDisplayedOpt: bool option)
                   : LessonCategory =
    { Index = index
      Title = title
      Description = description
      NumOfQubits = numOfQubits
      Gates = gates
      IsInAbsoluteQState = isInAbsoluteQState
      IsHueDisplayedOpt = isHueDisplayedOpt }

// constants

let descriptions: string list =
    [ """A qubit is like a bit. When we measure it, it is either ZERO or ONE. The outcomes of a measured qubit are represented with black and white disks.
When the qubit is ZERO, a black disk is placed on the left and a white disk is placed on the right.
When the qubit is ONE, a black disk is placed on the right and a white disk is placed on the left."""
      """Before we measure a qubit, we don't know if it is going to be ZERO or ONE. Sometimes though, we know the probability of the outcomes. The outcomes of an unmeasured qubit are represented with gray disks.
When the qubit is more likely to be ZERO, a dark gray disk is placed on the left and a light gray disk is placed on the right.
When the qubit is more likely to be ONE, a dark gray disk is placed on the right and a light gray disk is placed on the left."""
      """For two qubits, we have four outcomes: ZER0-ZER0, ZERO-ONE, ONE-ZERO and ONE-ONE.
The disks on the left represent the outcomes of the first qubit to be ZERO.
The disks on the right represent the outcomes of the first qubit to be ONE.
The disks on the front represent the outcomes of the second qubit to be ZERO.
The disks on the back represent the outcomes of the second qubit to be ONE."""
      """For three qubits, we have eight outcomes: ZER0-ZER0-ZERO, ZER0-ZER0-ONE, ZER0-ONE-ZERO, ZER0-ONE-ONE, ONE-ZER0-ZERO, ONE-ZER0-ONE, ONE-ONE-ZERO and ONE-ONE-ONE.
The disks on the bottom represent the outcomes of the third qubit to be ZERO.
The disks on the top represent the outcomes of the third qubit to be ONE."""
      """We can change the probability of the outcomes by making a qubit pass through a gate. The ⊕ gate turns ONE into ZERO and vise versa. Click the gate bellow to see it happen!"""
      """When a qubit gets through the ⊕ gate, the disks switch places. Click the regenerate button and the gate symbol bellow to see it happen multiple times."""
      """For two qubits, when the first one gets through the ⊕ gate, the disks on the left switch places with the ones on the right. When the second qubit gets through the ⊕ gate, the disks on the front switch places with the ones at the back.
The qubits pass through the gates from left to right. Consequently, the order of the gate selection doesn't matter."""
      """For three qubits, when the third one gets through the ⊕ gate, the disks on the top switch places with the ones on the bottom."""
      """When a qubit gets through the Z gate, its relative phase changes. We don't need to know what the relative phase is to solve the puzzles. We can see that only the color of the right circle changes. In the colored clock bellow, when the Z gate is selected, the gray line points to the opposite direction."""
      """For two qubits, when the first one gets through the Z gate, the circles on the right change color. When the second qubit gets through the Z gate, the circles at the back change color."""
      """For three qubits, when the third one gets through the Z gate, the circles on the top change color."""
      """The Y gate turns ONE into ZERO and vise versa. It also changes the color of the circles."""
      """By comparing the colored clock instances we can find a pattern of how the color of the circles change. This process involves an angle bisector, two supplementary angles and the right-hand rule. Can you see it?"""
      """The H gate inverts the certainty of the outcomes. Black and white disks turn into gray."""
      """The more certain the outcomes are at the beginning, the more uncertain they become after the qubit gets through the H gate."""
      """The Swap gate exchanges the disks and the circles that are placed in a diagonal."""
      """Compared to a simple gate, the controlled one affects the half of the disks and circles.""" ]

let lessonCategories: LessonCategory list =
    // Title NumOfQubits Gates IsInAbsoluteQState IsHueDisplayedOpt
    [ ("Absolute Quantum State", 1, [], true, None)
      ("Random Quantum State", 1, [], false, None)
      ("Two Qubits", 2, [], false, None)
      ("Three Qubits", 3, [], false, None)

      ("Gate ⊕, Absolute State", 1, [ Quantum.Gate.XGate 0 ], true, None)
      ("Gate ⊕, Random State", 1, [ Quantum.Gate.XGate 0 ], false, None)
      ("Gate ⊕, Two Qubits",
       2,
       [ Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       None)
      ("Gate ⊕, Three Qubits",
       3,
       [ Quantum.Gate.XGate 2
         Quantum.Gate.XGate 1
         Quantum.Gate.XGate 0 ],
       false,
       None)

      ("Gate Z, One Qubit", 1, [ Quantum.Gate.ZGate 0 ], false, Some true)
      ("Gate Z, Two Qubits",
       2,
       [ Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       Some false)
      ("Gate Z, Three Qubits",
       3,
       [ Quantum.Gate.ZGate 2
         Quantum.Gate.ZGate 1
         Quantum.Gate.ZGate 0 ],
       false,
       Some false)

      ("Gate Y, Absolute State", 1, [ Quantum.Gate.YGate 0 ], true, None)
      ("Gate Y, Random State", 1, [ Quantum.Gate.YGate 0 ], false, Some true)

      ("Gate H, Absolute State", 1, [ Quantum.Gate.HGate 0 ], true, None)
      ("Gate H, Random State", 1, [ Quantum.Gate.HGate 0 ], false, Some true)

      ("Gate Swap, Two Qubits", 2, [ Quantum.Gate.SwapGate(1, 0) ], false, None)

      ("Controlled ⊕ Gate, Two Qubits",
       2,
       [ Quantum.Gate.CXGate(1, 0)
         Quantum.Gate.CXGate(0, 1) ],
       false,
       None) ]
    |> List.indexed
    |> List.map2 (fun description (i, (title, numOfQubits, gates, isInAbsoluteQState, isHueDisplayedOpt)) ->
        lessonCategory i title description numOfQubits gates isInAbsoluteQState isHueDisplayedOpt) descriptions

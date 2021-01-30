module internal QuantumPuzzleGenerator.Secrets

let internal secrets: Map<string, string> =
    [ ("AppCenterIOSSecret", "12345")
      ("AppCenterAndroidSecret", "12345") ]
    |> Map.ofList

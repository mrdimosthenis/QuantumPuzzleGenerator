module internal QuantumPuzzleGenerator.Secrets

let internal secrets: Map<string, string> =
    [ ("password", "12345")
      ("token", "12345")
      ("secret", "12345") ]
    |> Map.ofList

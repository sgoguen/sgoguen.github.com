open System.IO

// [snippet:Imperative F#]
let ReadFolder = Directory.GetFiles >> Array.map File.ReadAllText
// [/snippet]
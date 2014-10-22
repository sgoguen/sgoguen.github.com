open System.IO

let FileCount path = 
  printfn "Fetching files %s" path
  Directory.EnumerateFiles(path) |> Seq.length
let root = lazy FileCount(@"C:\")

// Executes the first time
root.Value

// Not the second time
root.Value
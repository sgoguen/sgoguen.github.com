open System.IO

// [snippet:Defining an Operator]
let (>>) f g = fun x -> g (f x)
// [/snippet]

// [snippet:Calls every item in the array with f]
module Array =
  let map f list =
    [| for item in list do
          yield f item |]
// [/snippet]
 
// [snippet:Our original ReadFolder function]
let ReadFolder = Directory.GetFiles >> Array.map File.ReadAllText
// [/snippet]
open System.IO

module Seq =
  let map f list =
    seq { 
      for item in list do
        yield f item
    }

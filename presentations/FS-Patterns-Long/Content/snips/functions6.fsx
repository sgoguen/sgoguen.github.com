open System.IO

module Seq =
  let map testFunction list =
    seq { 
      for item in list do
        if testFunction(item) = true then
          yield item
    }

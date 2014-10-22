open System
open System.Text

module Seq =
  let fold foldingFunction initialResult list =
    let mutable result = initialResult
    for item in list do
      result <- foldingFunction result item
    result

let add x y = x + y
let sum list = Seq.fold add 0 list

let SBCat (sb:StringBuilder) (s:string) = sb.Append(s)
let concat list = Seq.fold SBCat (new StringBuilder()) list
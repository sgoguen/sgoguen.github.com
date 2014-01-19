open System

type Option<'a> = None | Some of 'a

let AsInteger (s:string) =
  let (success, value) = Int32.TryParse(s)
  if success = true then Some(value) else None

let WontParse = "Hello" |> AsInteger
let WillParse = "1" |> AsInteger

let message = 
  match WillParse with
  | Some(x) -> "You entered: " + x.ToString()
  | None    -> "Enter something better"
type JSONValue =
| JSNumber of float
| JSString of string
| JSBool of bool

let (|Bool|_|) (str: string) =
   let mutable value = false
   if System.Boolean.TryParse(str, &value) then Some(value)
   else None

let (|Float|_|) (str: string) =
   let mutable floatvalue = 0.0
   if System.Double.TryParse(str, &floatvalue) then Some(floatvalue)
   else None

let parseJsonValue str =
   match str with
     | Bool(b) -> JSBool(b)
     | Float(f) -> JSNumber(f)
     | s -> JSString(s)

parseJsonValue "1.1"
parseJsonValue "false"
parseJsonValue "yes"
parseJsonValue "Hello World"
type JSONValue =
| JSNumber of float
| JSString of string
| JSBool of bool

let (|Bool|_|) str = match System.Boolean.TryParse(str) with
                     | (true, value) -> Some(value)
                     | _ -> None

let (|Double|_|) str = match System.Double.TryParse(str) with
                       | (true, value) -> Some(value)
                       | _ -> None

let parseJsonValue = function
  | Bool(b) -> JSBool(b)
  | Double(f) -> JSNumber(f)
  | s -> JSString(s)

assert (parseJsonValue "1.1" = JSNumber(1.1))
assert (parseJsonValue "false" = JSBool(false))
assert (parseJsonValue "Hello World" = JSString("Hello World"))
//  Let's say we wanted to create a really simple function that
//  simply printed HTML like structure.

//  Well, we could define HTML like this:

type HTML = 
    | Text of string
    | Tag of Name:string * Attributes:Map<string,string> * Body:HTML list

let makeTag name attributes body = 
    Tag(name, (attributes |> Map.ofList), body)

let div = makeTag "div"
let h1 = makeTag "h1"
let p = makeTag "p"

let example1 = 
    div [("class", "content")] [
        h1 [] [Text("My Web Page!")]
        p [] [Text("Welcome to Zombocom!")]
    ]

//  Can we implement a function that will print our HTML using tail-recursion?

let print = ()


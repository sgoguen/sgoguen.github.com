(*[omit:(implementation omitted)]*)

type Expr =
  | Add of Expr * Expr
  | Mul of Expr * Expr
  | Value of int
  | Variable of string

let ExprEx = Add(Mul(Value(5),Value(2)),Variable("x"))

let rec Eval e =
  match e with
  | Add(Value(x),Value(y)) -> Value(x + y)
  | Mul(Value(x),Value(y)) -> Value(x * y)
  | Add(x,y)               -> Add(Eval(x),Eval(y))
  | Mul(x,y)               -> Mul(Eval(x),Eval(y))
  | x                      -> x


let e1 = Add(Value(2),Value(3))
let e2 = Add(Value(2),Variable("x"))

let rec Print e =
  match e with
  | Add(x,y)     -> sprintf "(%s + %s)" (Print x) (Print y)
  | Mul(x,y)     -> sprintf "(%s * %s)" (Print x) (Print y)
  | Value(x)     -> sprintf "%i" x
  | Variable(x)  -> sprintf "%s" x

let PrintE1 = e1 |> Print
let PrintE2 = e2 |> Print

let rec Replace oldValue newValue expr =
  let doRec = Replace oldValue newValue
  match expr with
  | x when x = oldValue -> newValue
  | Add(x,y) -> Add((doRec x), (doRec y))
  | Mul(x,y) -> Mul((doRec x), (doRec y))
  | x -> x

let ReplaceEx1 = Add(Value(2),Variable("x")) |> Replace (Variable("x")) (Value(4))

let ReplaceEx2 = 
  Add(Variable("x"),Variable("x")) 
  |> Replace (Add(Variable("x"),Variable("x"))) 
             (Mul(Value(2), Variable("x")))


let SetVar varname value expr = Replace (Variable(varname)) (Value(value)) expr

let SerVarEx1 = Add(Value(2),Variable("x")) |> SetVar "x" 7 |> Eval






open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

exception QuotationError of Quotations.Expr

(*[/omit]*)

// [snippet:Converts F# code to our object hierarchy]
let rec FromQuotation (q:Quotations.Expr) = 
  match q with
  | Var(v) -> Variable(v.Name)
  | Value((o,t)) when t = typeof<int> -> Value(o :?> int)
  | Call(_,m,[x;y]) when m.Name = "op_Addition" -> Add( (FromQuotation x), (FromQuotation y) )
  | Call(_,m,[x;y]) when m.Name = "op_Multiply" -> Mul( (FromQuotation x), (FromQuotation y) )
  | Let(v, defined, inBlock) -> 
      let inBlock = inBlock |> FromQuotation
      let defined = (defined |> FromQuotation)
      inBlock |> Replace (Variable(v.Name)) defined
  | e -> 
    printfn "Quote: %A" e 
    raise (QuotationError(e))
// [/snippet]

// [snippet:An example]
let code = 
  <@ 
    let x = 5 * 3
    let y = 2 + 3
    x + y
  @> |> FromQuotation
// [/snippet]

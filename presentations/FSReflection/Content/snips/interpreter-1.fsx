open System

// Declare Our Class Hierarchy
type Expr =
  | Add of Expr * Expr
  | Mul of Expr * Expr
  | Value of int
  | Variable of string

// Create an Instance
let ExprEx = Add(Mul(Variable("x"),Value(2)),Value(3))

// Use Pattern Matching to Walk It - (Visitors on Steroids)
let rec Print e =
  match e with
  | Add(x,y)     -> sprintf "(%s + %s)" (Print x) (Print y)
  | Mul(x,y)     -> sprintf "(%s * %s)" (Print x) (Print y)
  | Value(x)     -> sprintf "%i" x
  | Variable(x)  -> sprintf "%s" x
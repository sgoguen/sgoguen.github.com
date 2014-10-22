open System

// Declare Our Class Hierarchy
type Expr =
  | Add of Expr * Expr
  | Mul of Expr * Expr
  | Value of int
  | Variable of string

// Pattern Matching is more than a switch statement
let rec Eval e =
  match e with
  | Add(Value(x),Value(y)) -> Value(x + y)
  | Mul(Value(x),Value(y)) -> Value(x * y)
  | Add(x,y)               -> Add(Eval(x),Eval(y))
  | Mul(x,y)               -> Mul(Eval(x),Eval(y))
  | x                      -> x
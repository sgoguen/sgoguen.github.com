//namespace Demo.Lambda

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

type var = string
type term = 
  | Lambda of Variable:var * Body:term
  | Var of Variable:var
  | App of Left:term * Right:term

type Fn = delegate of Fn -> Fn

module Lambda =
  let lambdaSymbol = System.Char.ConvertFromUtf32(0x000003BB)
  let rec ToString this = 
    match this with
    | Lambda(var, body) -> 
      sprintf "%s%s.%s" lambdaSymbol var (ToString body)
    | Var(var) -> var
    | App(left,right) -> 
      sprintf "(%s %s)" (ToString left) (ToString right)
  let rec fromExpr (e:Microsoft.FSharp.Quotations.Expr) =
    match e with
    | Patterns.Lambda(v, e) -> Lambda(v.Name, fromExpr e)
    | Patterns.Application(left, right) -> App(fromExpr left, fromExpr right)
    | Patterns.Var(var) -> Var(var.Name)
    | Patterns.Call(Some(left), methodInfo, [right]) when methodInfo.Name = "Invoke" -> 
      App(fromExpr left, fromExpr right)
    | e -> 
      let typeName = e.Type.ToString()
      raise (System.Exception(sprintf "Cannot convert %s to lambda" typeName))
  let rec substitute (varName:var) (withExpr:term) (expr:term) =
    let doSubRec = substitute varName withExpr 
    match expr with
    | Var(name) when varName = name -> withExpr
    | App(left, right) -> App(doSubRec left, doSubRec right)
    | Lambda(var, body) when var <> varName -> Lambda(var, doSubRec body)
    | x -> x
  let rec evalStep = function
    | App(Lambda(var, expr), right) -> substitute var right expr
    | App(left, right) -> App(evalStep left, evalStep right)
    | Lambda(var, body) -> Lambda(var, evalStep body)
    | x -> x
  let rec eval (term:term) =
    term |> Seq.unfold (fun previous -> 
        let next = evalStep previous
        if previous = next then None
        else Some(next, next)
    )
     

module BooleanLogic =
  let FsId = <@@ fun x -> x @@>
  let FsTrue = <@@ fun x y -> x @@>
  let FsFalse = <@@ fun x y -> y @@>
  let FsNot = <@@ fun p a b -> p b a @@>
  let FsAnd = <@@ fun (p:Fn) (q:Fn) -> p.Invoke(q).Invoke(p) @@>
  
  let Id = FsId |> Lambda.fromExpr
  let True = FsTrue |> Lambda.fromExpr
  let False = FsFalse |> Lambda.fromExpr
  let Not = FsNot |> Lambda.fromExpr
  let And = FsAnd |> Lambda.fromExpr
  
  let step1 = App(And, True) |> Lambda.evalStep
  let step2 = step1 |> Lambda.evalStep
  let step3 = step2 |> Lambda.evalStep
  let step4 = step3 |> Lambda.evalStep
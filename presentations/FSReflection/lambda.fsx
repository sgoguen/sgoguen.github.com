//namespace Demo.Lambda

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

type var = string
type term = 
  | Lambda of Variable:var * Body:term
  | Var of Variable:var
  | App of Left:term * Right:term

type Fn = delegate of Fn -> Fn

module Expression =
  let lambdaSymbol = System.Char.ConvertFromUtf32(0x000003BB)
  let rec ToString this = 
    match this with
    | Lambda(var, body) -> 
      sprintf "%s%s.%s" lambdaSymbol var (ToString body)
    | Var(var) -> var
    | App(left,right) -> 
      sprintf "(%s %s)" (ToString left) (ToString right)
  let rec toLambda (e:Microsoft.FSharp.Quotations.Expr) =
    match e with
    | Patterns.Lambda(v, e) -> Lambda(v.Name, toLambda e)
    | Patterns.Application(left, right) -> App(toLambda left, toLambda right)
    | Patterns.Var(var) -> Var(var.Name)
    | Patterns.Call(Some(left), methodInfo, [right]) when methodInfo.Name = "Invoke" -> 
      App(toLambda left, toLambda right)
    | e -> 
      let typeName = e.Type.ToString()
      raise (System.Exception(sprintf "Cannot convert %s to lambda" typeName))
  let rec substitute varName withExpr expr =
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

module BooleanLogic =
  let FsId = <@@ fun x -> x @@>
  let FsTrue = <@@ fun x y -> x @@>
  let FsFalse = <@@ fun x y -> y @@>
  let FsNot = <@@ fun p a b -> p b a @@>
  let FsAnd = <@@ fun (p:Fn) (q:Fn) -> p.Invoke(q).Invoke(p) @@>
  
  let Id = FsId |> Expression.toLambda
  let True = FsTrue |> Expression.toLambda
  let False = FsFalse |> Expression.toLambda
  let Not = FsNot |> Expression.toLambda
  let And = FsAnd |> Expression.toLambda
  
  let step1 = App(And, True) |> Expression.evalStep
  let step2 = step1 |> Expression.evalStep
  let step3 = step2 |> Expression.evalStep
  let step4 = step3 |> Expression.evalStep
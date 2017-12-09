module Expr = begin

    type Operator = Add | Sub | Mul | Div | Mod
    type Id = string

    type Expression = 
        | BinaryApp of Expression * Operator * Expression
        | FuncCall of string * Expression list
        | Assignment of Id * Expression
        | Identifier of Id
        | Number of float

    type Function = Function of Id * Id list * Expression

    type State(functions: Map<string, Function>, variables: Map<string, float>) as this = 
        let ret x = (this, x)
        member this.Assign(name, value) = 
            let newVariables = variables |> Map.add name value
            State(functions, newVariables)
        member this.Eval = function
            | Number(x) -> ret(x)
            | Identifier(id) -> 
                variables |> Map.tryFind id |> Option.get |> ret
            | Assignment(id, expr) ->
                let (newState, value) = this.Eval(expr)
                let newState = newState.Assign(id, value)
                (newState, value)
    //     // | FuncCall(fn, args)  -> 
    //     //     match Map.tryFind fn (state.Functions) with
    //     //     | Some(Function(f, parameters, body))

    //     //     Some(1.0)

end

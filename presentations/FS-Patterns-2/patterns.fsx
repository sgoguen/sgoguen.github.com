
module Factories =
  type IEmployee =
    abstract member Name: string
    abstract member GetCheckAmount: unit -> decimal

  let createSalaryEmployee(name,position) = 
    let yearlySalary = if position = "boss" then 300000.0m else 40000.0m
    { new IEmployee with
      member this.Name = name
      member this.GetCheckAmount() = yearlySalary / 52.0m }

  //  Let's try this out
  let employee = createSalaryEmployee
  let employees = [
    employee("Larry", "stooge") 
    employee("Curly", "stooge")
    employee("Moe", "boss")
  ]

module Singleton1 =
  type A private () =
      static let instance = A()
      static member Instance = instance
      member this.Action() = printfn "action"
  let DesignPattern1() = 
      let a = A.Instance;
      a.Action()

module FunctionalSingleton =
  let makeSingleton f =
    let instance = lazy f()
    fun () -> instance.Value

  type Folder =
    abstract member Name: string
    abstract member GetFiles: unit -> string list

  let getRoot() = 
      printfn "Creating root object"
      { new Folder with 
          member this.Name = @"C:\"
          member this.GetFiles() = [] }

  let root1 = getRoot()
  let root2 = getRoot()

  let getRoot2 = makeSingleton getRoot
  let root3 = getRoot2()
  let root4 = getRoot2()

module DecoratorPattern =
  type Coffee =
    abstract member getCost: unit -> double
    abstract member getIngredients: unit -> string list
  let SimpleCoffee = 
    { new Coffee with 
      member this.getCost() = 1.0
      member this.getIngredients() = ["Coffee"]
    }
  let WithMilk(coffee:Coffee) =
    { new Coffee with 
      member this.getCost() = coffee.getCost() + 0.5
      member this.getIngredients() = "milk" :: coffee.getIngredients() }
  let WithSyrup (syrup:string) (coffee:Coffee) =
    { new Coffee with 
      member this.getCost() = coffee.getCost() + 0.25
      member this.getIngredients() = syrup :: coffee.getIngredients() }

  let myCoffee = WithMilk(SimpleCoffee)
  let yourCoffee = SimpleCoffee |> WithMilk |> WithSyrup "pumpkin spice"
  yourCoffee.getCost()
  yourCoffee.getIngredients()

//module Multiton =
//  let instances = new Map<string,'a>()
//  let mulition<'a> (key:string) =
    

module CompositePattern =

  let makeComposite<'a> (constr:'a list -> 'a) = constr


  type Point = Point of X:int * Y:int

  type DrawingInstructions =
  | Line of Start:Point * Stop:Point
  | SetPenColor of Red:int * Green:int * Blue:int

  type Shape =
    abstract Draw: unit -> DrawingInstructions list
    abstract HitTest: Point -> bool

  let line (p1:Point) (p2:Point) = 
      { new Shape with
        member this.Draw() = [Line(p1, p2)]
        member this.HitTest(point) = true
      }    

  //  Here we're using a function to "scaffold" 
  let compositeShape (shapes:Shape list) = 
    { new Shape with 
      member this.Draw() = 
        [ for shape in shapes do 
            for instr in shape.Draw() do
              yield instr ]
      member this.HitTest(point) = 
        shapes |> List.exists(fun shape -> shape.HitTest(point))
      }



module ChainOfResposibility =
  let makeChain (chain:List<'a -> Option<'b>>) =
    fun a -> chain |> List.tryPick (fun f -> f a)
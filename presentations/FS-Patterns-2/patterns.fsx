
//module Multiton =
//  let instances = new Map<string,'a>()
//  let mulition<'a> (key:string) =
    

module CompositePattern =

  //  Is the essence of the composite pattern simply 
  //  implementing a function with this signature??
  let makeComposite<'a> (constr:'a list -> 'a) = constr


module CompositeShapeExample =
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
  let compositeShape = CompositePattern.makeComposite (fun (shapes:Shape list) ->
      { new Shape with 
        member this.Draw() = 
          [ for shape in shapes do 
              for instr in shape.Draw() do
                yield instr ]
        member this.HitTest(point) = 
          shapes |> List.exists(fun shape -> shape.HitTest(point))
        }
     )



module ChainOfResposibility =
  let makeChain (chain:List<'a -> Option<'b>>) =
    fun a -> chain |> List.tryPick (fun f -> f a)
﻿type Point = Point of int * int
type DrawingInstruction = Line of Point * Point

type Shape =
  abstract Draw: unit -> DrawingInstruction list
  abstract HitTest: Point -> bool
let line (p1:Point) (p2:Point) = 
    { new Shape with
      member this.Draw() = [Line(p1, p2)]
      member this.HitTest(point) = true
    }
let compositeShape (shapes:Shape list) = 
  { new Shape with 
    member this.Draw() = 
      [ for shape in shapes do 
          for instr in shape.Draw() do
            yield instr ]
    member this.HitTest(point) = 
      shapes |> List.exists(fun shape -> shape.HitTest(point)) }
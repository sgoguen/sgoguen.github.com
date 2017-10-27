//  1.  Start by solving your program as well defined data type
//  2.  Then try solving it with simple transformations
//  3.  Then try a conventional workflow
//  4.  If a conventional workflow becomes too complicated


(*
 - A restaurant serves meal orders to customers.
 - Meals are prepared by employees by using ingredients 
   available in the inventory.
 - A menu item has a name and a specific list of instructions that 
   needs to happen in order to prepare that menu item.

   1. An order for a regular hamburger, french fries and milkshake is place.
   2. The person behind the counter sends a message places an order at each station.
     - The person assembling the burger will need a patty from the burger station.
     - The person cooking fries
     - The person assembling the burger will begin to assemble a burger if he
       has enough patties available.
     - The grilling station will add a patty to the 
      

   Drink
      - Employee grabs a cup from cup dispenser - 2
      - Employee places cup in dispenser and pushes for size - 2
      - Machine fills cup for

   Hamburger
      - Fryers receive orders

    - Employees perform actions that consumes time
    - Dick McDonald logs
*)

open System

[<Measure>] type second

type Task = { Name: string; Station: string; Time: int; }

let waitFor(t:int<second>) = Async.Sleep(int(t) * 1000)

let log station msg = 
    printfn "%s - %s - %s" (DateTime.Now.ToLongTimeString()) station msg



module GrillingStation = begin

    type PattyOrder = Rare | Medium | WellDone
    type Patty = Patty of PattyOrder

    let log = log "Grilling Station"

    let getCookTime = function
        | Rare -> 5<second>
        | Medium -> 10<second>
        | WellDone -> 15<second>

    let cookPatty(order:PattyOrder) = 
        async {
            log <| sprintf "Started cooking %s patty" (order.ToString())
            do! waitFor( getCookTime order)
            let cookedPatty = Patty(order)
            log <| sprintf "Returning %O" (cookedPatty)
            return cookedPatty
        }

end

// module AssemblyStation = begin
//     let 
// end

Async.Start <| async {
    let! patty = GrillingStation.cookPatty(Rare)
    ()
}


type Bun = Sesame | NoSeed | NoBun

let AssembledBurger

let assembleBurger(patty:Patty, bun:Bun) = async {
    return "Burger"
}

let simulateTask (task:Task) employee = async {
    printfn "%s - %s - %s begins %s" (DateTime.Now.ToLongTimeString()) task.Station employee task.Name
    do! Async.Sleep(task.Time * 1000)
    printfn "%s - %s - %s completes %s" (DateTime.Now.ToLongTimeString()) task.Station employee task.Name
}

module BurgerStation = 
    let logEvent = Logs.log "Burger Station"
    let cookBurger() = async {
        logEvent "Begin cooking burger"
    }
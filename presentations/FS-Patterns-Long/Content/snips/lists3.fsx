open System

// A Record Type
type stooge = { name: string; salary: int }

// Let's define a list
let stooges = [
    { name="Moe"; salary=200000 }
    { name="Curly"; salary=40000 }
    { name="Larry"; salary=40000 }
  ]

let weeklySalary (s:stooge) = s.salary / 52
let printStooge (s:stooge) = 
  sprintf "Stooge: %s, Weekly Salary %i" s.name (weeklySalary s)

// Send result to list instead of printing it out
let ledger =
 [ for stooge in stooges do
     yield printStooge stooge]


let checkAmounts = stooges |> List.map weeklySalary 
let weeklyTotal = checkAmounts |> List.sum
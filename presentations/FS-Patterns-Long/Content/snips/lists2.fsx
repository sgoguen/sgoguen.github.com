open System

// A Record Type
type stooge = { name: string; salary: int }

// Let's define a list
let stooges = [
    { name="Moe"; salary=200000 }
    { name="Curly"; salary=40000 }
    { name="Larry"; salary=40000 }
  ]

// Loop through and print all the stooges
for stooge in stooges do
  let printStooge name salary = 
    let weeklySalary = salary / 52
    printfn "Stooge: %s, Weekly Salary %i" name weeklySalary
  printStooge stooge.name stooge.salary


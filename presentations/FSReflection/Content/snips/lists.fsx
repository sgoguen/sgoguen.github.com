open System

// A Record Type
type stooge = { name: string; salary: int }

// Let's define a list
let stooges = [
    { name="Moe"; salary=200000 }
    { name="Curly"; salary=40000 }
    { name="Larry"; salary=40000 }
  ]

// We can "deconstruct" lists
let (boss::knuckleheads) = stooges
let (Moe::Curly::Larry::[]) = stooges


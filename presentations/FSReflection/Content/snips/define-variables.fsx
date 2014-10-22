// [snippet:Defining Variables]
let aBooleanValue = true
let ``Can phrase be a variable name?`` = true
let greeting = "Hello World"
let answerToTheUniverse = 42
let pair = ("Joe", 21)
let (name,age) = pair
// [/snippet]

// [snippet:Variables are Read Only by Default]
let fsharpIsGreat = true    //  Everything is readonly by default
fsharpIsGreat = false     //  This doesn't change the variable
// [/snippet]

// [snippet:Read/Write Variables]
let mutable Weather = "Very Rainy"  //  Read/Write
Weather <- "Dry"
// [/snippet]
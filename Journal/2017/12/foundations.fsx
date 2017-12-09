module BooleanFuncs = begin
    //  This inline function accepts two parameters
    //  and returns a function that returns one of those
    //  two parameters.
    let ifXThen isTrue isFalse = 
        fun x -> if x then isTrue else isFalse

    //  We can use it to make the 4 basic boolean functions
    let constF = ifXThen false false
    let not    = ifXThen false true
    let id     = ifXThen true false
    let constT = ifXThen true true

    // //  Using our 4 basic boolean functions, we can create
    // //  all 16 boolean binary functions
    // let constF2 = ifXThen constF constF
    // let _1 = ifXThen constF not
    // let _1 = ifXThen constF id
    // let _3 = ifXThen constF constT
    // let _4 = ifXThen not constF
    // let _4 = ifXThen not not
    // let _4 = ifXThen not id
    // let _4 = ifXThen not constT
    // let projX = ifXThen id constF
    // let _4 = ifXThen id not
    // let _4 = ifXThen id id
    // let _4 = ifXThen id constT
    // let _4 = ifXThen constT constF
    // let _4 = ifXThen constT not
    // let _4 = ifXThen constT id
    // let _4 = ifXThen constT constT
end

module Terniary = begin

    type tern = Neg | Zero | Pos

    let makeFn isNeg isZero isPos = function
        | Neg -> isNeg
        | Zero -> isZero
        | Pos -> isPos

       

    let allFuncs = [
        "constNeg", makeFn Neg Neg Neg
        "dip", makeFn Neg Neg Zero
        "dipZero", makeFn Neg Neg Pos
        "pullPos", makeFn Neg Zero Neg
        "flattenPos", makeFn Neg Zero Zero
        "id", makeFn Neg Zero Pos
        "", makeFn Neg Pos Neg
        "", makeFn Neg Pos Zero
        "", makeFn Neg Pos Pos
        "", makeFn Zero Neg Neg
        "", makeFn Zero Neg Zero
        "", makeFn Zero Neg Pos
        "", makeFn Zero Zero Neg
        "", makeFn Zero Zero Zero
        "", makeFn Zero Zero Pos
        "", makeFn Zero Pos Neg
        "", makeFn Zero Pos Zero
        "", makeFn Zero Pos Pos
        "", makeFn Pos Neg Neg
        "", makeFn Pos Neg Zero
        "", makeFn Pos Neg Pos
        "inv", makeFn Pos Zero Neg
        "", makeFn Pos Zero Zero
        "", makeFn Pos Zero Pos
        "", makeFn Pos Pos Neg
        "", makeFn Pos Pos Zero
        "", makeFn Pos Pos Pos
    ]

end
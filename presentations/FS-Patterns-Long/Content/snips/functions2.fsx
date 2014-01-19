open System

// [snippet:These are not the same function]
let addUsingCurrying x y = x + y
let addUsingTuples(x,y) = x + y
// [/snippet]

// [snippet:They are called differently]
let x = addUsingCurrying(3)(5)
let y = addUsingCurrying 3 5
let z = addUsingTuples(3,5)  //  x should be 8
// [/snippet]
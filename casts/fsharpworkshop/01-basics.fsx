let rec sumList acc = function
    | x::rest -> sumList (acc + x) rest 
    | [] -> acc

sumList 0 [1;2;3;4;5]
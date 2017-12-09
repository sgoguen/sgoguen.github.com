//  Let's do this thing in F# by creating a script.

open System.IO

let findInFiles text path = [
        for file in Directory.EnumerateFiles(path) do
            for line in File.ReadAllLines(file) do
                if line.Contains(text) then yield (file, line)
    ]

findInFiles "TODO" "./"
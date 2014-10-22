open System.IO

//  We can create lazy sequences easily
let ReadLines filename =
  seq { use reader = File.OpenText(filename)
        while reader.EndOfStream = false do
          yield reader.ReadLine() }



  
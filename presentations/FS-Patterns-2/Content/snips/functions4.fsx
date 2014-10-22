open System.IO

// [snippet:Now with Nested Functions and Pipelining]
let FindFilesBiggerThan minimumSize root =
  let getFileSize filename = System.IO.FileInfo(filename).Length
  let files = Directory.EnumerateFiles(root, "*.*", SearchOption.AllDirectories)
  files |> Seq.filter (fun f -> getFileSize(f) >= minimumSize)
// [/snippet]

// [snippet:An Example]
let GetBigFiles = FindFilesBiggerThan 1000000L
GetBigFiles(@"C:\Projects")
// [/snippet]

// [snippet:The Pipeline operator defined]
let (|>) x f = f(x)
// [/snippet]
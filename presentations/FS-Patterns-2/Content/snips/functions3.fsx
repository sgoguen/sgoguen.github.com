﻿open System.IO

let FindFilesBiggerThan minimumSize root =
  let files = Directory.EnumerateFiles(root, "*.*", SearchOption.AllDirectories)
  seq { 
    for filename in files do
      let info = System.IO.FileInfo(filename)
      if info.Length >= minimumSize then
        yield filename
  }

let GetBigFiles = FindFilesBiggerThan 1000000L
GetBigFiles(@"C:\Projects")

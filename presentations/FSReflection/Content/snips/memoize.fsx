open System.IO
open System.Collections.Generic

// [snippet:Caches any function]
let memoize (f:'a -> 'b) =
  let dict = new Dictionary<'a,'b>()
  fun x ->
    match dict.TryGetValue(x) with
    | (true, value) -> value
    | (false, _) ->
        let result = f(x)
        dict.Add(x,result)
        result
// [/snippet]

// [snippet:Our uncached function]
let GetFileCount root = root |> Directory.EnumerateFiles |> Seq.length
// [/snippet]
// [snippet:Let's cache it]
let GetFileCountCached = GetFileCount |> memoize
// [/snippet]

// [snippet:Subsequent calls will read from the dictionary]
GetFileCountCached(@"C:\Projects\Websites\")
// [/snippet]
open System.Net
open System.Text.RegularExpressions

let extractLinks s =
  let pattern = "http://[a-z-A-Z0-9./_]*"
  Regex.Matches(s, pattern) 
  |> Seq.cast<Match>
  |> Seq.map (fun m -> m.Value)


let fetchLinks(name, url:string) =
  async { 
        try
          let uri = new System.Uri(url)
          let webClient = new WebClient()
          let! html = webClient.AsyncDownloadString(uri)
          printfn "Read %d characters for %s" html.Length name
          return extractLinks html
        with
        | ex -> return Seq.empty<string>
  }

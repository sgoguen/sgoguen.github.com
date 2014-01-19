open System.Net
open System.Text.RegularExpressions
open Microsoft.FSharp.Control.WebExtensions

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

let urlList = [ 
                "Microsoft.com", "http://www.microsoft.com/"
                "MSDN", "http://msdn.microsoft.com/"
                "Bing", "http://www.bing.com"
              ]
              
let pages = urlList |> Seq.map fetchLinks |> Async.Parallel |> Async.RunSynchronously
let links = [for links in pages do
               for link in links do
                 yield link]

open System.Windows.Forms

let SendToWindow (list:seq<'a>) =
  let f = new Form(Text = "Results")
  let source = list |> Seq.toArray :> obj
  let g = new DataGrid(Name = "Grid",Dock = DockStyle.Fill, DataSource = source)
  f.Controls.Add(g)
  f.Show()
  f

links |> SendToWindow
# Html Type Providers

Html type providers are a fun way to extract information from a semi-structured website.  Let's take this page for example.

Let's say we wanted to provide some simple test data for a test application.  We could create a users table:

## Users

First Name | Last Name | Title
-----------|-----------|-------
Steve      | Goguen    | Software Developer
Larry      | David     | Comedian

We could then load the data with a simple F# script and run in it LINQPad

```fsharp
open FSharp.Data

[<Literal>]
let sourceUrl = "http://sgoguen.github.io/Journal/2017/01/html-type-providers.html"
type TestData = HtmlProvider<sourceUrl>

module Users = begin
  type User = { FirstName:string; LastName: string; Title:string }

  let fetch() = 
    let rows = TestData.Load(sourceUrl).Tables.Users.Rows
    [ for r in rows -> { FirstName = r.``First Name``; LastName = r.``Last Name``; Title = r.Title } ]
    
end

Users.fetch().Dump()
```

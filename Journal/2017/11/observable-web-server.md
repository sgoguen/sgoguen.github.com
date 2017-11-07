# An Observable Web Server

I've been playing around with an idea recently that 

## Step 1. Abstracting a Regular Web Server

At its core, HTTP can be viewed as a relatively simple protocol.  If we were to approximate it, we might say the HTTP is a Request/Response protocol that could be abstract with this notation:

```fsharp
type HttpServer = HttpRequest -> HttpResponse
```

Here we're simply we can abstract an HttpServer as a function that simply takes a request object as its input parameter and returns an HttpResponse as its output.

We might define the request and response types like this:

```fsharp
type Method = GET | POST

type Header = {
    Name: string
    Value: string
}

type HttpRequest = {
    Method: Method
    Headers: Header list
    MessageBody: string
}

type HttpResponse = {
    Headers: Header list
    MessageBody: string
}
```


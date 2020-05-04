# Giraffe.ViewEngine
Giraffe's functional view engine http handlers for ~~Giraffe~~ web applications.

The ViewEngine is useful in 'it's own right' and has no dependency on the Giraffe framework.

Splitting the ViewEngine from Giraffe means that the Giraffe ViewEngine can be used *anywhere*

## About
The Giraffe.ViewEngine is an extremely light weight FSharp DSL (Domain Specific Language) for building HTML.

The Giraffe.ViewEngine is centered around the following types:
``` FSharp
type XmlAttribute =
    | KeyValue of string * string
    | Boolean  of string

type XmlElement   = string * XmlAttribute[]    // Name * XML attributes

type XmlNode =
    | ParentNode  of XmlElement * XmlNode list // An XML element which contains nested XML elements
    | VoidElement of XmlElement                // An XML element which cannot contain nested XML (e.g. <hr /> or <br />)
    | Text        of string                    // Text content
```

The DSL has nodes of all of the standard html tags, head, body, h1, etc.

## Usage
Build your HTML and render! The following is attached as a standalone project:

``` FSharp
module GithubExample = 
  open Giraffe.GiraffeViewEngine
  
  let bodyTemplate (nameList : string list) : XmlNode = 
    body [] [
      h1 [] [Text "Welcome:"]
      ol [] 
        (nameList |> List.map (fun x -> li [] [Text x]))
    ]

  let navTemplate = 
    nav [] [
      a [_href "./About"] [Text "About"]
    ]

  let documentTemplate (nav : XmlNode) (body : XmlNode ) = 
    html [] [
      nav
      body
      ]

  let render welcomeUsers = 
    bodyTemplate welcomeUsers
    |> (documentTemplate navTemplate)
    |> Giraffe.GiraffeViewEngine.renderHtmlDocument

[<EntryPoint>]
let main args = 
  let tfn = 
    (System.IO.Path.GetTempFileName())
    |> sprintf "%s.html" 

  args 
  |> Seq.toList
  |> GithubExample.render
  |> fun x -> System.IO.File.WriteAllText(tfn, x)
  |> ignore 

  let process = new System.Diagnostics.Process()
  process.StartInfo.FileName <- tfn
  process.StartInfo.UseShellExecute <- true
  process.Start() |> ignore

  0
```

The above code yields the following:
![alt text][GithubExample]

[GithubExample]: ./GithubExample.jpg "Example for Github"

## Attribution to original authors of this code
This code has been originally ported from Suave with small modifications afterwards.

The original code has been authored by
* Henrik Feldt (https://github.com/haf)
* Ademar Gonzalez (https://github.com/ademar)

You can find the original implementation here:
https://github.com/SuaveIO/suave/blob/master/src/Experimental/Html.fs

Thanks to Suave (https://github.com/SuaveIO/suave) for letting us borrow their code
and thanks to Florian Verdonck (https://github.com/nojaf) for porting it to Giraffe.

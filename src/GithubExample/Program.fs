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
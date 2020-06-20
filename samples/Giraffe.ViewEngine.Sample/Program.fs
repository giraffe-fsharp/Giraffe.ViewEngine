module Sample =
    open Giraffe.ViewEngine

    let bodyTemplate (nameList: string list): XmlNode =
        body []
            [ h1 [] [ Text "Welcome:" ]
              ol [] (nameList |> List.map (fun x -> li [] [ Text x ])) ]

    let navTemplate =
        nav [] [ a [ _href "./About" ] [ Text "About" ] ]

    let documentTemplate (nav: XmlNode) (body: XmlNode) =
        html [] [ nav; body ]

    let render welcomeUsers =
        bodyTemplate welcomeUsers
        |> (documentTemplate navTemplate)
        |> Giraffe.ViewEngine.renderHtmlDocument

[<EntryPoint>]
let main args =
    let tfn =
        (System.IO.Path.GetTempFileName()) |> sprintf "%s.html"

    args
    |> Seq.toList
    |> Sample.render
    |> fun x -> System.IO.File.WriteAllText(tfn, x)
    |> ignore

    let p = new System.Diagnostics.Process()
    p.StartInfo.FileName <- tfn
    p.StartInfo.UseShellExecute <- true
    p.Start() |> ignore

    0

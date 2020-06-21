module Giraffe.ViewEngine.Tests

open System
open Xunit
open Giraffe.ViewEngine

let removeNewLines (html : string) : string =
    html.Replace(Environment.NewLine, String.Empty)

[<Fact>]
let ``Single html root should compile`` () =
    let doc  = html [] []
    let html =
        doc
        |> RenderView.AsString.htmlDocument
        |> removeNewLines
    Assert.Equal("<!DOCTYPE html><html></html>", html)

[<Fact>]
let ``Anchor should contain href, target and content`` () =
    let anchor =
        a [ attr "href" "http://example.org";  attr "target" "_blank" ] [ str "Example" ]
    let html = RenderView.AsString.xmlNode anchor
    Assert.Equal("<a href=\"http://example.org\" target=\"_blank\">Example</a>", html)

[<Fact>]
let ``Script should contain src, lang and async`` () =
    let scriptFile =
        script [ attr "src" "http://example.org/example.js";  attr "lang" "javascript"; flag "async" ] []
    let html = RenderView.AsString.xmlNode scriptFile
    Assert.Equal("<script src=\"http://example.org/example.js\" lang=\"javascript\" async></script>", html)

[<Fact>]
let ``Nested content should render correctly`` () =
    let nested =
        div [] [
            comment "this is a test"
            h1 [] [ str "Header" ]
            p [] [
                rawText "Lorem "
                strong [] [ str "Ipsum" ]
                str " dollar"
        ] ]
    let html =
        nested
        |> RenderView.AsString.xmlNode
        |> removeNewLines
    Assert.Equal("<div><!-- this is a test --><h1>Header</h1><p>Lorem <strong>Ipsum</strong> dollar</p></div>", html)

[<Fact>]
let ``Void tag in XML should be self closing tag`` () =
    let unary =  br [] |> RenderView.AsString.xmlNode
    Assert.Equal("<br />", unary)

[<Fact>]
let ``Void tag in HTML should be unary tag`` () =
    let unary =  br [] |> RenderView.AsString.htmlNode
    Assert.Equal("<br>", unary)

[<AutoOpen>]
module Giraffe.Tests.Helpers
open System

let removeNewLines (html : string) : string =
    html.Replace(Environment.NewLine, String.Empty)
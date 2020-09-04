Release Notes
=============

## 1.2.0

- Added missing `iframe` element ([#9](https://github.com/giraffe-fsharp/Giraffe.ViewEngine/issues/9))

## 1.1.1

- Fixed the `strf` function (see [#6](https://github.com/giraffe-fsharp/Giraffe.ViewEngine/issues/6))

## 1.1.0

- Added `strf` which is a shortcut for the commonly used `sprintf fmt |> encodedText` function.
- Added a few missing HTML elements and attributes:
    - Elements: `picture`
    - Attributes: `_color`, `_property`, `_srcset`

## 1.0.0

Original port from Giraffe with additional improvements/changes:

- Zero dependency library
    - One can create a separate project for Giraffe views in their solution (`.sln`) with zero dependencies. This significantly speeds up compilation time and therefore improves an active development experience via `dotnet watch`.
- Dropped support for all target frameworks except `netcoreapp3.1` in preparation for the upcoming .NET 5 release
- Renamed the namespace from `GiraffeViewEngine` to `Giraffe.ViewEngine`
- Made the `ViewBuilder` module private and exposed public render functions via three new modules:
    - `RenderView.IntoStringBuilder` replaces the old `ViewBuilder` functions
    - `RenderView.AsString` replaces all old `render...` functions
    - `RenderView.AsBytes` new module to efficiently output views as byte arrays
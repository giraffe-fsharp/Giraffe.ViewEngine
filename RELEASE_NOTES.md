Release Notes
=============

## 2.0.0-alpha-1

- Updated to .NET 6 and Giraffe 6.0.0-alpha-*
- Improved performance by removing redundant `ToArray` functions and making `XmlElement` a struct

## 1.4.0

- Added `slot` and `template` elements
- Added .NET Standard 2.0 support for full framework support

## 1.3.0

Upgraded to `net5.0` target framework.

`Giraffe.ViewEngine` version `1.2.0` and version `1.3.0` are identical in functionality. The only difference is that `1.2.0` targets `netcoreapp3.1` and `1.3.0` targets `net5.0`. If you cannot upgrade your .NET Core project to `net5.0` yet then stay on version `1.2.0`.

New features and bug fixes will continue off the `1.3.0` version and therefore only target `net5.0` going forward. The upgrade path from `netcoreapp3.1` to `net5.0` is so minimal that it is a reasonable expectation and not worth the effort to support both target frameworks.

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
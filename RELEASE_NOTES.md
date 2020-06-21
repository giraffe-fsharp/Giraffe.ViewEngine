Release Notes
=============

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
namespace Giraffe.ViewEngine

open System
open System.Text

module internal PoolLimits =
    let internal MinimumCapacity = 5000
    let internal MaximumCapacity = 40000
    let internal MaximumLifetime = TimeSpan.FromMinutes 10.0

type public StringBuilderPool =
    [<DefaultValue(true); ThreadStatic>]
    static val mutable private isEnabled : bool

    [<DefaultValue; ThreadStatic>]
    static val mutable private instance : StringBuilder

    [<DefaultValue; ThreadStatic>]
    static val mutable private created : DateTimeOffset

    static member public IsEnabled
        with get ()   = StringBuilderPool.isEnabled
        and  set flag = StringBuilderPool.isEnabled <- flag

    static member internal Rent () =
        match StringBuilderPool.IsEnabled with
        | false -> new StringBuilder(PoolLimits.MinimumCapacity)
        | true  ->
            let lifetime = DateTimeOffset.Now - StringBuilderPool.created
            let expired  = lifetime > PoolLimits.MaximumLifetime
            let sb       = StringBuilderPool.instance
            if not expired && sb <> null then
                StringBuilderPool.instance <- null
                sb.Clear()
            else new StringBuilder(PoolLimits.MinimumCapacity)

    static member internal Release (sb : StringBuilder) =
        if sb.Capacity <= PoolLimits.MaximumCapacity then
            StringBuilderPool.instance <- sb
            StringBuilderPool.created  <- DateTimeOffset.Now
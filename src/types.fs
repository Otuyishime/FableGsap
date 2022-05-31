namespace FableGsap

open Fable.Core

type ITarget = interface end
type IPosition = interface end
type IVar = interface end
type IVars = interface end

type ITween = interface end
type ITimeline = interface end

// gsap config types
type IConfig = interface end
type IConfigs = interface end
type IUnit = interface end

// gsap custom effects
type IEffect = interface end
type IEffectProp = interface end

[<StringEnum; RequireQualifiedAccess>]
type callbackType = 
    | OnComplete
    | OnInterrupt
    | OnRepeat
    | OnReverseComplete
    | OnStart
    | OnUpdate

[<StringEnum; RequireQualifiedAccess>]
type _unit = 
    | [<CompiledName("ch")>] Ch
    | [<CompiledName("deg")>] Deg
    | [<CompiledName("%")>] Percentage
    | [<CompiledName("px")>] Px
    | [<CompiledName("rad")>] Rad
    | [<CompiledName("em")>] Em
    | [<CompiledName("ex")>] Ex
    | [<CompiledName("vw")>] Vw
    | [<CompiledName("vh")>] Vh
    | [<CompiledName("vmin")>] Vmin
    | [<CompiledName("vmax")>] Vmax
    | [<CompiledName("rem")>] Rem

/// <summary>
/// Tweens and/or timelines nested in a timeline.
/// </summary>
type TimelineChildren = 
    /// <summary>
    /// Tweens nested in a timeline.
    /// </summary>
    | Tweens of ITween array

    /// <summary>
    /// Timelines nested in a timeline.
    /// </summary>
    | Timelines of ITimeline array

    /// <summary>
    /// Tweens and Timelines nested in a timeline.
    /// </summary>
    /// <param name='tweens'>Nested Tweens</param>
    /// <param name='timelines'>Nested Timelines</param>
    | TweensAndTimelines of tweens: ITween array * timelines: ITimeline array

    /// <summary>
    /// No nested children
    /// </summary>
    | Empty

/// <summary>
/// Gsap children.
/// </summary>
type GsapChildren = 
    /// <summary>
    /// Gsap Tween
    /// </summary>
    | Tween of ITween

    /// <summary>
    /// Gsap Timeline.
    /// </summary>
    | Timeline of ITimeline

    /// <summary>
    /// No Child
    /// </summary>
    | Undefined

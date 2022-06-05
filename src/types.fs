namespace FableGsap

open Fable.Core

// gsap animation props
type ITarget = interface end
type IPosition = interface end
type IEase = interface end
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

type TimelineChildren = 
    | Tweens of ITween array
    | Timelines of ITimeline array
    | TweensAndTimelines of tweens: ITween array * timelines: ITimeline array
    | Empty

type GsapChildren = 
    | Tween of ITween
    | Timeline of ITimeline
    | Undefined

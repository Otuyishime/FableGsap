namespace FableGsap

open Fable.Core
open Fable.Core.JsInterop

[<RequireQualifiedAccessAttribute>]
module internal Interop =
    let gsapApi: IGsapApi = importDefault "gsap"
    let IsNullOrUndefined = isNullOrUndefined
    let IsFunction f = (jsTypeof f = "function")
    let IsTypeOf name t = (jsTypeof t = name)
    let IsTween t = IsTypeOf "Tween" t
    let IsTimeline t = IsTypeOf "Timeline" t

    [<Emit("$0[$1]($2)")>]
    let createTimelineUseEffect (timeline: ITimeline) (effectName) (target: ITarget): ITimeline = jsNative

    [<Emit("$0[$1]($2, { duration: 2})")>]
    let createTimelineUseEffectWithOverrides (timeline: ITimeline) (effectName: string) (target: ITarget) (overrides: IVars): ITimeline = jsNative

    [<Emit("$0[$1]([$2])")>]
    let createUseEffect (gsap:obj, effectName: string, target: ITarget): unit = jsNative

    let makeObjectFromList vars = (unbox (createObj !!vars))
    let makeAnimationTarget (target: 'T): ITarget = unbox<ITarget> target
    let makeAnimationVar (key: string) (value: 'T): IVar = unbox<IVar> (key, value)
    let makeAnimationVars (animationVars: IVar seq): IVars = unbox<IVars> (createObj !!animationVars)
    let makeAnimationPosition (position: 'T): IPosition = unbox<IPosition> position

    let makeConfigVars (configs: IConfig seq): IConfigs = unbox<IConfigs> (createObj !!configs)
    let makeConfigVar (key: string) (value: 'T): IConfig = unbox<IConfig> (key, value)
    let makeConfigUnit (key: string) (value: 'T): IUnit = unbox<IUnit> (key, value)
    
    let makeEffectProp (key: string) (value: 'T): IEffectProp = unbox<IEffectProp> (key, value)
    let makeEffect (effectProps: IEffectProp seq): IEffect = unbox<IEffect> (createObj !!effectProps)

    [<Emit("$0[$1]")>]
    let makeGetConfigValue (config: obj) (name: string): 'T = jsNative

namespace FableGsap

open Fable.Core
open Fable.Core.JsInterop

[<RequireQualifiedAccessAttribute>]
module Interop =
    let gsapApi: IGsapApi = importDefault "gsap"
    let IsFunction f = (jsTypeof f = "function")
    let IsTween t = (jsTypeof t) = "Tween"
    let IsTimeline t = (jsTypeof t) = "Timeline"

    [<Emit "undefined">]
    let undefined : obj = jsNative

    [<Emit("$0[$1]($2)")>]
    let createTimelineUseEffect (timeline: ITimeline) (effectName) (target: ITarget): ITimeline = jsNative

    [<Emit("$0[$1]($2, { duration: 2})")>]
    let createTimelineUseEffectWithOverrides (timeline: ITimeline) (effectName: string) (target: ITarget) (overrides: IVars): ITimeline = jsNative

    [<Emit("$0[$1]([$2])")>]
    let createUseEffect (gsap:obj, effectName: string, target: ITarget): unit = jsNative

    let inline makeObjectFromList vars = (unbox (createObj !!vars))
    let inline makeAnimationTarget (target: 'T): ITarget = unbox<ITarget> target
    let inline makeAnimationVar (key: string) (value: 'T): IVar = unbox<IVar> (key, value)
    let inline makeAnimationVars (animationVars: IVar seq): IVars = unbox<IVars> (createObj !!animationVars)
    let inline makeAnimationPosition (position: 'T): IPosition = unbox<IPosition> position

    let inline makeConfigVars (configs: IConfig seq): IConfigs = unbox<IConfigs> (createObj !!configs)
    let inline makeConfigVar (key: string) (value: 'T): IConfig = unbox<IConfig> (key, value)
    let inline makeConfigUnit (key: string) (value: 'T): IUnit = unbox<IUnit> (key, value)
    
    let inline makeEffectProp (key: string) (value: 'T): IEffectProp = unbox<IEffectProp> (key, value)
    let inline makeEffect (effectProps: IEffectProp seq): IEffect = unbox<IEffect> (createObj !!effectProps)

    [<Emit("$0[$1]")>]
    let makeGetConfigValue (config: obj) (name: string): 'T = jsNative

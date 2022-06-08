namespace FableGsap
open Fable.Core

type IConfig= 
    abstract duration: int with get

type internal ITicker = 
    abstract add: callBack: (unit -> unit) -> unit
    abstract deltaRatio: fps: float -> unit
    abstract fps: value: float -> unit
    abstract lagSmoothing: threshold:float * adjustedLag:float -> unit
    abstract remove: callBack: (unit -> unit) -> unit
    abstract sleep: unit -> unit
    abstract tick: unit -> unit
    abstract wake: unit -> unit
    abstract frame: float
    abstract time: float

type internal IUtils = 
    abstract checkPrefix: property:string * element:'E * preferPrefix:bool -> unit
    abstract clamp: minimum:float -> maximum:float -> valueToClamp:float -> unit
    abstract getUnit: value:string -> string
    abstract snap:  snapTo: float -> value: float -> float

type internal IGlobalTimeline = 
    abstract pause: unit -> ITimeline
    abstract pause: atTime:float * suppressEvents:bool -> ITimeline
    abstract play: unit -> ITimeline
    abstract play: atTime:float * suppressEvents:bool -> ITimeline
    abstract paused: unit -> bool
    abstract paused: value:bool -> ITimeline
    abstract timeScale: unit -> float
    abstract timeScale: value:float -> ITimeline

type internal IGsapApi = 
    abstract from: target: ITarget * vars: 'V -> ITween
    abstract fromTo: target: ITarget * fromVars: 'V * toVars: 'V -> ITween
    [<Emit("$0.to($1, $2)")>]
    abstract to_: target: ITarget * vars: 'V -> ITween
    [<Emit("$0.to($1, $2, $3)")>]
    abstract to_: target: ITarget * vars: 'V * position: 'Y -> ITween
    abstract timeline: vars: 'V -> ITimeline
    abstract config: configs: IConfigs -> unit
    abstract defaults: vars: 'V -> unit
    abstract delayedCall: seconds: int * callBack: (unit -> unit) -> ITween
    abstract exportRoot: unit -> ITimeline
    abstract getById: id: string -> 'T
    
    abstract getProperty: target: ITarget * property: string -> 'T
    abstract getProperty: target: ITarget * property: string * cssUnit:_unit -> 'T

    abstract getTweensOf: tartget: ITarget -> ITween array
    abstract isTweening: tartget: ITarget -> bool

    abstract killTweensOf: tartget: 'T -> unit
    abstract killTweensOf: tartget: 'T * properties: string -> unit
    
    abstract parseEase: easeStr: string -> (int -> IVar)
    abstract set: target: ITarget * vars: 'V -> ITween
    abstract updateRoot: seconds: int -> unit

    abstract registerEffect: effect: 'E -> unit
    abstract effects: obj

    abstract ticker: ITicker
    abstract utils: IUtils
    abstract globalTimeline: IGlobalTimeline
    abstract version: string
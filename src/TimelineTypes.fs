namespace FableGsap
open Fable.Core

type internal ITimelineAPI = 
    abstract add: child:'T * position:'A -> ITimeline
    abstract addLabel: label:string * position:'A -> ITimeline
    
    abstract addPause: position:'A -> ITimeline
    abstract addPause: position:'A * callback:(unit -> unit) -> ITimeline

    abstract call: callback:(unit -> unit) * position:'A -> ITimeline
    abstract clear: labels:bool -> ITimeline

    abstract currentLabel: value:string -> ITimeline
    abstract currentLabel: value:unit -> string

    abstract delay: value:int -> ITimeline
    abstract delay: value:unit -> int

    abstract duration: value:int -> ITimeline
    abstract duration: value:unit -> int

    abstract endTime: includeRepeats: bool -> ITimeline

    abstract eventCallback: callbackType:string * callbackFunction: (unit -> unit) option -> ITimeline
    abstract eventCallback: callbackType:string -> (unit -> unit)

    abstract getById: id: string -> 'T

    abstract getChildren: nested:bool * tweens:bool * timelines:bool -> 'T array
    abstract getChildren: nested:bool * tweens:bool * timelines:bool * ignoreBeforeTime:float -> 'T array

    abstract getTweensOf: target:'T * nested:bool -> ITween array

    abstract invalidate: unit -> ITimeline
    abstract isActive: unit -> ITimeline

    abstract iteration: unit -> int
    abstract iteration: number:int -> ITimeline

    abstract kill: unit -> ITimeline

    abstract nextLabel: unit -> string
    abstract nextLabel: time:int -> string

    abstract pause: unit -> ITimeline
    abstract pause: atTime:'T -> ITimeline
    abstract pause: atTime:'T * suppressEvents:bool -> ITimeline

    abstract paused: unit -> ITimeline
    abstract paused: value:bool -> ITimeline

    abstract play: unit -> ITimeline
    abstract play: from:'T -> ITimeline
    abstract play: from:'T * suppressEvents:bool -> ITimeline

    abstract previousLabel: unit -> ITimeline
    abstract previousLabel: time:int -> ITimeline

    abstract progress: unit -> ITimeline
    abstract progress: value:int * suppressEvent:bool -> ITimeline

    abstract recent: unit -> 'T
    abstract recent: unit -> (unit -> unit)

    abstract remove: value: 'T -> ITimeline

    abstract removeLabel: value:string -> ITimeline

    abstract removePause: position:'T -> ITimeline

    abstract repeat: unit -> int
    abstract repeat: value:int -> ITimeline

    abstract repeatDelay: unit -> int
    abstract repeatDelay: value:int -> ITimeline

    abstract restart: includeDelay:bool * suppressEvent:bool -> ITimeline

    abstract resume: unit -> ITimeline

    abstract reverse: unit -> ITimeline
    abstract reverse: from:int -> ITimeline
    abstract reverse: from:int * suppressEvents:bool -> ITimeline

    abstract reversed: unit -> bool
    abstract reversed: value:bool -> ITimeline

    abstract seek: position:'P * suppressEvents:bool -> ITimeline
    abstract set: target:'T * vars:'V * position:'P -> ITimeline
    abstract shiftChildren: amount:float * adjustLabels:bool * ignoreBeforeTime:float -> ITimeline

    abstract startTime: unit -> float
    abstract startTime: value:float -> ITimeline

    abstract time: unit -> float
    abstract time: value:float * suppressEvents:bool -> ITimeline

    abstract timeScale: unit -> float
    abstract timeScale: value:float -> ITimeline
    [<Emit("$0.to($1, $2)")>]
    abstract to_: target:'T * vars:'V -> ITimeline
    [<Emit("$0.to($1, $2, $3)")>]
    abstract to_: target:'T * vars:'V * position:'P -> ITimeline

    abstract totalDuration: unit -> float
    abstract totalDuration: value:float -> ITimeline

    abstract totalProgress: unit -> float
    abstract totalProgress: value:float * suppressEvents:bool -> ITimeline

    abstract totalTime: unit -> float
    abstract totalTime: value:float * suppressEvents:bool -> ITimeline

    abstract tweenFromTo: fromPosition:'F * toPosition:'T -> ITween
    abstract tweenFromTo: fromPosition:'F * toPosition:'T * vars:'V -> ITween

    abstract tweenTo: position:'P -> ITween
    abstract tweenTo: position:'P * vars:'V -> ITween

    abstract yoyo: unit -> ITimeline
    abstract yoyo: value:bool -> ITimeline

    inherit ITimeline
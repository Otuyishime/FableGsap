namespace FableGsap
open System

type ITweenAPI = 
    abstract delay: unit -> int
    abstract delay: value: int -> ITween

    abstract duration: unit -> int
    abstract duration: value: int -> ITween

    abstract endTime: includeRepeats: bool -> int

    // NOTE: Work on delete and get Event Callback
    abstract eventCallback: eventType: string * callbackFunction: (unit -> unit) option  -> ITween
    abstract eventCallback: eventType: string -> (unit -> unit)

    abstract invalidate: unit -> ITween

    abstract isActive: unit -> bool

    abstract iteration: unit -> int
    abstract iteration: number: int -> ITween

    abstract kill: unit -> ITween
    abstract kill: target: 'T -> ITween
    abstract kill: target: option<'T> * propertiesList: string -> ITween

    abstract pause: unit -> ITween
    abstract pause: atTime: int -> ITween
    abstract pause: atTime: int * suppressEvents:bool -> ITween

    abstract paused: unit -> ITween
    abstract paused: value:bool -> ITween

    abstract play: unit -> ITween
    abstract play: from: int -> ITween
    abstract play: from: int * suppressEvents:bool -> ITween

    abstract progress: unit -> ITween
    abstract progress: value: float -> ITween
    abstract progress: value: float * suppressEvents: bool -> ITween

    abstract repeat: unit -> int
    abstract repeat: value: int -> ITween

    abstract repeatDelay: unit -> int
    abstract repeatDelay: value: int -> ITween

    abstract restart: unit -> ITween
    abstract restart: includeDelay: bool * suppressEvents: bool -> ITween

    abstract resume: unit -> ITween

    abstract reverse: unit -> ITween
    abstract reverse: from: int -> ITween
    abstract reverse: from: int * suppressEvents: bool -> ITween

    abstract reversed: unit -> int
    abstract reversed: value: bool -> ITween

    abstract seek: time: int -> ITween
    abstract seek: time: int * suppressEvents: bool -> ITween

    abstract startTime: unit -> ITween
    abstract startTime: value: int  -> ITween

    abstract targets: unit -> array<obj>

    abstract then_: callback: (unit -> unit) -> ITween

    abstract time: unit -> ITween
    abstract time: value: int -> ITween
    abstract time: value: int * suppressEvents: bool -> ITween

    abstract timeScale: unit -> float
    abstract timeScale: value: float -> ITween

    abstract totalDuration: unit -> int
    abstract totalDuration: value: int -> ITween

    abstract totalProgress: unit -> ITween
    abstract totalProgress: value: float -> ITween
    abstract totalProgress: value: float * suppressEvents: bool -> ITween

    abstract totalTime: unit -> ITween
    abstract totalTime: value: int -> ITween
    abstract totalTime: value: int * suppressEvents: bool -> ITween

    abstract yoyo: unit -> int
    abstract yoyo: value: bool -> ITween

    inherit ITween
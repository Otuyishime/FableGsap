namespace FableGsap
open Fable.Core.JsInterop

module Helpers =

    let IsFunction = Interop.IsFunction
    let GetFunction someFunc  = 
        if (IsFunction someFunc) 
        then (Some someFunc) 
        else None

    let convertCallbackType = 
        function
        | callbackType.OnComplete -> "onComplete"
        | callbackType.OnInterrupt -> "onInterrupt"
        | callbackType.OnRepeat -> "onRepeat"
        | callbackType.OnReverseComplete -> "onReverseComplete"
        | callbackType.OnStart -> "onStart"
        | callbackType.OnUpdate -> "onUpdate"

    let TryGetTween (t: obj) =
        if (isNullOrUndefined t) || not (Interop.IsTween t)
        then None 
        else Some (t :?> ITween)

    let TryGetTimeline (t: obj) =
        if (isNullOrUndefined t) || not (Interop.IsTimeline t)
        then None 
        else Some (t :?> ITimeline)

    let TryGetCallback f =
        if (isNullOrUndefined f) || not (Interop.IsFunction f)
        then None 
        else Some f

    let TryGetTweens tweensResults =
            tweensResults
            |> Array.map TryGetTween
            |> Array.filter (fun t -> t.IsSome)
            |> Array.map (fun t -> t.Value)

    let TryGetTimelines timelinesResults =
            timelinesResults
            |> Array.map TryGetTimeline
            |> Array.filter (fun t -> t.IsSome)
            |> Array.map (fun t -> t.Value)
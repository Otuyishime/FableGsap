namespace FableGsap

open Fable.Core
open Fable.Core.JsInterop

type Gsap =
    /// Lets you configure GSAP’s settings that aren't Tween-specific.
    /// Check greensock docs for full details
    static member inline config (configs: IConfig seq) = 
        Interop.gsapApi.config (Interop.makeConfigVars configs)
    
    /// Lets you set properties that should be inherited by `ALL` tweens 
    /// (that don't have `inherit:false` set) unless overridden by that tween.
    static member inline defaults (vars: IVar seq) = 
        Interop.gsapApi.defaults (Interop.makeAnimationVars vars)
    
    /// Provides a simple way to call a function after a set amount of time, completely in-sync with the whole rendering 
    /// loop (unlike a `setTimeout()` which may fire outside of the browser's screen refresh cycle).
    static member inline delayedCall (seconds: int, callBack: unit -> unit) = 
        Interop.gsapApi.delayedCall (seconds, callBack)
    
    /// Returns a new Timeline instance containing the root tweens and timelines.
    static member inline exportRoot = Interop.gsapApi.exportRoot
    
    /// Think of a backwards tween where you define where the values should START, and then 
    /// it animates to the current state which is perfect for animating objects onto the screen 
    /// because you can set them up the way you want them to look at the end and then animate in `from` elsewhere. 
    static member inline from (target: ITarget, vars: IVars) = 
        Interop.gsapApi.from (target, vars)
    
    /// lets you define `BOTH` the starting and ending values for an animation 
    /// (as opposed to `from()` and `to_()` tweens which use the current state as either the start or end). 
    /// This is great for having full control over an animation, especially when it is chained with other animations. 
    static member inline fromTo (target: ITarget, fromVars: IVars, toVars: IVars) = 
        Interop.gsapApi.fromTo (target, fromVars, toVars)
    
    /// Returns a tween associated with the given ID. Returns `None` if no tween or timeline has that ID.
    static member inline getTweenById (id: string) = 
        id |>Interop.gsapApi.getById |> Helpers.TryGetTween
    
    /// Returns a timeline associated with the given ID. Returns `None` if no tween or timeline has that ID.
    static member inline getTimelineById (id: string) = 
        id |>Interop.gsapApi.getById |> Helpers.TryGetTimeline
    
    /// Returns the value of the property requested as a number (if possible) 
    /// unless you specify a unit in which case it will be added to the number, 
    /// making it a string. Returns None if it doesn’t exist.
    static member inline getProperty (target: ITarget, var: string, ?cssUnit: _unit): string option =
        match cssUnit with
        | Some css_unit -> Interop.gsapApi.getProperty (target, var, css_unit)
        | None -> Interop.gsapApi.getProperty (target, var)
    
    /// Returns an array containing all the tweens of a particular target (or group of targets) 
    /// that have not yet been released for garbage collection.
    static member inline getTweensOf (target: ITarget) = 
        Interop.gsapApi.getTweensOf target

    /// Reports whether or not a particular object is actively animating.
    /// If a tween is paused, completed, or hasn’t started yet, it isn’t considered active.
    static member inline isTweening(tartget: ITarget) = 
        Interop.gsapApi.isTweening tartget

    /// The most common type of animation because it allows you to define the `destination values` 
    /// (and most people think in terms of animating `to` certain values)
    static member inline to_ (target: ITarget, vars: IVar seq) = 
        Interop.gsapApi.to_ (target, (Interop.makeAnimationVars vars))

    static member inline to_ (target: ITarget, vars: IVars) = 
        Interop.gsapApi.to_ (target, vars)

    /// Returns a timeline, a powerful sequencing tool that acts as a container for tweens and other timelines, making it 
    /// simple to control them as a whole and precisely manage their timing.
    static member inline timeline (vars: IVars) = 
        Interop.gsapApi.timeline vars

    /// Parses an easing string and returns the corresponding parsed easing function.
    static member inline parseEase (easeStr: string) = 
        Interop.gsapApi.parseEase easeStr

    /// Immediately sets properties of the target(s) accordingly - essentially a zero-duration `to_()` tween
    /// with a more intuitive name
    static member inline set (target: ITarget, vars: IVars) = 
        Interop.gsapApi.set (target, vars)
    
    /// Manually update the root (global) timeline. `This is only intended for advanced users.`
    static member inline updateRoot (seconds: int) = 
        Interop.gsapApi.updateRoot seconds

    /// The GSAP version that is currently being used (in String form)
    static member inline version = 
        Interop.gsapApi.version
    
    /// Register custom effects
    static member inline registerEffect (effect: IEffectProp seq) =
        Interop.gsapApi.registerEffect (Interop.makeEffect effect)

    
module Gsap =

    type effects = 
        static member inline execute (effectName: string, target: ITarget) =
             Interop.createUseEffect (Interop.gsapApi.effects, effectName, target)

    type globalTimeline = 
        static member inline pause () = Interop.gsapApi.globalTimeline.pause ()
        static member inline pause (atTime: float) = Interop.gsapApi.globalTimeline.pause (atTime, true)
        static member inline pause (atTime: float, suppressEvents: bool) = 
            Interop.gsapApi.globalTimeline.pause (atTime, suppressEvents)
        
        static member inline play () = Interop.gsapApi.globalTimeline.play ()
        static member inline play (from: float) = Interop.gsapApi.globalTimeline.play (from, true)
        static member inline play (from: float, suppressEvents: bool) = Interop.gsapApi.globalTimeline.play (from, suppressEvents)
        
        static member inline paused () = Interop.gsapApi.globalTimeline.paused ()
        static member inline paused (value: bool) = Interop.gsapApi.globalTimeline.paused value
        
        static member inline timeScale () = Interop.gsapApi.globalTimeline.timeScale ()
        static member inline timeScale (value: float) = Interop.gsapApi.globalTimeline.timeScale value

    type ticker = 
        static member inline add (callback: unit -> unit) = Interop.gsapApi.ticker.add callback
        static member inline deltaRatio (fps: float) = Interop.gsapApi.ticker.deltaRatio fps
        static member inline fps (value: float) = Interop.gsapApi.ticker.fps value
        static member inline lagSmoothing (threshold:float,  adjustedLag:float) = Interop.gsapApi.ticker.lagSmoothing (threshold, adjustedLag)
        static member inline remove (callback: unit -> unit) = Interop.gsapApi.ticker.remove callback
        static member inline sleep () = Interop.gsapApi.ticker.sleep ()
        static member inline tick () = Interop.gsapApi.ticker.tick ()
        static member inline wake () = Interop.gsapApi.ticker.wake ()
        static member inline frame = Interop.gsapApi.ticker.frame
        static member inline time = Interop.gsapApi.ticker.time

    type utils = 
        static member inline checkPrefix (property: string, element: 'E, preferPrefix: bool) = Interop.gsapApi.utils.checkPrefix (property, element, preferPrefix)  
        static member inline clamp (min: float) (max: float) (value: float) = Interop.gsapApi.utils.clamp min max value
        static member inline getUnit (value: string) = Interop.gsapApi.utils.getUnit value
        static member inline snap (snapTo: float) (value: float) = Interop.gsapApi.utils.snap snapTo value
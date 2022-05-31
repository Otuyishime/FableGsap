namespace FableGsap
open System

/// <summary>
/// A Tween is what does all the animation work - think of it like a high-performance property setter. 
/// You feed in targets (the objects you want to animate), a duration, and any properties you want to 
/// animate and when its playhead moves to a new position, it figures out what the property values 
/// should be at that point applies them accordingly.
/// Common methods for creating a Tween are gsap.to(), gsap.from(),  gsap.fromTo()
/// </summary>
type Tween =

    /// Sets the animation's initial delay which is the length of time in seconds before the animation should begin.
    static member inline delay (value: int) = fun (tween: ITween) -> (tween :?> ITweenAPI).delay value
    /// Gets the animation's initial delay which is the length of time in seconds before the animation should begin.
    static member inline delay (tween: ITween) = (tween :?> ITweenAPI).delay ()
    
    /// Sets the animation's duration, not including any repeats or repeatDelays.
    static member inline duration (duration: int) = 
        fun (tween: ITween) -> 
            (tween :?> ITweenAPI).duration duration
    /// Gets the animation's duration, not including any repeats or repeatDelays.
    static member inline duration (tween: ITween) = (tween :?> ITweenAPI).duration ()

    /// Returns the time at which the animation will finish according to the parent timeline's local time.
    static member inline endTime (tween: ITween) = (tween :?> ITweenAPI).endTime true
    /// Returns the time at which the animation will finish according to the parent timeline's local time.
    static member inline endTime (includeRepeats: bool) = 
        fun (tween: ITween) -> 
            (tween :?> ITweenAPI).endTime includeRepeats

    /// Sets or deletes an event callback like "onComplete", "onUpdate", "onStart", "onReverseComplete" or "onRepeat" 
    /// along with any parameters that should be passed to that callback.
    /// Use `None` to remove a callback function
    static member inline eventCallback (callbackType: callbackType, callback: (unit -> unit) option) = 
        fun (tween: ITween) ->
            (callbackType |> unbox<string>, callback) 
            |> (tween :?> ITweenAPI).eventCallback
    /// Gets an event callback like "onComplete", "onUpdate", "onStart", "onReverseComplete" or "onRepeat" 
    /// along with any parameters that should be passed to that callback.
    static member inline eventCallback (callbackType: callbackType) = 
        fun (tween: ITween) ->
            callbackType 
            |> unbox<string>
            |> (tween :?> ITweenAPI).eventCallback
            |> Helpers.GetFunction
    
    /// <summary>
    /// Flushes any internally-recorded starting/ending values which can be useful 
    /// if you want to restart an animation without reverting to any previously recorded starting values.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline invalidate (tween: ITween) = (tween :?> ITweenAPI).invalidate ()

    /// <summary>
    /// Indicates whether or not the animation is currently active 
    /// (meaning the virtual playhead is actively moving across this instance's time span and it is not paused, nor are any of its ancestor timelines).
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline isActive (tween: ITween) = (tween :?> ITweenAPI).isActive ()

    /// Sets the iteration (the current repeat) of tweens.
    static member inline iteration (number: int) = function (tween: ITween) -> (tween :?> ITweenAPI).iteration number
    
    /// <summary>
    /// Gets the iteration (the current repeat) of tweens.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline iteration (tween: ITween) = (tween :?> ITweenAPI).iteration ()

    /// <summary>
    /// kills the entire animation.
    /// To kill means to immediately stop the animation, remove it from its parent timeline, and release it for garbage collection.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline kill (tween: ITween) = (tween :?> ITweenAPI).kill ()

    /// <summary>
    /// kills all parts of the animation related to the target (if the tween has multiple targets, the others will not be affected).
    /// </summary>
    /// <param name='target'>Animation target object</param>
    static member inline kill (target: obj) = fun (tween: ITween) -> (tween :?> ITweenAPI).kill target

    /// <summary>
    /// kills only animation properties of the animation for all targets.
    /// </summary>
    /// <param name='propertyList'>List of property names that should no longer be animated by this Tween.</param>
    static member inline kill (propertyList: string) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).kill (None, propertyList)

    ///kills only animation properties of animations of the given targets.
    /// <summary>
    /// kills only animation properties of the animation for all targets.
    /// </summary>
    /// <param name='targets'>List of animation target objects</param>
    /// <param name='propertyList'>List of property names that should no longer be animated by this Tween.</param>
    static member inline kill (targets: seq<obj>, propertyList: string) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).kill (Some targets, propertyList)

    /// <summary>
    /// Pauses wherever the playhead currently is.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline pause (tween: ITween) = (tween :?> ITweenAPI).pause ()

    /// <summary>
    /// Pauses - Jumps to exactly "atTime".
    /// </summary>
    /// <param name='atTime'>seconds into the animation</param>
    /// <param name='suppressEvents'>suppress events during the initial move</param>
    static member inline pause (atTime: int, ?suppressEvents: bool) = 
        fun (tween: ITween) -> 
            match suppressEvents with 
            | Some suppressEv -> (tween :?> ITweenAPI).pause (atTime, suppressEv)
            | None -> (tween :?> ITweenAPI).pause (atTime, false)

    /// Sets the animation's paused state which indicates whether or not the animation is currently paused.
    static member inline paused (value: bool) = fun (tween: ITween) -> (tween :?> ITweenAPI).paused value

    /// <summary>
    /// Gets the animation's paused state which indicates whether or not the animation is currently paused.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline paused (tween: ITween) = (tween :?> ITweenAPI).paused ()

    /// <summary>
    /// Begins playing from wherever the playhead currently is.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline play (tween: ITween) = (tween :?> ITweenAPI).play ()

    /// <summary>
    /// Jumps to exactly "from" seconds into the animation and starts playing but doesn't suppress events 
    /// (meaning it will trigger any callbacks between the old and new playhead positions)
    /// </summary>
    /// <param name='from'>The time from which the animation should begin playing.</param>
    /// <param name='suppressEvents'> If true, no events or callbacks will be triggered when the playhead moves to the new position defined in the atTime parameter.</param>
    static member inline play (from: int, ?suppressEvents: bool) = 
        fun (tween: ITween) -> 
            match suppressEvents with 
            | Some suppressEv -> (tween :?> ITweenAPI).play (from, suppressEv)
            | None -> (tween :?> ITweenAPI).play (from, false)

    /// <summary>
    /// Gets the tween's progress which is a value between 0 and 1 
    /// indicating the position of the virtual playhead (excluding repeats) 
    /// where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline progress (tween: ITween) = (tween :?> ITweenAPI).progress ()

    /// <summary>
    /// Sets the tween's progress which is a value between 0 and 1 
    /// indicating the position of the virtual playhead (excluding repeats) 
    /// where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    /// </summary>
    /// <param name='value'>Sets progress value</param>
    /// <param name='suppressEvents'>If true, no events or callbacks will be triggered when the playhead moves to the new position.</param>
    static member inline progress (value: float, ?suppressEvents: bool) = 
        fun (tween: ITween) -> 
            match suppressEvents with 
            | Some suppressEv -> (tween :?> ITweenAPI).progress (value, suppressEv)
            | None -> (tween :?> ITweenAPI).progress (value, false)

    /// <summary>
    /// Gets the number of times that the tween should repeat after its first iteration.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline repeat (tween: ITween) = (tween :?> ITweenAPI).repeat ()

    /// <summary>
    /// Sets the number of times that the tween should repeat after its first iteration.
    /// </summary>
    /// <param name='value'>Number of repetitions</param>
    static member inline repeat (value: int) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).repeat value

    /// <summary>
    /// Gets the amount of time in seconds between repeats.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline repeatDelay (tween: ITween) = (tween :?> ITweenAPI).repeatDelay ()

    /// <summary>
    /// Sets the amount of time in seconds between repeats.
    /// </summary>
    /// <param name='seconds'>Delay between repeats</param>
    static member inline repeatDelay (seconds: int) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).repeatDelay seconds

    /// <summary>
    /// Restarts and begins playing forward from the beginning.
    /// </summary>
    /// <param name='includeDelay'>Determines whether or not the delay (if any) is honored when restarting.</param>
    /// <param name='suppressEvents'>If true, no events or callbacks will be triggered when the playhead moves to the new position defined in the time parameter.</param>
    static member inline restart (?includeDelay: bool, ?suppressEvents: bool) =
        fun (tween: ITween) ->
            match (includeDelay, suppressEvents) with
            | (Some delay, Some suppress) -> (tween :?> ITweenAPI).restart (delay, suppress)
            | (None, Some suppress) -> (tween :?> ITweenAPI).restart (false, suppress)
            | (Some delay, None) -> (tween :?> ITweenAPI).restart (delay, false)
            | (None, None) -> (tween :?> ITweenAPI).restart (false, false)

    /// <summary>
    /// Resumes playing without altering direction (forward or reversed).
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline resume (tween: ITween) = (tween :?> ITweenAPI).resume ()

    /// <summary>
    /// Reverses playback so that all aspects of the animation are oriented backwards including, for example, a tween's ease.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline reverse (tween: ITween) = (tween :?> ITweenAPI).reverse ()

    /// <summary>
    /// Reverses playback so that all aspects of the animation are oriented backwards including, for example, a tween's ease.
    /// </summary>
    /// <param name='from'> The time from which the animation should begin playing in reverse. To begin at the very end of the animation, use 0. Negative numbers are relative to the end of the animation, so -1 would be 1 second from the end.</param>
    /// <param name='suppressEvents'>If true (the default), no events or callbacks will be triggered when the playhead moves to the new position defined in the from parameter</param>
    static member inline reverseWithSuppressEvents (from: int) (suppressEvents: bool) =
        fun (tween: ITween) -> (tween :?> ITweenAPI).reverse (from, suppressEvents)

    /// <summary>
    /// Gets the animation's reversed state which indicates whether or not the animation should be played backwards.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline reversed (tween: ITween) = (tween :?> ITweenAPI).reversed ()

    /// <summary>
    /// Sets the animation's reversed state which indicates whether or not the animation should be played backwards.
    /// </summary>
    /// <param name='value'>Sets reversed value</param>
    static member inline setReversed (value: bool) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).reversed value

    /// <summary>
    /// Jumps to a specific time without affecting whether or not the instance is paused or reversed.
    /// </summary>
    /// <param name='time'>The time to go to.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline seek (time: int) (tween: ITween) = (tween :?> ITweenAPI).seek time

    /// <summary>
    /// Jumps to a specific time without affecting whether or not the instance is paused or reversed.
    /// </summary>
    /// <param name='time'>The time to go to.</param>
    /// <param name='suppressEvents'>If true (the default), no events or callbacks will be triggered when the playhead moves to the new position defined in the time parameter.</param>
    static member inline seekWithSuppressEvents (time: int) (suppressEvents: bool) = 
        fun (tween: ITween) -> (tween :?> ITweenAPI).seek (time, suppressEvents)

    /// <summary>
    /// Gets the time at which the animation begins on its parent timeline (after any delay that was defined).
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline startTime (tween: ITween) = (tween :?> ITweenAPI).startTime ()

    /// <summary>
    /// Sets the time at which the animation begins on its parent timeline (after any delay that was defined).
    /// </summary>
    /// <param name='value'>Start time value</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setStartTime (value: int) (tween: ITween) = (tween :?> ITweenAPI).startTime value

    /// <summary>
    /// Gets an array of target objects whose properties the Tween animates.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline targets (tween: ITween) = (tween :?> ITweenAPI).targets ()

    /// <summary>
    /// Returns a promise so that you can uses promises to track when a tween or timeline is complete.
    /// NOTE: You may prefer to use a Promise instead of an onComplete callback - that's exactly what then() is for. 
    /// It returns a Promise that will get resolved when the animation completes.
    /// </summary>
    /// <param name='callback'>The function that you want to handle the Tween's promise that is generated.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline then_ (callback: unit -> unit) (tween: ITween) = (tween :?> ITweenAPI).then_ callback

    /// <summary>
    /// Gets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline time (tween: ITween) = (tween :?> ITweenAPI).time ()

    /// <summary>
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays
    /// </summary>
    /// <param name='value'>Sets the value. Negative values will be interpreted from the END of the animation.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTime (value: int) (tween: ITween) = (tween :?> ITweenAPI).time value

    /// <summary>
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays
    /// </summary>
    /// <param name='value'>Sets the value. Negative values will be interpreted from the END of the animation.</param>
    /// <param name='suppressEvents'>If true, no events or callbacks will be triggered when the playhead moves to the new position defined in the value parameter.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTimeWithSuppressEvents (value: int) (suppressEvents: bool) (tween: ITween) = (tween :?> ITweenAPI).time (value, suppressEvents)

    /// <summary>
    /// Gets the time scale. Factor that's used to scale time in the animation where 1 = normal speed (the default), 0.5 = half speed, 2 = double speed, etc.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline timeScale (tween: ITween) = (tween :?> ITweenAPI).timeScale ()

    /// <summary>
    /// Sets the time scale. Factor that's used to scale time in the animation where 1 = normal speed (the default), 0.5 = half speed, 2 = double speed, etc.
    /// </summary>
    /// <param name='value'>Sets the value (setter) and returns the instance itself for easier chaining.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTimeScale (value: float) (tween: ITween) = (tween :?> ITweenAPI).timeScale value

    /// <summary>
    /// Gets the total duration of the tween in seconds including any repeats or repeatDelays.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline totalDuration (tween: ITween) = (tween :?> ITweenAPI).totalDuration ()

    /// <summary>
    /// Sets the total duration of the tween in seconds including any repeats or repeatDelays.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTotalDuration (value: int) (tween: ITween) = (tween :?> ITweenAPI).totalDuration value

    /// <summary>
    /// Gets the tween's totalProgress which is a value between 0 and 1 indicating the position of the virtual playhead 
    /// (including repeats) where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline totalProgress (tween: ITween) = (tween :?> ITweenAPI).totalProgress ()

    /// <summary>
    /// Sets the tween's totalProgress which is a value between 0 and 1 indicating the position of the virtual playhead 
    /// (including repeats) where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTotalProgress (value: float) (tween: ITween) = (tween :?> ITweenAPI).totalProgress value

    /// <summary>
    /// Sets the tween's totalProgress which is a value between 0 and 1 indicating the position of the virtual playhead 
    /// (including repeats) where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='suppressEvents'>If true, no events or callbacks will be triggered when the playhead moves to the new position.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTotalProgressWithSuppressEvents (value: float) (suppressEvents: bool) (tween: ITween) = 
        (tween :?> ITweenAPI).totalProgress (value, suppressEvents)

    /// <summary>
    /// Gets the position of the playhead according to the totalDuration which includes any repeats and repeatDelays.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline totalTime (tween: ITween) = (tween :?> ITweenAPI).totalTime ()

    /// <summary>
    /// Sets the position of the playhead according to the totalDuration which includes any repeats and repeatDelays.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTotalTime (value: int) (tween: ITween) = (tween :?> ITweenAPI).totalTime value

    /// <summary>
    /// Sets the position of the playhead according to the totalDuration which includes any repeats and repeatDelays.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='suppressEvents'>If true, no events or callbacks will be triggered when the playhead moves to the new position defined in the time parameter.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setTotalTimeWithSuppressEvents (value: int) (suppressEvents: bool) (tween: ITween) = 
        (tween :?> ITweenAPI).totalTime (value, suppressEvents)

    /// <summary>
    /// Gets the tween's yoyo state, where true causes the tween to go back and forth, alternating backward and forward on each repeat.
    /// </summary>
    /// <param name='tween'>Tween instance</param>
    static member inline yoyo (tween: ITween) = (tween :?> ITweenAPI).yoyo ()

    /// <summary>
    /// Sets the tween's yoyo state, where true causes the tween to go back and forth, alternating backward and forward on each repeat.
    /// </summary>
    /// <param name='value'>Sets the value and returns the instance itself for easier chaining.</param>
    /// <param name='tween'>Tween instance</param>
    static member inline setYoyo (value: bool) (tween: ITween) = (tween :?> ITweenAPI).yoyo value
namespace FableGsap

/// <summary>
/// A Timeline is a powerful sequencing tool that acts as a container for tweens and other timelines, 
/// making it simple to control them as a whole and precisely manage their timing. 
/// </summary>
type Timeline =

    /// Adds a tween to the timeline at specified position.
    static member inline add (tweens: ITween seq, ?position: IPosition) = 
        fun (timeline: ITimeline) ->
            match position with
            | Some position -> (timeline :?> ITimelineAPI).add (tweens, position)
            | None -> (timeline :?> ITimelineAPI).add (tweens, prop.position.gap 0.0)
    
    /// Adds a timeline to the timeline  at specified position.
    static member inline add (timelines: ITimeline seq, ?position: IPosition) = 
        fun (timeline: ITimeline) ->
            match position with
            | Some position -> (timeline :?> ITimelineAPI).add (timelines, position)
            | None -> (timeline :?> ITimelineAPI).add (timelines, prop.position.gap 0.0)

    /// Adds a callback to the timeline at specified position.
    static member inline add (callbacks: (unit -> unit) seq, ?position: IPosition) = 
        fun (timeline: ITimeline) ->
            match position with
            | Some position -> (timeline :?> ITimelineAPI).add (callbacks, position)
            | None -> (timeline :?> ITimelineAPI).add (callbacks, prop.position.gap 0.0)

    /// Adds an array of labels to the timeline at specified position.
    static member inline add (labels: string seq, ?position: IPosition) =  
        fun (timeline: ITimeline) ->
            match position with
            | Some position -> (timeline :?> ITimelineAPI).add (labels, position)
            | None -> (timeline :?> ITimelineAPI).add (labels, prop.position.gap 0.0)

    /// Adds a label to the timeline at specified position.
    static member inline addLabel (label: string, ?position: IPosition) = 
        fun (timeline: ITimeline) -> 
            match position with
            | Some position -> (timeline :?> ITimelineAPI).add (label, position)
            | None -> (timeline :?> ITimelineAPI).add (label, prop.position.gap 0.0)

    /// Inserts a special callback that pauses playback of the timeline at a particular time or label.
    static member inline addPause (position: IPosition, ?callback: (unit -> unit)) = 
        fun (timeline: ITimeline) ->
            match callback with
            | Some callback -> (timeline :?> ITimelineAPI).addPause (position, callback)
            | None -> (timeline :?> ITimelineAPI).addPause (position)

    /// Adds a callback to the end of the timeline (or elsewhere using the position parameter). 
    /// This is a convenience method that accomplishes exactly the same thing as add(gsap.delayedCall(...)) but with less code.
    static member inline call (callback: (unit -> unit), ?position: IPosition) = 
        fun (timeline: ITimeline) ->
            match position with
            | Some position -> (timeline :?> ITimelineAPI).call (callback, position)
            | None -> (timeline :?> ITimelineAPI).call (callback, prop.position.gap 0.0)

    /// Empties the timeline of all tweens, timelines, and callbacks (and optionally labels too).
    static member inline clear (timeline: ITimeline) = (timeline :?> ITimelineAPI).clear true
    /// Empties the timeline of all tweens, timelines, and callbacks (and optionally labels too).
    static member inline clear (clearLabels: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).clear clearLabels

    /// Gets the closest label that is at or before the current time.
    static member inline currentLabel (timeline: ITimeline) = (timeline :?> ITimelineAPI).currentLabel ()
    /// Jumps to a provided label.
    static member inline currentLabel (value: string) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).currentLabel value
    
    /// Gets the animation's initial delay which is the length of time in seconds before the animation should begin.
    static member inline delay (timeline: ITimeline) = (timeline :?> ITimelineAPI).delay ()
    /// Sets the animation's initial delay which is the length of time in seconds before the animation should begin.
    static member inline delay (value: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).delay value
    
    /// Gets the timeline's duration or, if used as a setter, adjusts the timeline's timeScale to fit it within the specified duration.
    static member inline duration (timeline: ITimeline) = (timeline :?> ITimelineAPI).duration ()
    /// Sets the timeline's duration or, if used as a setter, adjusts the timeline's timeScale to fit it within the specified duration.
    static member inline duration (value: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).duration value
    
    /// Returns the time at which the animation will finish according to the parent timeline's local time.
    /// By default, repeats are included when calculating the end time.
    static member inline endTime (timeline: ITimeline) = (timeline :?> ITimelineAPI).endTime true
    /// Returns the time at which the animation will finish according to the parent timeline's local time.
    static member inline endTime (includeRepeats: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).endTime includeRepeats
    
    /// Sets an event callback like onComplete, onUpdate, onStart, onReverseComplete, or onRepeat.
    static member inline eventCallback (callbackType: callbackType, callbackFunction: (unit -> unit) option) = 
        fun (timeline: ITimeline) -> 
            (timeline :?> ITimelineAPI).eventCallback (Helpers.convertCallbackType callbackType, callbackFunction)
    /// Gets an event callback like onComplete, onUpdate, onStart, onReverseComplete, or onRepeat.
    static member inline eventCallback (callbackType: callbackType) =
        fun (timeline: ITimeline) ->
            let callback = (timeline :?> ITimelineAPI).eventCallback (Helpers.convertCallbackType callbackType)
            if (Helpers.IsFunction callback) then (Some callback) else None

    /// <summary>
    /// Gets nested Tween by Id. Returns the first descendant with a matching Id.
    /// </summary>
    /// <param name='tweenId'>Id of tween you want to get back.</param>
    /// <param name='timeline'>Timeline instance</param>
    static member inline getTweenById (tweenId: string) (timeline: ITimeline) =
        (timeline :?> ITimelineAPI).getById tweenId |> Helpers.TryGetTween

    /// <summary>
    /// Gets nested Timeline by Id. Returns the first descendant with a matching Id.
    /// </summary>
    /// <param name='timelineId'>Id of Tiemline you want to get back</param>
    /// <param name='timeline'>Timeline instance</param>
    static member inline getTimelineById (timelineId: string) (timeline: ITimeline) = 
        (timeline :?> ITimelineAPI).getById timelineId |> Helpers.TryGetTimeline

    /// Returns an array containing all the tweens and/or timelines nested in this timeline.
    static member inline getChildren (nested:bool, tweens:bool, timelines:bool) = 
        fun (timeline: ITimeline) -> 
            match (tweens, timelines) with
            | (true, false) ->
                (timeline :?> ITimelineAPI).getChildren (nested, true, false)
                |> Helpers.TryGetTweens |> Tweens
            | (false, true) ->
                (timeline :?> ITimelineAPI).getChildren (nested, true, false)
                |> Helpers.TryGetTimelines |> Timelines
            | (true, true) ->
                let children = (timeline :?> ITimelineAPI).getChildren (nested, true, true)
                let _tweens = children |> Helpers.TryGetTweens
                let _timelines = children |> Helpers.TryGetTimelines
                TweensAndTimelines(_tweens, _timelines)
            | (false, false) -> Empty

    /// Returns an array containing all the tweens and/or timelines nested in this timeline.
    static member inline getChildren (nested:bool, tweens:bool, timelines:bool, ignoreBeforeTime: float) = 
        fun (timeline: ITimeline) -> 
            match (tweens, timelines) with
            | (true, false) ->
                (timeline :?> ITimelineAPI).getChildren (nested, true, false, ignoreBeforeTime)
                |> Helpers.TryGetTweens |> Tweens
            | (false, true) ->
                (timeline :?> ITimelineAPI).getChildren (nested, true, false, ignoreBeforeTime)
                |> Helpers.TryGetTimelines |> Timelines
            | (true, true) ->
                let children = (timeline :?> ITimelineAPI).getChildren (nested, true, true, ignoreBeforeTime)
                let _tweens = children |> Helpers.TryGetTweens
                let _timelines = children |> Helpers.TryGetTimelines
                TweensAndTimelines(_tweens, _timelines)
            | (false, false) -> Empty

    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: obj) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, true)
    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: obj, nested:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, nested)
    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: string) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, true)
    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: string, nested:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, nested)
    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: obj array) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, true)
    /// Returns the tweens of a particular object that are inside this timeline.
    static member inline getTweensOf (target: obj array, nested:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).getTweensOf (target, nested)

    /// Flushes any internally-recorded starting/ending values which can be useful 
    /// if you want to restart an animation without reverting to any previously recorded starting values.
    static member inline invalidate (timeline: ITimeline) = (timeline :?> ITimelineAPI).invalidate ()

    /// Indicates whether or not the animation is currently active (meaning the virtual playhead is actively moving across 
    /// this instance's time span and it is not paused, nor are any of its ancestor timelines).
    static member inline isActive (timeline: ITimeline) = (timeline :?> ITimelineAPI).isActive ()

    /// Sets the iteration (the current repeat) of timelines.
    static member inline iteration (number: int) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).iteration number
    /// Gets the iteration (the current repeat) of timelines.
    static member inline iteration (timeline: ITimeline) = (timeline :?> ITimelineAPI).iteration ()

    /// Immediately kills the timeline and removes it from its parent timeline, stopping its animation.
    static member inline kill (timeline: ITimeline) = (timeline :?> ITimelineAPI).kill ()

    /// Returns the next label in the timeline from the provided time.
    static member inline nextLabel (time: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).nextLabel time
    /// Returns the next label in the timeline from current playhead time.
    static member inline nextLabel (timeline: ITimeline) = (timeline :?> ITimelineAPI).nextLabel ()

    /// Pauses wherever the playhead currently is.
    static member inline pause (timeline: ITimeline) = (timeline :?> ITimelineAPI).pause ()
    /// Jumps to exactly "atTime" seconds into the animation and then pauses.
    static member inline pause (atTime: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).pause atTime
    /// Jumps to exactly "label" into the animation and then pauses.
    static member inline pause (label: string) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).pause label
    /// Jumps to exactly "label" into the animation and pauses but does/does not suppress events during the initial move.
    static member inline pause (atTime: int, suppressEvents: bool) = 
        fun (timeline: ITimeline) ->(timeline :?> ITimelineAPI).pause (atTime, suppressEvents)
    /// Jumps to exactly "label" into the animation and pauses but does/does not suppress events during the initial move.
    static member inline pause (label: string, suppressEvents: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).pause (label, suppressEvents)

    /// Sets the animation's paused state which indicates whether or not the animation is currently paused.
    static member inline paused (value: bool) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).paused value
    /// Gets the animation's paused state which indicates whether or not the animation is currently paused.
    static member inline paused (timeline: ITimeline) = (timeline :?> ITimelineAPI).paused ()

    /// Begins playing from wherever the playhead currently is.
    static member inline play  (timeline: ITimeline) = (timeline :?> ITimelineAPI).play ()
    /// Begins playing from exactly "from" seconds into the animation.
    static member inline play (from: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).play from
    /// Begins playing from exactly "from" label into the animation.
    static member inline play (label: string) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).play label
    /// Jumps to exactly "from" seconds into the animation and starts playing but doesn't suppress events 
    /// (meaning it will trigger any callbacks between the old and new playhead positions)
    static member inline play (from: int, suppressEvents: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).play (from, suppressEvents)
    /// Jumps to exactly "label" into the animation and starts playing but doesn't suppress events 
    /// (meaning it will trigger any callbacks between the old and new playhead positions)
    static member inline play (label: string, suppressEvents: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).play (label, suppressEvents)

    /// Returns the previous label in the timeline from the provided time.
    static member inline previousLabel (time: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).previousLabel time
    /// Returns the previous label in the timeline from current playhead time.
    static member inline previousLabel (timeline: ITimeline) = (timeline :?> ITimelineAPI).previousLabel ()

    /// Gets the timeline's progress which is a value between 0 and 1 indicating the position of the virtual playhead (excluding repeats) 
    /// where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    static member inline progress (timeline: ITimeline) = (timeline :?> ITimelineAPI).progress ()
    /// Gets the timeline's progress which is a value between 0 and 1 indicating the position of the virtual playhead (excluding repeats) 
    /// where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    static member inline progress (value: int) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).progress (value, false)
    /// Gets the timeline's progress which is a value between 0 and 1 indicating the position of the virtual playhead (excluding repeats) 
    /// where 0 is at the beginning, 0.5 is halfway complete, and 1 is complete.
    static member inline progress (value: int, suppressEvent: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).progress (value, suppressEvent)

    /// Returns the most recently added child Tween regardless of its position in the timeline.
    static member inline recentTween (timeline: ITimeline) =  (timeline :?> ITimelineAPI).recent () |> Helpers.TryGetTween
    /// Returns the most recently added child Timeline regardless of its position in the timeline.
    static member inline recentTimeline (timeline: ITimeline) =  (timeline :?> ITimelineAPI).recent () |> Helpers.TryGetTimeline
    /// Returns the most recently added child Callback regardless of its position in the timeline.
    static member inline recentCallback (timeline: ITimeline) = (timeline :?> ITimelineAPI).recent ()

    /// Removes a tween from the timeline.
    static member inline remove (tween: ITween) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).remove tween
    /// Removes a timeline from the timeline.
    static member inline remove (timeline: ITimeline) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).remove timeline
    /// Removes a callback from the timeline.
    static member inline remove (callback: (unit -> unit)) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).remove callback
    /// Removes a label from the timeline.
    static member inline remove (label: string) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).remove label
    /// Removes a label from the timeline.
    static member inline remove (labels: string array) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).remove labels

    /// Removes a label from the timeline.
    /// NOTE: CHECK GSAP DOCS. https://greensock.com/docs/v3/GSAP/Timeline/removeLabel()
    static member inline removeLabel (label: string) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).removeLabel label

    /// Removes pauses that were added to a timeline via its .addPause() method.
    static member inline removePause (time: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).removePause time
    /// Removes pauses that were added to a timeline via its .addPause() method.
    static member inline removePause (label: string) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).removePause label

    /// Gets the number of times that the timeline should repeat after its first iteration.
    static member inline repeat (timeline: ITimeline) = (timeline :?> ITimelineAPI).repeat ()
    /// Sets the number of times that the timeline should repeat after its first iteration.
    static member inline repeat (value: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).repeat value

    /// Gets the amount of time in seconds (or frames for frames-based timelines) between repeats.
    static member inline repeatDelay (timeline: ITimeline) = (timeline :?> ITimelineAPI).repeatDelay ()
    /// Sets amount of time in seconds (or frames for frames-based timelines) between repeats.
    static member inline repeatDelay (value: int) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).repeatDelay value

    /// Restarts and begins playing forward from the beginning.
    static member inline restart (timeline: ITimeline) = (timeline :?> ITimelineAPI).restart (false, true)
    /// Restarts and begins playing forward from the beginning.
    static member inline restart (includeDelay: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).restart (includeDelay, true)
    /// Restarts and begins playing forward from the beginning.
    static member inline restart (includeDelay: bool, suppressEvents:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).restart (includeDelay, suppressEvents)

    /// Resumes playing without altering direction (forward or reversed).
    static member inline resume (timeline: ITimeline) = (timeline :?> ITimelineAPI).resume ()

    /// Reverses playback so that all aspects of the animation are oriented backwards including, for example, a tween's ease.
    static member inline reverse (timeline: ITimeline) = (timeline :?> ITimelineAPI).reverse ()
    /// Reverses playback so that all aspects of the animation are oriented backwards including, for example, a tween's ease.
    static member inline reverse (from: int) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).reverse from
    /// Reverses playback so that all aspects of the animation are oriented backwards including, for example, a tween's ease.
    static member inline reverse (from: int, suppressEvents:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).reverse (from, suppressEvents)

    /// Gets the animation's reversed state which indicates whether or not the animation should be played backwards.
    static member inline reversed (timeline: ITimeline) = (timeline :?> ITimelineAPI).reversed ()
    /// Sets the animation's reversed state which indicates whether or not the animation should be played backwards.
    static member inline reversed (value: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).reversed value

    /// Jumps to a specific time (or label) without affecting whether or not the instance is paused or reversed.
    static member inline seek (position: IPosition) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).seek (position, true)
    /// Jumps to a specific time (or label) without affecting whether or not the instance is paused or reversed.
    static member inline seek (position: IPosition, suppressEvents: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).seek (position, suppressEvents)

    /// Adds a zero-duration tween to the end of the timeline (or elsewhere using the position parameter) that sets values immediately 
    /// when the virtual playhead reaches that position on the timeline - this is a convenience method that accomplishes exactly the same thing as 
    /// ```f# 
    /// add(gsap.to(target, {duration: 0, ...}) ) 
    /// ```
    /// but with less code.
    static member inline set (target: ITarget, vars: IVars) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).set (target, vars, prop.position.gap 0.0)
    /// Adds a zero-duration tween to the end of the timeline (or elsewhere using the position parameter) that sets values immediately 
    /// when the virtual playhead reaches that position on the timeline - this is a convenience method that accomplishes exactly the same thing as 
    /// ```f# 
    /// add(gsap.to(target, {duration: 0, ...}) ) 
    /// ```
    /// but with less code.
    static member inline set (target: ITarget, vars: IVars, position: IPosition) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).set (target, vars, position)

    /// Shifts the startTime of the timeline's children by a certain amount and optionally adjusts labels too.
    static member inline shiftChildren (amount: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).shiftChildren (amount, false, 0.0)
    /// Shifts the startTime of the timeline's children by a certain amount and optionally adjusts labels too.
    static member inline shiftChildren (amount: float, adjustLabels: bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).shiftChildren (amount, adjustLabels, 0.0)
    /// Shifts the startTime of the timeline's children by a certain amount and optionally adjusts labels too.
    static member inline shiftChildren (amount: float, adjustLabels, ignoreBeforeTime: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).shiftChildren (amount, adjustLabels, ignoreBeforeTime)

    /// Gets the time at which the animation begins on its parent timeline (after any delay that was defined).
    static member inline startTime (timeline: ITimeline) = (timeline :?> ITimelineAPI).startTime ()
    /// Sets the time at which the animation begins on its parent timeline (after any delay that was defined).
    static member inline startTime (value: float) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).startTime (value)

    ///  Gets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline time (timeline: ITimeline) = (timeline :?> ITimelineAPI).time ()
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline time (value: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).time (value, false)
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline time (value: float, suppressEvents:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).time (value, suppressEvents)

    ///  Adds a gsap.to() tween to the end of the timeline (or elsewhere using the position parameter)
    /// - this is a convenience method that accomplishes exactly the same thing as add( gsap.to(...) ) but with less code.
    static member inline to_ (target: ITarget, vars: IVars) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).to_ (target, vars)
    ///  Adds a gsap.to() tween to the end of the timeline (or elsewhere using the position parameter)
    /// - this is a convenience method that accomplishes exactly the same thing as add( gsap.to(...) ) but with less code.
    static member inline to_ (target: ITarget, vars: IVars, position: IPosition) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).to_ (target, vars, position)

    ///  Gets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline timeScale (timeline: ITimeline) = (timeline :?> ITimelineAPI).timeScale ()
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline timeScale (value: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).timeScale value

    /// Gets the total duration of the timeline in seconds including any repeats or repeatDelays.
    static member inline totalDuration (timeline: ITimeline) = (timeline :?> ITimelineAPI).timeScale ()
    /// Sets the total duration of the timeline in seconds including any repeats or repeatDelays.
    static member inline totalDuration (value: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).timeScale value

    /// Gets the timeline's total progress which is a value between 0 and 1 indicating the position of the virtual playhead (including repeats) 
    /// where 0 is at the beginning, 0.5 is at the halfway point, and 1 is at the end (complete).
    static member inline totalProgress (timeline: ITimeline) = (timeline :?> ITimelineAPI).totalProgress ()
    /// Sets the timeline's total progress which is a value between 0 and 1 indicating the position of the virtual playhead (including repeats) 
    /// where 0 is at the beginning, 0.5 is at the halfway point, and 1 is at the end (complete).
    static member inline totalProgress (value: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).totalProgress (value, false)
    /// Sets the timeline's total progress which is a value between 0 and 1 indicating the position of the virtual playhead (including repeats) 
    /// where 0 is at the beginning, 0.5 is at the halfway point, and 1 is at the end (complete).
    static member inline totalProgress (value: float, suppressEvents:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).totalProgress (value, suppressEvents)

    ///  Gets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline totalTime (timeline: ITimeline) = (timeline :?> ITimelineAPI).totalTime ()
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline totalTime (time: float) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).totalTime (time, false)
    /// Sets the local position of the playhead (essentially the current time), not including any repeats or repeatDelays.
    static member inline totalTime (time: float, suppressEvents:bool) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).totalTime (time, suppressEvents)

    /// Creates a linear tween that essentially scrubs the playhead from a particular time or label to another time or label and then stops.
    static member inline tweenFromTo (fromPosition: IPosition, toPosition: IPosition) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).tweenFromTo (fromPosition, toPosition)
    /// Creates a linear tween that essentially scrubs the playhead from a particular time or label to another time or label and then stops.
    static member inline tweenFromTo (fromPosition: IPosition, toPosition: IPosition, vars: IVars) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).tweenFromTo (fromPosition, toPosition, vars)

    // Creates a linear tween that essentially scrubs the playhead from a particular time or label to another time or label and then stops.
    static member inline tweenTo (position: IPosition) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).tweenTo (position)
    /// Creates a linear tween that essentially scrubs the playhead from a particular time or label to another time or label and then stops.
    static member inline tweenTo (position: IPosition, vars: IVars) = 
        fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).tweenTo (position, vars)

    /// Gets the timeline's yoyo state, where true causes the timeline to go back and forth, alternating backward and forward on each repeat.
    static member inline yoyo (timeline: ITimeline) = (timeline :?> ITimelineAPI).yoyo ()
    /// Sets the timeline's yoyo state, where true causes the timeline to go back and forth, alternating backward and forward on each repeat.
    static member inline yoyo (value: bool) = fun (timeline: ITimeline) -> (timeline :?> ITimelineAPI).yoyo value

    static member inline useEffect (effectName: string, target: ITarget) = 
        fun (timeline: ITimeline) -> Interop.createTimelineUseEffect timeline effectName target

    static member inline useEffect (effectName: string, target: ITarget, overrides: IVars) = 
        fun (timeline: ITimeline) -> Interop.createTimelineUseEffectWithOverrides timeline effectName target overrides
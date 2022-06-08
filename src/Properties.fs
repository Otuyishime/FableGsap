namespace FableGsap

open Fable.Core

/// <summary>
/// Describes GSAP parameters
/// </summary>
[<Erase>]
type prop = 
    /// <summary>
    /// The object(s) whose properties you want to animate. This can be selector text like ".class", "#id", etc. 
    /// (GSAP uses document.querySelectorAll() internally) or it can be direct references to elements,  generic objects, or even an array of objects
    /// </summary>
    /// /// <param name='targets'>List of elements to animate.</param>
    static member inline target (targets: string seq) =
        if (Seq.isEmpty targets) then failwith "Missing Targets"
        Interop.makeAnimationTarget (targets |> Seq.reduce (fun s1 s2 -> s1 + "," + s2))

    /// <summary>
    /// The object(s) whose properties you want to animate. This can be selector text like ".class", "#id", etc. 
    /// (GSAP uses document.querySelectorAll() internally) or it can be direct references to elements,  generic objects, or even an array of objects
    /// </summary>
    /// /// <param name='targets'>List of elements to animate.</param>
    static member inline target (targets: Browser.Types.Element seq) =
        if (Seq.isEmpty targets) then failwith "Missing Targets" 
        Interop.makeAnimationTarget targets

    /// <summary>
    /// The object(s) whose properties you want to animate. This can be selector text like ".class", "#id", etc. 
    /// (GSAP uses document.querySelectorAll() internally) or it can be direct references to elements,  generic objects, or even an array of objects
    /// </summary>
    /// /// <param name='targets'>List of elements to animate.</param>
    static member inline target (targets: obj seq) =
        if (Seq.isEmpty targets) then failwith "Missing Targets" 
        Interop.makeAnimationTarget targets

    /// <summary>
    /// All the properties/values you want to animate, along with any special properties like ease, duration, delay, or onComplete
    /// </summary>
    /// <param name='vars'>List of elements to animate.</param>
    static member inline vars (vars: IVar seq) = Interop.makeAnimationVars vars

    /// <summary>
    /// GSAP’s settings that aren't Tween-specific, like autoSleep, force3D, and units.
    /// </summary>
    /// <param name='configs'>List of GSAP settings.</param>
    static member inline configs (configs: IConfig seq) = Interop.makeConfigVars configs

/// <summary>
/// Describes applicable config parameters.
/// </summary>
[<Erase>]
type config =
    /// <summary>
    /// Sets the number of frames that should elapse between internal checks to see if GSAP should power-down 
    /// the internal ticker to conserve system resources and battery life on mobile devices. The default is 120 (about every 2 seconds).
    /// </summary>
    /// <param name='value'>Number of frames</param>
    static member inline autoSleep (value: int) = Interop.makeConfigVar "autoSleep" value

    /// <summary>
    /// GSAP automatically attempts to maximize rendering performance by applying transforms with 3D components like translate3d() 
    /// instead of translate() during The animation to activate GPU acceleration, and then switches back to the 2D variant 
    /// at the end (if possible) to conserve GPU memory.
    /// </summary>
    /// <param name='value'>False disables the behavior. True will force all transform-related tweens to use the 3D component and NOT switch back to 2D at the end of the tween.</param>
    static member inline force3D (value: bool) = Interop.makeConfigVar "force3D" value

    /// <summary>
    /// By default, GSAP will throw a warning when attempting to tween elements that don't exist (are null).
    /// </summary>
    /// <param name='value'> You can suppress this warning by setting `nullTargetWarn: false`.</param>
    static member inline nullTargetWarn (value: bool) = Interop.makeConfigVar "nullTargetWarn" value

    /// <summary>
    /// By default, GSAP will throw a warning when attempting to tween elements that don't exist (are null).
    /// </summary>
    /// <param name='value'> You can suppress this warning by setting `nullTargetWarn: false`.</param>
    static member inline trialWarn (value: bool) = Interop.makeConfigVar "trialWarn" value

    /// <summary>
    /// Set the default CSS unit to be used for various properties when no unit is provided.
    /// </summary>
    /// <param name='units'>List of units.</param>
    static member inline units (units: IUnit seq) = Interop.makeConfigVar "units" (Interop.makeObjectFromList units)

[<Erase>]
type effect =
    static member inline name (value: string) = Interop.makeEffectProp "name" value
    static member inline defaults (vars: IVar seq) = Interop.makeEffectProp "defaults" (Interop.makeAnimationVars vars)
    static member inline extendTimeline (value: bool) = Interop.makeEffectProp "extendTimeline" value
    static member inline callBack (effect: (ITarget * IVar) -> ITween) = Interop.makeEffectProp "effect" effect
    static member inline getConfigValue (config: obj) (name: string) = Interop.makeGetConfigValue config name


/// <summary>
/// Describes GSAP parameters
/// </summary>
module prop =

    /// <summary>
    /// Describes applicable position parameters.
    /// </summary>
    [<Erase>]
    type position = 
        static member inline absolute (value: float) =
            Interop.makeAnimationPosition value
        /// <summary>
        /// Postion at a label. Converted into "value"
        /// </summary>
        /// <param name='value'>Label name</param>
        static member inline label (value: string) =
            Interop.makeAnimationPosition value

        /// <summary>
        /// Postion relative to the end of a timeline allowing for gaps. Converted to "+=seconds"
        /// </summary>
        /// <param name='seconds'>Gap length in seconds</param>
        static member inline gap (seconds: float) =
            Interop.makeAnimationPosition (sprintf "+=%f" seconds)

        /// <summary>
        /// Postion relative to the end of a timeline allowing for overlaps. Converted to "-=seconds"
        /// </summary>
        /// <param name='seconds'>Overlapping seconds</param>
        static member inline overlap (seconds: float) =
            Interop.makeAnimationPosition (sprintf "-=%f" seconds)

        /// <summary>
        /// Postion relative to a label. Converted to "label+=seconds"
        /// </summary>
        /// <param name='label'>Label name</param>
        /// <param name='seconds'>Length in seconds</param>
        static member inline relativeToLabel (label: string, seconds: float) =
            Interop.makeAnimationPosition (sprintf "%s+=%f" label seconds)

        /// <summary>
        /// position relative to the most recently added animation. 
        /// Insert at the START of the most recently added animation. Converted to "less than sign" 
        /// </summary>
        static member inline startOfLastAddedAnimation () =
            Interop.makeAnimationPosition "<"

        /// <summary>
        /// position relative to the most recently added animation. 
        /// Insert at the END of the most recently added animation. Converted to "less than sign" 
        /// </summary>
        static member inline endOfLastAddedAnimation () =
            Interop.makeAnimationPosition ">"

        /// <summary>
        /// position relative to the previously added animation. 
        /// Insert seconds after the START of the most recently added animation. Converted to "less than sign seconds"
        /// </summary>
        /// <param name='seconds'>Wait in seconds</param>
        static member inline afterStartOfLastAddedAnimation (seconds: float) =
            Interop.makeAnimationPosition (sprintf "<%f" seconds)

        /// <summary>
        /// position relative to the previously added animation. 
        /// Insert seconds before the START of the most recently added animation. Converted to "less than sign -seconds"
        /// </summary>
        /// <param name='seconds'>Wait in seconds</param>
        static member inline beforeStartOfLastAddedAnimation (seconds: float) =
            Interop.makeAnimationPosition (sprintf "<-%f" seconds)

        /// <summary>
        /// position relative to the previously added animation. 
        /// Insert seconds after the END of the most recently added animation. Converted to "greater than sign -seconds"
        /// </summary>
        /// <param name='seconds'>Wait in seconds</param>
        static member inline afterEndOfLastAddedAnimation (seconds: float) =
            Interop.makeAnimationPosition (sprintf ">%f" seconds)

        /// <summary>
        /// position relative to the previously added animation. 
        /// Insert seconds before the END of the most recently added animation. Converted to "greater than sign -seconds"
        /// </summary>
        /// <param name='seconds'>Wait in seconds</param>
        static member inline beforeEndOfLastAddedAnimation (seconds: float) =
            Interop.makeAnimationPosition (sprintf ">-%f" seconds)

/// <summary>
/// Describes gsap var.
/// </summary>
[<Erase>]
type var =
    static member inline id (value: string) = Interop.makeAnimationVar "id" value

    static member inline duration (value: int) = Interop.makeAnimationVar "duration" value
    static member inline duration (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "duration" setterFunc
    static member inline duration (cssUnit: _unit) = Interop.makeConfigUnit "duration" cssUnit
    static member inline duration () = "x"

    static member inline rotation (value: int) = Interop.makeAnimationVar "rotation" value
    static member inline rotation (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "rotation" setterFunc
    static member inline rotation (cssUnit: _unit) = Interop.makeConfigUnit "rotation" cssUnit
    static member inline rotation () = "rotation"

    static member inline opacity (value: float) = Interop.makeAnimationVar "opacity" value
    static member inline opacity (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "opacity" setterFunc
    static member inline opacity (cssUnit: _unit) = Interop.makeConfigUnit "opacity" cssUnit
    static member inline opacity () = "opacity"

    static member inline autoAlpha (value: float) = Interop.makeAnimationVar "autoAlpha" value
    static member inline autoAlpha (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "autoAlpha" setterFunc
    static member inline autoAlpha (cssUnit: _unit) = Interop.makeConfigUnit "autoAlpha" cssUnit
    static member inline autoAlpha () = "autoAlpha"

    static member inline top (value: int) = Interop.makeAnimationVar "top" value
    static member inline top (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "top" setterFunc
    static member inline top (cssUnit: _unit) = Interop.makeConfigUnit "top" cssUnit
    static member inline top () = "top"

    static member inline right (value: int) = Interop.makeAnimationVar "right" value
    static member inline right (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "right" setterFunc
    static member inline right (cssUnit: _unit) = Interop.makeConfigUnit "right" cssUnit
    static member inline right () = "right"

    static member inline left (value: int) = Interop.makeAnimationVar "left" value
    static member inline left (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "left" setterFunc
    static member inline left (cssUnit: _unit) = Interop.makeConfigUnit "left" cssUnit
    static member inline left () = "left"

    static member inline bottom (value: int) = Interop.makeAnimationVar "bottom" value
    static member inline bottom (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "bottom" setterFunc
    static member inline bottom (cssUnit: _unit) = Interop.makeConfigUnit "bottom" cssUnit
    static member inline bottom () = "bottom"

    static member inline x (value: int) = Interop.makeAnimationVar "x" value
    static member inline x (value: string) = Interop.makeAnimationVar "x" value
    static member inline x (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "x" setterFunc
    static member inline x (cssUnit: _unit) = Interop.makeConfigUnit "x" cssUnit
    static member inline x () = "x"

    static member inline y (value: int) = Interop.makeAnimationVar "y" value
    static member inline y (value: string) = Interop.makeAnimationVar "y" value
    static member inline y (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "y" setterFunc
    static member inline y (cssUnit: _unit) = Interop.makeConfigUnit "y" cssUnit
    static member inline y () = "y"

    static member inline z (value: int) = Interop.makeAnimationVar "z" value
    static member inline z (value: string) = Interop.makeAnimationVar "z" value
    static member inline z (setterFunc: (int * 'element * 'element seq) -> int) = Interop.makeAnimationVar "z" setterFunc
    static member inline z (cssUnit: _unit) = Interop.makeConfigUnit "z" cssUnit
    static member inline z () = "z"

    static member inline xPercent (value: float) = Interop.makeAnimationVar "xPercent" value
    static member inline xPercent (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "xPercent" setterFunc
    static member inline xPercent () = "xPercent"

    static member inline yPercent (value: float) = Interop.makeAnimationVar "yPercent" value
    static member inline yPercent (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "yPercent" setterFunc
    static member inline yPercent () = "yPercent"

    static member inline transformOrigin (value: string) = Interop.makeAnimationVar "transformOrigin" value
    static member inline transformOrigin (setterFunc: (int * 'element * 'element seq) -> string) = Interop.makeAnimationVar "transformOrigin" setterFunc
    static member inline transformOrigin () = "transformOrigin"

    static member inline backgroundColor (value: string) = Interop.makeAnimationVar "backgroundColor" value
    static member inline backgroundColor (setterFunc: (int * 'element * 'element seq) -> string) = Interop.makeAnimationVar "backgroundColor" setterFunc
    static member inline backgroundColor () = "backgroundColor"

    static member inline stagger (value: float) = Interop.makeAnimationVar "stagger" value
    static member inline stagger (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "stagger" setterFunc
    static member inline stagger () = "stagger"

    static member inline scale (value: float) = Interop.makeAnimationVar "scale" value
    static member inline scale (cssUnit: _unit) = Interop.makeConfigUnit "scale" cssUnit
    static member inline scale (setterFunc: (int * 'element * 'element seq) -> float) = Interop.makeAnimationVar "scale" setterFunc
    static member inline scale () = "scale"

    static member inline immediateRender (value: bool) = Interop.makeAnimationVar "immediateRender" value
    static member inline data (value: obj) = Interop.makeAnimationVar "data" value
    static member inline inherit_ (value: bool) = Interop.makeAnimationVar "data" value
    static member inline lazy_ (value: bool) = Interop.makeAnimationVar "lazy" value

    static member inline overwrite (value: string) = Interop.makeAnimationVar "overwrite" value
    static member inline reversed (value: bool) = Interop.makeAnimationVar "reversed" value
    static member inline runBackwards (value: bool) = Interop.makeAnimationVar "runBackwards" value
    static member inline startAt (vars: IVar seq) =  Interop.makeAnimationVar "startAt" (Interop.makeObjectFromList vars)
    static member inline keyframes (vars: IVar seq seq) =
        vars 
        |> Seq.map (fun vars -> Interop.makeObjectFromList vars) 
        |> Seq.toArray
        |> Interop.makeAnimationVar "keyframes" 

    static member inline autoRemoveChildren (value: bool) = Interop.makeAnimationVar "autoRemoveChildren" value
    static member inline delay (value: float) = Interop.makeAnimationVar "delay" value
    static member inline paused (value: bool) = Interop.makeAnimationVar "paused" value
    static member inline repeat (value: int) = Interop.makeAnimationVar "repeat" value
    static member inline repeatDelay (value: int) = Interop.makeAnimationVar "repeatDelay" value
    static member inline repeatRefresh (value: bool) = Interop.makeAnimationVar "repeatRefresh" value
    static member inline smoothChildTiming (value: bool) = Interop.makeAnimationVar "smoothChildTiming" value
    static member inline yoyo (value: bool) = Interop.makeAnimationVar "yoyo" value
    
    // event listeners
    static member inline onComplete (handler: unit -> unit) = Interop.makeAnimationVar "onComplete" handler
    static member inline onInterrupt (handler: unit -> unit) = Interop.makeAnimationVar "onInterrupt" handler
    static member inline onRepeat (handler: unit -> unit) = Interop.makeAnimationVar "onRepeat" handler
    static member inline onReverseComplete (handler: unit -> unit) = Interop.makeAnimationVar "onReverseComplete" handler
    static member inline onStart (handler: unit -> unit) = Interop.makeAnimationVar "onStart" handler
    static member inline onUpdate (handler: unit -> unit) = Interop.makeAnimationVar "onUpdate" handler

    // Ease
    static member inline ease (value: string) = Interop.makeAnimationVar "ease" value
    static member inline defaults (vars: IVar seq) = Interop.makeAnimationVar "defaults" (Interop.makeObjectFromList vars)

    /// Custom var for missing css properties. Be careful and use this for animatable properties only.
    static member inline custom (cssProperty: string, value: int) =  Interop.makeAnimationVar cssProperty value
    /// Custom var for missing css properties. Be careful and use this for animatable properties only.
    static member inline custom (cssProperty: string, value: float) =  Interop.makeAnimationVar cssProperty value
    /// Custom var for missing css properties. Be careful and use this for animatable properties only.
    static member inline custom (cssProperty: string, value: string) =  Interop.makeAnimationVar cssProperty value

    static member inline custom (cssProperty: string, setterFunc: (int * 'element * 'element seq) -> float) =  Interop.makeAnimationVar cssProperty setterFunc
    static member inline custom (cssProperty: string, setterFunc: (int * 'element * 'element seq) -> int) =  Interop.makeAnimationVar cssProperty setterFunc
    static member inline custom (cssProperty: string, setterFunc: (int * 'element * 'element seq) -> string) =  Interop.makeAnimationVar cssProperty setterFunc
    
    // static member inline getVarNames (vars: ('T -> IVar) seq) = 
    //     vars
    //     |> Seq.map (fun v -> nameof v) 
    //     |> Seq.reduce (fun s1 s2 -> s1 + ", " + s2)

/// <summary>
/// Describes gsap var.
/// </summary>
module var =
    
    [<Erase>]
    type ease =
        static member inline none = Interop.makeAnimationVar "ease" "none"

        static member inline power1In = Interop.makeAnimationVar "ease" "power1.in"
        static member inline power1InOut = Interop.makeAnimationVar "ease" "power1.inOut"
        static member inline power1Out = Interop.makeAnimationVar "ease" "power1.out"

        static member inline power2In = Interop.makeAnimationVar "ease" "power2.in"
        static member inline power2InOut = Interop.makeAnimationVar "ease" "power2.inOut"
        static member inline power2Out = Interop.makeAnimationVar "ease" "power2.out"

        static member inline power3In = Interop.makeAnimationVar "ease" "power3.in"
        static member inline power3InOut = Interop.makeAnimationVar "ease" "power3.inOut"
        static member inline power3Out = Interop.makeAnimationVar "ease" "power3.out"

        static member inline power4In = Interop.makeAnimationVar "ease" "power4.in"
        static member inline power4InOut = Interop.makeAnimationVar "ease" "power4.inOut"
        static member inline power4Out = Interop.makeAnimationVar "ease" "power4.out"

        static member inline bounceIn = Interop.makeAnimationVar "ease" "bounce.in"
        static member inline bounceInOut = Interop.makeAnimationVar "ease" "bounce.inOut"
        static member inline bounceOut = Interop.makeAnimationVar "ease" "bounce.out"

        static member inline circIn  = Interop.makeAnimationVar "ease" "circ.in"
        static member inline circInOut  = Interop.makeAnimationVar "ease" "circ.in"
        static member inline circOut  = Interop.makeAnimationVar "ease" "circ.in"

        static member inline expoIn  = Interop.makeAnimationVar "ease" "expo.in"
        static member inline expoInOut  = Interop.makeAnimationVar "ease" "expo.in"
        static member inline expoOut  = Interop.makeAnimationVar "ease" "expo.in"

        static member inline sineIn = Interop.makeAnimationVar "ease" "sine.in"
        static member inline sineInOut = Interop.makeAnimationVar "ease" "sine.inOut"
        static member inline sineOut = Interop.makeAnimationVar "ease" "sine.out"

        static member inline backIn (value: float) = Interop.makeAnimationVar "ease" (sprintf "back.in(%f)" value)
        static member inline backInOut (value: float) = Interop.makeAnimationVar "ease" (sprintf "back.inOut(%f)" value)
        static member inline backOut (value: float) = Interop.makeAnimationVar "ease" (sprintf "back.out(%f)" value)

        static member inline steps (value: int) = Interop.makeAnimationVar "ease" (sprintf "steps(%d)" value)

        static member inline elastic (a: float, b: float) = Interop.makeAnimationVar "ease" (sprintf "elastic(%f, %f)" a b)
        static member inline elasticIn (a: float, b: float) = Interop.makeAnimationVar "ease" (sprintf "elastic.in(%f, %f)" a b)
        static member inline elasticInOut (a: float, b: float) = Interop.makeAnimationVar "ease" (sprintf "elastic.inOut(%f, %f)" a b)
        static member inline elasticOut (a: float, b: float) = Interop.makeAnimationVar "ease" (sprintf "elastic.out(%f, %f)" a b)

        static member inline function_ (easeFunction: float -> float) = Interop.makeAnimationVar "ease" easeFunction

    [<Erase>]
    type yoyoEase =
        static member inline none = Interop.makeAnimationVar "yoyoEase" "none"

        static member inline power1In = Interop.makeAnimationVar "yoyoEase" "power1.in"
        static member inline power1InOut = Interop.makeAnimationVar "yoyoEase" "power1.inOut"
        static member inline power1Out = Interop.makeAnimationVar "yoyoEase" "power1.out"

        static member inline power2In = Interop.makeAnimationVar "yoyoEase" "power2.in"
        static member inline power2InOut = Interop.makeAnimationVar "yoyoEase" "power2.inOut"
        static member inline power2Out = Interop.makeAnimationVar "yoyoEase" "power2.out"

        static member inline power3In = Interop.makeAnimationVar "yoyoEase" "power3.in"
        static member inline power3InOut = Interop.makeAnimationVar "yoyoEase" "power3.inOut"
        static member inline power3Out = Interop.makeAnimationVar "yoyoEase" "power3.out"

        static member inline power4In = Interop.makeAnimationVar "yoyoEase" "power4.in"
        static member inline power4InOut = Interop.makeAnimationVar "yoyoEase" "power4.inOut"
        static member inline power4Out = Interop.makeAnimationVar "yoyoEase" "power4.out"

        static member inline bounceIn = Interop.makeAnimationVar "yoyoEase" "bounce.in"
        static member inline bounceInOut = Interop.makeAnimationVar "yoyoEase" "bounce.inOut"
        static member inline bounceOut = Interop.makeAnimationVar "yoyoEase" "bounce.out"

        static member inline circIn  = Interop.makeAnimationVar "yoyoEase" "circ.in"
        static member inline circInOut  = Interop.makeAnimationVar "yoyoEase" "circ.in"
        static member inline circOut  = Interop.makeAnimationVar "yoyoEase" "circ.in"

        static member inline expoIn  = Interop.makeAnimationVar "yoyoEase" "expo.in"
        static member inline expoInOut  = Interop.makeAnimationVar "yoyoEase" "expo.in"
        static member inline expoOut  = Interop.makeAnimationVar "yoyoEase" "expo.in"

        static member inline sineIn = Interop.makeAnimationVar "yoyoEase" "sine.in"
        static member inline sineInOut = Interop.makeAnimationVar "yoyoEase" "sine.inOut"
        static member inline sineOut = Interop.makeAnimationVar "yoyoEase" "sine.out"

        static member inline backIn (value: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "back.in(%f)" value)
        static member inline backInOut (value: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "back.inOut(%f)" value)
        static member inline backOut (value: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "back.out(%f)" value)

        static member inline steps (value: int) = Interop.makeAnimationVar "yoyoEase" (sprintf "steps(%d)" value)

        static member inline elasticIn (a: float, b: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "elastic.in(%f, %f)" a b)
        static member inline elasticInOut (a: float, b: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "elastic.inOut(%f, %f)" a b)
        static member inline elasticOut (a: float, b: float) = Interop.makeAnimationVar "yoyoEase" (sprintf "elastic.out(%f, %f)" a b)

        static member inline false_ = Interop.makeAnimationVar "yoyoEase" false
        static member inline true_ = Interop.makeAnimationVar "yoyoEase" true

    [<Erase>]
    type overwrite =
        static member inline auto = Interop.makeAnimationVar "overwrite" "auto"
        static member inline false_ = Interop.makeAnimationVar "overwrite" false
        static member inline true_ = Interop.makeAnimationVar "overwrite" true

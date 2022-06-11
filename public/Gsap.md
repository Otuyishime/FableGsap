### Gsap - Methods
---

#### Methods
#### gsap.config()
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.config())
```fs
Gsap.config [
    config.autoSleep 60
    config.force3D false
    config.nullTargetWarn false
    config.trialWarn false
    config.units [
        var.left _unit.Percentage
        var.top _unit.Percentage
        var.rotation _unit.Rad
    ]
]
```
#### gsap.defaults()
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.defaults())
```fs
Gsap.defaults [
    var.ease.power2In
    var.duration 1
    var.delay 0.5
]
```

#### gsap.delayedCall()
Returns: `Tween`
To make things simple, the callback function signature is `unit -> unit`.
You can use a higher order funtion to pass as arguments as you want. 
Check example below.
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.delayedCall())
```fs
let seconds = 5 
let someFunction seconds arg1 arg2 = 
    fun () -> 
        Fable
            .Core
            .JS
            .console
            .log (sprintf "Called after %d seconds with arguments %s and %s" seconds arg1 arg2)

let tween = 
    Gsap.delayedCall (
        seconds, 
        someFunction seconds "arg1" "arg2"
    )
```

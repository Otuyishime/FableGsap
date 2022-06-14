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
        var.left.units _unit.Percentage
        var.top.units _unit.Percentage
        var.rotation.units _unit.Rad
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
You can use a higher order funtion to pass as many arguments as you want. 
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

#### gsap.exportRoot()
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.exportRoot())
```fs
let timeline = Gsap.exportRoot ()
```

#### gsap.from()
Returns: `Tween`
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.from())
```fs
let tween = 
    Gsap.from (
        prop.target [ 
            ".class" 
        ],
        prop.vars [
            var.opacity 0.0
            var.y 100
            var.duration 1
            var.custom ("borderRadius", 100)
        ]
    )
```

#### gsap.fromTo()
Returns: `Tween`
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.fromTo())
```fs
let tween = 
    Gsap.fromTo (
        prop.target [ 
            ".class" 
        ],
        prop.vars [
            var.opacity 0.0
            var.autoAlpha 0.0
            var.y 100
        ],
        prop.vars [
            var.opacity 0.5
            var.autoAlpha 0.5
            var.y 300
            var.duration 1
        ]
    )
```

#### gsap.getById()
Returns: `Tween` or `Timeline`
To make things simple, we have two function `getTweenById` and `getTimelineById`.
`getTweenById` has signature `string -> Option<Tween>`
`getTimelineById` has signature `string -> Option<Timeline>`
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.getById())
```fs
let tween = 
    Gsap.from (
        prop.target [ 
            ".class" 
        ],
        prop.vars [
            var.id "someTween"
            var.opacity 0.0
            var.y 100
            var.duration 1
            var.custom ("borderRadius", 100)
        ]
    )

let sameTween = Gsap.getTweenById "someTween"
```

#### gsap.getProperty()
Returns the value of the property requested as a number (if possible) unless you specify a unit in which case it will be added to the number, making it a string. 
For more information, check [Official Documentation](https://greensock.com/docs/v3/GSAP/gsap.getProperty())
```fs
let value = 
    Gsap.getProperty (
        prop.target [
            box target
        ],
        var.right
    )

// Or pass css units
let value = 
    Gsap.getProperty (
        prop.target [
            box target
        ],
        var.right,
        _unit.Em
    ) 
```

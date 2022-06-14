namespace FableGsap

[<RequireQualifiedAccess>]
module GsapExamples = 

    let example_config = 
        fun () ->
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

    let example_defaults = 
        fun () ->
            Gsap.defaults [
                var.ease.power2In
                var.duration 1
                var.delay 0.5
            ]

    let example_delayedCall=
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
        tween

    let example_exportRoot = 
        fun () -> Gsap.exportRoot ()

    let example_from = 
        fun target -> 
            Gsap.from (
                prop.target [ 
                    box target 
                ],
                prop.vars [
                    var.opacity 0.0
                    var.y 100
                    var.duration 1
                    var.custom ("borderRadius", 100)
                ]
            )

    let example_fromTo = 
        fun target -> 
            Gsap.fromTo (
                prop.target [ 
                    box target 
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

    let example_getById = 
        fun target -> 
            Gsap.to_ (
                prop.target [ 
                    box target 
                ],
                prop.vars [
                    var.id "someTween"
                    var.x 100
                    var.duration 1
                ]
            )
            |> ignore

            Gsap.getTweenById "someTween"

    let example_getProperty = 
        fun target ->
            Gsap.getProperty (
                prop.target [
                    box target
                ],
                var.right
            )


            // Or pass css units
            // Gsap.getProperty (
            //     prop.target [
            //         box target
            //     ],
            //     var.right,
            //     _unit.Em
            // )

    let example_getTweensOf = 
        fun target ->
            Gsap.getTweensOf (
                prop.target [
                    box target
                ]
            )

    let example_isTweening = 
        fun target ->
            Gsap.isTweening (
                prop.target [
                    box target
                ]
            )

    let example_killTweensOf = 
        fun target ->
            Gsap.killTweensOf (
                prop.target [
                    box target
                ],
                [
                    var.x
                    var.duration
                    var.autoAlpha
                ]
            )

            // Or kill for all target properties
            // Gsap.getProperty (
            //     prop.target [
            //         box target
            //     ]
            // )

    let example_to = 
        fun target -> 
            Gsap.to_ (
                prop.target [ 
                    box target 
                ],
                prop.vars [
                    var.x 500
                    var.rotation 180
                    var.duration 1
                    var.ease.power2InOut
                    var.paused true
                    var.backgroundColor "red"
                    var.custom ("borderRadius", 100)
                ]
            )
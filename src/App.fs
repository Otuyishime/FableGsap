module App

open Fable.Core.JsInterop

open Browser.Dom
open FableGsap
open Fable.Core.JS



[<Measure>] type Rep
type Set = int<Rep>

let wSet = 10<Rep>


// Get a reference to our button and cast the Element to an HTMLButtonElement
let playButton = document.querySelector(".play-button") :?> Browser.Types.HTMLButtonElement
let pauseButton = document.querySelector(".pause-button") :?> Browser.Types.HTMLButtonElement
let reverseButton = document.querySelector(".reverse-button") :?> Browser.Types.HTMLButtonElement

let myGreenBox = document.querySelector(".green")
let myBlueBox = document.querySelector(".blue")
let randomObj = {| x = 10|}

//console.log Interop.gsapApi


// register effect
Gsap.registerEffect [
  effect.name "fadeSlideTo"
  effect.extendTimeline true
  effect.defaults [ 
    var.duration 2
    var.delay 0.5
  ]
  effect.callBack (
    fun (targets: ITarget, config: IVar) ->
      let newVars = [
        var.opacity 0.5
        var.x 300
        var.duration 4
        var.yoyo true
        var.repeat -1
        var.custom ("borderRadius", 100)
      ]
      console.log newVars
      Gsap.to_(targets, prop.vars newVars)
  )
]

Gsap.effects.execute (
  "fadeSlideTo", 
  prop.target [ box myBlueBox ]
)

//console.log (Gsap.effects.addAnything "a" "b")
// Gsap.registerEffect [
//   effect.name "fadeSlideFrom"
//   effect.extendTimeline true
//   effect.defaults [ 
//     var.duration 1
//   ]
//   effect.callBack (
//     fun (targets: ITarget, configs: IVar seq) ->
//       let newVars = configs |> Seq.append [
//         var.opacity 0.5
//         var.x 300
//         var.yoyo true
//         var.repeat -1
//       ]
//       Gsap.from(targets, prop.vars newVars)
//   )
// ]
// Gsap.registerEffect [
//   effect.name "fadeSlideFromTo"
//   effect.extendTimeline true
//   effect.defaults [ 
//     var.duration 1
//   ]
//   effect.callBack (
//     fun (targets: ITarget, configs: IVar seq) ->
//       let toVars = configs |> Seq.append [
//         var.opacity 0.5
//         var.x 600
//         var.yoyo true
//         var.repeat -1
//       ]
//       let fromVars = [
//         var.opacity 0.1
//         var.x 300
//       ]
//       Gsap.fromTo(targets, prop.vars fromVars, prop.vars toVars)
//   )
// ]

// use effect



let animation = 
    Gsap.to_ (
        prop.target [ 
            box myGreenBox
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
//console.log animation

playButton.onclick <- (fun e -> animation |> Tween.play |> ignore)
pauseButton.onclick <- (fun e -> animation |> Tween.pause |> ignore)
reverseButton.onclick <- (fun e -> animation |> Tween.reverse |> ignore)

// let tl = Gsap.timeline (prop.vars [])
// let t = tl |> Timeline.useEffect ("fadeSlideTo", prop.target [ box myBlueBox ], prop.vars [ var.duration 3 ])
// |> Timeline.useEffect ("fadeSlideTo", prop.target [ box myBlueBox ])
// |> Timeline.useEffect ("fadeSlideTo", prop.target [ box myBlueBox ], prop.vars [ var.duration 3 ])
// |> Timeline.useEffect ("fadeSlideFromTo", prop.target [ ".fadeSlideFromTo" ], prop.vars [])
// console.log ( prop.vars [ var.duration 3 ])
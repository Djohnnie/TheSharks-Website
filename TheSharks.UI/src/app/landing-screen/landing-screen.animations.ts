import { animate, state, style, transition, trigger } from "@angular/animations";

const speed = 1

export const LandinScreenAnimations = [
    trigger("landingScreen", [
        state(":leave", style({
            display: "none"
        })),
        state(":enter", style({
            display: "initial"
        })),
        transition("* <=> *", [
            animate(speed / 2 + "s")
        ])
    ]),
    trigger("overlay", [
        state("open", style({
            transform: "translateY(-100%)"
        })),
        state("closed", style({
            transform: "translateY(100%)"
        })),
        transition("closed => open", [
            animate(speed + "s")
        ])
    ])
]
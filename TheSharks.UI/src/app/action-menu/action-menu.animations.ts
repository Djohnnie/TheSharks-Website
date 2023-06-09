import { trigger, state, style, transition, animate, query, stagger, keyframes } from "@angular/animations";

export const ActionMenuAnimations = [
    trigger("openClose", [
        state("open", style({
            transform: "rotate(135deg)"
        })),
        state("closed", style({
            transform: "rotate(0deg)"
        })),
        transition("* => *", [
            animate("0.2s")
        ]),
    ]),
    trigger("buttons", [
        transition('* => *', [
            query(':enter', style({ opacity: 0 }), { optional: true }),

            query(':enter', stagger('40ms',
                [
                    animate('200ms cubic-bezier(0.4, 0.0, 0.2, 1)',
                        keyframes(
                            [
                                style({
                                    opacity: 0,
                                    transform: 'translateY(10px)'
                                }),
                                style({
                                    opacity: 1,
                                    transform: 'translateY(0)'
                                }),
                            ]
                        )
                    )
                ]
            ), { optional: true }),

            query(':leave', stagger('40ms',
                animate('200ms cubic-bezier(0.4, 0.0, 0.2, 1)',
                    keyframes([
                        style({
                            opacity: 1
                        }),
                        style({
                            opacity: 0
                        }),
                    ])
                )), { optional: true }
            )
        ])
    ])
]
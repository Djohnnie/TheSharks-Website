import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UntypedFormControl } from '@angular/forms';
import { loadFull } from 'tsparticles';
import { RecursivePartial, IOptions, Engine, tsParticles, HoverMode, ClickMode } from 'tsparticles-engine';
import { LandinScreenAnimations } from './landing-screen.animations';


@Component({
    selector: 'app-landing-screen',
    templateUrl: './landing-screen.component.html',
    styleUrls: ['./landing-screen.component.scss'],
    animations: [LandinScreenAnimations]
})
export class LandingScreenComponent implements OnInit {
    id = "tsparticles";

    particlesOptionsSmall: RecursivePartial<IOptions> = {
        fpsLimit: 60,
        autoPlay: true,
        pauseOnBlur: true,
        pauseOnOutsideViewport: true,
        background: {
            color: "#26C6DA"
        },
        particles: {
            color: { value: "#B2EBF2" },
            move: {
                enable: true,
                speed: 1,
                straight: true,
                gravity: {
                    enable: true,
                    maxSpeed: 2,
                    inverse: true
                }
            },
            number: { value: 50 },
            opacity: {
                value: 0.4,
            },
            shape: {
                type: "circle"
            },
            size: {
                value: { min: 5, max: 10 }
            },
            wobble: {
                enable: true,
                distance: 5,
                speed: 10
            }
        }
    };

    showAgain = new UntypedFormControl(false)
    entered = false
    particles = new UntypedFormControl(true)

    constructor(
        private _location: Location) { }

    ngOnInit(): void {
        console.log(this._location.path())
        if( this._location.path().includes("reset-password") || this._location.path().includes("privacy"))
        {
            this.entered = true
        }

        if (sessionStorage.getItem("landingscreen") === "true") 
        {
            this.entered = true
        }

        let bubbles = localStorage.getItem("bubbles")
        if(bubbles === null) bubbles = "true"
        this.particles.setValue(bubbles === "true")        
        
        sessionStorage.setItem("landingscreen", "true")
    }

    async particlesInit(engine: Engine): Promise<any> {
        await loadFull(engine)
    }

    enter() {
        this.entered = true
        localStorage.setItem("nolandingscreen", this.showAgain.value)
    }

    toggleBubbles() {
        if (this.particles.value) localStorage.setItem("bubbles", "true")
        else localStorage.setItem("bubbles", "false")
    }
}

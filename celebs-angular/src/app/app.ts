import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ParticleBackgroundComponent } from './components/particle-background/particle-background.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ParticleBackgroundComponent],
  template: `
    <app-particle-background></app-particle-background>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  title = 'celeb-front';
}

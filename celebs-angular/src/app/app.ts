import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeToggleComponent } from './components/theme-toggle/theme-toggle.component';
import { ThemeService } from './services/theme.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ThemeToggleComponent],
  template: `
    <!-- Royal Background with Animated Gradient -->
    <div class="royal-bg min-h-screen">
      <!-- Theme Toggle - Fixed position top right -->
      <div class="fixed top-4 right-4 z-50">
        <app-theme-toggle></app-theme-toggle>
      </div>
      
      <router-outlet></router-outlet>
    </div>
  `
})
export class AppComponent {
  title = 'celeb-front';
  private themeService = inject(ThemeService);
  
  // Initialize theme service
  constructor() {
    // Theme service will auto-initialize
  }
}

import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeService } from '../../services/theme.service';

@Component({
  selector: 'app-theme-toggle',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './theme-toggle.component.html',
  styleUrls: ['./theme-toggle.component.css']
})
export class ThemeToggleComponent {
  private themeService = inject(ThemeService);
  
  // Expose theme service signals to template
  theme = this.themeService.theme;
  isDark = this.themeService.isDark;
  isLight = this.themeService.isLight;

  toggleTheme(): void {
    this.themeService.toggleTheme();
  }
}

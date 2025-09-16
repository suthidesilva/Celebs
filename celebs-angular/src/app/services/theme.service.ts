import { Injectable, signal } from '@angular/core';

export type Theme = 'light' | 'dark';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly THEME_KEY = 'celeb-app-theme';
  private readonly THEME_ATTRIBUTE = 'data-theme';
  
  // Signal for reactive theme state
  public theme = signal<Theme>('light');
  
  constructor() {
    this.initializeTheme();
  }

  private initializeTheme(): void {
    // Check localStorage first, then system preference
    const savedTheme = localStorage.getItem(this.THEME_KEY) as Theme;
    const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    
    const initialTheme = savedTheme || (systemPrefersDark ? 'dark' : 'light');
    this.setTheme(initialTheme);
  }

  public toggleTheme(): void {
    const newTheme = this.theme() === 'light' ? 'dark' : 'light';
    this.setTheme(newTheme);
  }

  public setTheme(theme: Theme): void {
    this.theme.set(theme);
    document.documentElement.setAttribute(this.THEME_ATTRIBUTE, theme);
    
    // Also set the 'dark' class for Tailwind CSS
    if (theme === 'dark') {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
    
    localStorage.setItem(this.THEME_KEY, theme);
  }

  public isDark(): boolean {
    return this.theme() === 'dark';
  }

  public isLight(): boolean {
    return this.theme() === 'light';
  }
}

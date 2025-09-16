/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  darkMode: 'class', // Enable class-based dark mode
  theme: {
    extend: {
      colors: {
        // Custom dark mode colors
        dark: {
          50: '#f8fafc',
          100: '#f1f5f9',
          200: '#e2e8f0',
          300: '#cbd5e1',
          400: '#94a3b8',
          500: '#64748b',
          600: '#475569',
          700: '#334155',
          800: '#1e293b',
          900: '#0f172a',
        }
      },
      animation: {
        'sun-pulse': 'sunPulse 2s ease-in-out infinite',
        'moon-glow': 'moonGlow 2s ease-in-out infinite',
        'theme-transition': 'themeTransition 0.3s ease-in-out',
      },
      keyframes: {
        sunPulse: {
          '0%, 100%': { transform: 'scale(1) rotate(0deg)' },
          '50%': { transform: 'scale(1.1) rotate(180deg)' },
        },
        moonGlow: {
          '0%, 100%': { 
            transform: 'scale(1) rotate(0deg)',
            filter: 'drop-shadow(0 0 0px currentColor)'
          },
          '50%': { 
            transform: 'scale(1.05) rotate(0deg)',
            filter: 'drop-shadow(0 0 8px currentColor)'
          },
        },
        themeTransition: {
          '0%': { opacity: '0.8' },
          '100%': { opacity: '1' },
        },
      },
    },
  },
  plugins: [],
};

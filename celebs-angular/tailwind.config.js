/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  darkMode: 'class', // Enable class-based dark mode
  theme: {
    extend: {
      colors: {
        // Royal Purple & Black Theme
        royal: {
          50: '#faf5ff',
          100: '#f3e8ff',
          200: '#e9d5ff',
          300: '#d8b4fe',
          400: '#c084fc',
          500: '#a855f7',
          600: '#9333ea',
          700: '#7c3aed',
          800: '#6b21a8',
          900: '#581c87',
          950: '#3b0764',
        },
        midnight: {
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
          950: '#020617',
        },
        gold: {
          50: '#fffbeb',
          100: '#fef3c7',
          200: '#fde68a',
          300: '#fcd34d',
          400: '#fbbf24',
          500: '#f59e0b',
          600: '#d97706',
          700: '#b45309',
          800: '#92400e',
          900: '#78350f',
        }
      },
      backgroundImage: {
        'royal-gradient': 'linear-gradient(135deg, #581c87 0%, #3b0764 50%, #1e293b 100%)',
        'purple-gradient': 'linear-gradient(135deg, #a855f7 0%, #7c3aed 50%, #581c87 100%)',
        'glass-gradient': 'linear-gradient(135deg, rgba(168, 85, 247, 0.1) 0%, rgba(124, 58, 237, 0.05) 100%)',
        'gold-gradient': 'linear-gradient(135deg, #fbbf24 0%, #f59e0b 100%)',
      },
      boxShadow: {
        'royal-glow': '0 0 20px rgba(168, 85, 247, 0.3)',
        'purple-glow': '0 0 15px rgba(124, 58, 237, 0.4)',
        'gold-glow': '0 0 15px rgba(251, 191, 36, 0.3)',
        'glass': '0 8px 32px rgba(0, 0, 0, 0.1)',
        'glass-dark': '0 8px 32px rgba(0, 0, 0, 0.3)',
        'inner-glow': 'inset 0 1px 0 rgba(255, 255, 255, 0.1)',
      },
      backdropBlur: {
        'xs': '2px',
      },
      animation: {
        'royal-pulse': 'royalPulse 2s ease-in-out infinite',
        'purple-glow': 'purpleGlow 2s ease-in-out infinite',
        'gold-shimmer': 'goldShimmer 3s ease-in-out infinite',
        'float': 'float 6s ease-in-out infinite',
        'glow-pulse': 'glowPulse 2s ease-in-out infinite',
        'theme-transition': 'themeTransition 0.3s ease-in-out',
      },
      keyframes: {
        royalPulse: {
          '0%, 100%': { 
            transform: 'scale(1)',
            boxShadow: '0 0 20px rgba(168, 85, 247, 0.3)'
          },
          '50%': { 
            transform: 'scale(1.05)',
            boxShadow: '0 0 30px rgba(168, 85, 247, 0.5)'
          },
        },
        purpleGlow: {
          '0%, 100%': { 
            boxShadow: '0 0 15px rgba(124, 58, 237, 0.4)'
          },
          '50%': { 
            boxShadow: '0 0 25px rgba(124, 58, 237, 0.6)'
          },
        },
        goldShimmer: {
          '0%': { 
            backgroundPosition: '-200% 0'
          },
          '100%': { 
            backgroundPosition: '200% 0'
          },
        },
        float: {
          '0%, 100%': { transform: 'translateY(0px)' },
          '50%': { transform: 'translateY(-10px)' },
        },
        glowPulse: {
          '0%, 100%': { 
            filter: 'brightness(1) drop-shadow(0 0 5px currentColor)'
          },
          '50%': { 
            filter: 'brightness(1.2) drop-shadow(0 0 15px currentColor)'
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

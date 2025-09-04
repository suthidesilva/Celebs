# celeb-ui (React Frontend)

A modern and responsive React + TypeScript frontend for displaying and managing celebrities.

## Features

- View celebrities in grid or list view
- Sort celebrities by name, gender, or date
- Delete and reset operations with toast notifications
- Drag-and-drop reordering (optional, configurable)
- Responsive UI using Tailwind CSS
- Animated gradient background for a modern look
- Centralized configuration for easy customization
- Accessible and keyboard-friendly UI

## Setup

```bash
cd celeb-ui
npm install
npm run dev
```

Ensure the backend API is running on the port defined in `config.ts`.

## Configuration

Edit `src/config.ts` to set:

- `apiBaseUrl` — Backend API URL
- `appName` — Application name (used in UI and browser tab)
- `defaultSortBy` — Default sort field (`'name'`, `'gender'`, or `'date'`)
- `defaultViewMode` — `'grid'` or `'list'`
- `enableDragAndDrop` — Enable/disable drag-and-drop
- `enableDarkMode` — Enable/disable dark mode (future feature)
- `primaryColor`, `secondaryColor` — Theme colors
- `appLogoUrl` — Logo path

## Tech Stack

- React
- TypeScript
- Tailwind CSS
- react-hot-toast

## Deployment

Use Vercel, Netlify, or your preferred static hosting provider to deploy the frontend.

## Project Structure

```
src/
  api/           # API calls
  components/    # Reusable UI components (CelebCard, CelebToolbar, etc.)
  models/        # TypeScript types/interfaces
  pages/         # Page-level components (ListCelebsPage)
  config.ts      # Central configuration
  App.tsx        # App entry point
```

## Backend

This frontend expects a compatible backend API (such as the provided .NET Core Web API).  
Make sure the backend ```CelebApi``` is running.


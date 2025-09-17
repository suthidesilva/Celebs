# ⭐ Celebrity Full-Stack Application ⭐

![pic2.png](https://github.com/suthidesilva/Celebs/blob/master/pic2.png)
This project is a professional-grade full-stack web application that scrapes data from IMDb, stores it in JSON files, and presents a user-friendly UI for managing a list of celebrities.

The system is designed to demonstrate modern software engineering practices, including multi-project structuring, API development, parallel scraping, UI configuration, and clean coding principles.

---

## 🧱 Architecture Overview

<br>/Celebs
<br>│
<br>├── celebs-api/ # ASP.NET Core Web API (.NET 8) – Serves celeb data and supports CRUD
<br>├── ImdbScraper/ # Console app – Scrapes and saves celeb data from IMDb to JSON
<br>├── celebs-react or celebs-angular/ # React or Angular + TypeScript + Tailwind CSS frontend – Interactive user interface
<br>└── Data/ # Central JSON storage – Shared by Scraper and API

---

## 🔧 Core Features

### ✅ Backend (.NET API - `celebs-api`)
- Exposes a RESTful API with endpoints to get, delete, reset, update, and insert celebrities.
- Uses `CelebService` and `CelebRepository` with a **thread-safe in-memory caching strategy**.
- File-based JSON persistence (no DB for simplicity).
- Config-driven (`appsettings.json`) with production-ready exception handling middleware.
- CORS enabled for local frontend development.
- Includes unit tests.


### ✅ UI (`celebs-angular`)
- Built with **Angular**, **TypeScript**, and **Tailwind CSS**.
- Clean, modern design with a **fully responsive layout**.
- Supports viewing, **deleting**, and **resetting** the celebrity list.
- Live feedback via **toast notifications**.
- Easy config via `config.ts` (API URL, app name, drag & drop settings, etc.).
- Built for **quick onboarding and extendability**.

### ✅ UI (`celebs-react`)
- Built with **React**, **TypeScript**, and **Tailwind CSS**.
- Clean, modern design with a **fully responsive layout**.
- Supports viewing, **deleting**, and **resetting** the celebrity list.
- Live feedback via **toast notifications**.
- Easy config via `config.ts` (API URL, app name, drag & drop settings, etc.).
- Built for **quick onboarding and extendability**.
----

### ✅ Scraper (`ImdbScraper`)
- Extracts top 100 celebrities from IMDb using HtmlAgilityPack.
- Fetches detailed info: **name, profile URL, birth date, gender, role(s), and profile image**.
- Uses a configurable level of **parallelism** for performance tuning.
- Saves output to `SharedData/celebs.original.json` and `celebs.json`.

---

## 🚀 Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/Celebs
cd Celebs

2. Run the Backend API

cd ../celebs-api
dotnet run
API available at: http://localhost:5555/api/celebs/v1/celebs

3. Run the Frontend UI

cd ../celeb-angular
npm i
npm start
UI available at: http://localhost:4200



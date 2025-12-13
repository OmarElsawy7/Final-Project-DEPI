# 🎫 EventHub – Modern Event Booking & Management Platform

EventHub is a full-featured event management and ticketing system built with **ASP.NET Core MVC**, designed for both **attendees** and **event organizers**.
The platform delivers a smooth, secure, and scalable user experience—from browsing events to purchasing tickets, generating QR codes, and scanning them at the event gate.

---

## 🚀 Table of Contents

* [Overview](#-overview)
* [Core Features](#-core-features)
* [System Roles](#-system-roles)
* [Architecture](#-architecture)
* [Tech Stack](#-tech-stack)
* [Project Advantages](#-project-advantages)
* [Screens & UX](#-screens--ux)
* [QR Ticketing System](#-qr-ticketing-system)
* [Security](#-security)
* [Setup Instructions](#-setup-instructions)
* [Future Enhancements](#-future-enhancements)

---

## 🔍 Overview

EventHub simplifies how users interact with events:

✔ Attendees can browse, filter, and purchase tickets
✔ Organizers can manage events, track sales, and scan QR codes at check-in
✔ The system ensures secure login, protected dashboards, and accurate ticket validation

EventHub is designed to be scalable, maintainable, and production-ready.

---

## ⭐ Core Features

### 🎉 For Attendees (Users)

* Browse all events with **search & category filters**
* View event details and ticket availability
* Purchase multiple tickets in one transaction
* Secure **payment flow** (simulated)
* Access a full **My Tickets** dashboard
* Automatically generated **QR codes** for each ticket
* Ticket download/printing support
* View ticket status (Valid, Expired, Used)

### 🏢 For Organizers

* Full **Organizer Dashboard**
* Create, edit, update, and delete events
* Track available vs sold tickets
* Real-time ticket check-in system with QR scanning
* View full check-in history

### 🔐 Authentication & Roles

* Identity-based login and registration
* Role separation:

  * **Guest** → Home, Events, Login
  * **User** → Home, Events, Profile, Logout
  * **Organizer** → Home, Events, Profile, Dashboard, Check-In, Logout
* Custom user claims (FullName, ProfilePhoto)

---

## 👥 System Roles

| Role                | Permissions                                    |
| ------------------- | ---------------------------------------------- |
| **Guest**           | Browse events only                             |
| **User (Attendee)** | Buy tickets, view profile, view tickets        |
| **Organizer**       | Manage events, check-in QR codes, view history |

---

## 🧱 Architecture

EventHub follows a clean, layered architecture:

### **Controllers**

* `EventController` – Display & filter events
* `DashboardController` – Organizer event management
* `TicketController` – Ticket generation, listing, and details
* `PaymentController` – Payment process & ticket creation
* `ProfileController` – User profile management
* `CheckInController` – QR scanning & validation
* `AccountController` – Login, register, logout

### **Services**

Abstracted logic for:

* Events
* Tickets
* Payments
* Users

### **Database**

EF Core + Identity:

* Users
* Events
* Tickets
* Check-in history

---

## 🛠 Tech Stack

### **Backend**

* ASP.NET Core MVC 8
* Entity Framework Core
* Identity User + Roles
* LINQ & Async Repositories

### **Frontend**

* Razor Views
* HTML5 / CSS3
* Vanilla JavaScript
* HTML5-QR-Code library
* Dynamic AJAX requests

### **Other**

* QR Code generation
* Image uploading
* Secure cookie-based authentication

---

## 💎 Project Advantages

### ✔ Professional Real-World Architecture

Separation of concerns with:

* Controllers
* Services
* Repositories
* ViewModels

### ✔ Real Ticketing Logic

Each ticket has:

* Unique Ticket ID
* QR Code
* Buyer
* Price
* Timestamp
* Status (Valid / Used / Expired)

### ✔ Modern UI & UX

* Animated UI
* Clean cards for events & tickets
* Interactive dashboards
* Smooth modal interactions

### ✔ Full Check-In System

* Direct QR scanning from camera
* Reads ticket data
* Validates owner + event
* Marks ticket as used
* Saves history
* Prevents duplicate scanning

### ✔ Scalable & Extendable

You can easily add:

* Online payments (Stripe)
* Organizer analytics dashboard
* Event categories & banners

---

## 🖼 Screens & UX

### Attendee Experience

* Event listing
* Event filtering
* Ticket purchase modal
* Payment page
* Ticket dashboard
* QR display & print

### Organizer Experience

* Dashboard with event stats
* Event CRUD (Create/Read/Update/Delete)
* QR scanning interface
* Real-time check-in updates

---

## 🎟 QR Ticketing System

Each generated ticket includes a **QR payload**:

```json
{
  "ticketId": 2345,
  "eventId": 12,
  "holderName": "John Doe",
  "price": 29.99,
  "eventName": "Tech Summit",
  "eventDate": "2025-01-12T18:00:00Z"
}
```

### Check-in workflow:

1. Organizer scans QR
2. System sends QR token to backend
3. Backend validates:

   * Ticket exists
   * Ticket belongs to this event
   * Ticket not used
4. Marks ticket as checked-in
5. Returns UI response
6. Adds entry to history

---

## 🔐 Security

EventHub implements:

* Role-Based Authorization
* Anti-forgery tokens on all forms
* Claims for extended user profile
* Secure cookie-based authentication
* Validation of QR tokens
* Organizer-only access for dashboards and scanning

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the repo

```sh
git clone https://github.com/your-user/eventhub.git
cd eventhub
```

### 2️⃣ Update `appsettings.json`

Set your SQL connection string.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EventHubDb;Trusted_Connection=True;"
}
```

### 3️⃣ Apply migrations

```sh
dotnet ef database update
```

### 4️⃣ Run the project

```sh
dotnet run
```

### 5️⃣ Create roles (required)

* User
* Organizer

(Or configure seeding.)

---

## 📌 Future Enhancements

* Stripe / PayPal real payment gateway
* Organizer analytics & revenue dashboard
* Refund or ticket transfer
* Event seating map
* Mobile check-in app
* Notifications (Email/SMS)

---

## 📄 License

MIT License — free to use and modify.


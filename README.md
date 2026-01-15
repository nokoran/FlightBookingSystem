# FlightBookingSystem API

A RESTful Web API for booking flight seats, developed using **ASP.NET Core 8**.
This project implements a multi-layered architecture to ensure a clear separation between business logic and data access, adhering to modern development practices.

## Project Overview

The goal of this project is to provide a backend system for managing flights and passenger reservations. It supports role-based access control (RBAC) with **Admin** and **Passenger** roles, secured via **JWT Authentication**.

### Key Features

* **RESTful API**: Full implementation of GET, POST, PUT, and DELETE methods.
* **Architecture**: Layered architecture (Clean Architecture approach) separating Domain, Data Access, and Business Logic.
* **Database**: MS SQL Server using **Entity Framework Core** (Code-First approach).
* **Security**: JWT Bearer Authentication and Role-Based Authorization.
* **Documentation**: Integrated **Swagger/OpenAPI** for endpoint testing.

---

## Technology Stack

* **Framework**: .NET 8 (ASP.NET Core Web API)
* **ORM**: Entity Framework Core
* **Database**: Microsoft SQL Server
* **Auth**: ASP.NET Core Identity + JWT Tokens
* **Testing**: NUnit 3 (Unit Tests) & Swagger UI

---

## Functional Requirements

### User Roles
1.  **Administrator**: Manages flights and system data.
2.  **Passenger**: Books seats and manages their own reservations.

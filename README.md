# Tickets Management System

This repository contains two serices: a REST API service and a web application. The Tickets Management System allows users to manage providers and tickets with basic CRUD operations

## Table of Contents
- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the API](#running-the-api)
  - [Running the Web App](#running-the-web-app)
- [API Endpoints](#api-endpoints)
- [Web App Pages](#web-app-pages)
- [Contributing](#contributing)
- [License](#license)
- [Blog](#blog)

## Overview

The Tickets Management System consists of two main services:

- **TicketsAPI**: A RESTful API for managing providers and their associated tickets
- **TicketsWebApp**: A Web application that interacts with the API

## Technologies Used

### API Service:
- .NET 8
- Entity Framework Core (In-Memory Database)
- ASP.NET Core Minimal APIs
- Swagger for API documentation
- API Key Authentication

### Web App:
- .NET 8
- Razor Pages

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/denisalibasic/TicketsApp.git
   cd TicketsApp
   ```
2. **Restore the dependencies**:
    ```bash
   dotnet restore
   ```

### Running the API

1. **Navigate to the API project**:
   ```bash
   cd TicketsAPI
   ```
2. **Run the API service**:
    ```bash
   dotnet run
   ```
The API will start and should be accessible at http://localhost:5001

### Running the Web App

1. **Navigate to the Web App project**:
   ```bash
   cd TicketsWebApp
   ```
2. **Run the Web App**:
    ```bash
   dotnet run
   ```
The web app will start and should be accessible at http://localhost:5002

## API Endpoints
The API provides the following endpoints for managing tickets and providers:

Providers
- GET /providers: Retrieve a list of all providers.
- GET /provider/{id}: Retrieve a specific provider by ID.
- GET /tickets/provider/{id}: Retrieve all tickets associated with a specific provider.

Tickets
- GET /tickets: Retrieve a list of all tickets.
- GET /tickets/{id}: Retrieve a specific ticket by ID.
- POST /tickets: Create a new ticket.
- PUT /tickets/{id}: Update an existing ticket by ID.
- DELETE /tickets/{id}: Delete a ticket by ID.

## Web App Pages
The web application provides the following pages:
- Providers: Displays a list of providers with options to view tickets associated with each provider.
- Provider Details: Displays the tickets associated with a specific provider.
- Manage Tickets: View, create, update, or delete tickets.

## Contributing
Contributions are welcome! Please follow these steps to contribute:
1. Fork the Project.
2. Create your Feature Branch:
   ```bash
   git checkout -b feature/someFeature
   ```
3. Commit your Changes:
   ```bash
   git commit -m 'Add some feature'
   ```
4. Push to the Branch:
   ```bash
   git push origin feature/someFeature
   ```
5. Open a Pull Request


### License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Blog
Link to related blog post: [Cloud codeblock](https://cloudcodeblock.com/2024/09/08/asp-net-core-web-app-that-consumes-an-api/)

# Talabat Delivery APIs

## Project Description

This repository contains the APIs for Talabat's delivery services, providing endpoints for managing products, categories, brands, user accounts, shopping baskets, and order processing. It leverages .NET 8, Entity Framework Core, and ASP.NET Core to create a robust and scalable backend solution.

## Features and Functionality

*   **Product Management:**
    *   Retrieve a paginated list of products with filtering and sorting options.
    *   Fetch a single product by its ID.
    *   Get lists of product brands and categories.
*   **Account Management:**
    *   User registration and login.
    *   Retrieve and update the current user's information and address.
    *   Check if an email address is already registered.
*   **Basket Management:**
    *   Retrieve, update, and delete customer baskets.
*   **Order Management:**
    *   Create new orders.
    *   Retrieve order history for a user.
    *   Fetch an order by ID.
    *   Get available delivery methods.
*   **Error Handling:**
    *   Comprehensive error handling with standardized API responses.
    *   Custom exception middleware for handling application-specific exceptions.
*   **Authentication and Authorization:**
    *   JWT-based authentication for securing API endpoints.
    *   Role-based authorization for controlling access to specific resources.

## Technology Stack

*   .NET 8
*   ASP.NET Core 8
*   Entity Framework Core
*   SQL Server
*   Redis (for basket management)
*   AutoMapper
*   Swashbuckle/Swagger (for API documentation)

## Prerequisites

Before you begin, ensure you have met the following requirements:

*   .NET 8 SDK installed: [Download .NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
*   SQL Server instance:  (SQL Server Express LocalDB is suitable for development)
*   Redis server:  Install and configure a Redis server.
*   An IDE such as Visual Studio or VS Code.

## Installation Instructions

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/muhammadabdelgawad/Talabat-Delivery-APIs.git
    cd Talabat-Delivery-APIs
    ```

2.  **Update Database Connection Strings:**

    *   Open `Talabat.APIs/appsettings.json` and `Talabat.APIs/appsettings.Development.json`.
    *   Modify the `StoreConnection` connection string to point to your SQL Server instance:

        ```json
        "ConnectionStrings": {
          "StoreConnection": "Server=your_server;Database=TalabatStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
          "IdentityConnection": "Server=your_server;Database=TalabatIdentityDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
          "Redis": "localhost"
        }
        ```

        Replace `your_server` with your SQL Server instance name.
    *   Modify the `IdentityConnection` similarly to point to a different Database for Identity
    *   Configure the Redis connection string as needed. Default is `localhost`.

3.  **Apply Database Migrations:**

    *   Open a command prompt in the `Talabat.Infrastructure.Persistence` directory.
    *   Run the following commands to create and update the databases:

        ```bash
        dotnet ef database update -c StoreDbContext
        dotnet ef database update -c StoreIdentityDbConetxt
        ```

4.  **Configure JWT Settings:**

    *  Update `Talabat.APIs/appsettings.json` or `Talabat.APIs/appsettings.Development.json` with appropriate JWT settings.

        ```json
        "jwtSettings": {
            "key": "YourSecretKeyForJWTAuthentication",
            "Audience": "https://localhost:7070",
            "Issuer": "https://localhost:7070",
            "DurationInMinutes": 60
          }
        ```

    *   Replace `"YourSecretKeyForJWTAuthentication"` with a strong, randomly generated secret key. Adjust Issuer and Audience if necessary.

5.  **Initialize the Database**

    *The Application automatically initializes the databases, there is nothing to do.*

## Usage Guide

1.  **Run the Application:**

    *   Navigate to the `Talabat.APIs` directory in your command prompt.
    *   Execute the following command:

        ```bash
        dotnet run
        ```

2.  **Access the API Documentation:**

    *   Open your web browser and navigate to `https://localhost:7070/swagger` (or the appropriate URL based on your launch settings).
    *   This will display the Swagger UI, where you can explore the available endpoints and test the APIs.

3.  **Example API Usage (Products):**

    *   **Get all products (paginated):** `GET /api/Products?pageIndex=1&pageSize=10`
    *   **Get a specific product:** `GET /api/Products/{id}`  (e.g., `GET /api/Products/1`)
    *   **Get all brands:** `GET /api/Products/brands`
    *   **Get all categories:** `GET /api/Products/categories`

4.  **Authentication:**

    *   Register a new user using the `POST /api/Account/register` endpoint, providing the required details (DisplayName, UserName, Email, PhoneNumber, Password).
    *   Login using `POST /api/Account/login` with your email and password.  The API will return a JWT token.
    *   Include the JWT token in the `Authorization` header of subsequent requests that require authentication (e.g., `Authorization: Bearer <your_jwt_token>`).

## API Documentation

A comprehensive API documentation is available via Swagger UI at `https://localhost:7070/swagger` after running the application.  Refer to Swagger for detailed information on request parameters, response formats, and authentication requirements for each endpoint.

**Key Endpoints:**

*   `/api/Account`: User registration, login, and management.
*   `/api/Products`: Product retrieval and listing.
*   `/api/Basket`: Basket management.
*   `/api/Orders`: Order creation, retrieval, and management.
*   `/api/Buggy`: Endpoints used for testing Error Handling and API Response codes, for testing purposes only.
*   `/Errors/{Code}`: Endpoint used to test Error Handling responses from server.

## Contributing Guidelines

We welcome contributions to this project. Please follow these guidelines:

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Implement your changes, ensuring code quality and adherence to coding standards.
4.  Write appropriate unit tests.
5.  Submit a pull request with a clear description of your changes.

## License Information

This project is licensed under the MIT License. See the `LICENSE` file for details.


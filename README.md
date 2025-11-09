# Talabat Food Delivery APIs

This repository contains the APIs for a Talabat-like food delivery application.

## Features and Functionality

*   **Product Management:**
    *   Browse a paginated list of products with filtering and sorting options (name, price, brand, category).
    *   Retrieve a specific product by its ID.
    *   List available brands and categories.
*   **Basket Management:**
    *   Retrieve, update, and delete customer baskets using a unique basket ID.
*   **Account Management:**
    *   Register new users with display name, username, email, phone number, and password.
    *   Login existing users using email and password.
    *   Retrieve current user information (requires authentication).
    *   Retrieve and update user address information (requires authentication).
    *   Check if an email address already exists.
*   **Order Management:**
    *   Create new orders for authenticated users based on a basket ID and shipping address.
    *   Retrieve orders for a specific user (requires authentication).
    *   Retrieve a specific order by its ID (requires authentication).
    *   List available delivery methods.
*   **Error Handling:**
    *   Comprehensive error handling using custom `ApiResponse` objects for various status codes (400, 401, 404, 500).
    *   Custom exception middleware to handle exceptions like `NotFoundException`, `ValidationException`, `BadRequestException`, and `UnAuthorizedException`.
    *   Model state validation for API requests.
*   **Authentication and Authorization:**
    *   JWT-based authentication for securing API endpoints.
    *   Role-based authorization is possible but not implemented in the provided files.
    *   Configuration of JWT settings via `jwtSettings` section in `appsettings.json` or similar.

## Technology Stack

*   **.NET 9.0:**  (Inferred from the project structure and namespaces)
*   **ASP.NET Core Web API:** For building RESTful APIs.
*   **Entity Framework Core:**  ORM for database interactions (SQL Server).
*   **Microsoft.AspNetCore.Identity:**  For identity and access management.
*   **StackExchange.Redis:**  For basket management.
*   **AutoMapper:** For object-to-object mapping.
*   **JWT (JSON Web Tokens):** For authentication.
*   **SQL Server:** Relational database for product, order and identity information.

## Prerequisites

*   .NET SDK 9.0 or later.
*   SQL Server instance.
*   Redis server instance.

## Installation Instructions

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/muhammadabdelgawad/Talabat-Food-Delivery-APIs.git
    cd Talabat-Food-Delivery-APIs
    ```

2.  **Configure Database Connections:**

    *   Modify the connection strings in `Talabat.APIs/appsettings.json` (or `appsettings.Development.json`) for both `StoreConnection` (for product and order data) and `IdentityConnection` (for user data):

        ```json
        {
          "ConnectionStrings": {
            "StoreConnection": "Server=YOUR_SERVER;Database=TalabatStoreDB;Trusted_Connection=True;MultipleActiveResultSets=true",
            "IdentityConnection": "Server=YOUR_SERVER;Database=TalabatIdentityDB;Trusted_Connection=True;MultipleActiveResultSets=true",
            "Redis": "localhost"
          },
          // ... rest of settings
        }
        ```

        Replace `YOUR_SERVER` with the actual server address of your SQL Server instance. If you are using SQL Express on your local machine, it is usually `(localdb)\MSSQLLocalDB`.

        Ensure that Redis server is running and accessible. If not, configure the host in the `Redis` connection string.

3.  **Apply Migrations:**

    Open a terminal in the `Talabat.Infrastructure.Persistence` directory and run the following commands:

    ```bash
    dotnet ef database update -c StoreDbContext
    dotnet ef database update -c StoreIdentityDbConetxt
    ```
    These commands will create the necessary databases and tables based on the Entity Framework Core models.

4.  **Run the Application:**

    Navigate to the `Talabat.APIs` directory and run the application:

    ```bash
    dotnet run
    ```

## Usage Guide

The API endpoints can be accessed through tools like Postman, Swagger UI, or any HTTP client.  By default, the application will run on `https://localhost:5001`.  Swagger UI is enabled in development mode, which can be accessed by browsing to `https://localhost:5001/swagger`.

### Authentication

The `AccountController` provides endpoints for user registration and login.  Successful login will return a `UserDto` containing a JWT token. This token must be included in the `Authorization` header (Bearer scheme) for accessing protected endpoints.

### Products

*   `GET /api/Products`: Retrieves a paginated list of products.  Query parameters can be used to filter, sort, and paginate the results.
    *   `sort`: Sorting criteria (e.g., `priceAsc`, `priceDesc`).
    *   `brandId`: Filter by brand ID.
    *   `categoryId`: Filter by category ID.
    *   `pageIndex`: Page number.
    *   `pageSize`: Number of items per page.
    *   `search`: Search term for product name.
*   `GET /api/Products/{id}`: Retrieves a specific product by its ID.
*   `GET /api/Products/brands`: Retrieves all product brands.
*   `GET /api/Products/categories`: Retrieves all product categories.

### Basket

*   `GET /api/Basket?id={id}`: Retrieves a customer basket by its ID.
*   `POST /api/Basket`: Updates a customer basket.  The request body should contain a `BasketDto`.
*   `DELETE /api/Basket?id={id}`: Deletes a customer basket by its ID.

### Account

*   `POST /api/Account/register`: Registers a new user. The request body should contain a `RegisterDto`.
*   `POST /api/Account/login`: Logs in an existing user. The request body should contain a `LoginDto`.
*   `GET /api/Account`: Retrieves the current user. Requires authentication.
*   `GET /api/Account/address`: Retrieves the user's address. Requires authentication.
*   `PUT /api/Account/address`: Updates the user's address. The request body should contain an `AddressDto`. Requires authentication.
*   `GET /api/Account/emailexisits?email={email}`: Checks if an email exists.

### Orders

*   `POST /api/Orders`: Creates a new order. The request body should contain an `OrderToCreateDto`. Requires authentication.
*   `GET /api/Orders`: Retrieves orders for the authenticated user. Requires authentication.
*   `GET /api/Orders/{id}`: Retrieves a specific order by ID for the authenticated user. Requires authentication.
*   `GET /api/Orders/delivery`: Retrieves all delivery methods.

### Example API Request (Get Products)

```
GET https://localhost:5001/api/Products?pageSize=10&pageIndex=1&sort=priceAsc&brandId=2
```

This request retrieves the first page of products (10 items per page), sorted by price in ascending order, and filtered by brand ID 2.

## API Documentation

The API is self-documenting using Swagger UI, which is available in development environments.  Browse to `https://localhost:5001/swagger` after running the application.

## Contributing Guidelines

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Implement your changes and write appropriate tests.
4.  Ensure that all tests pass.
5.  Submit a pull request with a clear description of your changes.

## License Information

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact/Support Information

For any questions or support, please contact muhammadabdelgawad at [insert contact info here].
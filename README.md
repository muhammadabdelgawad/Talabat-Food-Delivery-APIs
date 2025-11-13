# Talabat-Food-Delivery-APIs

This repository contains the backend APIs for a Talabat-like food delivery platform. The APIs are built using ASP.NET Core and leverage Entity Framework Core for data persistence with SQL Server.  Identity management is implemented using ASP.NET Identity.  Redis is used for basket management and Stripe for payments.

## Features and Functionality

*   **Product Management:**
    *   Fetch paginated lists of products with sorting, filtering by brand and category, and searching.
    *   Retrieve a specific product by its ID.
    *   Retrieve lists of available brands and categories.
*   **Shopping Basket:**
    *   Retrieve, update, and delete customer baskets stored in Redis.
    *   Handles item additions, quantities, and basket lifetime.
*   **Order Management:**
    *   Create orders for users.
    *   Retrieve order history for a given user.
    *   Retrieve a specific order by ID.
    *   Fetch delivery methods.
*   **Account Management:**
    *   User registration with email confirmation, phone number confirmation, and password complexity validation.
    *   User login and generation of JWT tokens for authentication.
    *   Retrieval and updating of user addresses.
    *   Email existence check.
*   **Payment Integration:**
    *   Create or update Stripe Payment Intents.
    *   Webhooks to handle successful payments and other payment-related events.
*   **Authentication and Authorization:**
    *   Secured endpoints using JWT Bearer authentication.
    *   Role-based authorization implemented in the Admin Panel.
*   **Error Handling:**
    *   Global exception handling middleware.
    *   Custom API response structure for errors.
    *   Detailed error logging in development environments.
*   **Caching:**
    *   Cache responses from product listing endpoint using Redis.
*   **Admin Panel:**
    *   Basic functionality for admin user login and logout.
    *   Role management, including viewing, creating, editing, and deleting roles.
    *   User management, including viewing and editing user roles.

## Technology Stack

*   ASP.NET Core 8.0
*   Entity Framework Core 8.0
*   SQL Server
*   ASP.NET Core Identity
*   JWT Authentication
*   StackExchange.Redis
*   Stripe.net
*   AutoMapper

## Prerequisites

*   .NET 8.0 SDK
*   SQL Server
*   Redis Server (optional, for caching and basket management)
*   Stripe Account (for payment processing)

## Installation Instructions

1.  Clone the repository:

    ```bash
    git clone https://github.com/muhammadabdelgawad/Talabat-Food-Delivery-APIs.git
    cd Talabat-Food-Delivery-APIs
    ```

2.  Configure Database Connections:

    *   Open `AdminPanel/appsettings.json` and `Talabat.APIs/appsettings.json`
    *   Modify the `ConnectionStrings` section to point to your SQL Server instance.  Ensure the `IdentityConnection` and `StoreConnection` strings are correctly configured.  For example:
        ```json
        "ConnectionStrings": {
            "IdentityConnection": "Server=your_server;Database=TalabatIdentity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
            "StoreConnection": "Server=your_server;Database=TalabatStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
            "Redis": "localhost"
        }
        ```

3.  Configure Redis Connection:

    *   In `Talabat.APIs/appsettings.json`, ensure the `Redis` connection string points to your Redis server. For example:

        ```json
          "ConnectionStrings": {            
            "Redis": "localhost"
          },
         ```
    * In  `Talabat.APIs/appsettings.json`, configure `TimeToLiveInDays` in `RedisSettings` section. For example:
       ```json
        "RedisSettings": {
            "TimeToLiveInDays": 30
        }
       ```

4.  Configure Stripe API Keys:

    *   In `Talabat.APIs/appsettings.json`, configure your Stripe Secret Key:

        ```json
         "StripeSettings": {
           "SecretKey": "your_stripe_secret_key"
         }
        ```

5.  Apply Database Migrations:

    *   Open a terminal in the `Talabat.Infrastructure.Persistence` directory.
    *   Run the following commands:

        ```bash
        dotnet ef database update -c StoreDbContext
        dotnet ef database update -c StoreIdentityDbConetxt
        ```

6.  Run the application:

    *   Open a terminal in the `Talabat.APIs` directory.
    *   Run the following command:

        ```bash
        dotnet run
        ```
    *   Open a terminal in the `AdminPanel` directory.
    *   Run the following command:

        ```bash
        dotnet run
        ```

## Usage Guide

The API endpoints are structured around resources like products, baskets, orders, and accounts. Here's a basic guide:

### Authentication

*   **Register:** `POST /api/Account/register`
    *   Requires a JSON payload with `DisplayName`, `UserName`, `Email`, `PhoneNumber`, and `Password`.
*   **Login:** `POST /api/Account/login`
    *   Requires a JSON payload with `Email` and `Password`.
    *   Returns a `UserDto` containing user information and a JWT `Token`.
*   **Accessing Secured Endpoints:**  Include the `Authorization: Bearer <token>` header with the JWT token obtained during login.

### Products

*   **Get Products:** `GET /api/Products`
    *   Supports pagination, sorting, and filtering via query parameters.
    *   Example: `GET /api/Products?sort=priceAsc&brandId=1&categoryId=2&pageIndex=2&pageSize=10&search=Laptop`
*   **Get Product by ID:** `GET /api/Products/{id}`
*   **Get Brands:** `GET /api/Products/brands`
*   **Get Categories:** `GET /api/Products/categories`

### Basket

*   **Get Basket:** `GET /api/Basket?id={basketId}`
*   **Update Basket:** `POST /api/Basket`
    *   Requires a JSON payload with `Id` and `Items` (list of `BasketItemDto`).
*   **Delete Basket:** `DELETE /api/Basket?id={basketId}`

### Orders

*   **Create Order:** `POST /api/Orders`
    *   Requires a JSON payload with `BasketId`, `DeliveryMethodId`, and `ShippingAddress` (AddressDto). Requires JWT authentication.
*   **Get Orders for User:** `GET /api/Orders`
    *   Requires JWT authentication.
*   **Get Order by ID:** `GET /api/Orders/{id}`
    *   Requires JWT authentication.
*   **Get Delivery Methods:** `GET /api/Orders/delivery`

### Account

*   **Get Current User:** `GET /api/Account`
    *   Requires JWT authentication.
*   **Get User Address:** `GET /api/Account/address`
    *   Requires JWT authentication.
*   **Update User Address:** `PUT /api/Account/address`
    *   Requires a JSON payload with address details (`AddressDto`). Requires JWT authentication.

### Admin Panel

1.  Navigate to the Admin Panel URL (default pattern is `https://localhost:<port>/Admin/Login`).
2.  Log in with a valid admin user's credentials. (Default seeding does not have an admin. You will need to manually create one.  See Contributing Guide below.)
3.  The Admin Panel offers interfaces for managing roles and users.

## API Documentation

Swagger UI is enabled in the development environment.  Navigate to `https://localhost:<port>/swagger` to view the API documentation and interact with the endpoints.

## Contributing Guidelines

Contributions are welcome! Please follow these guidelines:

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Implement your changes.
4.  Test your changes thoroughly.
5.  Create a pull request with a clear description of your changes.

### Creating an Admin User
The default database seeding in `StoreIdentityDbInitializer.cs` does NOT create an admin user. You will need to create one programmatically using the `UserManager` in the `Program.cs` or a dedicated seeding class.

1.  **Modify `StoreIdentityDbInitializer.cs`**:
    Add the necessary code to create an Admin role and assign an existing user to the role, or create a new admin user. The following is only an example and should be adjusted to best fit your configuration.

```csharp
using Microsoft.AspNetCore.Identity;
using Talabat.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Identity;

namespace Talabat.Infrastructure.Persistence._Data
{
    public class StoreIdentityDbInitializer(StoreIdentityDbConetxt _dbContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : DbInitializer(_dbContext), IStoreIdentityInializer
    {
        public override async Task SeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }


            if (!_userManager.Users.Any())
            {
                var adminUser = new ApplicationUser
                {
                    DisplayName = "Admin User",
                    UserName = "admin",
                    Email = "admin@test.com",
                    PhoneNumber = "00000000000",
                };

                await _userManager.CreateAsync(adminUser, "Pa$$w0rd");

                var user = await _userManager.FindByEmailAsync("admin@test.com");
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
```
2. Call the InitializeDbAsync() from Program.cs. Now the Admin user is registered, you can run the migrations and then the application.


## License Information

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact/Support Information

For any questions or issues, please contact: [Your Name/Organization]
```

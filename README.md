# API Documentation for ATM Simulation Innovitics inc.

## Introduction

Welcome to the API documentation for the ATM Simulation API. This API allows you to simulate ATM machine operations, including user Login authentication, user registration, balance inquiries, and cash withdrawals.

### Technologie and Design
C#, OOP, DI, N-tiers, ASP.Net web API, Entity Framework (EF) and LINQ.

### Packages
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.Aspnetcore.Identity
Microsoft.Aspnetcore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

### To Install and Run
- Check packages installing.
- Change ConnectionStrings of your database in appsettings.json.
- In package manager console Run two commands.
    - => add-migration [name of migration].
    - => update-database.
### Test
After installing and running corectly, you can test by static data :
   - { CardNumber = "12345678955555", PIN = "123456" } 
   - { CardNumber = "56789012345678", PIN = "666666" }
   - { CardNumber = "12345678901234", PIN = "000000" }

### Authentication

Before using this API, users must be authenticated by providing a valid card number and PIN.

- **Base URL**: `https://api.example.com`

## Endpoints

### User Authentication

#### `POST /api/User/login`

Authenticate a user with their PIN and card number.

- **Request:**
  - Body:
    - `cardNumber` (string, required): The user's 14-digit card number.
    - `PIN` (string, required): The user's 6-digit PIN.

- **Response:**
  - `200 OK`: Successful authentication.
    - Body: A JSON object with an access token.
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "expiryDate": "2023-09-20T09:40:13.6831339+03:00"
    }
    ```
  - `401 Unauthorized`: Incorrect Card Number Not Found.
  - `401 Unauthorized`: Incorrect PIN.

### User Registration

#### `POST /api/User/register`

Register a new user with their card number, name, PIN ,balance

- **Request:**
  - Body:
    - `cardNumber` (string, required): The user's 14-digit card number.
    - `name` (string, required): Only English letters are allowed, max 100 letter.
    - `PIN` (string, required): The user's 6-digit PIN.
    - `balance` (int, required): Positive integer.


- **Response:**
  - `200 OK`: User registered successfully.
  - `400 BadRequest`: Messages of validations body of request.

### Balance Inquiry

#### `GET /api/User/balance`

Retrieve the account balance for the authenticated user.

- **Request:**
  - Headers:
    - `Authorization` (string, required): Bearer token obtained from authentication.

- **Response:**
  - `200 OK`: Successful retrieval.
    - Body: A JSON object with the account balance.
    ```json
    {
      "balance": 5000
    }
    ```
  - `401 Unauthorized`: Authentication failed.

### Cash Withdrawal

#### `POST /api/User/Cashwithdrawal`

Withdraw cash from the user's account (up to 1,000 L.E).

- **Request:**
  - Headers:
    - `Authorization` (string, required): Bearer token obtained from authentication.
  - URL Parameters:
    - `cash` (integer, required): The amount of cash to withdraw.

- **Response:**
  - `200 OK`: Cash withdrawal successfully.
    - Body: A message indicating the successful withdrawal.
    ```json
    {
      "message": "Cash withdrawal successful."
    }
    ```
  - `400 Bad Request`: balance low OR withdrawal limit 1000 exceeded.

## Error Handling

In case of errors, the API will respond with appropriate HTTP status codes and error messages. See individual endpoints for error details.

## Rate Limiting

Rate limiting is not applied to this API.

## API Versioning

The current API version is `v1`. Include the version in the request URL.


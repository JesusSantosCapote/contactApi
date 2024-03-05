# contactApi

Simple Web API for managing contacts of specific users. This project serves as a training ground for mastering the technologies and design patterns used in ASP.NET Core application development. The API requirements can be found in the ExerciseOrder.md file.

## API Tests
The API tests were conducted using Postman. In the test_postman folder, you’ll find a .json file containing the test collection that should be imported into Postman for use. These tests verify the API results both in case of success and failure. Additionally, they ensure the proper functioning of data validation processes and authentication and authorization procedures. It is crucial to verify that when you start the API server, the IP address and port match the global variable in the Postman tests, {{base_url}}. Otherwise, make sure to appropriately update the IP address and port to match.

## JWT Tokens
In the tests, three JWT tokens are utilized. One corresponds to a Cuban administrator, another to a non-Cuban administrator, and the third to a non-administrator user. These tokens are employed by the Postman tests to verify authorization requirements. Below, you’ll find information about the tokens used. The users info are seeded to the database when the aplication is executed.

#### Cuban Admin
Token:

eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHU0kgQ2hhbGxlbmdlIEF1dGhlbnRpY2F0b3IiLCJpYXQiOjE3MDk0ODY3MjAsImV4cCI6MTc0MTAyMjcyMCwiYXVkIjoid3d3LmdzaWNoYW5sbGVuZ2VhcGkuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjoiQWRtaW5pc3RyYXRvciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJDVSJ9.a_284iYQ7sKb9GqJcmOkvBUY-0tg0n64yrrYGzoC-0U

Data:
{
    "iss": "GSI Challenge Authenticator",
    "iat": 1709486720,
    "exp": 1741022720,
    "aud": "www.gsichanllengeapi.com",
    "sub": "jrocket@example.com",
    "GivenName": "Johnny",
    "Surname": "Rocket",
    "Email": "jrocket@example.com",
    "Role": "Administrator",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country": "CU"
}

#### Non Cuban Admin

Token: 
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHU0kgQ2hhbGxlbmdlIEF1dGhlbnRpY2F0b3IiLCJpYXQiOjE3MDk0ODY3MjAsImV4cCI6MTc0MTAyMjcyMCwiYXVkIjoid3d3LmdzaWNoYW5sbGVuZ2VhcGkuY29tIiwic3ViIjoiaXRhbGlhbkBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6Ikl0YWxpYW4iLCJTdXJuYW1lIjoiUm9ja2V0IiwiRW1haWwiOiJpdGFsaWFuQGV4YW1wbGUuY29tIiwiUm9sZSI6IkFkbWluaXN0cmF0b3IiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9jb3VudHJ5IjoiSVQifQ.IZu6HKfA5wYRaTZ4eSsX3yqFvGBYVNREKoUqnCtJRJQ

Data:
{
    "iss": "GSI Challenge Authenticator",
    "iat": 1709486720,
    "exp": 1741022720,
    "aud": "www.gsichanllengeapi.com",
    "sub": "italian@example.com",
    "GivenName": "Italian",
    "Surname": "Rocket",
    "Email": "italian@example.com",
    "Role": "Administrator",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country": "IT"
}

#### No Admin 

Token: 
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHU0kgQ2hhbGxlbmdlIEF1dGhlbnRpY2F0b3IiLCJpYXQiOjE3MDk0ODY3MjAsImV4cCI6MTc0MTAyMjcyMCwiYXVkIjoid3d3LmdzaWNoYW5sbGVuZ2VhcGkuY29tIiwic3ViIjoibGFyaWFuQGV4YW1wbGUuY29tIiwiR2l2ZW5OYW1lIjoiTGFyaWFuIiwiU3VybmFtZSI6IlN0dWRpbyIsIkVtYWlsIjoibGFyaWFuQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvY291bnRyeSI6IkNVIn0.buJnhPKBMG63JzmIt83QM-aPhT_WQ3oPCCttkd8lGsE

Data:
{
    "iss": "GSI Challenge Authenticator",
    "iat": 1709486720,
    "exp": 1741022720,
    "aud": "www.gsichanllengeapi.com",
    "sub": "larian@example.com",
    "GivenName": "Larian",
    "Surname": "Studio",
    "Email": "larian@example.com",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country": "CU"
}

## Architecture:
The application follows a 3-tier architecture, comprising the following layers:

### DataAccess Layer (DataAccess):
This layer contains the definitions of entity sets present in the SQL Server database.
It defines the EF Core context used for communication with the database server.
The defined entity sets include:

- User:
  - Id: GUID
  - Firstname: string (128 chars)
  - Lastname: string (128 chars)
  - Username: string (60 chars)
  - Password: string (256 chars)
    
- Contact:
  - Id: GUID
  - FirstName: string (128 chars)
  - LastName: string (128 chars)
  - Email: string (128 chars)
  - DateOfBirth: DateTime
  - Phone: string (20 chars)
  - Owner: GUID
    
Additionally, this layer implements a set of repositories to decouple the application from the data persistence mechanism.

### BusinessLogic Layer:
In this layer, all business logic is implemented, and data validation occurs.
Two Data Transfer Objects (DTOs) are implemented:
- One for receiving contact data that the user wants to add or update.
- Another for responding to user GET requests.

The response DTO includes an additional field called “Age” which stores the contact’s age calculated from their date of birth.
The ContactService acts as an intermediary between the Presentation layer requests and the repositories. It also handles data validation before storage.
The Result pattern is used to return query results from the database to the Presentation layer.

### Presentation Layer:
This layer houses the API controllers.
Authentication is utilized with JWT protocol.

# Exercise No. 2: Contact Manager
This coding challenge is for a .NET developer. We are making the assumption that you are familiar with .NET Core, MVC and REST.

Your task is to build an API that manages contacts. For this you must use .NET Core and C# and consider the following assumptions.
1.	The API should be a ASP.NET Core Web API project.
2.	Entity Framework should be used as ORM following the Code First approach.
3.	Configure the system to use an SQL Server DataBase called UserManger.
4.	The API should consume and return data as JSON.
5.	You can use any NuGet package but be prepared to justify its usage.
6.	You can use a configuration file to store Connection Strings or whatever you need.

### Specification
We want you to develop an API that exposes one RESTful endpoint. This endpoint should provide standard CRUD functionality for Contacts.

### Authentication Requirement

The API should use JWT bearer token as authorization, you can use the following tool to generate the jwt token http://jwtbuilder.jamiekurtz.com/. The subject claim is supposed to hold the username and should be used to find the user who is calling the API, check the User Data Model. You can validate using “GSI Challenge Authenticator” as the token’s issuer and www.gsichanllengeapi.com as the token’s audience. Please make sure to add the Country claim when you generate your own token which is supposed to store the ISO Code 2 of the country from where the user logged in.

If you prefer you can use the following token:

```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHU0kgQ2hhbGxlbmdlIEF1dGhlbnRpY2F0b3IiLCJpYXQiOjE2MzIyMzQ0MjcsImV4cCI6MTY2Mzc3MTk4NCwiYXVkIjoid3d3LmdzaWNoYW5sbGVuZ2VhcGkuY29tIiwic3ViIjoiZ3NpY29kaW5nY2hhbGxlbmdlIiwiR2l2ZW5OYW1lIjoiSm9obm55IiwiU3VybmFtZSI6IlJvY2tldCIsIkVtYWlsIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlJvbGUiOlsiTWFuYWdlciIsIlByb2plY3QgQWRtaW5pc3RyYXRvciIsIkFkbWluaXN0cmF0b3IiXSwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvY291bnRyeSI6IkNVIn0.o6pSpmbOEz1F7SZYlWUInfr6G15J8C9hEGW65zu1beo
```

Generated Claim Set (plain text)

```js
{
    "iss": "GSI Challenge Authenticator",
    "iat": 1632234427,
    "exp": 1663771984, // expiration date 2022-09-21T14:53:04.970Z
    "aud": "www.gsichanllengeapi.com",
    "sub": "gsicodingchallenge",
    "GivenName": "Johnny",
    "Surname": "Rocket",
    "Email": "jrocket@example.com",
    "Role": [
        "Manager",
        "Project Administrator",
        "Administrator"
    ],
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country": "CU",
}
```

### DATA MODEL:

#### User
- Id: GUID
- Firstname: string (128 chars)
- Lastname: string (128 chars)
- Username: string (60 chars)
- Password: string (256 chars)

#### Contact
- Id: GUID
- FirstName: string(128 chars)
- LastName: string(128 chars) [not required]
- Email: string(128 chars)
- DateOfBirth: DateTime
- Phone: string (20 chars)
- Owner: GUID


### ContactController.

- POST
  - **Url: /api/contacts**
  - *Description:* Creates a new contact. Should follow the field specification described below 
  - *Output:* Should return a 400 Status Code if any validation fails and a 201 in case the contact has been created successfully together with a Location response header containing the newly created contact's URL.

- GET
  - **Url: /api/contacts**
  - *Description:* Gets the entire list of contacts.
  - *Output:* (200 Status Code) contact list.

- GET
  - **Url: /api/contacts/{id}**
  - *Description:* Gets a contact from the collection.
  - *Output:* 200 Status Code with the contact record or a 404 Status Code if no contact matches with the provided Id. 

- DELETE
  - **Url: /api/contacts/{id}**
  - *Description:* Remove a contact from the collection.
  - *Output:* Error if no contact matches with the provided Id otherwise a success response.

- PUT
  - **Url: /api/contacts/{id}**
  - *Description:* Update a contact record.
  - *Output:* Error if no contact matches with the provided Id otherwise a success response. 

### Fields

|   Field Name  |   Data Type   |	Required    |	Validation                                  |
|---------------|---------------|---------------|-----------------------------------------------|
|   FirstName   |	string      |	true	    |   max length 128                              |
|   LastName    |	string      |	false       |	max length 128                              |
|   Email	    |   string      |	true	    |   must validate to a typical email address.   |
|   DateOfBirth	|   DateTime    |	true	    |   must evaluate to a valid date               |
|   Phone	    |   string      |	true	    |   Not validation to avoid complexity          |

- Every user must have a unique email address.
- The contact when created must be 18 years or older at the time of the request.
- When returning one contact or a list of contacts, return back the age of the contact in addition to the date of birth.
- Any validation error must return the proper error response.
- The endpoint DELETE /api/contact/{id} should be authorized only for Cuban Administrators.
- Make sure to validate to return the HTTP status code for invalid URL calls. Ex: POST /api/contacts/{id} or DELETE /api/contacts.


### How we will assess your work
We are looking for production quality code which utilises design patterns where appropriate and conforms to best practices and principles such as SOLID, etc. Other things we will take into consideration:
- Code should be testable.
- We expect you to be mindful of correct HTTP status code usage according to the action that it is being executed.

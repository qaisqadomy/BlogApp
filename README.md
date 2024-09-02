User Management API
Prerequisites
Ensure you have the following installed on your machine:

.NET 8 SDK
SQL Server (Optional, only if not using InMemoryDb)
Postman (for testing the API)
Setup
Clone the Repository
bash
Copy code
git clone https://github.com/yourusername/your-repo.git
cd your-repo
Configure the Database
The API uses an InMemory database for testing and development purposes. If you wish to use a persistent database like SQL Server, update the appsettings.json or appsettings.Development.json with the appropriate connection string.

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server Connection String"
}
Install Dependencies
Run the following command to restore all necessary packages:

bash
Copy code
dotnet restore
Update the Database
If using a persistent database, apply migrations:

bash
Copy code
dotnet ef database update
Running the Application
Build the Solution
bash
Copy code
dotnet build
Run the Application
bash
Copy code
dotnet run
By default, the API will be hosted at https://localhost:5001/.

Testing
This project uses xUnit for testing. To run the tests, execute the following command:

bash
Copy code
dotnet test
Note: If you encounter issues where tests pass individually but fail when run together, ensure that the tests are properly isolated, especially when checking for 401 Unauthorized responses.

Swagger Documentation
Swagger is integrated for API documentation. Once the application is running, you can access the Swagger UI at:

bash
Copy code
https://localhost:5001/swagger
Using Postman
Postman is a powerful tool for testing APIs. Follow these steps to test the User endpoints:

User Registration
Open Postman and create a new POST request.
Set the URL to https://localhost:5176/api/user/register.
In the Body tab, select raw and JSON format, then enter the registration details.
Click Send to register the user.
User Login
Create a new POST request in Postman.
Set the URL to https://localhost:5176/api/user/login.
In the Body tab, select raw and JSON format, then enter the login credentials.
Click Send to log in.
The response will include a JWT token. Copy this token for authorization in subsequent requests.
Update User
Create a new PUT request in Postman.
Set the URL to https://localhost:5176/api/user/update.
In the Headers tab, add a new header:
Key: Authorization
Value: Bearer <Your JWT Token>
In the Body tab, select raw and JSON format, then enter the updated user details.
Click Send to update the user information.
Using Postman with the JWT token ensures that you are authenticated when accessing protected endpoints.

/*   Endpoints documentation     */

Article Endpoints
Base URL: /article
This set of endpoints allows for managing articles, including creating, updating, deleting, and retrieving articles.

GET /article/

Description: Retrieves a list of articles, optionally filtered by tag, author, or favorited status.
Parameters:
tag (optional): Filter by tag.
author (optional): Filter by author.
favorited (optional): Filter by favorited status.
Response: A list of ArticleViewDTO objects.
GET /article/articles

Description: Retrieves all articles.
Response: A list of ArticleViewDTO objects.
POST /article/

Description: Adds a new article.
Body:
ArticleDTO object representing the article to be added.
Response: The added ArticleDTO object.
PUT /article/{id}

Description: Updates an existing article by ID.
Parameters:
id: The ID of the article to update.
Body:
ArticleDTO object representing the updated article data.
Response: The updated ArticleDTO object.
DELETE /article/{id}

Description: Deletes an article by ID.
Parameters:
id: The ID of the article to delete.
Response: No content (204 status code) on successful deletion.
Comment Endpoints
Base URL: /comment
This set of endpoints allows for managing comments, including creating, deleting, and retrieving comments.

GET /comment/

Description: Retrieves a list of all comments.
Response: A list of CommentViewDTO objects.
POST /comment/

Description: Adds a new comment.
Body:
CommentDTO object representing the comment to be added.
Response: An HTTP response indicating the result of the operation.
DELETE /comment/{id}

Description: Deletes a comment by ID.
Parameters:
id: The ID of the comment to delete.
Response: No content (204 status code) on successful deletion.
User Endpoints
Base URL: /user
This set of endpoints allows for managing users, including registration, login, and retrieving or updating the current user's details.

POST /user/register

Description: Registers a new user.
Body:
UserDTO object representing the user to register.
Response: An HTTP response indicating the result of the registration.
POST /user/login

Description: Logs in a user and returns a JWT token.
Body:
Email: The user's email.
Password: The user's password.
Response: A JWT token for authentication.
GET /user/

Description: Retrieves the details of the currently authenticated user.
Headers:
Authorization: Bearer token for authentication.
Response: The GetUserDTO object representing the user details, or an unauthorized response if the token is invalid.
PUT /user/

Description: Updates the details of the currently authenticated user.
Headers:
Authorization: Bearer token for authentication.
Body:
UserDTO object representing the updated user details.
Response: The updated UserDTO object.

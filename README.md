📚 Reading Book API

Reading Book API is a robust, scalable web API built using ASP.NET Core. It provides a platform to manage books, user reviews, and ratings. With comprehensive features like pagination, user authentication via JWT, and review/rating aggregation, this project demonstrates modern API development practices and clean code structure.

🚀 Key Features

Book Management: Add, update, delete, and fetch books with various details like title, genre, and description.
User Authentication & Authorization: Secure access using JWT tokens and role-based authorization for admin and users.
Pagination: Efficient pagination for large datasets of books.
Review & Rating System: Users can leave reviews and ratings, with automatic average rating calculation for each book.
Search: Search books by title, description, or author.

🛠️ Technologies Used

ASP.NET Core 8
Entity Framework Core for database management.
JWT (JSON Web Tokens) for secure user authentication.
AutoMapper for object-to-object mapping.
SQL Server as the database.

📑 Setup & Installation

Add your database connection string and JWT settings in appsettings.json.

Migrate Database.

Swagger Documentation: Once the app is running, you can access the API documentation via Swagger:


🔒 Authentication
The API uses JWT-based authentication. To access protected endpoints, you'll need to:

Register as a user.
Log in to receive a JWT token.
Include the token in the Authorization header for all subsequent requests.


🌟 Key API Endpoints
Books:

GET /api/books?page=1&size=10: Fetch paginated books.

GET /api/book?bookid=id: Get a single book with reviews and ratings.

Reviews:

POST /api/reviews: Add a review for a book.

PUT /api/reviews?reviewid=id: Update a review.

DELETE /api/reviews?reviewid=id: Delete a review.

🤝 Contributions & Contact
This project is open to contributions. Feel free to fork the repository and submit pull requests.
For business inquiries or freelance opportunities, contact [meisam_sohrabi@yahoo.com] or connect via [Your LinkedIn].

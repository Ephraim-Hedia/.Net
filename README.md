# TaskApiProject

A simple **.NET Web API** project that provides **user authentication** and **contact management** features.
Users can register, log in with JWT authentication, and manage their personal address book with advanced search, pagination, and sorting.

---

## 🚀 Features

* **User Authentication**

  * Register new accounts
  * Login with JWT-based authentication
* **Contact Management**

  * Add a new contact
  * List all contacts
  * Get a contact by ID
  * Delete a contact
* **Advanced Contact Queries**

  * **Pagination**: Retrieve results in pages for efficiency
  * **Search**: Filter contacts by `firstName`, `lastName`, `phoneNumber`, or `email`
  * **Sorting**:

    * Default: by `CreatedAt` (newest first)
    * Optional: `NameAsc` or `NameDesc`

---

## 🛠 Technologies

* **.NET 8 Web API**
* **Entity Framework Core**
* **ASP.NET Core Identity**
* **JWT Authentication**
* **AutoMapper**
* **SQL Server**

---

## 📦 Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server) (or local DB)
* [Postman](https://www.postman.com/) (for testing APIs)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Ephraim-Hedia/.Net.git
   cd .Net/TaskApiProject
   ```

2. Update the database:

   ```bash
   dotnet ef database update
   ```

3. Run the project:

   ```bash
   dotnet run
   ```

4. The API will be available at:

   ```
   https://localhost:5001/api
   ```

---

## 🔑 API Endpoints

### Authentication

* `POST /api/auth/register` → Register a new user
* `POST /api/auth/login` → Login and get JWT token

### Contacts

* `POST /api/contacts` → Add a new contact
* `GET /api/contacts` → Get all contacts (with pagination, search, sorting)
* `GET /api/contacts/{id}` → Get a single contact by ID
* `DELETE /api/contacts/{id}` → Delete a contact

---

## 📖 Example Usage

**Search + Pagination + Sorting**

```
GET /api/contacts?search=John&pageNumber=1&pageSize=10&sortBy=NameAsc
```

**Register**

```json
POST /api/auth/register
{
  "username": "testuser",
  "email": "test@example.com",
  "password": "P@ssw0rd"
}
```

**Login**

```json
POST /api/auth/login
{
  "email": "test@example.com",
  "password": "P@ssw0rd"
}
```

**Add Contact**

```json
POST /api/contacts
{
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "123456789",
  "email": "john.doe@example.com",
  "birthDate": "1995-05-10"
}
```

---

## 📜 License

This project is licensed under the MIT License.

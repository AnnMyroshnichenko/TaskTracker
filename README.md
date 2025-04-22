# TaskTracker
TaskTrekApp is a task management web application that allows users to create, update, and organize tasks in a column-based interface, inspired by the Kanban workflow. It includes a simple authentication system and a user-friendly dashboard.

## Features

🔐 User registration and login

🧾 Create, edit, and delete tasks

🧩 Organize tasks by columns

🏷️ Tags and ddeadlines

🧠 Simple and clean UI

🔄 Drag-and-drop functionality

## Tech Stack
- ASP.NET Core MVC
- SQLServer
- Entity Framework Core
- HTML, CSS, JavaScript

## How to Run Locally
### Clone the repo

``` 
git clone https://github.com/AnnMyroshnichenko/TaskTracker.git
```

### Create a .env file in the root directory of the project

Insert an email address credentials for sending sign up confirmation emails

```
SMTP_SERVER=smtp.gmail.com
SMTP_PORT=587
SMTP_SENDERNAME=[your SMTP_SENDERNAME]
SMTP_SENDEREMAIL=[your SMTP_SENDEREMAIL]
SMTP_USERNAME=[your SMTP_USERNAME]
SMTP_PASSWORD=[your SMTP_PASSWORD]
```

### Create a SQLServer database

Add the connection string to the .env file:

```
DEFAULT_CONNECTION_STRING=[your connection string]
```

### Apply migrations

Open up a Package Manager Console in Visual Studio and run the following command:

```
Update-Database
```

## Contributions
Contributions are welcome! Please fork the repository and submit a pull request.

## License
Distributed under the MIT License.

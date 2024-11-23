# Contact Monthly Claim System (CMCS)

The **Contact Monthly Claim System (CMCS)** is a streamlined web-based application designed to simplify the process of submitting, reviewing, and approving monthly claims for independent contractor lecturers. The system ensures efficient handling of claim submissions, providing a transparent and user-friendly interface for all stakeholders.

## Table of Contents
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Installation](#installation)
- [Usage](#usage)
- [System Roles](#system-roles)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## Features
- **Lecturer Claim Submission**:  
  Lecturers can submit claims with supporting documents, track their status, and view claim history.

- **Claim Review and Approval**:  
  Program Coordinators and Academic Managers can review, verify, and approve claims.

- **File Upload**:  
  Support for uploading supporting documents such as timesheets and additional evidence.

- **Secure Access**:  
  Role-based authentication and authorization for system users.

- **Claim Dashboard**:  
  A detailed view of claims, including submitted, pending, and approved claims.

---

## Technology Stack
- **Frontend**: HTML, CSS, Bootstrap
- **Backend**: ASP.NET Core MVC
- **Database**: SQL Server
- **Authentication**: ASP.NET Identity
- **Unit Testing**: xUnit
- **Tools**: Visual Studio, Entity Framework Core

---

## Installation

### Prerequisites
1. [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. [Visual Studio](https://visualstudio.microsoft.com/)

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/cmcs.git
   ```
2. Navigate to the project directory:
   ```bash
   cd cmcs
   ```
3. Set up the database:
   - Update the connection string in `appsettings.json`.
   - Run the following command to apply migrations:
     ```bash
     dotnet ef database update
     ```
4. Build and run the application:
   ```bash
   dotnet run
   ```

---

## Usage

1. Open the application in your browser:
   ```plaintext
   http://localhost:5000
   ```
2. Log in as a **Lecturer**, **Program Coordinator**, or **Academic Manager**.
3. Follow the role-specific options to submit or manage claims.

---

## System Roles

### Lecturer
- Submit monthly claims.
- Upload supporting documents.
- View claim status and history.

### Program Coordinator
- Review and verify claims submitted by lecturers.
- Provide feedback or flag claims for correction.

### Academic Manager
- Approve claims after coordinator review.
- Oversee the claim submission process and generate system-wide reports.

---

## Project Structure
```
CMCS/
│
├── Controllers/           # Handles HTTP requests
├── Models/                # Data models and business logic
├── Views/                 # Razor views for the UI
├── Data/                  # Database context and migrations
├── wwwroot/               # Static files (CSS, JS, images)
├── Tests/                 # Unit tests for controllers and models
├── appsettings.json       # Application configuration
└── Program.cs             # Application entry point
```

---

## Contributing
We welcome contributions!  
To contribute:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature
   ```
3. Commit your changes and open a pull request.

---

## License
This project is licensed under the **MIT License**. See the `LICENSE` file for details.

---

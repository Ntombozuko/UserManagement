# UserManagement Assessment

## Overview

This project is a .NET 9 MVC web application built to manage users, their group assignments, and the permissions associated with those groups. It supports both full API endpoints and user-friendly views for core entities.

---

##  Assessment Coverage

### **Task 1: CRUD and Relationship Setup**

* [x] Implemented CRUD for **Users**.
* [x] Set up **many-to-many** relationship: Users ↔ Groups.
* [x] Set up **many-to-many** relationship: Groups ↔ Permissions.



### **Task 2: API Implementation**

* [x] Created API Controllers for **User**, **Group**, and **Permission**.
* [x] Exposed all CRUD operations and relationship assignments.
* [x] Enabled and tested using **Swagger** UI.

### **Task 3: MVC UI Views**

* [x] Views for creating/editing/listing **Users**.
* [x] Assign **Groups to Users**.
* [x] Assign **Permissions to Groups**.
* [x] Group overview page displays related Users and Permissions.

---

## Technologies Used

* ASP.NET Core 8 MVC
* Entity Framework Core (Code-First)
* SQL Server (LocalDB)
* Bootstrap 5
* Swagger (Swashbuckle)

---

##  Setup Instructions

1. **Clone the Repository**:

```bash
git clone https://github.com/Ntombozuko/UserManagement.git
cd UserManagement
```

2. **Run the Application**:

```bash
dotnet build
dotnet ef database update
dotnet run
```

3. **Access in Browser**:

* Swagger UI: `https://localhost:<port>/swagger`
* MVC UI: `https://localhost:<port>/`

> Replace `<port>` with the port defined in `launchSettings.json`.

---

##  Notes

* Database is seeded with demo  **Groups**, and **Permissions**.
* UI allows assigning group and permission relationships.
* Fully testable via Swagger or the web UI.

---

##  Exclusions

* Unit tests were not included.

---

## Status

**Completed Tasks 1–3. Ready for review.**

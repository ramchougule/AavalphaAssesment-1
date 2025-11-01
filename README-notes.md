# README-notes

## Overview
This project includes updates to a .NET backend and a React frontend for commission calculation functionality.


## Backend (.NET)

### Implemented
- Commission calculation logic implemented in the **controller** (per task requirements).
- Added a new **xUnit test** for the calculation logic — test passes successfully.
- Validations checked and working correctly.
- Configured **CORS** in `Program.cs` to allow frontend API calls.

### Pending / Notes
- Logic is currently in the controller; no separate service layer was added to match the task instructions.



## Frontend (React)

### Implemented
- Called backend API using **Axios**.
- Added **error remark handling** if the backend returns any remarks.

### Pending / Notes
- Test cases for React components are still pending.

---

## Decisions / Trade-offs
- Kept calculation logic in the controller for simplicity and to match the original task.
- Did not add additional abstraction layers to keep the solution aligned with requirements.

---

## How to Run

### Backend
1. Navigate to the backend project folder.
2. Run the project:
   dotnet run
3. For Test:
   dotnet test

### Frontend
1. Navigate to the frontend project folder.
2. Run the project:
   npm start
3. For Test:
   npm test

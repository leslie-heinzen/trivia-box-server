## Trivia Box

A real-time trivia application for fun and profit.

This repository is for the server.

To run:

- Download the repository.
- Restore the solution.
- Bootstrap the database:
```
dotnet ef migrations add InitialCreate

dotnet ef database update
```
- Run the project however you prefer.
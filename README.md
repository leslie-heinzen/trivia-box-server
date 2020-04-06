## Trivia Box

A real-time trivia application for fun and profit.

This repository is for the server.

To run:

1. Download the repository.
2. Restore the solution.
3. Bootstrap the database:
```
dotnet ef migrations add InitialCreate

dotnet ef database update
```
4. Run the project however you prefer.
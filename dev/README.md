# ShipItApp

## Features
- **Email Support**  
  Prompts for, validates, stores, and displays email for each user.  
- **Password Support**  
  Prompts for a password on creation, validates minimum length, stores it (in CSV), and requires it at login.  
- **Users Menu**  
  “Users” option in the signed‑in menu reads and lists all users (excluding passwords) from `users.csv`.  
- **Refactoring**  
  Menu logic extracted into `Menu.cs`.  
- **Validation**  
  - `ValidateStateAbbreviation` ensures proper two‑letter state codes.  
  - `ValidateEmail` ensures proper email format.  
  - `ValidatePassword` ensures a minimum of 6 characters.  
- **Console Formatting**  0
  Colored headers, menu items, prompts, and feedback for clarity.

## Usage
1. 'cd (Your project path)dev/src/ShipItApp'
2. `dotnet restore`  
3. `dotnet run`  
4. Create or log in to a user (password required), then explore the menu.

## Extra
1. 'added sln file manually'
2. 'Added password validations'

   ****Important: The application was built in .NET 8.0.****

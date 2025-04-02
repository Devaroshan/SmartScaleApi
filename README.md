# SmartScaleApi

## Overview
SmartScaleApi is a web API designed to handle operations related to custom units of measurement. It allows users to save custom units, evaluate values for selected units, and convert between different units.

## Project Structure
The project is organized into several key folders:

- **Controllers**: Contains the API controllers that handle HTTP requests.
  - `UnitsController.cs`: Manages requests related to units, including saving and evaluating custom units.

- **Domain**: Contains the core business logic and entities.
  - **Entities**: Defines the data structures used in the application.
    - `CustomUnit.cs`: Represents a custom unit with properties for its name and conversion formula.
  - **Interfaces**: Declares interfaces for repository patterns.
    - `ICustomUnitRepository.cs`: Defines methods for interacting with custom unit data in the database.

- **Infrastructure**: Contains the implementation details for data access.
  - **Data**: Manages database context and repositories.
    - `AppDbContext.cs`: Configures the database context and includes a DbSet for custom units.
    - `CustomUnitRepository.cs`: Implements the repository interface for managing custom units.

- **Services**: Contains business logic services.
  - `CustomUnitService.cs`: Contains logic for saving and retrieving custom units using the repository.

## Setup Instructions
1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Restore the project dependencies using the command:
   ```
   dotnet restore
   ```
4. Update the connection string in `AppDbContext.cs` to point to your database.
5. Run the migrations to set up the database schema:
   ```
   dotnet ef database update
   ```
6. Start the application:
   ```
   dotnet run
   ```

## Usage
- **Save Custom Unit**: Send a POST request to `/api/units/save` with a JSON body containing the custom unit details.
- **Evaluate Value**: Send a GET request to `/api/units/evaluate` with query parameters for the unit and value.
- **Convert Units**: Send a POST request to `/api/units/convert` with a JSON body containing the conversion request details.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
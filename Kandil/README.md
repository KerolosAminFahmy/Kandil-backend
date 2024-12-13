# Kandil For Clean Architecture And Repository Pattern
#### This kandil provides a robust structure for building scalable applications using Clean Architecture and the Repository Pattern. The project is organized into several layers to promote separation of concerns, maintainability, and testability.

## 🏗️ Project Structure

1. ### Kandil.Domain

    * Entities: Core business models are defined here. This layer represents the heart of the application and contains business rules that are independent of any external systems like databases or APIs.
    * Dependencies: Shared classes or utilities that are used across the domain layer.

2. ### Kandil.Application

    * RepositoryInterfaces: Defines interfaces for repositories used for accessing data. Each entity in the Domain layer will have a corresponding repository interface in this layer.
    * Services: Contains application logic and use cases. This layer acts as a mediator between the presentation/UI layer and the domain layer.
    * DTO (Data Transfer Objects): Classes that are used to pass data between layers.
    * ConstantOrEnum: Definitions of constants and enumerations used across the application.

3. Kandil.Infrastructure

    * RepositoryImplementation: Concrete implementations of the repository interfaces declared in the Application layer. This is where the actual database operations occur, utilizing Entity Framework Core or other ORM frameworks.
    * Data: Database context and configurations for working with data.
    * Migrations: Database migrations for managing schema changes over time.
    * Dependencies: Shared classes or utilities required by the infrastructure layer.

4. Kandil.Web

    * Web Project: The entry point for the application. This project hosts the API, handles HTTP requests, and connects the Application layer with the outside world.

## 🧩 Repository Pattern

#### The project contains a generic repository interface IBaseRepository<T> that provides basic CRUD operations:

* GetById(int id)
* GetAll()
* Add(T entity)
* Update(T entity)
* Delete(T entity)

-> Additional Features:
Asynchronous versions of the basic methods: GetByIdAsync, GetAllAsync, AddAsync, etc.
Support for filtering data with Find() and FindAsync() methods that accept expressions. 

-> Range operations like AddRange and DeleteRange for bulk data handling.

-> Count operations to retrieve data counts from the repository.


🔧 Getting Started 

1. Prerequisites
    .NET Core SDK
    Entity Framework Core

2. Setup Instructions
    1. Clone the Repository:

        ```bash 
        git clone https://github.com/your-repo-url.git
        ```

    2. Navigate to the Project Directory:
        ```bash 
        cd KandilCleanArchitectureAndRepositoryPattern
        ```

    3. Update the Database Connection: In appsettings.json, configure the database connection string to match your environment.

    4. Run Database Migrations:

        ```dotnet ef database update```

    3. Build and Run the Application:
        ```bash
        dotnet build
        dotnet run
        ```
## 🏗️ Clean Architecture Overview
  * Domain Layer: Contains business entities and interfaces, ensuring that core business logic is independent of external dependencies.
  * Application Layer: Manages application logic, use cases, and interacts with the domain through services and repositories.
  * Infrastructure Layer: Contains implementation details, such as database connections, external services, and repository implementations.
  * Web Layer: Hosts the API or UI, connects users or external systems to the application.

## ⚙️ Customizing the Kandil

  1.  Adding New Entities:

  2. Create new entity classes in the Domain.Entities folder.
  3. Define the repository interface for the entity in Application.RepositoryInterfaces.
  4. Implement the repository in Infrastructure.RepositoryImplementation.
  5. Extending the Repository: You can add more methods to the IBaseRepository<T> or create specific repositories for entities as needed.

## 🔄 How to Rename the Kandil

Follow these steps to rename the kandil to your desired project name:

### 1. Rename the Solution and Project Files

* Rename the solution (.sln) file:
  * Right-click the solution file in Visual Studio or File Explorer, choose Rename, and change it to your desired project name.
* Rename all the project folders (Kandil.Application, Kandil.Domain, Kandil.Infrastructure, Kandil.Web) to the new name:
  * Right-click the project folder, choose Rename, and update each of the folder names with your desired project name.

### 2. Update Project Names in Visual Studio
* In Visual Studio, right-click on each project (e.g., Kandil.Application, Kandil.Domain) in Solution Explorer, choose Rename, and change the project name to match the folder name.

* Also, rename the project namespaces by:

  * Right-clicking the project, choosing Properties, and changing the Assembly name and Default namespace to the new project name.
### 3. Find and Replace Namespaces
Since the default namespaces in your codebase will be based on the original kandil name (e.g., Kandil.Application, Kandil.Domain), you will need to update these across all files.

* Use Find and Replace functionality in Visual Studio:
  * Press Ctrl + Shift + F to open the Find and Replace dialog.
  * Search for Kandil and replace it with your new project name (e.g., MyProjectName).
  * Make sure to update all occurrences in code, including the namespace declarations in each file.
### 4. Update the appsettings.json and Other Configuration Files
* Open appsettings.json and other configuration files to ensure that any project-specific paths or references are updated with the new project name.
### 5. Update Dependencies in Solution and Project Files
* Open the solution file (.sln) in a text editor (e.g., Visual Studio Code or Notepad++), and replace any remaining references to KandilCleanArchitectureAndRepositoryPattern with your new project name.

* For each .csproj file, ensure that the <AssemblyName> and <RootNamespace> tags (if present) are updated to reflect the new project name.

### 6. Rebuild the Solution
After renaming, rebuild the solution to ensure that all project references are correct and no issues exist with the new naming.
# Open Weather Api

### Objective

- Develop a Rest API using .NET 7 which will consume the Open Weather API and store its data in a database for future queries.

### Features

- Four-layer structure (API, Application, Domain, Infra): ensures a clear separation of responsibilities and facilitates code maintenance.
  

### Layers

- API: handles HTTP and HTTPS requests, routes these requests to the appropriate services, and provides the appropriate responses.
  
- Application: responsible for containing the application's business logic.
  
- Domain: represents the domain model, the entities, and everything related to the core of the business rule. It is independent of all other layers.
  
- Infra: responsible for storing and accessing data in the database and mapping external services such as automapper, external APIs, etc.

### Technologies Used

- .NET 7: version of the .NET framework used for developing the application.
  
- Entity Framework Core: this technology is the basis for database access, allowing interaction with MySQL efficiently.
  
- SQL SERVER: chosen database management system for the project.
  
- AutoMapper: this library helps map objects from different structures, facilitating data transfer between layers.
  
- FluentValidation: used to validate input data and ensure that only valid information is processed by the application.

- RestSharp: Used to make requests to external APIs.


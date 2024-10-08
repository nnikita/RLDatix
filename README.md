# weather-app

The purpose of this application is to fetch and display the weather information from OpenWeatherMap API based on latitude and longitude value.

## Getting started

The following prerequisites are required to build and run the solution:

* .NET 8.0 SDK (latest version)
* Visual Studio 2022
* Postman

Repository is placed on the Main branch. 
Download the code.
Build and run the app using Visual Studio. 
Download postman collection from the docs folder and use that to call api endpoints.
Call the API endpoint using postman to see the response.
The below input paramters should be passed in the API 
* lat
* lon

The application uses key based authentication. The API key needs to be passed in the Header information.


## Technologies
* ASP.NET Core 8
* MediatR
* Clean Architecture

## Project status
The project is currently incomplete. The below modules are to be added -
* SPA page to take the input and display the result is to be implemented. 
* Unit Testing and Integration Testing to be added with xUnit framework.
* Cross-cutting concerns like error handling,logging can be centralized using Pipeline behaviour.
* Validation can be done using fluent validation.


## Approach Taken

The Application follows Clean Architecture.

The Application project represents the Application layer and contains all business logic. 
This project implements CQRS (Command Query Responsibility Segregation), with each business use case represented by a single command or query (In this project there is no use case for Command only Query is implemented)
This layer has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. 
If we want to additional features a new interface would be added to the Application and the implementation would be created within Infrastructure.

The Infrastructure project represents the Infrastructure layer and contains classes for accessing external resources i.e web services. 
These classes should be based on interfaces defined within the Application layer.

The API project represents the Presentation layer. This project is based on  ASP.NET Core. This layer depends on both the Application and Infrastructure layers. Please note the dependency on Infrastructure is only to support dependency injection. Therefore Program.cs should include the only reference to Infrastructure.



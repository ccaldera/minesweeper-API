# Minesweeper API
## Fully functional Minesweeper Web API

Minesweeper API is a cloud-enabled, mobile-ready, C# & Angular.io-powered application.

## Features

- This minesweeper implementation can be fully played using only it's web api endpoints.
- Web app client presented is just an example of it's implementation.
- Secured using simple bearer tokens.
- Backend designed using DDD architecture.

## Dependencies

Minesweeper-API uses a number of open-source projects to work properly:

- [ASP.NET CORE](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0) - v5.0
- [Angular.io](https://angular.io/) - v11.0.5
- [Automapper](https://automapper.org/) - v10.1.1
- [MongoDb](https://www.mongodb.com/) - v2.11.5
- [Fluent Validator](https://docs.fluentvalidation.net/en/latest/aspnet.html) - v9.3.0
- [Moq](https://github.com/moq/Moq.AutoMocker) - v4.15.1

And of course Minesweeper-API itself is open source with a [public repository](https://github.com/ccaldera/minesweeper-API)
 on GitHub.
 
## Technical decisions

- MongoDb was selected for this project due to its simple and efficient way to store complex objects like th eones used in the project
- There are 4 main folders for this solution
    1. CC.Minesweeper.Core: Contains all the game and security logic
    2. CC.Minesweeper.Infrastructure: Contains the infrastructure needed by the core project and specific implementations
    3. CC.Minesweeper.Api: Contains the web api layer to expose the app available methods
    4. CC.Minesweeper.Tests: Contains all unit tests for this solution
- There is one folder named CC.Minesweeper.Web, that contains the angular.io web application
- There is one folder named SolutionItems that contains the postman tests for integration test of the web API


## Documentation

You can visit the swagger endpoint containing the methods and resources required to call the endpoints [here](https://minesweeper-api.azurewebsites.net/swagger/index.html)

1. Register a user using **/api/users/register**
 - Email and password are required.
2. Get a security token using **/api/token**
 - Token format is "bearer {{token}}" and must be included in the "Authorization" header.
3. Create a new game **/api/games/new**
 - 3.1 It must contain rows, columns, and number of mines, the bigger the grid the longer it will take to process later.
4. You can reveal a cell by requesting **/api/games/{id}/reveal**
 - 4.1 x and y values are required.
6. You can flag and un-flag a cell by calling **/api/games/{id}/switch-flag**
7. Finally, you can delete a game by calling **/api/games/{id}**

You can test the web api client [here](https://ccaldera-minesweeper.azurewebsites.net/login)


## And that's it, have fun!!

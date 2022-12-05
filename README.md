[![CI](https://github.com/wiktoriakeller/go-code/actions/workflows/ci.yml/badge.svg)](https://github.com/wiktoriakeller/go-code/actions/workflows/ci.yml)

# Go-Code
REST Web API that follows the Onion architecture and CQRS pattern with React Native mobile app. Users can take part in online courses and solve quizes.

## Technologies
* ASP.NET Core 6
* Entity Framework Core
* MediatR
* FluentValidation
* AutoMapper
* xUnit
* FluentAssertions
* Moq
* AutoFixture
* React Native
* TypeScript
* Docker

## Setup
To run the application create an .env file with necessary variables.

Next run the following command:
```
docker compose up --build
```

Then you can access the API at ``localhost:5000/swagger``.

To stop the running containers use:
```
docker compose stop
```

To delete containers use:
```
docker compose down
```

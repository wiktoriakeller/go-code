[![CI](https://github.com/wiktoriakeller/go-code/actions/workflows/ci.yml/badge.svg)](https://github.com/wiktoriakeller/go-code/actions/workflows/ci.yml)

[![CI](https://github.com/wiktoriakeller/go-code/actions/workflows/cd.yml/badge.svg)](https://github.com/wiktoriakeller/go-code/actions/workflows/cd.yml)

# Go-Code

REST Web API that follows the Onion architecture and CQRS pattern with React Native mobile app. Users can take part in online courses and solve quizes.

## Technologies

- ASP.NET Core 7
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- xUnit
- FluentAssertions
- Moq
- AutoFixture
- React Native
- TypeScript
- Docker

## Setup

Create `.env` file in the root directory, it should contain the following variables:

- `SA_PASSWORD` - password to the database,
- `JWT_KEY` - secret key for the JWT tokens,
- `ADMIN_EMAIL` - admin email,
- `ADMIN_USERNAME` -admin username,
- `ADMIN_PASSWORD` - admin password

Example file:

```
SA_PASSWORD=Your_password123
JWT_KEY=super_secret_key
ADMIN_EMAIL=admin@admin
ADMIN_USERNAME=admin
ADMIN_PASSWORD=P@ssw0rd123
```

Next run the following command:

```
docker compose up --build
```

Then you can access the API at `localhost:5000/swagger`.

To stop the running containers use:

```
docker compose stop
```

To delete containers use:

```
docker compose down
```

To run the mobile application locally, create `.env` file in the `\src\mobile` directory with base url to the api:

```
BASE_API_URL=http://192.168.1.74:5219/api/v1/
```

Then in the `\src\mobile` directory run:
```
expo start
```

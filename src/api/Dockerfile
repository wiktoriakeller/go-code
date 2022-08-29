FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy .csproj files and restore project dependencies
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY *.sln .
COPY /GoCode.Domain/*.csproj ./GoCode.Domain/
COPY /GoCode.Application/*.csproj ./GoCode.Application/
COPY /GoCode.Infrastructure/*.csproj ./GoCode.Infrastructure/
COPY /GoCode.WebAPI/*.csproj ./GoCode.WebAPI/
RUN dotnet restore ./GoCode.WebAPI/GoCode.WebAPI.csproj

# Copy everything else to our current working directory and publish
COPY . .
RUN dotnet publish ./GoCode.WebAPI/GoCode.WebAPI.csproj -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GoCode.WebAPI.dll"]

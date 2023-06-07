# PostgreSQL and csharp

## setup
```
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Injecting the connection string
- instead of a static one, goal is to make the connection string dynamically available from the docker container
- placerholder in our code. that the docker compose should inject the real-current connection string into.

First we add the placeholder to the appsettings.json:
```json
"AllowedHosts": "*",
  "ConnectionStrings": {
    "Defaultconnection" : ""
  }
```

Then we inject the db-context into the builder
```cs
var builder = WebApplication.CreateBuilder(args);

// inject the connection string:
var conn = builder.Configuration.GetConnectionString("DefaulCconnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(conn)
);
```

## Add entitiy migrations
```
dotnet ef migrations add "initial-migrations"
```

## Dockerfile

```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 433

## copy project files over and restore dependencies etc.
COPY *.csproj ./
RUN dotnet restore

## build the binary
COPY . ./
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
## copy over the binary from build container
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "CS_Postgre_Example.dll"]
```
- docker build -t postgre_api
- docker run -p 8081:80 -e ASPNETCORE_URLS=http://+:80 postgre_api

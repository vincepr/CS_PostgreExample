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
ENTRYPOINT ["dotnet", "CS_PostgreExample.dll"]
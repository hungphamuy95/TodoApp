FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./TodoApp/*.csproj ./
RUN dotnet restore

# copy and build everything else
COPY TodoApp/. ./TodoApp/
WORKDIR /app/TodoApp
RUN dotnet publish -c Release -o app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TodoApp.dll"]

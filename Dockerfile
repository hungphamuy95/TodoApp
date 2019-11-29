FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./TodoApp/*.csproj ./
COPY ./TodoApp.Test/*.csproj ./

RUN dotnet restore

# copy and build everything else
COPY . ./
WORKDIR /app/TodoApp
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/TodoApp/out ./
ENTRYPOINT ["dotnet", "TodoApp.dll"]

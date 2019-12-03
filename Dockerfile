FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY TodoApp.sln .
COPY TodoApp/TodoApp.csproj TodoApp/TodoApp.csproj
COPY TodoApp.Test/TodoApp.Test.csproj TodoApp.Test/TodoApp.Test.csproj
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish TodoApp -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out/ .
CMD ["dotnet", "TodoApp.dll"]
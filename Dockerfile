FROM mcr.microsoft.com/dotnet/core/sdk:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY TodoApp.sln .
COPY TodoApp/TodoApp.csproj TodoApp/TodoApp.csproj
COPY TodoApp.Test/TodoApp.Test.csproj TodoApp.Test/TodoApp.Test.csproj
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish TodoApp -c Release -o out
RUN dotnet build TodoApp.Test


FROM mcr.microsoft.com/dotnet/core/aspnet:2.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/TodoApp/out/ .
CMD ["dotnet", "TodoApp.dll"]
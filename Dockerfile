FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./TodoApp/*.csproj ./
RUN dotnet restore

# copy and build everything else
COPY TodoApp/. ./TodoApp/
WORKDIR /app/TodoApp
RUN dotnet publish -c Release -o app

# Build runtime image
FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app
COPY --from=build /app/TodoApp/out ./
ENTRYPOINT ["dotnet", "TodoApp.dll"]

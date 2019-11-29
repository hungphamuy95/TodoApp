FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./TodoApp/*.csproj ./

RUN dotnet restore

# copy and build everything else
COPY TodoApp/. ./TodoApp/
WORKDIR /app
RUN dotnet publish -c Release -o app

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TodoApp.dll"]

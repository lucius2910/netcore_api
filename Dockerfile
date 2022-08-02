FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
# COPY . .
# RUN for file in $(ls /*/*.csproj); do dotnet restore ; done
# RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish "src/Seisankanri.Api/Seisankanri.Api.csproj" -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Seisankanri.Api.dll"]
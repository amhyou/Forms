# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["forms.csproj", "./"]
RUN dotnet restore "./forms.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "forms.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "forms.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "forms.dll"]

# Use the official ASP.NET 6 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 6 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["PeopleCrud.csproj", "."]
RUN dotnet restore "./PeopleCrud.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PeopleCrud.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "PeopleCrud.csproj" -c Release -o /app/publish

# Use the base image and copy the published output
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleCrud.dll"]

# Set environment variables
ENV ASPNET_ENVIRONMENT=Production
ENV ConnectionStrings__PeopleCrudContext="Server=IRANGAMSH-WLT\\SQLEXPRESS;Database=PeopleCrud.Data;Trusted_Connection=True;MultipleActiveResultSets=true"

# Add a health check
HEALTHCHECK --interval=30s --timeout=3s CMD curl --fail http://localhost:80/ || exit 1

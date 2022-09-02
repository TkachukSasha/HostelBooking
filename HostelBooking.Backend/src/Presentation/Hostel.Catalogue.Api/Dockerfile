#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presentation/Hostel.Catalogue.Api/Hostel.Catalogue.Api.csproj", "src/Presentation/Hostel.Catalogue.Api/"]
COPY ["src/Infrastructure/Hostel.Catalogue.Infrastructure/Hostel.Catalogue.Infrastructure.csproj", "src/Infrastructure/Hostel.Catalogue.Infrastructure/"]
COPY ["src/Core/Application/Hostel.Catalogue.Application/Hostel.Catalogue.Application.csproj", "src/Core/Application/Hostel.Catalogue.Application/"]
COPY ["src/Core/Domain/Hostel.Catalogue.Domain/Hostel.Catalogue.Domain.csproj", "src/Core/Domain/Hostel.Catalogue.Domain/"]
COPY ["src/Shared/Hostel.Shared/Hostel.Shared.csproj", "src/Shared/Hostel.Shared/"]
RUN dotnet restore "src/Presentation/Hostel.Catalogue.Api/Hostel.Catalogue.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Hostel.Catalogue.Api"
RUN dotnet build "Hostel.Catalogue.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hostel.Catalogue.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hostel.Catalogue.Api.dll"]

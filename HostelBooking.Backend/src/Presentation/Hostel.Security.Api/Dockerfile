#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presentation/Hostel.Security.Api/Hostel.Security.Api.csproj", "src/Presentation/Hostel.Security.Api/"]
COPY ["src/Infrastructure/Hostel.Security.Infrastructure/Hostel.Security.Infrastructure.csproj", "src/Infrastructure/Hostel.Security.Infrastructure/"]
COPY ["src/Shared/Hostel.Shared/Hostel.Shared.csproj", "src/Shared/Hostel.Shared/"]
COPY ["src/Core/Domain/Hostel.Security.Domain/Hostel.Security.Domain.csproj", "src/Core/Domain/Hostel.Security.Domain/"]
COPY ["src/Core/Application/Hostel.Security.Application/Hostel.Security.Application.csproj", "src/Core/Application/Hostel.Security.Application/"]
RUN dotnet restore "src/Presentation/Hostel.Security.Api/Hostel.Security.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Hostel.Security.Api"
RUN dotnet build "Hostel.Security.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hostel.Security.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hostel.Security.Api.dll"]

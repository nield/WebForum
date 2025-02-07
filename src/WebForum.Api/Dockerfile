#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src

COPY ./NuGet.config /src

COPY ["src/WebForum.Api/WebForum.Api.csproj", "src/WebForum.Api/"]
COPY ["src/WebForum.Application/WebForum.Application.csproj", "src/WebForum.Application/"]
COPY ["src/WebForum.Domain/WebForum.Domain.csproj", "src/WebForum.Domain/"]
COPY ["src/WebForum.Infrastructure/WebForum.Infrastructure.csproj", "src/WebForum.Infrastructure/"]
RUN dotnet restore "src/WebForum.Api/WebForum.Api.csproj" --configfile /src/NuGet.config
COPY . .
WORKDIR "/src/src/WebForum.Api"
RUN dotnet build "WebForum.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebForum.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebForum.Api.dll"]
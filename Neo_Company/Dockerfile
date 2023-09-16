#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update && apt-get install -y \
    libegl1-mesa

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Neo_Company/Neo_Company.csproj", "Neo_Company/"]
RUN dotnet restore "Neo_Company/Neo_Company.csproj"
COPY . .
WORKDIR "/src/Neo_Company"
RUN dotnet build "Neo_Company.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Neo_Company.csproj" -c Release -o /app/publish -r linux-musl-x64 --no-self-contained /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neo_Company.dll"]
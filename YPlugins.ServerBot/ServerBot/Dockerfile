#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
#SDK нужен чтобы могли вызвать restore

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build  
WORKDIR /src
COPY ["ServerBot.csproj", "./"]
RUN dotnet restore "./ServerBot.csproj"
COPY . .
RUN dotnet build "ServerBot.csproj" -c Release -o /app/build

RUN dotnet publish "ServerBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR /app/publish
#COPY /app/publish .
ENTRYPOINT ["dotnet", "ServerBot.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TelegramBot.csproj", "."]
COPY Localization.dll .
RUN dotnet restore "TelegramBot.csproj"
COPY . .
RUN dotnet build "TelegramBot.csproj" -c Release -o /app/build

RUN dotnet publish "TelegramBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR /app/publish
ENTRYPOINT ["dotnet", "TelegramBot.dll"]
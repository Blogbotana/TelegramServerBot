FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TelegramBot.csproj", "."]
RUN dotnet restore "TelegramBot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TelegramBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelegramBot.dll"]
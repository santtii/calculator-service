#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CalculatorAPI.Client/CalculatorAPI.Client.csproj", "src/CalculatorAPI.Client/"]
COPY ["src/CalculatorAPI.Core/CalculatorAPI.Core.csproj", "src/CalculatorAPI.Core/"]
RUN dotnet restore "src/CalculatorAPI.Client/CalculatorAPI.Client.csproj"
COPY . .
WORKDIR "/src/src/CalculatorAPI.Client"
RUN dotnet build "CalculatorAPI.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculatorAPI.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculatorAPI.Client.dll"]
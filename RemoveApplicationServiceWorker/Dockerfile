#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RemoveApplicationServiceWorker/RemoveApplicationServiceWorker.csproj", "RemoveApplicationServiceWorker/"]
RUN dotnet restore "RemoveApplicationServiceWorker/RemoveApplicationServiceWorker.csproj"
COPY . .
WORKDIR "/src/RemoveApplicationServiceWorker"
RUN dotnet build "RemoveApplicationServiceWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RemoveApplicationServiceWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RemoveApplicationServiceWorker.dll"]
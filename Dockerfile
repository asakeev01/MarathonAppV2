FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["Application/Application.csproj", "Application/"]

COPY ["Domain/Domain.csproj", "Domain/"]

COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

COPY ["WebApi/WebApi.csproj", "WebApi/"]

RUN dotnet restore "WebApi/WebApi.csproj"

COPY . .

WORKDIR "/src/WebApi"

RUN dotnet build "WebApi.csproj" -c Release -o /app/build



FROM build AS publish

RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish



FROM base AS final

WORKDIR /app

RUN mkdir /app/staticfiles

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]

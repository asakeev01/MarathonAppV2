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

RUN apt-get update && \
    apt-get install -y \
        libasound2 \
        libatk-bridge2.0-0 \
        libatk1.0-0 \
        libatspi2.0-0 \
        libc6 \
        libcairo2 \
        libcups2 \
        libdbus-1-3 \
        libdrm2 \
        libexpat1 \
        libfontconfig1 \
        libgbm1 \
        libgcc1 \
        libglib2.0-0 \
        libgtk-3-0 \
        libnspr4 \
        libnss3 \
        libpango-1.0-0 \
        libpangocairo-1.0-0 \
        libstdc++6 \
        libuuid1 \
        libx11-6 \
        libx11-xcb1 \
        libxcb-dri3-0 \
        libxcb1 \
        libxcomposite1 \
        libxcursor1 \
        libxdamage1 \
        libxext6 \
        libxfixes3 \
        libxi6 \
        libxkbcommon0 \
        libxrandr2 \
        libxrender1 \
        libxshmfence1 \
        libxss1 \
        libxtst6 \
        ca-certificates \
        fonts-liberation \
        lsb-release \
        wget \
        xdg-utils && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

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

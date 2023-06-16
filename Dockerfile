# API katman�n� build etmek i�in SDK g�r�nt�s�n� kullan�n
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# API projesini kopyalay�n ve ba��ml�l�klar� restore edin
COPY StarWars.Api/StarWars.Api.csproj ./StarWars.Api/
COPY StarWars.Core/StarWars.Core.csproj ./StarWars.Core/
RUN dotnet restore StarWars.Api/StarWars.Api.csproj

# API kodunu kopyalay�n ve build edin
COPY StarWars.Api/. ./StarWars.Api/
COPY StarWars.Core/. ./StarWars.Core/
WORKDIR /app/StarWars.Api
RUN dotnet build -c Release -o out

# Geli�tirici sertifikas� olu�turun
RUN dotnet dev-certs https --trust

# API uygulamas�n� ba�lat�n
CMD ["dotnet", "run", "--project", "StarWars.Api.csproj", "--urls", "http://0.0.0.0:44444"]

# UI katman�n� build etmek i�in SDK g�r�nt�s�n� kullan�n
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# UI projesini kopyalay�n ve ba��ml�l�klar� restore edin
COPY StarWars.UI.csproj ./
RUN dotnet restore

# UI kodunu kopyalay�n ve build edin
COPY . ./
RUN dotnet build -c Release -o out

# Geli�tirici sertifikas� olu�turun
RUN dotnet dev-certs https --trust

# UI uygulamas�n� ba�lat�n
CMD ["dotnet", "run", "--project", "StarWars.UI.csproj", "--urls", "http://0.0.0.0:80"]

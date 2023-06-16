# API katmanýný build etmek için SDK görüntüsünü kullanýn
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# API projesini kopyalayýn ve baðýmlýlýklarý restore edin
COPY StarWars.Api/StarWars.Api.csproj ./StarWars.Api/
COPY StarWars.Core/StarWars.Core.csproj ./StarWars.Core/
RUN dotnet restore StarWars.Api/StarWars.Api.csproj

# API kodunu kopyalayýn ve build edin
COPY StarWars.Api/. ./StarWars.Api/
COPY StarWars.Core/. ./StarWars.Core/
WORKDIR /app/StarWars.Api
RUN dotnet build -c Release -o out

# Geliþtirici sertifikasý oluþturun
RUN dotnet dev-certs https --trust

# API uygulamasýný baþlatýn
CMD ["dotnet", "run", "--project", "StarWars.Api.csproj", "--urls", "http://0.0.0.0:44444"]

# UI katmanýný build etmek için SDK görüntüsünü kullanýn
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# UI projesini kopyalayýn ve baðýmlýlýklarý restore edin
COPY StarWars.UI.csproj ./
RUN dotnet restore

# UI kodunu kopyalayýn ve build edin
COPY . ./
RUN dotnet build -c Release -o out

# Geliþtirici sertifikasý oluþturun
RUN dotnet dev-certs https --trust

# UI uygulamasýný baþlatýn
CMD ["dotnet", "run", "--project", "StarWars.UI.csproj", "--urls", "http://0.0.0.0:80"]

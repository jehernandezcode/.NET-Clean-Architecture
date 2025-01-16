# Etapa 1: Restaurar dependencias y construir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos .csproj de cada proyecto para restaurar las dependencias
COPY ["src/Web.API/Web.API.csproj", "Web.API/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Infraestructure/Infraestructure.csproj", "Infraestructure/"]
RUN dotnet restore "Web.API/Web.API.csproj"

# Copiar el resto del código y compilar la aplicación
COPY ./src/Web.API /src/Web.API
COPY ./src/Application /src/Application
COPY ./src/Domain /src/Domain
COPY ./src/Infraestructure /src/Infraestructure
WORKDIR /src/Web.API
RUN dotnet publish -c Release -o /app

EXPOSE 8080
EXPOSE 80

# Etapa 2: Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Web.API.dll"]

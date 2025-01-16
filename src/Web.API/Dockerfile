# Etapa 1: Restaurar dependencias y construir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos .csproj de cada proyecto para restaurar las dependencias
COPY ["src/Web.API/Web.API.csproj", "Web.API/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Infraestructure/Infraestructure.csproj", "Infraestructure/"]

# Restaurar dependencias como usuario root
RUN dotnet restore "Web.API/Web.API.csproj"

# Copiar el resto del código y compilar la aplicación
COPY ./src/Web.API /src/Web.API
COPY ./src/Application /src/Application
COPY ./src/Domain /src/Domain
COPY ./src/Infraestructure /src/Infraestructure
WORKDIR /src/Web.API

RUN dotnet publish -c Release -o /app

# Etapa 2: Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Crear un usuario no privilegiado
RUN useradd -m appuser

WORKDIR /app

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build /app .

# Cambiar al usuario no privilegiado
USER appuser

# Ejecutar la aplicación como usuario no privilegiado
ENTRYPOINT ["dotnet", "Web.API.dll"]

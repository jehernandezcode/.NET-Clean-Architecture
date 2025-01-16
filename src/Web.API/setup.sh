#!/bin/bash

# Variables de entorno
USER=${MSSQL_USER:-nuevo_usuario}
PASSWORD=${MSSQL_PASSWORD:-NuevaContraseña1234}

# Esperar a que el servidor SQL esté listo
until /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "SELECT 1" &> /dev/null
do
    echo "Esperando a que SQL Server esté listo..."
    sleep 5
done

# Verificar si el usuario ya existe
USER_EXISTS=$(/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "IF EXISTS (SELECT name FROM sys.sql_logins WHERE name = N'$USER') SELECT 1 ELSE SELECT 0" -h -1)

if [ $USER_EXISTS -eq 0 ]; then
    # Crear el usuario con el password si no existe
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "CREATE LOGIN [$USER] WITH PASSWORD=N'$PASSWORD';"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "CREATE USER [$USER] FOR LOGIN [$USER];"
    echo "Usuario y contraseña creados correctamente"
else
    echo "El usuario [$USER] ya existe, no se creó nuevamente."
fi

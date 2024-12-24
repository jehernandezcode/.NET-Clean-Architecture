# .NET-Clean-Architecture + DDD

Este es un proyecto en .net Core 8 en la creacion de customers, adoptando clean arquitecture + cqrs (MediaTR) + SqlServer + unit test. Ademas se agregan elementos de CI/CD como pipelines con github actions y analisis estatico del codigo.

## Stack tecnologico

**Client:** Open Api, Postman

**Server:** .Net Core 8 (C#)

**DB:** SqlServer


## Variables de entorno

Para ejecutar este proyecto, deberá agregar las siguientes variables de entorno a su archivo appsettings.Development

```bash
"ConnectionStrings": {
    "Database": ""
}
```

## Deployment

Para este proyecto debe ejecutar con el sdk de dotnet compatible con la version 8, en la raiz ejecutar

Instalar dependiencias

```bash
  dotnet restore
```

Puede usar dotnet build y dotnet run correspondientemente o abrir la solucion con microsoft visual studio u otro IDE

```bash
  dotnet build
```

```bash
  dotnet run
```

## Github Actions y SonarQube
Este proyecto tiene definidos jobs para verificar build, test y parametros de SonarQube(cobertura, duplicacion, codesmells), ademas de la definicion de exepciones para el analisis estatico.
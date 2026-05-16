# Random User Console App

Aplicación de consola desarrollada en C# que consume la API pública de [Random User Generator](https://randomuser.me/api/) para obtener información de usuarios aleatorios de forma asíncrona.

## Descripción

Este proyecto permite al usuario indicar cuántos usuarios aleatorios desea obtener. Luego, la aplicación realiza varias solicitudes a la API usando programación asíncrona con `async` y `await`, permitiendo ejecutar múltiples tareas al mismo tiempo sin bloquear la consola.

La aplicación muestra información básica de cada usuario obtenido, como:

- Nombre completo
- Género
- Correo electrónico
- País

También incluye una barra de progreso para mostrar el avance de las solicitudes y manejo de errores con reintentos en caso de fallos de conexión o respuestas inválidas.

## Tecnologías utilizadas

- C#
- .NET
- Aplicación de consola
- HttpClient
- Programación asíncrona con `async` y `await`
- API Random User Generator

## Funcionalidades

- Solicita al usuario la cantidad de usuarios aleatorios que desea obtener.
- Consume la API de Random User Generator.
- Ejecuta múltiples solicitudes de forma concurrente.
- Muestra una barra de progreso en consola.
- Muestra los datos principales de cada usuario.
- Maneja errores de conexión o respuestas no válidas.
- Realiza reintentos cuando ocurre un error.
- Permite buscar más usuarios o salir de la aplicación.

## Estructura del proyecto

```text
RandomUserConsoleApp
│
├── Program.cs
│
├── App
│   └── RandomUserApp.cs
│
├── Dtos
│   └── UserDto.cs
│
├── Interfaces
│   └── IRandomUserService.cs
│
├── Models
│   └── RandomUserResponse.cs
│
└── Services
    └── RandomUserService.cs
# MiAppBD2

Aplicación móvil multiplataforma desarrollada con **.NET MAUI (.NET 10)** para la gestión de tareas usando **SQLite** como almacenamiento local.

## Descripción

Este proyecto implementa una app sencilla de tareas con persistencia local en SQLite.

> **Nota importante:** para simplificar el aprendizaje y la estructura del código, el acceso a base de datos se implementó con métodos **síncronos** (`SQLiteConnection`).
>
> Para cualquier entorno de **producción**, se recomienda migrar a métodos **asíncronos** (`SQLiteAsyncConnection`) para evitar bloqueos del hilo principal y mejorar la experiencia de usuario.

## Tecnologías

- .NET MAUI
- .NET 10
- SQLite
- Paquete `sqlite-net-pcl`

## Requisitos previos

- Visual Studio 2026 (o versión compatible con .NET 10 y MAUI)
- Workloads de MAUI instalados
- SDK de .NET 10
- Android SDK (y opcionalmente iOS/MacCatalyst/Windows según plataforma destino)

## Ejecución del proyecto

1. Clona el repositorio:
   - `git clone https://github.com/enedamian/MiAppBD2.git`
2. Abre la solución en Visual Studio.
3. Selecciona el destino (Android Emulator/Device, Windows, etc.).
4. Ejecuta con **F5**.

## Estructura básica

- `MiAppBD2/Models` → modelos de datos
- `MiAppBD2/Services` → servicios (incluye acceso a SQLite)
- `MiAppBD2/*.xaml` y `*.xaml.cs` → interfaz y lógica de vistas

## Base de datos

La base de datos SQLite se crea en el almacenamiento local de la app (`FileSystem.AppDataDirectory`) con el archivo `tareas.db`.

## Estado del proyecto

Proyecto educativo / demostrativo. Antes de publicarlo como app final se recomienda:

- Migrar operaciones de BD a asíncronas.
- Añadir manejo robusto de errores.
- Incorporar pruebas y validaciones adicionales.


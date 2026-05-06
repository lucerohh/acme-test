# ACME Task Management System

## 1. Descripción del proyecto

Sistema de gestión de tareas desarrollado para la empresa ACME.  
Permite a los usuarios autenticarse, crear y gestionar tareas, organizarlas por categorías y persistir la información en una base de datos relacional.

El sistema está compuesto por un backend en .NET 8 (Web API) y un frontend en Angular.

---

## 2. Arquitectura del sistema

### Backend (.NET 8)

El backend está estructurado en una arquitectura en capas:

- Domain
  Contiene las entidades del negocio y reglas principales.
  Entidades: TaskItem, TaskCategory, User.

- Application
  Contiene las implementaciones de los servicios del sistema (commands y queries).
  Maneja la lógica de negocio.

- Infrastructure
  Implementa el acceso a datos utilizando Entity Framework Core.
  Contiene repositorios y configuración de base de datos.

- API (Presentation)
  Expone los endpoints REST.
  Maneja autenticación y controladores.

---

### Frontend (Angular)

El frontend está estructurado siguiendo una arquitectura basada en features.

#### Estructura del proyecto

- Core
  Contiene la lógica transversal de la aplicación, utilizada a nivel global.
  Incluye:
  - Interceptor HTTP para manejo de JWT
  - Guards para protección de rutas
  - Servicios base de autenticación
  - Configuraciones globales

- Features
  Contiene los módulos funcionales del sistema.
  Incluye:
  - Gestión de tareas (listado, creación y edición)
  - Gestión de autenticación (login)
  - Lógica específica de negocio por funcionalidad

- Shared
  Contiene elementos reutilizables en toda la aplicación.

---

## 3. Tecnologías utilizadas

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt.Net

### Frontend
- Angular
- RxJS
- TypeScript

---

## 4. Autenticación

El sistema utiliza autenticación basada en JWT.

Flujo:
1. El usuario inicia sesión con email y contraseña.
2. El backend valida las credenciales.
3. Se genera un token JWT.
4. El token se envía al frontend.
5. El frontend lo almacena y lo incluye en cada solicitud.

Las contraseñas se almacenan utilizando hashing con BCrypt.

---

## 5. Backend - Funcionalidades

- Autenticación de usuarios con JWT
- CRUD de tareas
- CRUD de categorías
- Relación entre usuario y tareas
- Filtros por estado y categoría
- Validaciones de negocio

---

## 6. Base de datos

Se utilizó Entity Framework Core con enfoque Code First.

Entidades principales:
- User
- TaskItem
- TaskCategory

Relaciones:
- Un usuario tiene muchas tareas
- Un usuario tiene muchas categorías
- Una categoría tiene muchas tareas

---

### Migraciones

La base de datos se generó mediante migraciones de Entity Framework Core:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Cómo ejecutar el proyecto

#### Backend
dotnet restore  
dotnet ef database update  
dotnet run  

#### Frontend
npm install  
npm start
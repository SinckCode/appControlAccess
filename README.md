# 📱 appControlAccess1 - Aplicación de Control de Acceso (MAUI)

Este repositorio contiene una aplicación multiplataforma desarrollada en **.NET MAUI**, diseñada para gestionar el sistema de **Control de Acceso** conectado a un ESP32 mediante una API REST.  
La app permite a los guardias y personal autorizado administrar el acceso a las instalaciones de manera remota y en tiempo real.

---

## 🧭 Características Principales

✅ **Autenticación de Guardias**
- Inicio de sesión con correo electrónico y contraseña.
- Validación contra la API REST.

✅ **Control de Pluma**
- Apertura y cierre manual de la barrera desde la app.
- Compatibilidad con múltiples plumas.

✅ **Registro de Invitados**
- Alta de invitados con nombre y UID de tarjeta RFID.
- Envío de datos a la API.

✅ **Estado del Estacionamiento**
- Visualización de los espacios ocupados y libres.
- Actualización periódica de estado.

✅ **Historial de Accesos**
- Consulta de logs recientes de accesos y eventos.

✅ **Gestión de Usuarios**
- Listado y administración de empleados e invitados.

---

## 📂 Estructura del Proyecto

```
appControlAccess1/
├── appControlAccess1.csproj           # Proyecto MAUI principal
├── App.xaml                           # Recursos globales de la app
├── AppShell.xaml                      # Navegación de Shell
├── Models/                            # Modelos de datos
│   ├── AccessLog.cs
│   ├── ParkingSpot.cs
│   └── User.cs
├── Services/                          # Servicios HTTP
│   ├── ApiConfig.cs
│   ├── AuthService.cs
│   ├── GateService.cs
│   └── UserService.cs
├── Views/                             # Vistas de la aplicación
│   ├── LoginPage.xaml / .cs
│   ├── DashboardPage.xaml / .cs
│   ├── GuestRegistrationPage.xaml / .cs
│   ├── GateControlPage.xaml / .cs
│   └── UserManagementPage.xaml / .cs
├── Resources/                         # Imágenes, fuentes y estilos
└── Program.cs                         # Configuración de la aplicación
```

---

## ⚙️ Requisitos

- **.NET 8 SDK** (o superior)
- Visual Studio 2022 (o superior) con carga de trabajo **.NET MAUI**
- Conexión a la API REST desplegada previamente (por ejemplo, vía Ngrok)
- Windows/macOS para compilación
- Android/iOS/Windows como plataforma de destino

---

## 🚀 Instalación y Configuración

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tu-usuario/appControlAccess1.git
   cd appControlAccess1
   ```

2. **Configurar la URL base de la API**
   Edita el archivo:
   ```
   /Services/ApiConfig.cs
   ```
   Cambia la constante:
   ```csharp
   public const string BaseUrl = "https://tu-ngrok-url.ngrok-free.app/api";
   ```

3. **Restaurar paquetes**
   ```bash
   dotnet restore
   ```

4. **Compilar**
   ```bash
   dotnet build
   ```

5. **Ejecutar**
   ```bash
   dotnet run
   ```
   O desde Visual Studio, pulsa **Iniciar Depuración**.

---

## 🌐 Funcionalidades

### 🔑 Autenticación

- Página de Login:
  - Campos: email, contraseña.
  - Botón de iniciar sesión.
  - Lógica de autenticación mediante `AuthService`.

---

### 🏠 Dashboard Principal

- Vista resumen:
  - Estado de espacios de estacionamiento.
  - Historial reciente de accesos.
  - Botón para abrir/cerrar la pluma.
  - Botón para registrar invitados.
  - Botón para gestión de usuarios.
  - Botón de cerrar sesión.

---

### 🚦 Control de Pluma

- Selector de pluma (Pluma 1 o Pluma 2).
- Botones:
  - **Abrir pluma**
  - **Cerrar pluma**
- Confirmaciones visuales y por mensaje.

---

### 🧑‍💼 Registro de Invitados

- Formulario:
  - Nombre del invitado.
  - UID de la tarjeta RFID.
- Botón para registrar.
- Mensaje de éxito/error.

---

### 🗃️ Gestión de Usuarios

- Listado de usuarios existentes.
- Opciones de agregar, editar o eliminar usuarios.
- Diseño amigable y adaptado a tabletas.

---

## 📦 Servicios HTTP

La aplicación consume endpoints de la API REST:

| Servicio         | Ruta Base                      |
|------------------|--------------------------------|
| Autenticación    | `/api/auth/`                   |
| Pluma            | `/api/pluma/` y `/api/pluma2/`|
| Parking          | `/api/parking/`               |
| Logs             | `/api/logs/`                  |
| Invitados        | `/api/access/`               |
| Usuarios         | `/api/users/`                 |

Ejemplo de envío de solicitud en `AuthService.cs`:
```csharp
var response = await client.PostAsync(
    $"{ApiConfig.BaseUrl}/auth/login",
    new StringContent(json, Encoding.UTF8, "application/json")
);
```

---

## 🎨 Personalización Visual

- Colores principales: tonos rosa claro y blanco.
- Íconos y botones adaptados a dispositivos táctiles.
- Navegación con **Shell** y páginas independientes.

---

## 📝 Notas Importantes

- **La URL de la API debe estar actualizada** antes de ejecutar.
- La app requiere que el servidor API esté en línea.
- Para producción se recomienda usar HTTPS.

---

## 🙌 Contribuciones

Si deseas contribuir:

- Haz un **Fork**
- Crea una nueva rama
- Realiza tus cambios
- Envía un **Pull Request**

---

## 📄 Licencia

Este proyecto se distribuye bajo la licencia MIT.

---

## 📬 Contacto

Si tienes dudas o sugerencias, puedes escribirme a:

📧 [soyangeldavid1@gmail.com]

---

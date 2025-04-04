# API de Harry Potter ✨🧙‍♂️

Una API RESTful construida con .NET Core 8 y SQL Server que permite gestionar personajes del universo de Harry Potter. Incluye autenticación mediante JWT para asegurar el acceso, ideal para ser consumida desde aplicaciones de escritorio como WinForms.

---

## 🧰 Tecnologías Utilizadas
- .NET Core 8
- SQL Server (con procedimientos almacenados)
- Swagger
- Microsoft.EntityFrameworkCore
- Microsoft.AspNetCore.Authentication.JwtBearer

---

## 🔒 Autenticación JWT
Para acceder a los endpoints de esta API, es necesario autenticarse utilizando el endpoint de login. Este genera un token **Bearer** que debe enviarse en los encabezados de las peticiones.

### Endpoint de Login
```http
POST /api/Auth/login
```

**Body (JSON):**
```json
{
  "usuario": "Usuario",
  "password": "Usuario@"
}
```

**Respuesta (JSON):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

Usa este token en cada petición:
```
Authorization: Bearer TU_TOKEN
```

---

## 📦 Endpoints CRUD de Personajes
Todos los siguientes endpoints requieren autenticación:

| Método | Ruta               | Acción                        |
|--------|--------------------|-------------------------------|
| GET    | /api/personajes    | Obtiene todos los personajes  |
| GET    | /api/personajes/{id} | Obtiene un personaje por ID |
| POST   | /api/personajes    | Crea un nuevo personaje       |
| PUT    | /api/personajes/{id} | Actualiza un personaje     |
| DELETE | /api/personajes/{id} | Elimina un personaje       |

### Modelo de Datos del Personaje
```json
{
  "id": 1,
  "nombre": "Hermione Granger",
  "apodo": "La más brillante de su edad",
  "imagen": "https://ruta.com/hermione.jpg"
}
```

---

## ⚙️ Requisitos Previos
- .NET SDK 8
- Servidor SQL Server

---

## 🛠 Instalación y Configuración

1. **Clonar el repositorio**
```bash
git clone https://github.com/tuusuario/api-hogwarts.git
cd api-hogwarts
```

2. **Configurar `appsettings.json`**
Edita los valores de conexión a la base de datos y la clave secreta del token JWT:
```json
"ConnectionStrings": {
  "PersonajesContext": "Data Source=TU_SERVIDOR;Initial Catalog=TU_DB;User Id=TU_USUARIO;Password=TU_PASS;Encrypt=True;Trust Server Certificate=True"
},
"JwtSettings": {
    "SecretKey": "ClaveDeAlMenos32Caracteres"
  }
```

3. **Ejecutar la API**
```bash
dotnet run
```

---

## 🧪 Pruebas
La API incluye Swagger para probar todos los endpoints directamente desde la interfaz web:
```
http://localhost:5000/swagger
```

---

## 🔮 Posibles Mejoras
- Registro y recuperación de contraseña.
- Paginación y búsqueda avanzada.
- Carga de imágenes en base64 o almacenamiento en la nube.

---

📚 *Desarrollado como una práctica mágica para aprender autenticación, CRUDs y consumo desde aplicaciones WinForms.*

# Ordenes de compra API

API REST construida con .NET 8, que implementa operaciones basicas para productos, clientes y órdenes con autenticación JWT.

## Ejecución

### Local

```bash
cd SimpleApi
dotnet run
```

La API estará disponible en `http://localhost:port` (el puerto exacto se muestra en la consola al iniciar).

La base de datos SQLite (`app.db`) se crea automáticamente al arrancar la aplicación.

### Docker

```bash
# Build
docker build -t simpleapi -f SimpleApi/Dockerfile .

# Run the container
docker run -p 8080:8080 simpleapi
```

La API estará disponible en `http://localhost:8080`.

## Swagger

Una vez iniciada la aplicación, puedes acceder a Swagger siguiente URL (docker):

```
http://localhost:8080/swagger
```


## Autenticación

Todos los endpoints (excepto `/api/auth/login`) requieren un token JWT en el header `Authorization`.

**Para obtener el token usa el siguiente payload en el login endpoint:**

```json
{
  "username": "admin",
  "password": "admin"
}
```

**Respuesta:**
```json
{
  "token": "jwt_token"
}
```
<img width="1498" height="396" alt="image" src="https://github.com/user-attachments/assets/35be4ece-3fd5-4cf7-87cc-8014fe1d967c" />
<img width="1462" height="305" alt="image" src="https://github.com/user-attachments/assets/90fdda29-b6b7-4fa2-8f34-03e19e6cc11e" />


**Usar el token en las peticiones (o agregarlo en todas las request de Swagger usando el Authorize button):**
```
Authorization: Bearer <jwt_token>
```
<img width="1578" height="624" alt="image" src="https://github.com/user-attachments/assets/dc5abaa4-4580-4477-8af3-16d6685e7e26" />


> **Nota:** Las credenciales `admin/admin` son solo para demostración. En una aplicación real, la autenticación debería delegarse a un proveedor de identidad como Azure AD.

---

## Endpoints disponibles

### Autenticación

| Método | Ruta | Descripción | Auth requerida |
|--------|------|-------------|----------------|
| POST | `/api/auth/login` | Obtiene un token JWT | No |

---

### Productos (`/api/products`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/products` | Lista todos los productos |
| POST | `/api/products` | Crea un nuevo producto |
| PUT | `/api/products/{id}` | Actualiza un producto existente |
| DELETE | `/api/products/{id}` | Elimina un producto |

**Crear producto**
```json
{
  "name": "Laptop",
  "price": 999.99,
  "stock": 10
}
```

**Actualizar producto**
```json
{
  "name": "Laptop Pro",
  "price": 1199.99,
  "stock": 5
}
```

---

### Clientes (`/api/customers`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/customers` | Lista todos los clientes |
| POST | `/api/customers` | Crea un nuevo cliente |

**Crear cliente**
```json
{
  "fullname": "Carlos Mendoza",
  "email": "carlos.mendoza@gmail.com"
}
```

---

### Órdenes (`/api/orders`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/orders` | Lista todas las órdenes |
| GET | `/api/orders/{id}` | Obtiene el detalle de una orden con sus ítems |
| POST | `/api/orders` | Crea una nueva orden |

**Crear orden**
```json
{
  "customerId": 1,
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}
```

> Al crear una orden se valida que el cliente exista, que los productos existan y que haya stock suficiente. El stock de cada producto se descuenta automaticamente.

---

## Manejo de errores

| Código | Descripción |
|--------|-------------|
| 400 | Error de validación — se devuelven los mensajes de error específicos |
| 401 | No autenticado o token inválido/expirado |
| 404 | Recurso no encontrado |
| 500 | Error interno del servidor |

**Ejemplo de respuesta 400:**
```json
{
  "message": "Wrong request with the following errors:",
  "errors": [
    "Product name is required.",
    "Price must be greater than zero."
  ]
}
```

---

## Tests creatos con XUnit, FluentAssertions y AAA pattern

```bash
dotnet test
```

## Notas generales
- Test unitarios creados como extra, los cuales usan una base de datos en memoria (sin dependencias externas).
- En una aplicación real, la autenticación debería delegarse a un proveedor de identidad como Azure AD o un Auth Service dedicado
- Algunos servicios usan AutoMapper para demostración, aunque personalemnte prefiero mapear manualmente para mayor control y mejor performance
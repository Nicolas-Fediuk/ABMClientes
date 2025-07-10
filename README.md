# ABM de clientes 

Proyecto web de gestión de clientes (alta, baja y modificación).

### Tecnologías usadas
- Frontend: Blazor WebAssembly
- BackEnd: Web API REST
- Base de datos: SQL Server
- Lenguaje: C#

### API

Base URL: https://localhost:7172/

#####  Obtener todos los clientes
Metodo: GET

URL: api/cliente

Respuesta:
```json
[
  {
    "cliente_ID": 1,
    "cliente_NOMBRES": "Nicolas",
    "cliente_APELLIDOS": "Fediuk",
    "cliente_FECNAC": "1998-11-23",
    "cliente_CUIT": "20123456789",
    "cliente_DOMICILIO": "Calle Falsa 123",
    "cliente_TELEFONO": "123456789",
    "cliente_EMAIL": "nf@gmail.com"
  }
]
```

#####  Obtener cliente por ID
Metodo: GET

URL: api/cliente/{id}

Respuesta:
```json
{
  "cliente_ID": 1,
  "cliente_NOMBRES": "Nicolas",
  "cliente_APELLIDOS": "Fediuk",
  "cliente_FECNAC": "1998-11-23",
  "cliente_CUIT": "20123456789",
  "cliente_DOMICILIO": "Calle Falsa 123",
  "cliente_TELEFONO": "123456789",
  "cliente_EMAIL": "nf@gmail.com"
}
```



#####  Obtener clientes filtrados
Metodo: POST

URL: api/cliente/filtro

Respuesta:
```json
[
  {
    "cliente_ID": 1,
    "cliente_NOMBRES": "Nicolas",
    "cliente_APELLIDOS": "Fediuk",
    "cliente_FECNAC": "1998-11-23",
    "cliente_CUIT": "20123456789",
    "cliente_DOMICILIO": "Calle Falsa 123",
    "cliente_TELEFONO": "123456789",
    "cliente_EMAIL": "nf@gmail.com"
  }
]
```

#####  Crear clientes
Metodo: POST

URL: api/cliente/crear

Respuesta: OK, BadRequest o Server Error

#####  Editar cliente
Metodo: PUT

URL: api/cliente/editar

Respuesta: OK, BadRequest o Server Error

#####  Eliminar cliente
Metodo: DELETE

URL: api/cliente/eliminar/{id}

Respuesta: Eliminado

# TaskMaster 🚀

TaskMaster es una solución integral para la gestión de proyectos colaborativos desarrollada en .NET MAUI. Esta aplicación permite a equipos de trabajo organizar tareas, realizar seguimientos en tiempo real y mantener la información sincronizada gracias a su API REST integrada.<br>

📋 Tabla de Contenidos
- Requisitos Previos
- Configuración del Entorno
- Estándares de Código
- Cómo Ejecutar el Proyecto
- Tecnologías Utilizadas

🛠️ Requisitos Previos<br>
Antes de comenzar, asegúrate de tener instalado lo siguiente:
- Visual Studio 2022 con la carga de trabajo de .NET MAUI.
- .NET 9 SDK (o superior).
- Docker Desktop (para el despliegue de la API y base de datos).

⚙️ Configuración del Entorno

Clonar el repositorio:
Bash

```bash
git clone https://github.com/Alejandro-Del-Valle-Valles/Desarrollo_Interfaces.git
cd TaskMaster
```

Restaurar dependencias:
Bash

```bash
dotnet restore
```

📏 Estándares de Código

Para mantener la calidad y consistencia del proyecto, seguimos estas reglas:

- **Documentación**: Todo método público debe incluir **comentarios XML** para compatibilidad con DocFX.
- **Nomenclatura**: Uso de **PascalCase** para clases y métodos, y **camelCase** para variables locales.
- **Idioma**: El código y los comentarios internos deben estar en **español**.
- **Pull Requests**: Todo cambio debe pasar por una revisión de código antes de integrarse a la rama **main**.


🚀 Cómo Ejecutar el Proyecto

Opción A: Usando Docker (Recomendado para la API) 

Para levantar la infraestructura necesaria (Base de Datos y API REST):
Bash
```bash
docker-compose up -d
```


Opción B: Comandos de .NET CLI 

Para ejecutar la aplicación móvil/escritorio:
- **Android**: ```dotnet build -t:Run -f net8.0-android```
- **Windows**: ```dotnet build -t:Run -f net8.0-windows10.0.19041.0```

🧰 Tecnologías Utilizadas

- **Frontend**: .NET MAUI (Multi-platform App UI).
- **Backend**: ASP.NET Core Web API.
- **Persistencia**: Entity Framework Core & PostgreSQL.
- **Documentación**: Markdown & XML Comments.
# Proyecto de Facturación en C#

## Descripción
Este proyecto es un sistema de facturación desarrollado en **C#** utilizando una **arquitectura en capas**. Fue realizado como parte de una asignatura universitaria para aplicar conceptos de **programación orientada a objetos** y **buenas prácticas** en el desarrollo de software. Permite la gestión de clientes, productos y facturas, con almacenamiento de datos en **SQL Server** mediante **Entity Framework**.

## Características
- **Gestión de clientes, productos y facturas**.
- **Arquitectura en capas**:
  - **Presentación**: Interfaz gráfica (Windows Forms).
  - **Negocio**: Lógica de negocio y validaciones.
  - **Datos**: Acceso a base de datos mediante Entity Framework.
- Conexión a una base de datos SQL Server.

## Requisitos
- **Visual Studio 2022** o superior.
- **SQL Server**.
- **Entity Framework**.

## Instalación
1. Clonar este repositorio:
   ```bash
   git clone  https://github.com/dahiamm/Sistema_Facturacion.git
   ```

2. Abrir la solución en **Visual Studio**.

3. Configurar la cadena de conexión a la base de datos en el archivo `app.config`.

4. Restaurar los paquetes NuGet y compilar el proyecto.

5. **Restaurar la base de datos**:
   - En la carpeta `DatabaseBackup`, encontrarás el archivo de respaldo de la base de datos (`BACKUP_BD_FACTURAS.BAK`).
   - Restaurar el respaldo utilizando **SQL Server Management Studio (SSMS)**:
     1. Abrir SSMS y conectarse a la instancia de SQL Server.
     2. Clic derecho sobre "Bases de Datos" y seleccionar "Restaurar base de datos".
     3. Seleccionar la opción "Dispositivo" y buscar el archivo `BACKUP_BD_FACTURAS.BAK`.
     4. Completar el proceso de restauración.
   
6. Ejecutar las migraciones de **Entity Framework** si es necesario.

7. Ejecutar la aplicación desde Visual Studio.

## Uso
1. **Registrar clientes**: Añadir, modificar o eliminar clientes.
2. **Gestionar productos**: Crear, editar y eliminar productos del inventario.
3. **Emitir facturas**: Crear facturas asociando clientes y productos.

## Backup de la Base de Datos
El archivo de respaldo de la base de datos se encuentra en la carpeta `DatabaseBackup` y puede ser restaurado utilizando **SQL Server Management Studio (SSMS)**.

## Contribuciones
Las contribuciones son bienvenidas. Si deseas colaborar, por favor realiza un _fork_ del proyecto y crea un **pull request** con tus cambios.

# PIZZA_DEMO
Proyecto para Demo de programacion

#Librerias Usadas
Se hizo uso de Entity Framework Core 5.0.0
Librerias Security.Cryptography, Web.CodeGeneration.Design 5.0.0, EntityFrameworkCore.SqlServer 5.0.0,dotnet-aspnet-codegenerator, EntityFrameworkCore.Design 5.0.0
Se habilitaron las Migraciones para aplicar enfoque CodeFirst(Esto es para manejar versionamiento de la base de datos, es decir que sin el backup es posible generar todas las entidades y crear la base de datos)
Se hizo uso de manejo de autenticacion con la clase Identity para manejar los accesos de los usuarios

#Pasos para la restaurar la aplicacion en otra PC
1 - > Restaurar el Backup de Base de datos(DB_PIZZA.bak) ubicado en la carpeta "DataBaseBackUp" 
2 - > Copiar la solucion en una carpeta local y abrirla
3 - > Cambiar la cadena de conexion en el archivo "appsettings.json" reemplazando la propiedad Data Source  "JEREMY\\SQLEXPRESS" por el nombre del servidor Local donde se restaurara la aplicacion
4 - > Compilar el proyecto y agregar las librerias/referencias que sean necesarias en caso solicite alguna al momento del Build.
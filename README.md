# Proyecto de Taller de Lenguajes II - Tadeo Alonso Ruiz

El proyecto consta de un Kanban realizado con ASP.NET sigiendo el Modelo Vista-Controllador (MVC)
En él se pueden crear, editar y eliminar Usuarios, Tableros y Tareas que se almacenan en una base de datos SQLite

## Usuarios
Cada usuario cuenta con un Nombre de Usuario y una Contraseña que le permiten acceder al sistema así como un rol (administrador o simple).
Los usuarios del tipo 'simple' solo poseen acceso y control sobre sus Tableros y Tareas
Por otro lado los usuarios del tipo 'administrador' pueden editar y eliminar todas los Tableros y Tareas del sistema.

## Tableros
Los tableros poseen un ID, un usuario asignado, un nombre y una descripción, la cual puede ser nula
Si el usuario al que se encuentra asignado el tablero es borrado de la base de datos, también se eliminará el tablero.

## Tareas
Las tareas poseen un ID, un usuario y un tablero asignado, un nombre, un estado y un color.
En el caso de que se produzca un borraro de un usuario o un tablero, las tareas asignadas a dicho usuario o tablero no se borrarán, su usuario y tablero serán cambiados a NULL.
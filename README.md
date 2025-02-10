# SistemaGuanajuato
CRUD manejo de usuarios y roles en una plataforma, diseñado en Blazor Server C# .NET 7 


# Estructura del proyecto / Carpetas

- wwwroot => Recursos del lado del cliente puedes archivos js, css, paginas html externas.

- Data => aqui se incluye todo el modelado de nuestras tablas asi como diferentes Dto (data tranfer object) para que no hagamos uso de toda la tabla y exponer su seguridad (lo ideal es que manejemos todo esto en un API que este vinculada al proyecto y asi solo consumir los recursos de la base d datos).

- Page => contiene las vistas y codigo backend que se manejara del lado del cliente, al ser un proyecto diseñado en blazor podemos ejecutar codigo C# del lado del cliente, sin afectar a la expierencia del cliente, las paginas estan compuestas por atributos las cuales si un usuario no tiene la credencial o token, no se le permite el acceso.

- Services => Aqui vienen todas las funciones "fuertes" que maneja el proyecto desde la autenticacion por roles de los usuarios, llamada de datos a la Base de Datos y funciones que pueden se llamadas desde las diferenes vistas.

- Shared => La carpeta incluye las "plantillas" que podemos repetir en algunas vistas, como la barra lateral de navegacion, pie de pagina, encabeza o barra de tareas.

- Imports => Instanciamos las librerias que se utilizaran en todo el proyecto, en caso de que se repitan mucho podemos instanciarlas aqui y ya seran visibles en todo el proyecto.

- App.razor => Es el encargado de dibujar todo nuestro proyecto, tambien aqui se maneja la cascada de autenticacion es decir para que las demas paginas verifiquen si el usuario cumple con ciertos atributos.

- appssettings.json => Aqui vse incluyen la configuracion de nuestras conexiones, en este caso seria de la base de datos, es importante mencionar que aqui se deberia de poner tambien la configuracion para nuestro servicio de correo, en este proyecto se opto por ponerlo de forma directa en la clase CorreoService

- Program => Es el encargado de darle vida a nuestro proyecto, aqui se dan de alta servicios, funciones y metodos que maneja Blazor Server, aqui se puede observar que esta configurado nuestras conexion hacia la BD, servicios y varios metodos que manejamos en el proyecto.

#Base de datos

- SistemaGuanajuatoDB => Esta diseñada en Sql Server en ella se encuentran las cuatro principales tablas del proyecto, asi como varios Store Procedure los cuales ayudan para la creacion de usuarios, validacion de cuenta y Logueo de la plataforma, se manejo de esta forma para optar por la seguridad de la base de datos, ya que C# cuenta con una funcion llamada SqlParameter la cual nos apoya con la seguridad de la Base de datos, la base de datos no tiene relacion con sus llaves foranes y primarias pero su estructura esta pensado para que no se pueden eliminar registros sin antes pasar por otra tabla.

#Usuarios de prueba

Para probar el sistema la base de datos viene con varios usuarios.

Usuario administrador: mfabian440@gmail.com
Contrasena: 1234

Usuario registrado: mfabian440@hotmail.com
Contrasena: 1234

Se pueden agregar mas roles dependiendo de las necesidades pero para este proyecto solo se utilizaron dos.

#Posibles errores

- En el archivo appssettings compruebe que se esta utilizando la cadena de conexion correcta: Data Source=(Servidor);Initial Catalog=(Base de datos);User id=(Usuari);Password=(Contraseba);TrustServerCertificate=True;
- El proyecto no cuenta con migraciones directas, se tiene que restaurar la base de datos directo de Microsoft Sql Server Management Studio o el Administrador de su preferencia.
- Validar que visual estudio cuente con .NET 7, liga de descarga para 64 bits https://dotnet.microsoft.com/es-es/download/dotnet/thank-you/runtime-desktop-7.0.20-windows-x64-installer
- En algunas maquinas el firewall de windows puede que bloquee el proyecto debido a los puertos que utiliza, generar una regla para que permita el uso a este proyecto.

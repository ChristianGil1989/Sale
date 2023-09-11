# ProyectSaleApi

El proyecto Sale es un APi que desarrollada en .NET 7, usando entity framework, MVM, inyeccion de dependecias, DTO, entre otras tecnologias.

El proyecto usa un DBContext para la conexion con la base de datos, tambien cuenta con Seeder para que este valide si la base de datos existe o no y la cree de ser necesario poblando algunos datos.

Implementa el patron de diseño de MVC con tres controladores con protocolo de seguridad, Interfaces,DTOs y peticiones HTTP, dentro del proyecto esta la collecion de postman para realizar la pruebas, el Seeder se encarga de crear un usuario con userName: admi@gmail.com, pasword:abcd1234 y role admin el cual se puede usar para obtener el token de seguridad y hacer las peticiones que se requieran.

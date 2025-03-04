# Sistema de Gestión Hospitalaria - Hospital de Quito

## 1. Introducción
El presente documento describe en detalle la arquitectura, tecnologías utilizadas, funcionalidades y validaciones implementadas en el sistema de gestión hospitalaria desarrollado para el "Hospital de Quito". Este sistema está diseñado para ser utilizado exclusivamente por administradores y permite la gestión integral de pacientes, médicos, citas, tratamientos, especialidades y facturación. La aplicación se ha desarrollado siguiendo una arquitectura en capas para garantizar modularidad, mantenibilidad y eficiencia en la manipulación de los datos. Además, se han implementado diversas validaciones y procedimientos almacenados para asegurar la integridad y seguridad de los datos.

## 2. Tecnologías Utilizadas
Para el desarrollo del sistema se emplearon las siguientes tecnologías y herramientas:

- **Entorno de Desarrollo**: Visual Studio 2022
- **Lenguaje de Programación**: C#
- **Framework de Desarrollo**: .NET con Entity Framework y Bootstrap.
- **Base de Datos**: SQL Server con procedimientos almacenados
- **ORM**: Entity Framework para la gestión de datos
- **Seguridad**: Bcrypt.Net para encriptación de contraseñas
- **Frontend**: HTML, CSS, JavaScript, DataTables.js para la manipulación y visualización de datos
- **Arquitectura**: Modelo en capas (Entidad, Datos, Negocios y Presentación)

## 3. Arquitectura del Sistema
El sistema está diseñado bajo una arquitectura en capas para mejorar la organización del código y facilitar la escalabilidad:

### 3.1 Capa Entidad
Esta capa contiene las clases que representan las entidades del dominio. Cada entidad corresponde a una tabla en la base de datos, y se encarga de definir la estructura de los datos manejados, en donde se colocan los atributos de cada clase. Por ejemplo, la entidad `Paciente` tiene propiedades como `Id`, `Nombre`, `Apellido`, `FechaNacimiento`, `Telefono`, `Email`, y `Direccion`.
Las entidades también definen relaciones con otras entidades. Por ejemplo, la entidad Cita tiene una relación con Paciente y Médico, lo que permite acceder a los datos relacionados de manera sencilla.

### 3.2 Capa Datos
Es la capa responsable de la interacción directa con la base de datos. Utiliza Entity Framework para la manipulación de datos y también incluye la ejecución de procedimientos almacenados para mejorar la eficiencia y la seguridad de las consultas. Por ejemplo, se utilizan procedimientos almacenados para operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en las tablas de la base de datos.

### 3.3 Capa Negocios
La Capa Negocios es el corazón del sistema, ya que contiene la lógica de negocio y las reglas que garantizan la integridad y consistencia de los datos. Aquí se implementan las validaciones, reglas de negocio y procesos que definen cómo se deben manejar los datos antes de ser enviados a la base de datos o presentados al usuario.

#### Características Principales:
- **Validaciones**: Se asegura de que los datos cumplan con las reglas de negocio antes de ser procesados. Por ejemplo, validar que un correo electrónico tenga un formato válido o que una fecha de nacimiento no sea futura.
- **Reglas de Negocio**: Implementa lógica específica del negocio, como restricciones de horario para las citas.
- **Coordinación entre Capas**: Actúa como intermediario entre la Capa Presentación y la Capa Datos, asegurando que los datos se procesen correctamente antes de ser almacenados o mostrados.

### 3.4 Capa Presentación
La Capa Presentación es la interfaz entre el usuario y el sistema. Aquí se implementan los controladores y vistas que permiten a los usuarios interactuar con la aplicación. Esta capa se encarga de mostrar la información de manera clara y amigable, utilizando tecnologías como HTML, CSS, JavaScript y frameworks como Bootstrap para mejorar la experiencia del usuario.

#### Características Principales:
- **Controladores: Gestionan las solicitudes del usuario y coordinan las respuestas entre la Capa Negocios y las vistas.
- **Vistas:** Representan la interfaz gráfica del sistema, donde los usuarios pueden ver y manipular los datos.
- **Diseño Responsivo**: Utiliza Bootstrap para garantizar que la interfaz sea compatible con diferentes dispositivos (escritorio, tablet, móvil).
- **Interactividad**: Emplea JavaScript y librerías como DataTables.js para mejorar la interactividad y la visualización de datos.

## 4. Entity Framework
Entity Framework (EF) es un ORM (Object-Relational Mapping) que permite interactuar con la base de datos a través de objetos y clases en lugar de consultas SQL directas. Sus principales ventajas incluyen:

- **Abstracción de la base de datos**: Permite trabajar con objetos en lugar de tablas y registros.
- **Gestión automática de consultas SQL**: Facilita la inserción, actualización, eliminación y consulta de datos.
- **Mejor mantenimiento del código**: Reduce la complejidad de la gestión de datos y mejora la legibilidad del código.
- **Compatibilidad con procedimientos almacenados**: Se pueden ejecutar SP (Stored Procedures) dentro de EF.

### 4.1 DbContext
DbContext es una clase fundamental en Entity Framework Core, la clase DbContext actúa como un puente entre la aplicación y la base de datos, gestionando las entidades (objetos que representan tablas) y sus relaciones, así como realizando operaciones como consultas, inserciones, actualizaciones y eliminaciones. DbContext maneja la conexión con la base de datos, incluyendo la apertura y cierre de la conexión, lo que simplifica el trabajo del desarrollador.

### 4.2 HospitalDbContext: El Contexto de la Base de Datos
En la Capa Datos, se implementa la interacción con la base de datos utilizando Entity Framework Core. El núcleo de esta capa es la clase HospitalDbContext, que hereda de DbContext y actúa como puente entre las entidades del sistema y las tablas de la base de datos.

La clase HospitalDbContext es el componente central de la Capa Datos. Esta clase se encarga de:

- **Mapear las entidades a las tablas de la base de datos**: Cada DbSet<T> representa una tabla en la base de datos y permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre ella.
- **Configurar las relaciones entre entidades**: Define cómo las entidades están relacionadas entre sí (por ejemplo, un médico pertenece a una especialidad, una cita está asociada a un paciente y un médico, etc.).
- **Aplicar restricciones y validaciones**: Configura índices únicos, claves foráneas y otras restricciones para garantizar la integridad de los datos.

## 5. Base de Datos
La base de datos está basada en SQL Server y utiliza procedimientos almacenados para mejorar la eficiencia en el manejo de grandes volúmenes de datos. Las principales entidades en la base de datos son:

- **Pacientes**: Contiene información de los pacientes registrados en el hospital.
- **Médicos**: Registra los datos de los médicos disponibles en el hospital.
- **Citas**: Administra las citas médicas programadas dentro del horario permitido (7 AM - 6 PM, con intervalos de 30 minutos).
- **Tratamientos**: Contiene los tratamientos asignados a los pacientes.
- **Especialidades**: Lista las especialidades médicas disponibles.
- **Administrador**: Registra los administradores que pueden acceder al sistema.
- **Facturación**: Maneja el registro y visualización de facturas generadas.

### 5.1 Tabla Pacientes
Esta tabla almacena la información personal de los pacientes que son atendidos en el hospital. Cada registro incluye datos como el nombre, apellido, fecha de nacimiento, número de teléfono, correo electrónico y dirección. La tabla está diseñada para garantizar que cada paciente tenga un identificador único (`Id`) y que el correo electrónico sea único para evitar duplicados.

- **Campos**:
  - `Id`: Identificador único del paciente.
  - `Nombre`: Nombre del paciente.
  - `Apellido`: Apellido del paciente.
  - `FechaNacimiento`: Fecha de nacimiento del paciente.
  - `Telefono`: Número de teléfono del paciente.
  - `Email`: Correo electrónico del paciente (único).
  - `Direccion`: Dirección del paciente.

### 5.2 Tabla Médicos
Esta tabla contiene la información de los médicos que trabajan en el hospital. Cada médico tiene un identificador único (`Id`), un nombre, un apellido, una especialidad (relacionada con la tabla `Especialidades`), un número de teléfono y un correo electrónico único.

- **Campos**:
  - `Id`: Identificador único del médico.
  - `Nombre`: Nombre del médico.
  - `Apellido`: Apellido del médico.
  - `EspecialidadId`: Identificador de la especialidad del médico (relación con la tabla `Especialidades`).
  - `Telefono`: Número de teléfono del médico.
  - `Email`: Correo electrónico del médico (único).
  - `Activo`: Especifica si un medico se encuentra trabajando o tiene descanso.

### 5.3 Tabla Especialidades
Esta tabla lista las especialidades médicas disponibles en el hospital. Cada especialidad tiene un identificador único (`Id`) y un nombre, ademas cabe recalcar que no se puede borrar ninguna especialidad si hay un medico que tiene esa especialidad.

- **Campos**:
  - `Id`: Identificador único de la especialidad.
  - `Nombre`: Nombre de la especialidad (por ejemplo, Cardiología, Pediatría, etc.).

### 5.4 Tabla Citas
Esta tabla gestiona las citas médicas programadas. Cada cita tiene un identificador único (`Id`), un identificador del paciente (`PacienteId`), un identificador del médico (`MedicoId`), la fecha y hora de la cita, y el estado de la cita (por ejemplo, "Programada", "Cancelada", "Completada").

- **Campos**:
  - `Id`: Identificador único de la cita.
  - `PacienteId`: Identificador del paciente relacionado con la cita.
  - `MedicoId`: Identificador del médico relacionado con la cita.
  - `FechaHora`: Fecha y hora de la cita.
  - `Estado`: Estado actual de la cita (por ejemplo, "Programada", "Cancelada", "Completada").
  - `BHabilitado`: Si esta en 1 indica que esta habilitado.

### 5.5 Tabla Tratamientos
Esta tabla registra los tratamientos médicos asignados a los pacientes. Cada tratamiento tiene un identificador único (`Id`), un identificador del paciente (`PacienteId`), una descripción del tratamiento, la fecha en que se realizó y el costo asociado.

- **Campos**:
  - `Id`: Identificador único del tratamiento.
  - `PacienteId`: Identificador del paciente relacionado con el tratamiento.
  - `Descripcion`: Descripción del tratamiento.
  - `Fecha`: Fecha en que se realizó el tratamiento.
  - `Costo`: Costo del tratamiento.
  - `BHabilitado`: Si esta en 1 indica que esta habilitado.

### 5.6 Tabla Facturación
Esta tabla maneja la información de las facturas generadas por los servicios médicos. Cada factura tiene un identificador único (`Id`), un identificador del paciente (`PacienteId`), el monto total de la factura, el método de pago utilizado y la fecha en que se realizó el pago.

- **Campos**:
  - `Id`: Identificador único de la factura.
  - `PacienteId`: Identificador del paciente relacionado con la factura.
  - `Monto`: Monto total de la factura.
  - `MetodoPago`: Método de pago utilizado (por ejemplo, "Efectivo", "Tarjeta de Crédito").
  - `FechaPago`: Fecha en que se realizó el pago.
  - `BHabilitado`: Si esta en 1 indica que esta habilitado.

### 5.7 Tabla Administradores
Esta tabla almacena la información de los administradores que tienen acceso al sistema. Cada administrador tiene un identificador único (`Id`), un nombre, un apellido, una contraseña encriptada y un correo electrónico único.

- **Campos**:
  - `Id`: Identificador único del administrador.
  - `Nombre`: Nombre del administrador.
  - `Apellido`: Apellido del administrador.
  - `Clave`: Contraseña encriptada del administrador.
  - `Email`: Correo electrónico del administrador (único).

## 6. Funcionalidades Principales

### 6.1 Autenticación
El sistema cuenta con un módulo de autenticación seguro que permite a los administradores iniciar sesión utilizando sus credenciales. Las contraseñas se almacenan encriptadas utilizando Bcrypt.Net, lo que garantiza la seguridad de la información sensible.

### 6.2 Panel de Control (Dashboard)
El panel de control es el centro de operaciones del sistema, donde los administradores pueden navegar a través de un menú vertical con acceso a todas las secciones del sistema, incluyendo la gestión de pacientes, médicos, citas, tratamientos, especialidades y facturación.

### 6.3 Gestión de Entidades
El sistema permite la gestión completa de las entidades principales (Pacientes, Médicos, Citas, Tratamientos, Especialidades y Facturación) a través de operaciones CRUD (Crear, Leer, Actualizar, Eliminar). Además, se implementa filtrado y paginación de registros utilizando DataTables.js para una mejor experiencia de usuario.

### 6.4 Configuración del Administrador
Los administradores pueden cambiar sus datos personales y modificar su contraseña desde el panel de control, asegurando que siempre tengan acceso actualizado y seguro al sistema.

### 6.5 Facturación
El módulo de facturación permite la visualización de facturas dentro del programa. Aunque no se genera ni envía automáticamente, proporciona una interfaz clara y accesible para el manejo de la información financiera.

## 7. Validaciones Implementadas
Las validaciones fueron implementadas en la Capa de Negocios para garantizar la integridad de los datos. Entre ellas se incluyen:

- **Campos obligatorios**: Todos los campos deben completarse antes de agregar un registro.
- **Fecha de nacimiento**: No puede ser una fecha futura.
- **Agendamiento de citas**: No se pueden agendar citas en fechas pasadas.
- **Restricciones de horario**: Las citas solo pueden programarse entre 7 AM y 6 PM, con duraciones de 30 minutos.
- **Validaciones generales**: Comprobaciones adicionales para evitar inconsistencias en los datos.


## 8. Expresiones Regulares y Validaciones
Se utilizaron expresiones regulares para validar campos como el correo electrónico, el número de teléfono y la cédula de identidad. Estas validaciones se implementaron en la Capa de Negocios para asegurar que los datos ingresados cumplan con los formatos requeridos.

## 9. Módulo de Autenticación (Login)
El módulo de autenticación es una parte crucial del sistema, ya que garantiza que solo los administradores autorizados puedan acceder al sistema. A continuación, se detalla el funcionamiento del login:

### 9.1 AccesoController
El `AccesoController` es el controlador encargado de gestionar las operaciones relacionadas con el inicio de sesión de los administradores. Su funcionamiento se basa en la autenticación de usuarios mediante el correo electrónico y la contraseña, y utiliza la lógica de negocio definida en la clase `AdministradorBL` para realizar las validaciones necesarias.

#### 9.1.1 Método `Index` (GET)
Este método se encarga de mostrar la vista de inicio de sesión. Si hay algún mensaje de error almacenado en `TempData` (por ejemplo, debido a un intento fallido de inicio de sesión), este mensaje se pasa a la vista para ser mostrado al usuario.

- **Funcionamiento**:
  - Verifica si hay un mensaje de error en `TempData`.
  - Si existe, lo asigna a `ViewBag.Error` para que la vista pueda mostrarlo.
  - Retorna la vista de inicio de sesión.

#### 9.1.2 Método `Index` (POST)
Este método maneja la solicitud de inicio de sesión cuando el usuario envía el formulario. Realiza las siguientes acciones:

- **Validación de Campos**:
  - Verifica que tanto el correo electrónico como la contraseña no estén vacíos. Si alguno de los campos está vacío, se almacena un mensaje de error en `TempData` y se redirige al usuario de vuelta a la vista de inicio de sesión.

- **Autenticación**:
  - Utiliza el método `ObtenerAdministradores` de la clase `AdministradorBL` para obtener la lista de administradores.
  - Busca un administrador cuyo correo electrónico coincida con el proporcionado por el usuario.
  - Utiliza el método `VerificarClave` para comparar la contraseña ingresada con la contraseña encriptada almacenada en la base de datos.

- **Manejo de Resultados**:
  - Si no se encuentra un administrador con las credenciales proporcionadas, se almacena un mensaje de error en `TempData` y se redirige al usuario de vuelta a la vista de inicio de sesión.
  - Si las credenciales son correctas, se almacena el identificador del administrador en la sesión (`HttpContext.Session.SetInt32("AdminId", oAdministrador.Id)`) y se redirige al usuario al panel de control (`Home/Index`).

#### 9.1.3 Método `CerrarSesion`
Este método se encarga de cerrar la sesión del administrador. Redirige al usuario de vuelta a la vista de inicio de sesión (`Acceso/Index`), lo que efectivamente cierra la sesión al no mantener ningún dato de autenticación en la sesión.

## 10. Conclusión
El sistema de gestión hospitalaria desarrollado para el "Hospital de Quito" garantiza una administración eficiente de los recursos del hospital, asegurando la seguridad de los datos y la integridad de la información. La aplicación de una arquitectura en capas permite la escalabilidad y el mantenimiento futuro del sistema. Las validaciones implementadas aseguran que solo datos consistentes sean ingresados, y el uso de Entity Framework optimiza el manejo de la base de datos. Este sistema representa una solución confiable para la administración hospitalaria, con un enfoque en la seguridad, la eficiencia y la usabilidad.

## 11. Recomendaciones para el Sistema de Gestión Hospitalaria
### 11.1 Implementación de Autenticación de Dos Factores (2FA)
- **Recomendación**: Agregar un segundo factor de autenticación (por ejemplo, un código enviado al correo electrónico o una aplicación de autenticación como Google Authenticator) para aumentar la seguridad del acceso al sistema.
- **Beneficio**: Reduce el riesgo de acceso no autorizado, incluso si las credenciales son comprometidas.

## 11.2 Autenticación y Roles
 - **Recomendación**: Implementar un sistema de roles y permisos para diferenciar entre administradores y médicos. Los médicos solo tendrían acceso a las funcionalidades relevantes para su trabajo.
- **Beneficio**: Garantiza la seguridad y privacidad de los datos, ya que los médicos solo pueden acceder a la información necesaria para su rol.

## 11.3 Informes y Estadísticas
- **Recomendación**: Agregar un módulo de informes y estadísticas que permita a los administradores generar reportes sobre el rendimiento del hospital, como el número de citas atendidas, ingresos mensuales, etc.
- **Beneficio**: Facilita la toma de decisiones basada en datos.

## 11.4 Eliminación en Cascada Controlada
- **Recomendación**: Implementar políticas de eliminación en cascada de manera controlada. Por ejemplo, si se elimina un médico, asegurar que todas sus citas futuras también se cancelen o se reasignen a otro médico. Esto puede hacerse mediante procedimientos almacenados o lógica en la Capa de Negocios.
- **Beneficio**: Mantiene la consistencia de los datos y evita errores al eliminar registros que tienen dependencias.

window.onload = function () {
    ObtenerPacientes();
};

let objPacientes;

async function ObtenerPacientes() {
    objPacientes = {
        url: "Paciente/ObtenerPacientes",
        cabeceras: ["Id", "Nombre", "Apellido", "FechaNacimiento", "Teléfono", "Email", "Dirección"],
        propiedades: ["id", "nombre", "apellido", "fechaNacimiento", "telefono", "email", "direccion"],
        editar: true,
        eliminar: true
    };
    pintar(objPacientes);
}


window.onload = function () {
    ObtenerPacientes();
};

let objPacientes;

async function ObtenerPacientes() {
    objPacientes = {
        url: "Paciente/ObtenerPacientes",
        cabeceras: ["Id Paciente", "Nombre", "Apellido", "Fecha de Nacimiento", "Teléfono", "Correo Electrónico", "Dirección"],
        propiedades: ["id", "nombre", "apellido", "fechaNacimiento", "telefono", "email", "direccion"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true
    };
    pintar(objPacientes);
}

function buscarPaciente() {
    let form = document.getElementById("formBusquedaPaciente");
    let frm = new FormData(form);

    fetchPost("Paciente/FiltrarPaciente", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}

function agregarPaciente() {
    let form = document.getElementById("formBusquedaPaciente");
    let frm = new FormData(form);

    fetchPost("Paciente/AgregarPaciente", "text", frm, function (data) {
        ObtenerPacientes();
        limpiarDatos("formBusquedaSucursal");
    });
}

function limpiarPaciente() {
    limpiarDatos("formBusquedaPaciente");
    ObtenerPacientes();
}

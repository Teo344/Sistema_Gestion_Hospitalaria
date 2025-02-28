window.onload = function () {
    ObtenerPacientes();
};

let objPacientes;

async function ObtenerPacientes() {
    objPacientes = {
        url: "Paciente/ObtenerPacientes",
        cabeceras: ["Id Paciente", "Documento de Identificacion", "Nombre", "Apellido", "Fecha de Nacimiento", "Teléfono", "Correo Electrónico", "Dirección"],
        propiedades: ["id", "identificacion", "nombre", "apellido", "fechaNacimiento", "telefono", "email", "direccion"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "id"
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
    let form = document.getElementById("formAgregarPaciente");
    let frm = new FormData(form);

    fetchPost("Paciente/AgregarPaciente", "text", frm, function (data) {
        ObtenerPacientes();
        limpiarDatos("formAgregarPaciente");

        cerrarModal();

    });
}

function limpiarPaciente() {
    limpiarDatos("formBusquedaPaciente");
    ObtenerPacientes();
}

function Editar(id) {
    fetchGet(`Paciente/RecuperarPaciente?id=${id}`, "json", function (data) {
        if (data) {
            // Asignar valores a los campos del formulario
            document.querySelector("#formEditarPaciente input[name='id']").value = data.id;
            document.querySelector("#formEditarPaciente input[name='identificacion']").value = data.identificacion;
            document.querySelector("#formEditarPaciente input[name='nombre']").value = data.nombre;
            document.querySelector("#formEditarPaciente input[name='apellido']").value = data.apellido;
            document.querySelector("#formEditarPaciente input[name='fechanacimiento']").value = data.fechaNacimiento.split('T')[0]; // Formatear fecha
            document.querySelector("#formEditarPaciente input[name='telefono']").value = data.telefono;
            document.querySelector("#formEditarPaciente input[name='email']").value = data.email;
            document.querySelector("#formEditarPaciente input[name='direccion']").value = data.direccion;

            abrirModal("modalEditar");
        } else {
            alert("No se encontró el paciente.");
        }
    });
}
function editarPaciente() {
    let form = document.getElementById("formEditarPaciente");
    let frm = new FormData(form);

    fetchPost("Paciente/ActualizarPaciente", "text", frm, function (data) {
        ObtenerPacientes();
        limpiarDatos("formEditarPaciente");
        cerrarModal("modalEditar");
    });
}

function Eliminar(id) {
    fetchGet("Paciente/EliminarPaciente", "text", frm, function (data) {
        ObtenerPacientes();
    });
}
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

    Confirmacion(undefined, "¿Está seguro de guardar el nuevo paciente?", function () {
        fetchPost("Paciente/AgregarPaciente", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarMensaje("Éxito", "El paciente se ha agregado correctamente", "success");
                ObtenerPacientes();
                limpiarDatos("formAgregarPaciente");
                cerrarModal("modalAgregarPaciente");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El número de cédula ingresado no es válido");
                        break;
                    case -3:
                        mostrarError("Error", "El correo electrónico ingresado no es válido");
                        break;
                    case -4:
                        mostrarError("Error", "El número de teléfono ingresado no es válido");
                        break;
                    case -5:
                        mostrarError("Error", "La fecha de nacimiento ingresada no es válido");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar el paciente");
                }
            }
        });
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

    Confirmacion(undefined, "¿Está seguro de realizar los cambios?", function () {
        fetchPost("Paciente/ActualizarPaciente", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El paciente se ha actualizado correctamente");
                ObtenerPacientes();
                limpiarDatos("formEditarPaciente");
                cerrarModal("modalEditar");
            } else {
                // Error
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El número de cédula ingresado no es válido");
                        break;
                    case -3:
                        mostrarError("Error", "El correo electrónico ingresado no es válido");
                        break;
                    case -4:
                        mostrarError("Error", "El número de teléfono ingresado no es válido");
                        break;
                    case -5:
                        mostrarError("Error", "La fecha de nacimiento ingresada no es válido");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al actualizar el paciente");
                }
            }
        });
    });
}

function Eliminar(objPaciente) {
    Confirmacion(undefined, "¿Desea eliminar al paciente " + objPaciente.nombre + " " + objPaciente.apellido + "?", function () {
        fetchGet("Paciente/EliminarPaciente/?id=" + objPaciente.id, "text", function (res) {
            mostrarExito("Éxito", "El paciente se ha eliminado correctamente");
            ObtenerPacientes();    
        });
    });
}

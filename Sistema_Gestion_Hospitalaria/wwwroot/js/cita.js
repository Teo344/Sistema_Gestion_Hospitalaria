window.onload = function () {
    ObtenerCitas();
};

let objCitas;

async function ObtenerCitas() {
    objCitas = {
        url: "Cita/ObtenerCitas",
        cabeceras: ["ID Cita", "Paciente", "Médico", "Fecha y Hora", "Estado"],
        propiedades: ["id", "pacienteIdentificacion", "medicoIdentificacion", "fechaHora", "estado"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objCitas);
}

function buscarCita() {
    let form = document.getElementById("formBusquedaCita");
    let frm = new FormData(form);

    fetchPost("Cita/FiltrarCitas", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}

function agregarCita() {
    let form = document.getElementById("formAgregarCita");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de guardar la nueva cita?", function () {
        fetchPost("Cita/AgregarCita", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "La cita se ha agregado correctamente", "success");
                ObtenerCitas();
                limpiarDatos("formAgregarCita");
                cerrarModal("modalAgregarCita");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "La fecha y hora no pueden ser en el pasado");
                        break;
                    case -3:
                        mostrarError("Error", "El estado de la cita no es válido");
                        break;
                    case -4:
                        mostrarError("Error", "El paciente no existe en la base de datos");
                        break;
                    case -5:
                        mostrarError("Error", "El médico no existe en la base de datos");
                        break;
                    case -6:
                        mostrarError("Error", "La hora de la cita debe estar entre las 7:00 AM y las 6:00 PM");
                        break;
                    case -7:
                        mostrarError("Error", "El médico ya tiene una cita programada en ese horario, recuerda que cada cita tiene un tiempo estimado de 30 minutos");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar la cita");
                }
            }
        });
    });
}

function limpiarCita() {
    limpiarDatos("formBusquedaCita");
    ObtenerCitas();
}

function Editar(id) {
    fetchGet(`Cita/RecuperarCita?id=${id}`, "json", function (data) {
        if (data) {
            // Asignar valores a los campos del formulario
            document.querySelector("#formEditarCita input[name='id']").value = data.id;
            document.querySelector("#formEditarCita input[name='pacienteIdentificacion']").value = data.pacienteIdentificacion;
            document.querySelector("#formEditarCita input[name='medicoIdentificacion']").value = data.medicoIdentificacion;
            document.querySelector("#formEditarCita input[name='fechaHora']").value = data.fechaHora.split('T')[0] + "T" + data.fechaHora.split('T')[1].substring(0, 5); // Formatear fecha y hora
            document.querySelector("#formEditarCita select[name='estado']").value = data.estado;

            abrirModal("modalEditarCita");
        } else {
            alert("No se encontró la cita.");
        }
    });
}

function editarCita() {
    let form = document.getElementById("formEditarCita");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de realizar los cambios?", function () {
        fetchPost("Cita/ActualizarCita", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "La cita se ha actualizado correctamente");
                ObtenerCitas();
                limpiarDatos("formEditarCita");
                cerrarModal("modalEditarCita");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "La fecha y hora no pueden ser en el pasado");
                        break;
                    case -3:
                        mostrarError("Error", "El estado de la cita no es válido");
                        break;
                    case -4:
                        mostrarError("Error", "El paciente no existe en la base de datos");
                        break;
                    case -5:
                        mostrarError("Error", "El médico no existe en la base de datos");
                        break;
                    case -6:
                        mostrarError("Error", "La hora de la cita debe estar entre las 7:00 AM y las 6:00 PM");
                        break;
                    case -7:
                        mostrarError("Error", "El médico ya tiene una cita programada en ese horario, recuerda que cada cita tiene un tiempo estimado de 30 minutos");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al actualizar la cita");
                }
            }
        });
    });
}

function Eliminar(obj) {
    Confirmacion(undefined, "¿Desea eliminar la cita programada para " + obj.pacienteIdentificacion + "?", function () {
        fetchGet("Cita/EliminarCita/?id=" + obj.id, "text", function (res) {
            mostrarExito("Éxito", "La cita se ha eliminado correctamente");
            ObtenerCitas();
        });
    });
}
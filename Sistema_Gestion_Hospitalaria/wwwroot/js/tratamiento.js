window.onload = function () {
    ObtenerTratamientos();
};

let objTratamientos;

async function ObtenerTratamientos() {
    let objTratamientos = {
        url: "Tratamiento/ObtenerTratamientos",
        cabeceras: ["Id Tratamiento", "Identificación del Paciente", "Descripción", "Fecha", "Costo"],
        propiedades: ["id", "identificacionPaciente", "descripcion", "fecha", "costo"], // Asegúrate de que coincidan con TratamientoCLS
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objTratamientos);
}


function buscarTratamiento() {
    let form = document.getElementById("formBusquedaTratamiento");
    let frm = new FormData(form);

    fetchPost("Tratamiento/FiltrarTratamientos", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}

function agregarTratamiento() {
    let form = document.getElementById("formAgregarTratamiento");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de guardar el nuevo tratamiento?", function () {
        fetchPost("Tratamiento/AgregarTratamiento", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El tratamiento se ha agregado correctamente");
                ObtenerTratamientos();
                limpiarDatos("formAgregarTratamiento");
                cerrarModal("modalAgregarTratamiento");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El costo ingresado no es válido");
                        break;
                    case -3:
                        mostrarError("Error", "La fecha ingresada no es válida");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar el tratamiento");
                }
            }
        });
    });
}

function limpiarTratamiento() {
    limpiarDatos("formBusquedaTratamiento");
    ObtenerTratamientos();
}

function Editar(id) {
    fetchGet(`Tratamiento/RecuperarTratamiento?id=${id}`, "json", function (data) {
        if (data) {
            document.querySelector("#formEditarTratamiento input[name='id']").value = data.id;
            document.querySelector("#formEditarTratamiento input[name='identificacionPaciente']").value = data.identificacionPaciente;
            document.querySelector("#formEditarTratamiento input[name='descripcion']").value = data.descripcion;
            document.querySelector("#formEditarTratamiento input[name='fecha']").value = data.fecha.split('T')[0]; // Formatear fecha
            document.querySelector("#formEditarTratamiento input[name='costo']").value = data.costo;

            abrirModal("modalEditarTratamiento");
        } else {
            alert("No se encontró el tratamiento.");
        }
    });
}

function editarTratamiento() {
    let form = document.getElementById("formEditarTratamiento");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de realizar los cambios?", function () {
        fetchPost("Tratamiento/ActualizarTratamiento", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El tratamiento se ha actualizado correctamente");
                ObtenerTratamientos();
                limpiarDatos("formEditarTratamiento");
                cerrarModal("modalEditarTratamiento");
            } else {
                // Error
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El costo ingresado no es válido");
                        break;
                    case -3:
                        mostrarError("Error", "La fecha ingresada no es válida");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al actualizar el tratamiento");
                }
            }
        });
    });
}

function Eliminar(obj) {
    Confirmacion(undefined, "¿Desea eliminar el tratamiento de " + obj.identificacionPaciente + "?", function () {
        fetchGet("Tratamiento/EliminarTratamiento/?id=" + obj.id, "text", function (res) {
            mostrarExito("Éxito", "El tratamiento se ha eliminado correctamente");
            ObtenerTratamientos();
        });
    });
}

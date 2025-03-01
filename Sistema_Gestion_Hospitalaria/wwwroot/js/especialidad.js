window.onload = function () {
    ObtenerEspecialidades();
};

let objEspecialidades;

async function ObtenerEspecialidades() {
    objEspecialidades = {
        url: "Especialidad/ObtenerEspecialidades",
        cabeceras: ["Id Especialidad", "Nombre de la Especialidad"],
        propiedades: ["id","nombre"],
        divContenedorTabla: "divContenedorTabla",
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objEspecialidades);
}

function buscarEspecialidad() {
    let form = document.getElementById("formBusquedaEspecialidad");
    let frm = new FormData(form);

    fetchPost("Especialidad/FiltrarEspecialidad", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}

function agregarEspecialidad() {
    let form = document.getElementById("formAgregarEspecialidad");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de guardar la nueva especialidad?", function () {
        fetchPost("Especialidad/AgregarEspecialidad", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "La especialidad se ha agregado correctamente");
                ObtenerEspecialidades();
                limpiarDatos("formAgregarEspecialidad");
                cerrarModal("modalAgregarEspecialidad");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "Solo caracteres validos");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar la especialidad");
                }
            }
        });
    });
}

function limpiarEspecialidad() {
    limpiarDatos("formBusquedaEspecialidad");
    ObtenerEspecialidades();
}

function Editar(id) {
    fetchGet(`Especialidad/RecuperarEspecialidad?id=${id}`, "json", function (data) {
        if (data) {
            // Asignar valores a los campos del formulario
            document.querySelector("#formEditarEspecialidad input[name='id']").value = data.id;
            document.querySelector("#formEditarEspecialidad input[name='nombre']").value = data.nombre;

            abrirModal("modalEditar");
        } else {
            alert("No se encontró el paciente.");
        }
    });
}

function editarEspecialidad() {
    let form = document.getElementById("formEditarEspecialidad");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de realizar los cambios?", function () {
        fetchPost("Especialidad/ActualizarEspecialidad", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", `La Especialidad ${document.getElementById("editarNombreEspecialidad").value}  se ha actualizado correctamente`);
                ObtenerEspecialidades();
                limpiarDatos("formEditarEspecialidad");
                cerrarModal("modalEditar");
            } else {
                // Error
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El nombre no es valido");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al actualizar la especialidad");
                }
            }
        });
    });
}

function Eliminar(id) {
    fetchGet(`Especialidad/RecuperarEspecialidad?id=${id}`, "json", function (data) {
        Confirmacion(undefined, "¿Está seguro de eliminar la especialidad?", function () {
            fetchGet(`Especialidad/EliminarEspecialidad?id=${id}`, "text", function (response) {
                if (parseInt(response) > 0) {
                    ObtenerEspecialidades();
                    mostrarExito("Éxito", `La Especialidad: ${data.nombre} ha sido elminado`);
                } else {
                    mostrarError("Error", "Ha ocurrido un error la Especialidad");
                }
            });
        });
    });
}


        function fdsfd(objEspecialidades){ }
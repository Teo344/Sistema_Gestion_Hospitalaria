window.onload = function () {
    ObtenerMedicos();
    ObtenerEspecialidades();
};

let objMedicos;

async function ObtenerMedicos() {
    objMedicos = {
        url: "Medico/ObtenerMedicos",
        cabeceras: ["Id Medico", "Nombre", "Apellido", "Identificacion","Especialidad", "Teléfono", "Correo Electrónico", "Disponibilidad"],
        propiedades: ["id", "nombre", "apellido", "identificacion","nombreEspecialidad", "telefono", "email", "disponibilidad"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objMedicos);
}



function buscarMedico() {
    let form = document.getElementById("formBusquedaMedico");
    let frm = new FormData(form);
    let activoValue = document.querySelector("#selectEspecialidadActivoBusqueda").value;
    let EspecialidadValue = document.querySelector("#selectEspecialidadBusqueda").value;
    if (activoValue) {
        frm.set('Activo', (activoValue === "1"));
    }
    if (EspecialidadValue==0) {
        frm.set('Especialidad', -1);
    }


    fetchPost("Medico/FiltrarMedico", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}


function ObtenerEspecialidades() {
    fetchGet("Especialidad/ObtenerEspecialidades", "json", function (data) {
        let selects = [
            document.getElementById("selectEspecialidadBusqueda"),
            document.getElementById("selectEspecialidadAgregar"),
            document.getElementById("selectEspecialidadEditar")

        ];

        selects.forEach(select => {
            if (select) {
                data.forEach(item => {
                    let option = document.createElement("option");
                    option.value = item.id;
                    option.text = item.nombre;
                    select.add(option);
                });
            }
        });

    });
}



function agregarMedico() {
    let form = document.getElementById("formAgregarMedico");
    let frm = new FormData(form);


    Confirmacion(undefined, "¿Está seguro de guardar el nuevo medico?", function () {
        fetchPost("Medico/AgregarMedico", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El medico se ha agregado correctamente", "success");
                ObtenerMedicos();
                limpiarDatos("formAgregarMedico");
                cerrarModal("modalAgregarMedico");
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
                        mostrarError("Error", "Elija una opcion de Especialidad");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar el paciente");
                }
            }
        });
    });
}



function Editar(id) {
    fetchGet(`Medico/RecuperarMedico?id=${id}`, "json", function (data) {
        if (data) {
            // Asignar valores a los campos del formulario
            document.querySelector("#formEditarMedico input[name='id']").value = data.id;
            document.querySelector("#formEditarMedico input[name='identificacion']").value = data.identificacion;
            document.querySelector("#formEditarMedico input[name='nombre']").value = data.nombre;
            document.querySelector("#formEditarMedico input[name='apellido']").value = data.apellido;
            document.querySelector("#formEditarMedico input[name='telefono']").value = data.telefono;
            document.querySelector("#formEditarMedico input[name='email']").value = data.email;

            let disponibilidad = data.activo ? "1" : "0";

            document.querySelector("#selectEspecialidadEditar").value = data.especialidadId;
            document.querySelector("#selectEspecialidadActivoEditar").value = disponibilidad;

            abrirModal("modalEditar");
        } else {
            alert("No se encontró el paciente.");
        }
    });
}




function editarMedico() {
    let form = document.getElementById("formEditarMedico");
    let frm = new FormData(form);
    let activoValue = document.querySelector("#selectEspecialidadActivoEditar").value;
    frm.set('Activo', (activoValue === "1")); 

    Confirmacion(undefined, "¿Está seguro de realizar los cambios?", function () {
        fetchPost("Medico/ActualizarMedico", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El medico se ha actualizado correctamente");
                ObtenerMedicos();
                limpiarDatos("formEditarMedico");
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
                        mostrarError("Error", "Elija una opcion de Especialidad");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar el paciente");
                }
            }
        });
    });
}







function Eliminar(obj) {
    Confirmacion(undefined, "¿Desea eliminar el medico " + obj.nombre + "?", function () {
        fetchGet("Medico/EliminarMedico/?id=" + obj.id, "text", function (res) {
            mostrarExito("Éxito", "El Medico se ha eliminado correctamente");
            ObtenerMedicos();
        });
    });
}

function limpiarMedico() {
    limpiarDatos("formBusquedaMedico");
    ObtenerMedicos();
}
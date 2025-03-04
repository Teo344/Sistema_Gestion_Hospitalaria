window.onload = async function () {
    await cargarDatosAdministrador();
};

async function cargarDatosAdministrador() {
    const adminId = await obtenerIdAdministrador();
    console.log("Admin ID cargado:", adminId);

    if (adminId) {
        fetchGet(`Administrador/RecuperarAdministrador?id=${adminId}`, "json", function (data) {
            if (data) {
                setName("id", data.id);
                setName("nombre", data.nombre);
                setName("apellido", data.apellido);
                setName("email", data.email);
                document.querySelector("#idClave").value = data.id;
            } else {
                mostrarError("Error", "No se encontraron datos del administrador.");
            }
        });
    } else {
        mostrarError("Error", "No se pudo obtener el ID del administrador.");
    }
}

async function obtenerIdAdministrador() {
    let id = null;
    try {
        const response = await fetch("/Administrador/ObtenerIdSesion");
        const data = await response.json();

        if (data !== "No se pudo obtener el ID del administrador.") {
            id = data;
            localStorage.setItem("adminId", id);
            console.log("ID obtenido:", id);
        } else {
            console.log(data);
        }
    } catch (error) {
        console.error("Error al obtener el ID del administrador:", error);
    }
    return id;
}

function actualizarAdministrador() {
    const formEditar = document.querySelector("#formEditarAdministrador");
    const formData = new FormData(formEditar);
    Confirmacion(undefined, "¿Está seguro de actualizar sus datos?", function () {
        fetchPost("Administrador/ActualizarAdministrador", "text", formData, function (data) {
            let response = parseInt(data);
            if (response > 0) {
                mostrarExito("Éxito", "Datos actualizados correctamente.");
            } else {
                const mensajesError = {
                    '-1': "Por favor complete todos los campos requeridos.",
                    '-2': "El correo electrónico ingresado no es válido."
                };
                mostrarError("Error", mensajesError[response] || "Ha ocurrido un error al actualizar los datos.");
            }
        });
    });
}

function cambiarClave() {
    const formClave = document.querySelector("#formCambiarClave");
    const formData = new FormData(formClave);
    Confirmacion(undefined, "¿Está seguro de cambiar su contraseña?", function () {
        fetchPost("Administrador/CambiarClave", "text", formData, function (data) {
            let response = parseInt(data);
            if (response > 0) {
                mostrarExito("Éxito", "Contraseña actualizada correctamente.");
                document.querySelector("#nuevaClave").value = "";
            } else {
                mostrarError("Error", "Ha ocurrido un error al cambiar la contraseña.");
            }
        });
    });
}

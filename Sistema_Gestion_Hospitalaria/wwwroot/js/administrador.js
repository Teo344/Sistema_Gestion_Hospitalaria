window.onload = function () {
    ObtenerAdministradores();
};

let objAdministradores;

async function ObtenerAdministradores() {
    objAdministradores = {
        url: "Home/ObtenerAdministradores",
        cabeceras: ["Id", "Nombre", "Apellido", "Email"],
        propiedades: ["id", "nombre", "apellido", "email"],
        divContenedorTabla: "divContenedorTabla",
        editar: false, // No permitir editar
        eliminar: false // No permitir eliminar
    };
    pintar(objAdministradores);
}

function agregarAdministrador() {
    let form = document.getElementById("formAgregarAdministrador");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de guardar el nuevo administrador?", function () {
        fetchPost("Home/AgregarAdministrador", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "El administrador se ha agregado correctamente", "success");
                ObtenerAdministradores();
                limpiarDatos("formAgregarAdministrador");
                cerrarModal("modalAgregarAdministrador");
            } else {
                // Manejo de errores según el código de respuesta
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "El correo electrónico ingresado no es válido");
                        break;
                    case -3:
                        mostrarError("Error", "La contraseña no cumple con los requisitos de seguridad. Recuerde que debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar el administrador");
                }
            }
        });
    });
}
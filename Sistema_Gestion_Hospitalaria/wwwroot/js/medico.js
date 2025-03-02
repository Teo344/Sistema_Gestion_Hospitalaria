window.onload = function () {
    ObtenerMedicos();
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



function Eliminar(obj) {
    Confirmacion(undefined, "¿Desea eliminar el medico " + obj.nombre + "?", function () {
        fetchGet("Medico/EliminarMedico/?id=" + obj.id, "text", function (res) {
            mostrarExito("Éxito", "El Medico se ha eliminado correctamente");
            ObtenerMedicos();
        });
    });
}
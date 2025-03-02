window.onload = function () {
    ObtenerMedicos();
};

let objMedicos;

async function ObtenerMedicos() {
    objMedicos = {
        url: "Medico/ObtenerMedicos",
        cabeceras: ["Id Medico", "Nombre", "Apellido", "especialidad", "Teléfono", "Correo Electrónico"],
        propiedades: ["id", "nombre", "apellido", "especialidad", "telefono", "email"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objMedicos);
}


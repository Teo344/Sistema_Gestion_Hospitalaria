window.onload = function () {
    ObtenerFacturaciones();
};

let objFacturaciones;

async function ObtenerFacturaciones() {
    objFacturaciones = {
        url: "Facturacion/ObtenerFacturaciones",  
        cabeceras: ["Id", "Identificacion del Paciente","Nombre del Paciente"," Apellido del Paciente", "Fecha de la Factura", "Total"], 
        propiedades: ["id", "pacienteIdentificacion", "pacienteNombre", "pacienteApellido", "fechaPago", "monto"],  
        editar: true,  
        eliminar: true,  
        visualizar: true,  
        propiedadId: "id"  
    };

    pintar(objFacturaciones);

}




function ObtenerTratamientosPorPaciente() {
    let identifiacionPaciente = document.getElementById("inputIdentificacionPaciente").value;  // Capturar el ID del paciente

    if (!identifiacionPaciente) {
        mostrarError("Error","Por favor, ingrese la identificación del paciente.");
        return;
    }

    fetchGet(`Tratamientos/ObtenerTratamientosPorPaciente/${idPaciente}`, "json", function (data) {
        let select = document.getElementById("selectTratamientoAgregar");
        select.innerHTML = '<option value="">Seleccione un tratamiento</option>';

        data.forEach(item => {
            let option = document.createElement("option");
            option.value = item.id;
            option.text = item.nombre;
            select.add(option);
        });
    });
}



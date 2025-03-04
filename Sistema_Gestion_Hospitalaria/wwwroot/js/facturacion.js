window.onload = function () {
    ObtenerFacturaciones();
};

let objFacturaciones;
let tratamientosData = [];

async function ObtenerFacturaciones() {
    objFacturaciones = {
        url: "Facturacion/ObtenerFacturaciones",  
        cabeceras: ["Id", "Identificacion del Paciente","Nombre del Paciente"," Apellido del Paciente", "Fecha de la Factura", "Total"], 
        propiedades: ["id", "pacienteIdentificacion", "pacienteNombre", "pacienteApellido", "fechaPago", "monto"],  
        editar: false,  
        eliminar: true,  
        visualizar: true,  
        propiedadId: "id"  
    };

    pintar(objFacturaciones);

}


function buscarFacturacion() {
    let form = document.getElementById("formBusquedaFacturacion");
    let frm = new FormData(form);

    fetchPost("Facturacion/FiltrarFacturacion", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}




function comprobarPaciente() {
    let identificacion = document.getElementById("PacienteIdentificacion").value;
    console.log(identificacion);

    if (identificacion == "") {
        mostrarError("Error", "Debe ingresar la identificación del paciente");
        return;
    }

    fetchGet("Tratamiento/ObtenerTratamientosPorPaciente?identificacion=" + identificacion, "json", function (data) {
        if (!data || data.length === 0) {
            mostrarAdvertencia("Advertencia", "El paciente no tiene tratamientos registrados");
            document.getElementById("MontoAgregarFacturacion").value = "";
            document.getElementById("selectTratamientoAgregar").innerHTML = "";
        } else {
            mostrarInfo("Información", "El paciente tiene tratamientos registrados, ahora seleccione uno");

            tratamientosData = data;

            let selectTratamiento = document.getElementById("selectTratamientoAgregar");
            selectTratamiento.innerHTML = "";
            selectTratamiento.innerHTML = "<option value=''>Seleccione El tratamiento</option>";

            // Iteramos sobre el array de tratamientos
            data.forEach(function (tratamiento) {
                let option = document.createElement("option");
                option.value = tratamiento.id;
                option.text = tratamiento.descripcion;
                selectTratamiento.appendChild(option);
            });
        }
    });
}




document.getElementById("selectTratamientoAgregar").addEventListener("change", function () {
    let selectTratamiento = document.getElementById("selectTratamientoAgregar");
    let tratamientoId = selectTratamiento.value; 

    let tratamientoSeleccionado = tratamientosData.find(t => t.id == tratamientoId);

    if (tratamientoSeleccionado) {
        document.getElementById("MontoAgregarFacturacion").value = tratamientoSeleccionado.costo;
    } else {
        document.getElementById("MontoAgregarFacturacion").value = "";
    }
});

function agregarFacturacion() {
    let form = document.getElementById("formAgregarFacturacion");
    let frm = new FormData(form);

    Confirmacion(undefined, "¿Está seguro de guardar la nueva factura", function () {
        fetchPost("Facturacion/AgregarFacturacion", "text", frm, function (data) {
            let response = parseInt(data);

            if (response > 0) {
                mostrarExito("Éxito", "La Factura se ha agregado correctamente", "success");
                ObtenerFacturaciones();
                limpiarDatos("formAgregarFacturacion");
                cerrarModal("modalAgregarFacturacion");
            } else {
                switch (response) {
                    case -1:
                        mostrarError("Error", "Por favor complete todos los campos requeridos");
                        break;
                    case -2:
                        mostrarError("Error", "Elija una opcion de Tratamiento");
                        break;
                    default:
                        mostrarError("Error", "Ha ocurrido un error al agregar la facturacion");
                }
            }
        });
    });
}


function limpiarFacturacion() {
    limpiarDatos("formBusquedaFacturacion");
    ObtenerFacturaciones();
}

function Visualizar(id) {
    fetchGet(`Facturacion/RecuperarFacturacion?id=${id}`, "json", function (data) {
        if (data) {
            // Limpiar contenido anterior de la tabla
            let tableBody = document.getElementById("facturacionTableBody");
            tableBody.innerHTML = "";

            // Crear una nueva fila con los datos obtenidos
            let fila = `
                <tr>
                    <td>${data.id}</td>
                    <td>${data.pacienteNombre} ${data.pacienteApellido}</td>
                    <td>${data.pacienteIdentificacion}</td>
                    <td>${data.metodoPago}</td>
                    <td>${data.fechaPago}</td>
                    <td>${data.tratamientoId}</td>
                    <td>$${data.monto.toFixed(2)}</td>
                </tr>
            `;

            // Insertar la fila en la tabla
            tableBody.innerHTML = fila;

            // Abrir el modal
            abrirModal("facturacionModalVisualizar");
        } else {
            mostrarError("Error", "No se encontró la facturación.");
        }
    });
}


function Eliminar(obj) {
    Confirmacion(undefined, "¿Desea eliminar la factura del paciente " + obj.pacienteNombre + "?", function () {
        fetchGet("Facturacion/EliminarFacturacion/?id=" + obj.id, "text", function (res) {
            mostrarExito("Éxito", "La factura se ha eliminado correctamente");
            ObtenerFacturaciones();
        });
    });
}
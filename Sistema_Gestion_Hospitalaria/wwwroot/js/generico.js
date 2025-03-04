function get(idControl) {
    return document.getElementById(idControl).value;
}

function getName(nameControl) {
    return document.getElementByName(nameControl)[0].value;
}

function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setName(namecontrol, valor) {
    document.getElementsByName(namecontrol)[0].value = valor;
}

function abrirModal(modalId) {
    let modalElement = document.getElementById(modalId);
    if (modalElement) {
        let modalInstance = new bootstrap.Modal(modalElement);
        modalInstance.show();
    } else {
        console.error(`No se encontró el modal con ID: ${modalId}`);
    }
}

function cerrarModal(modalId) {
    let modalElement = document.getElementById(modalId);
    if (modalElement) {
        let modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        } else {
            new bootstrap.Modal(modalElement).hide();
        }

        // Espera un poco para asegurarte de que Bootstrap eliminó el modal antes de modificar el estilo
        setTimeout(() => {
            eliminarBackdrop();
            document.body.classList.remove("modal-open");
            document.body.style.overflow = "auto";  // Habilitar el scroll
            document.body.style.height = "auto";    // Evitar restricciones de altura
            document.documentElement.style.overflow = "auto"; // Restaurar en <html> también
            document.documentElement.style.height = "auto";
            document.body.style.paddingRight = ""; // Eliminar posibles márgenes agregados por Bootstrap
        }, 300);
    } else {
        console.error(`No se encontró el modal con ID: ${modalId}`);
    }
}




function eliminarBackdrop() {
    let backdrops = document.querySelectorAll(".modal-backdrop");
    backdrops.forEach(backdrop => backdrop.remove());
}


function limpiarDatos(idFormulario) {
    let elementosName = document.querySelectorAll(`#${idFormulario} [name]`);
    elementosName.forEach(elemento => {
        elemento.value = "";
    });
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta);
        if (!res.ok) {
            throw new Error(`Error en la solicitud: ${res.status} ${res.statusText}`);
        }

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        console.error("Error en fetchGet:", e);
        alert("Ocurrió un problema: " + e.message);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);

    } catch (e) {
        alert("Ocurrió un error en post" + e);
    }
}


let objConfiguracionGlobal;
function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    if (objConfiguracionGlobal.divContenedorTabla == undefined) {
        objConfiguracionGlobal.divContenedorTabla = "divContenedorTabla";
    }

    if (objConfiguracionGlobal.editar == undefined) {
        objConfiguracionGlobal.editar = false;
    }

    if (objConfiguracionGlobal.eliminar == undefined) {
        objConfiguracionGlobal.eliminar = false;
    }

    if (objConfiguracionGlobal.visualizar == undefined) {
        objConfiguracionGlobal.visualizar = false;
    }

    if (objConfiguracionGlobal.propiedadId == undefined) {
        objConfiguracionGlobal.eliminar = "";
    }
        

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "";


        contenido = "<div id='divContenedor'>"
        contenido += generarTabla(res);
        contenido += "</div>"
        document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = contenido;

        new DataTable('#myTable');
    });
}
function formatearFecha(fecha) {
    if (!fecha) return ''; 
    return fecha.replace("T", " 🕒 ");
}

function generarTabla(res) {
    let contenido = "";
    let cabeceras = objConfiguracionGlobal.cabeceras;
    let propiedades = objConfiguracionGlobal.propiedades;
    contenido += "<div class='table-responsive'>";
    contenido += "<table id='myTable' class='table table-striped table-bordered table-hover table-sm'>";
    contenido += "<thead class='table-dark'>";
    contenido += "<tr>";

    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<th class='text-center'>" + cabeceras[i] + "</th>";
    }

    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido += "<th class='text-center'>Operaciones</th>";
    }

    contenido += "</tr>";
    contenido += "</thead>";

    let numRegistros = res.length;
    contenido += "<tbody>";

    for (let i = 0; i < numRegistros; i++) {
        let obj = res[i];
        contenido += "<tr class='align-middle'>";
        for (let j = 0; j < propiedades.length; j++) {
            let propiedadActual = propiedades[j];
            if (propiedadActual === "fechaHora") {
                obj[propiedadActual] = formatearFecha(obj[propiedadActual]);
            }
            contenido += "<td class='text-center align-middle py-3'>" + obj[propiedadActual] + "</td>";
        }
        if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
            let propiedadId = objConfiguracionGlobal.propiedadId;
            contenido += "<td class='text-center align-middle'>";

            if (objConfiguracionGlobal.visualizar == true) {
                contenido += `<i onclick='Visualizar(${obj[propiedadId]})' class="btn btn-success btn-sm me-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye" viewBox="0 0 16 16">
                    <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z"/>
                    <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0"/>
                    </svg>
                </i>`;
            }

            if (objConfiguracionGlobal.editar == true) {
                contenido += `<i type="button" onclick="Editar(${obj[propiedadId]})" class="btn btn-primary btn-sm me-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                    <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                    </svg>
                </i>`;
            }

            if (objConfiguracionGlobal.eliminar == true) {
                contenido += `<i onclick='Eliminar(${JSON.stringify(obj)})' class="btn btn-danger btn-sm">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                        <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
                    </svg> 
                </i>`;
            }


            contenido += "</td>";
        }

        contenido += "</tr>";
    }

    contenido += "</tbody>";
    contenido += "</table>";
    contenido += "</div>";
    return contenido;
}

function Confirmacion(titulo = "Confirmacion", texto, callback) {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, estoy seguro"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

function mostrarExito(titulo, mensaje) {
    toastr.success(mensaje, titulo);
}

function mostrarError(titulo, mensaje) {
    toastr.error(mensaje, titulo);
}

function mostrarAdvertencia(titulo, mensaje) {
    toastr.warning(mensaje, titulo);
}

function mostrarInfo(titulo, mensaje) {
    toastr.info(mensaje, titulo);
}


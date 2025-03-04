// Función para activar el modo oscuro
function enableDarkMode() {
    document.body.classList.add('dark-mode');
    localStorage.setItem('theme', 'dark');
    document.getElementById('toggleDarkMode').innerHTML = '<i class="fas fa-sun"></i>'; // Cambiar a icono de sol
}

// Función para desactivar el modo oscuro
function disableDarkMode() {
    document.body.classList.remove('dark-mode');
    localStorage.setItem('theme', 'light');
    document.getElementById('toggleDarkMode').innerHTML = '<i class="fas fa-moon"></i>'; // Cambiar a icono de luna
}

// Verificar la preferencia del usuario al cargar la página
document.addEventListener('DOMContentLoaded', () => {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
        enableDarkMode();
    } else {
        disableDarkMode();
    }

    // Alternar entre modos al hacer clic en el botón
    document.getElementById('toggleDarkMode').addEventListener('click', () => {
        if (document.body.classList.contains('dark-mode')) {
            disableDarkMode();
        } else {
            enableDarkMode();
        }
    });
});
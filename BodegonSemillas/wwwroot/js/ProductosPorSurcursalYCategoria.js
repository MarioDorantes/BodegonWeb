$(document).ready(function () {
    cargarProductos();
});

function cargarProductos() {
    console.log("Entro a la funcion de productos");
    $.get('/VistaProductosCategoria/ObtenerProductosPorSucursalYCategoria', function (data) {
        
    })
        .catch(function (error) {
            console.log(error);
        });
}
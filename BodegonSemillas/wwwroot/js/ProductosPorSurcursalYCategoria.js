$(document).ready(function () {
    cargarProductos();
});

function cargarProductos() {
    var claveCategoriaElement = document.getElementById('claveCategoria');
    var clave = claveCategoriaElement.getAttribute('data-clave');

    $.get('/VistaProductosCategoria/ObtenerProductosPorSucursalYCategoria', { clave: clave })
        .done(function (data) {
            var contenedor = $('#contenedorCards');
            var filaActual;

            for (var i = 0; i < data.length; i++) {
                var producto = data[i];
                var card = crearCard(producto.nombre);

                if (i % 3 === 0) {
                    filaActual = $('<div class="row mb-4"></div>');
                    contenedor.append(filaActual);
                }

                var columna = $('<div class="col"></div>');
                columna.append(card);
                filaActual.append(columna);
            }
        })
        .fail(function (error) {
            console.log(error);
        });
}

function crearCard(nombreProducto) {
    var card = '<div class="card" style="width: 18rem;">' +
        '  <img src="..." class="card-img-top" alt="...">' +
        '  <div class="card-body">' +
        '    <p class="card-text">' + nombreProducto + '</p>' +
        '  </div>' +
        '</div>';
    return card;
}
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
                var card = crearCard(producto.nombre, producto.precioImpuestos, producto.id);

                if (i % 3 === 0) {
                    filaActual = $('<div class="row mb-4"></div>');
                    contenedor.append(filaActual);
                }

                var columna = $('<div class="col"></div>');
                columna.append(card);
                filaActual.append(columna);
            }

            // Agregar evento de clic a las cards
            $('.card').on('click', function () {
                var idProducto = $(this).data('id-producto');
                console.log('Producto seleccionado - ID:', idProducto);
            });

        })
        .fail(function (error) {
            console.log(error);
        });
}

function crearCard(nombreProducto, precioImpuestos, idProducto) {
    var card = $('<div class="card" style="width: 18rem; transition: transform 0.3s;"></div>');
    var iconoFav = $('<a href="#" class="btn btn-link" style="position: absolute; top: 0; right: 0;"><i class="far fa-heart" style="background-color: white; padding: 4px;"></i></a>');
    card.attr('data-id-producto', idProducto);

    card.append(iconoFav);
    card.append('<img src="..." class="card-img-top" alt="...">');
    card.append('<div class="card-body">');
    card.append('<p class="card-text" style="margin-bottom: 2px; padding-left: 8px; font-size: 25px;">$' + precioImpuestos + '</p>');
    card.append('<p class="card-text" style="margin-bottom: 2px; padding-left: 8px;">' + nombreProducto + '</p>');
    card.append('</div>');

    // Agregar eventos de mouse a la card
    card.on('mouseenter', function () {
        $(this).css('cursor', 'pointer');
        $(this).css('transform', 'scale(1.05)');
    });

    card.on('mouseleave', function () {
        $(this).css('cursor', 'default');
        $(this).css('transform', 'scale(1)');
    });

    return card;
}
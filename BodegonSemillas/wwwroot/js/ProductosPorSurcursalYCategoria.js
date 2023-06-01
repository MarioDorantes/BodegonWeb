$(document).ready(function () {
    var paginaActual = 1;

    function cargarProductos(pagina) {
        var claveCategoriaElement = document.getElementById('claveCategoria');
        var clave = claveCategoriaElement.getAttribute('data-clave');

        $.get('/VistaProductosCategoria/ObtenerProductosPorSucursalYCategoria', { clave: clave, pagina: pagina })
            .done(function (data) {
                var contenedor = $('#contenedorCards');
                var filaActual;

                for (var i = 0; i < data.length; i++) {
                    var producto = data[i];
                    var card = crearCard(producto.nombre, producto.precioImpuestos, producto.idPrecio);

                    if (i % 3 === 0) {
                        filaActual = $('<div class="row mb-4"></div>');
                        contenedor.append(filaActual);
                    }

                    var columna = $('<div class="col"></div>');
                    columna.append(card);
                    filaActual.append(columna);

                    obtenerImagenProducto(producto.idPrecio, card);
                }

                // Agregar evento de clic a las cards
                $('.card').on('click', function () {
                    var idPrecio = $(this).data('id-precio');
                    console.log('Producto:', idPrecio);
                    obtenerProductoID(idPrecio);
                });

            })
            .fail(function (error) {
                console.log(error);
            });
    }

    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() >= $(document).height()) {
            paginaActual++;
            cargarProductos(paginaActual);
        }
    });

    cargarProductos(paginaActual);
});


//Función donde se crean las cards, y crea una segun los productos que se reciban por cada categoria
function crearCard(nombreProducto, precioImpuestos, idPrecio) {
    var card = $('<div class="card" style="width: 18rem; transition: transform 0.3s;"></div>');
    var iconoFav = $('<a href="#" class="btn btn-link" style="position: absolute; top: 0; right: 0;"><i class="far fa-heart" style="background-color: white; padding: 4px;"></i></a>');
    card.attr('data-id-precio', idPrecio);

    var imagen = $('<img class="card-img-top">');
    imagen.css({
        width: '150px',
        height: '150px',
        display: 'block', 
        margin: '0 auto', 
        objectFit: 'cover'
    });

    card.append(iconoFav);
    card.append(imagen);
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

//Funcion para obtener el id de un producto individual, sirve para ir a la vista individual
function obtenerProductoID(idPrecio) {
    $.get('/VistaProductoIndividual/ObtenerProductoPorID', { producto: idPrecio })
        .then(function (data) {

        })
        .catch(function (error) {
            console.log('Error al obtener el producto:', error);
        });
}


//Función que sirve para obtener la imagen de cada producto, recibe la imagen y la ingresa al card
function obtenerImagenProducto(idPrecio, card) {
    $.ajax({
        url: '/VistaProductosCategoria/ObtenerImagenProducto',
        method: 'GET',
        data: { producto: idPrecio },
        xhrFields: {
            responseType: 'arraybuffer'
        }
    })
        .done(function (data) {
            var arrayBufferView = new Uint8Array(data);
            var blob = new Blob([arrayBufferView], { type: 'image/jpeg' });
            var imageUrl = URL.createObjectURL(blob);

            // Asignar la imagen a la card
            card.find('.card-img-top').attr('src', imageUrl);
        })
        .fail(function (error) {
            console.log('Error al obtener la imagen del producto:', error);
        });
}

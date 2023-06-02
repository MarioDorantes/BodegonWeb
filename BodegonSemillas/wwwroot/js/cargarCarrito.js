$(document).ready(function () {
    CargarCarrito();
});

function CargarCarrito() {
    $.get('/Carrito/ObtenerCarritoPorSucursal')
        .done(function (response) {

            var contenedor = $('#contenedorCardsCarrito');

            for (var i = 0; i < response.productos.length; i++) {
                var producto = response.productos[i];
                var card = crearCard(producto.nombre, producto.precioImpuestos, producto.cantidadCarrito);
                contenedor.append(card);

                obtenerImagenProducto(producto.idPrecio, card);
            }

            // Obtener el total del carrito
            var totalCarrito = response.totalCarrito;
            mostrarTotalCarrito(totalCarrito);
        })
        .fail(function (error) {
            console.log(error);
        });
}

function crearCard(nombreProducto, precioImpuestos, cantidadCarrito) {
    var card = $('<div class="card" style="width: 900px; height: 200px; margin-bottom: 20px;"></div>');
    var cardBody = $('<div class="card-body"></div>');
    var iconoCarrito = $('<a href="#" class="btn btn-link" style="position: absolute; bottom: 5px; right: 0; color: red;"><i class="fas fa-cart-arrow-down" style="background-color: white; padding: 4px;"></i></a>');
    var textoEliminar = $('<p style="position: absolute; bottom: 0; right: 40px; color: red; font-size: 12px;">Eliminar</p>');

    var row = $('<div class="row"></div>');
    var imagenCol = $('<div class="col-4"></div>'); 
    var contenidoCol = $('<div class="col-8"></div>'); 

    var nombre = $('<p class="card-text" style="font-size: 20px; font-weight: bold;">' + nombreProducto + '</p>');
    var precio = $('<p class="card-text" style="font-size: 30px;">' + '$' + precioImpuestos + ' pesos' + '</p>');
    var cantidad = $('<p class="card-text">' + 'Cantidad: ' + cantidadCarrito + '</p>');

    var imagen = $('<img class="card-img-top">');
    imagen.css({
        width: '150px',
        height: '150px',
        display: 'block',
        margin: '0 auto',
        objectFit: 'cover'
    });


    imagenCol.append(imagen);
    contenidoCol.append(nombre);
    contenidoCol.append(precio);
    contenidoCol.append(cantidad);

    row.append(imagenCol);
    row.append(contenidoCol);

    cardBody.append(row);
    card.append(iconoCarrito);
    card.append(textoEliminar);
    card.append(cardBody);

    return card;
}

function mostrarTotalCarrito(totalCarrito) {
    var totalElement = $('.total');
    totalElement.text('Total: $' + totalCarrito + " pesos");
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
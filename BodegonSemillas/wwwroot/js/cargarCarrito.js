$(document).ready(function () {
    CargarCarrito();
});

function CargarCarrito() {
    $.get('/Carrito/ObtenerCarritoPorSucursal')
        .done(function (response) {

            console.log("ASAAAAAAAAAAAAAAAAAA", response);
            var contenedor = $('#contenedorCardsCarrito');

            for (var i = 0; i < response.productos.length; i++) {
                var producto = response.productos[i];
                var card = crearCard(producto.nombre);
                contenedor.append(card);
            }

            // Obtener el total del carrito
            var totalCarrito = response.totalCarrito;
            mostrarTotalCarrito(totalCarrito);
        })
        .fail(function (error) {
            console.log(error);
        });
}

function crearCard(nombreProducto) {
    var card = $('<div class="card" style="width: 900px; height: 200px; margin-bottom: 20px;"></div>');
    var cardBody = $('<div class="card-body"></div>');
    var iconoFav = $('<a href="#" class="btn btn-link" style="position: absolute; top: 0; right: 0;"><i class="far fa-heart" style="background-color: white; padding: 4px;"></i></a>');

    var row = $('<div class="row"></div>');
    var imagenCol = $('<div class="col-4"></div>'); 
    var contenidoCol = $('<div class="col-8"></div>'); 

    var imagen = $('<img src="..." class="card-img-top" alt="..." style="width: 100%; height: 100%;">');
    var nombre = $('<p class="card-text">' + nombreProducto + '</p>');

    imagenCol.append(imagen);
    contenidoCol.append(nombre);

    row.append(imagenCol);
    row.append(contenidoCol);

    cardBody.append(row);
    card.append(iconoFav);
    card.append(cardBody);

    return card;
}

function mostrarTotalCarrito(totalCarrito) {
    var totalElement = $('.total');
    totalElement.text('Total: $' + totalCarrito + " pesos");
}
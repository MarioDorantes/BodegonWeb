$(document).ready(function () {
    cargarCategorias();
});

function cargarCategorias() {
    $.get('/Home/ObtenerCategorias', function (data) {
        var select = $('#selectCategorias');
        select.empty();
        select.append('<option value="">Categorias</option>');
        $.each(data, function (index, categoria) {
            select.append('<option value="' + categoria.clave + '-' + categoria.nombre + '">' + categoria.nombre + '</option>');
        });
    })
        .catch(function (error) {
            console.log(error);
        });
}
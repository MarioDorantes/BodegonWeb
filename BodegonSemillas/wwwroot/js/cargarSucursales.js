$(document).ready(function () {
    cargarSucursales();
});

function cargarSucursales() {
    $.get('/Home/ObtenerSucursalesCiudad', function (data) {
        var select = $('#selectSucursales');
        select.empty();
        select.append('<option>Sucursales</option>');
        $.each(data, function (index, sucursal) {
            select.append('<option>' + sucursal.nombreSucursal + '</option>');
        });
    })
        .catch(function (error) {
            console.log(error);
        });
}
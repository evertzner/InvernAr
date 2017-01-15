function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageVisualizer').attr('src', e.target.result);
        }
        $('#imageVisualizer').css("visibility", "visible");
        $("#EliminarImagen").css("visibility", "visible");
        reader.readAsDataURL(input.files[0]);
    }
}

function eliminarImg() {
    var input = $('#Main_fuImagen');
    if (input[0].files && input[0].files[0]) {
        input.val("");
        $('#imageVisualizer').attr('src', '');
        $("#EliminarImagen").css("visibility", "hidden");
    }
}
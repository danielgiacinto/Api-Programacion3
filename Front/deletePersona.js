$(document).ready(function(){

    $("#validarDelete").validate({
        rules: {
            documento:{
                required: true
            }
        },
        errorClass: "is-invalid",
        validClass: "is-valid",

        submitHandler: function() {
            borrarPersona();
        }
       });
})

function borrarPersona(){
    var ComandoDeletePersona = {};
    ComandoDeletePersona.Dni = $("#txtDocumento").val();

    console.log(JSON.stringify(ComandoDeletePersona));

    $.ajax({
        url: "https://localhost:7212/personas/borrar/documento/" + ComandoDeletePersona.Dni,
        type: "DELETE",
        dataType: "json",
        success: function(result) {
            if (result) {
                alert("Se eliminó la persona.");
            } else {
                alert("No existe la persona.");
            }
        },
        error: function(xhr, status, error) {
            console.log(xhr);
            console.log(error);
            alert("Error fatal.");
        }
    });
}
    

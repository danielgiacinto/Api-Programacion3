$.validator.addMethod("sexoValidar", function(value, element){
    if(value == 0){
      return false;
    } else {
      return true;
    }
  }, "Seleccione una opcion...")

$.validator.addMethod("paisValidar", function(value, element){
    if(value == 0){
      return false;
    } else {
      return true;
    }
  }, "Seleccione una opcion...")

$.validator.addMethod("provinciaValidar", function(value, element){

    if(value == 0){
        return false;
    } else {
        return true;
    }
    }, "Seleccione una opcion...")


$("#validar").validate({
    rules: {
        nombre: {
            required: true,
            minlength: 4
        },
        apellido: {
            required: true,
        },
        dni: {
            required: true
        },
        sexo: {
            required: true,
            sexoValidar: true
        },
        pais: {
            required: true,
            paisValidar: true
        },
        provincia: {
            required: true,
            provinciaValidar: true
        }
    },
    errorClass: "is-invalid",
    validClass: "is-valid",
    
    
    submitHandler: function() {
        crearPersona();
    }
   });

// Para traducir las validaciones
jQuery.extend(jQuery.validator.messages, {
    required: "Este campo es obligatorio.",
    remote: "Por favor, rellena este campo.",
    email: "Por favor, escribe una dirección de correo válida",
    url: "Por favor, escribe una URL válida.",
    date: "Por favor, escribe una fecha válida.",
    dateISO: "Por favor, escribe una fecha (ISO) válida.",
    number: "Por favor, escribe un número entero válido.",
    digits: "Por favor, escribe sólo dígitos.",
    creditcard: "Por favor, escribe un número de tarjeta válido.",
    equalTo: "Por favor, escribe el mismo valor de nuevo.",
    accept: "Por favor, escribe un valor con una extensión aceptada.",
    maxlength: jQuery.validator.format("Por favor, no escribas más de {0} caracteres."),
    minlength: jQuery.validator.format("Por favor, no escribas menos de {0} caracteres."),
    rangelength: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
    range: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1}."),
    max: jQuery.validator.format("Por favor, escribe un valor menor o igual a {0}."),
    min: jQuery.validator.format("Por favor, escribe un valor mayor o igual a {0}.")
});


// Contectar front con back


function crearPersona(){
    let ComandoCrearPersona = {};
    ComandoCrearPersona.Nombre = $("#txtNombre").val();
    ComandoCrearPersona.Apellido = $("#txtApellido").val();
    ComandoCrearPersona.Dni = $("#txtDni").val();
    ComandoCrearPersona.IdSexo = $("#txtSexo").val();
    ComandoCrearPersona.IdPais = $("#txtPais").val();
    ComandoCrearPersona.IdProvincia = $("#txtProvincia").val();

    console.log(JSON.stringify(ComandoCrearPersona));

    $.ajax({
        url: "https://localhost:7212/persona/crear",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(ComandoCrearPersona),
        success: function(result){
            if(result){
                Swal.fire(
                    'Exito !!',
                    'Se cargo correctamente la persona!',
                    'success'
                )
                $("#txtNombre").val() = "";
                $("#txtApellido").val() = "";
                $("#txtDni").val() = "";
                $("#txtSexo").val() = 0;
                $("#txtPais").val() = 0;
                $("#txtProvincias").val() = 0;
            }
        },
        error: function(error){
            Swal.fire(
                'Error',
                'No se cargo la persona, ya existe !!',
                'error'
            )
        }
    });
}
$(document).ready(function(){
    $("#validarLogin").validate({
        rules: {
            usuario: {
                required: true,
                minlength: 2
            },
            password: {
                required: true,
                minlength: 2
            }
        },
        errorClass: "is-invalid",
        validClass: "is-valid",

        submitHandler: function(){
            ingresarLogin();
        }

    });
});




function ingresarLogin(){
    let LoginUsuario = {};
    LoginUsuario.usuario1 = $("#txtUsuario").val();
    LoginUsuario.password = $("#txtPassword").val();

    console.log(JSON.stringify(LoginUsuario));
    
    $.ajax({
        url: "https://localhost:7212/usuario/login",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(LoginUsuario),
        success: function(result){
            console.log(result)
            if(result){
                alert("Ingreso exitoso !!")
                window.location.href = "/crearPersona.html";
            }
            else{
                alert("No se encuentra el usuario...");
            }
        },
        error: function(result){
            console.log(result)
            alert("error al realizar el login");
        }

    });
}
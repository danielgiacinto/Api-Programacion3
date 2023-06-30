$(document).ready(function(){

    $.ajax({
        url: "https://localhost:7212/personas/todos",
        type: "GET",
        success: function(result){
            for(let i= 0; i < result.length; i++){
                let fila = "<tr>";
                    fila += "<td>" + result[i].id + "</td>";
                    fila += "<td>" + result[i].nombre + "</td>";
                    fila += "<td>" + result[i].apellido + "</td>";
                    fila += "<td>" + result[i].dni + "</td>";
                    fila += "<td>" + result[i].sexo1 + "</td>";
                    fila += "<td>" + result[i].pais + "</td>";
                    fila += "<td>" + result[i].provincia + "</td>";
                    fila += "</tr>";
                $("#tableBody").append(fila)
            }
        },
        error: function(error){
            console.log(error);
        }
    });
})
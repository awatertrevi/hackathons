$(function() {
   $("#login").click(function () {
       $.post("/api/login/doctor", {
           email: $("#email").val(),
           password: $("#password").val()
       }, function (response) {
           if (response.success) {
               window.location = "/dashboard/home";
           } else {
                $("#message").text(response.message).parent().show();
           }
       });
   });
});
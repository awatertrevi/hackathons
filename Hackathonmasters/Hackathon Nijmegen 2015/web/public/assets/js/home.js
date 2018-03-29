$(document).ready(function(){
    $.material.init();
    $(document).on("click", ".open-ViewUser", function () {
        console.log($(this).data('id'));
        $.post("/api/patient", {
            id: $(this).data('id')
        }, function (response) {
            if (response.success) {
                for (var key in response.body) {
                    $(".modal-body #"+key).text(response.body[key]);
                }
            } else {
                console.error(response);
            }
        });
    });
});
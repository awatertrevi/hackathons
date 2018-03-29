$(function () {
    $.material.init();

    var name, id;

    $(document).on("click", ".open-ViewUser", function () {
        name = $(this).data('name');
        id = $(this).data('id');
        $.post("/api/appointment", {
            "id" : $(this).data('id')
        }, function(response) {
            if (response.success) {
                $(".modal-body #nameField").html( name );
                for (var key in response.body) {
                    $("#"+key).text(response.body[key]);
                }
            } else {
                console.error(response);
            }
        });
    });

    $("#plan-appointment").click(function () {
        $.post('/api/appointment/add', {
            name: $("#new-name").val(),
            subject: $("#new-subject").val(),
            symptoms: $("#new-symptoms").val(),
            date: $("#new-date").val() + " " + $("#new-time").val()
        }, function (response) {
            if (response.success) {
                window.location = window.location;
            } else {
                console.error(response);
            }
        })
    });

    $("#save-questions").click(function () {
        $.post("/api/appointment/add/result", {
            id: id,
            prescription: $("#prescription").val()
        }, function(response) {
            if (response.success) {
                $.post("/api/appointment/add/result/questions", {
                    id : response.body,
                    questions : [
                        {
                            question: $("#question-1").val(),
                            answer: $("#answer-1").val()
                        }, {
                            question: $("#question-2").val(),
                            answer: $("#answer-2").val()
                        }, {
                            question: $("#question-3").val(),
                            answer: $("#answer-3").val()
                        }
                    ]
                }, function(res) {
                    console.log(res);
                });
            } else {
                console.error(response);
            }
        });
    });

    $(document).on("click", "#postAppointmentButton", function () {
        $('#viewModal').modal('hide');
        $('#postAppointmentModal').modal('show');
    });

    $(document).on("click", "#preAppointmentButton", function () {
        $('#postAppointmentModal').modal('hide');
        $('#viewModal').modal('show');
    });
});
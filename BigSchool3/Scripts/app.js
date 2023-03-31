var CoursesController = function () {
    var init = function () {
        $(".js-togegle-attendance").click(function (e) {
            var button = $(e.target);
            if (button.hasClass("btn-default")) {
                $.post("/api/Attendances", { courseId: button.attr("data-course-id") })
                    .done(function () {
                        button
                            .removeClass("btn-default")
                            .addClass("btn-info")
                            .text("Going");
                    })
                    .fail(function () {
                        alert("Something failed!");
                    });
            } else {
                $.ajax({
                    url: "/api/Attendances/" + button.attr("data-course-id"),
                    method: "DELETE"
                })
                    .done(function () {
                        button
                            .removeClass("btn-info")
                            .addClass("btn-default")
                            .text("Going?");
                    })
                    .fail(function () {
                        alert("Something failed 2!");
                    });
            }

        });

    };
    return {
        init: init
    }
}();
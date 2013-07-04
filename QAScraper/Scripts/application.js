$(document).ready(function() {
    $('#update-button').click(function() {
        $.post("/Home/Update", function(data) {
            console.log(data);
        });
    });
})
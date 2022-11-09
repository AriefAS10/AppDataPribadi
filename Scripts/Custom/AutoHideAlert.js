window.setTimeout(function () {
    $(".alert").fadeTo(800, 0).slideUp(800, function () {
        $(this).remove();
    })
}, 4000);
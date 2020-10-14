//https://dotnetthoughts.net/how-to-use-bootstrap-style-validation-in-aspnet-core/
(function () {
    var settings = {
        validClass: "is-valid",
        errorClass: "is-invalid"
    };

    $.validator.setDefaults(settings);
    $.validator.unobtrusive.options = settings;
})();

// https://www.w3schools.com/bootstrap4/tryit.asp?filename=trybs_form_validation_needs&stacked=h
// Disable form submissions if there are invalid fields
(function () {
    'use strict';
    window.addEventListener('load', function () {

        $("*[data-val-required]").each(function (item, index) { $(this).attr("Required"); });


        // Get the forms we want to add validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();
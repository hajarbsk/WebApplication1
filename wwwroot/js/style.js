$(document).ready(function () {
    $('form').submit(function (event) {
        const length = parseFloat($('input[name="length"]').val());
        const width = parseFloat($('input[name="width"]').val());
        if (length <= 0 || width <= 0) {
            alert("Veuillez entrer des valeurs positives pour la longueur et la largeur !");
            event.preventDefault();
        } else {
            alert("Demande soumise avec succès !");
        }
    });
});
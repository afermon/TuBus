(function () {
    var proxied = window.alert;
    window.alert = function () {
        $("#AlertModal  .modal-body").text(arguments[0]);
        $("#AlertModal ").modal('show');
        $("#AlertModal").removeClass('hide');
    };
})();

// Create a Stripe client
var stripe = Stripe('pk_test_rhH71J4uq9xlXUHXioMIpNbF');

// Create an instance of Elements
var elements = stripe.elements();

// Custom styling can be passed to options when creating an Element.
// (Note that this demo uses a wider set of styles than the guide below.)
var style = {
    base: {
        color: '#32325d',
        lineHeight: '18px',
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: 'antialiased',
        fontSize: '16px',
        '::placeholder': {
            color: '#aab7c4'
        }
    },
    invalid: {
        color: '#fa755a',
        iconColor: '#fa755a'
    }
};

// Create an instance of the card Element
var card = elements.create('card', { style: style });

// Add an instance of the card Element into the `card-element` <div>
card.mount('#card-element');
[]
// Handle real-time validation errors from the card Element.
card.addEventListener('change', function (event) {
    var displayError = document.getElementById('card-errors');
    if (event.error) {
        displayError.textContent = event.error.message;
    } else {
        displayError.textContent = '';
    }
});

// Handle form submission
var form = document.getElementById('payment-form');
form.addEventListener('submit', function (event) {
    event.preventDefault();
    if (!$("#recarga").valid()) {
        alert("Por favor verifique los campos señalados");
        return;
    }
    stripe.createToken(card).then(function (result) {
        if (result.error) {
            // Inform the user if there was an error
            var errorElement = document.getElementById('card-errors');
            errorElement.textContent = result.error.message;
        } else {
            // Send the token to your server
            stripeTokenHandler(result.token);
            
        }
    });
});

function stripeTokenHandler(token) {
    // Insert the token ID into the form so it gets submitted to the server
    var form = document.getElementById('payment-form');
    var hiddenInput = document.createElement('input');
    hiddenInput.setAttribute('type', 'hidden');
    hiddenInput.setAttribute('name', 'stripeToken');
    hiddenInput.setAttribute('value', token.id);
    form.appendChild(hiddenInput);
    var urlToApi = $(location).attr("origin").replace("web", "api") + "/api/0";
    $.ajax({
        cache: false,
        type: "POST",
        url: urlToApi + "/payment/MakePayment",
        data: {
            CardToken: token.id,
            AmoutCrc: $("#txtMonto").val(), //direct for testing this should be taken from the core and not UI information.
            Description: $("#txtDescripcion").val(),
            UserCardUnicode: $("#tarjetas").val()
        },
        success: function(result) {
            console.log(result);
            alert(result.Message);
            $("#acept").click(function () { window.location.replace("/Transaction/UserTransactions") });
        },
        error: function(result) {
            console.log(result);
            alert(result.Message);
        }
    });
}

function LoadElements() {
    var control = new ControlActions();
    var userAuth = new Auth();
    var user = userAuth.GetCurrentUser();
    control.FillComboBox("Tarjeta/ListCardsByUser?userMail=" + user.Email, "tarjetas", "CodigoUnico", "NombreTarjeta");
    Retrieve();
}

LoadElements();

function Retrieve() {
    var control = new ControlActions();
    var auth = new Auth();
    var jqxhr = $.get({
        url: control.GetUrlApiService("Usuario/Current"),
        headers: { 'Authorization': auth.GetAccessToken() }
    }).done(function (response) {
        RenderUserInfo(response);
    }).fail(function (response) {
        var data = response.responseJSON;
        var ctrlActions = new ControlActions();
        ctrlActions.ShowMessage('E', data.ExceptionMessage);
        console.log(data);
    });
}

function RenderUserInfo(response) {
    $("#name").text(response.Data.Nombre + " " + response.Data.Apellidos);
    $("#email").text(response.Data.Email);
    $("#Phone").text("+(506) " + response.Data.Telefono);
}

$(document).ready(function () {
    $("#recarga").validate({
        rules: {
            Descripction: {
                required: true
            },
            Monto: {
                required: true,
                number: true
            }
        },
        messages: {
            Descripction: {
                required: "Por favor ingrese una descripción "
            },
            Monto: {
                required: "Por favor ingrese el monto a recargar",
                number: "Por favor ingrese solo números"
            }
        },

        errorElement: "label",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);

        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });
});

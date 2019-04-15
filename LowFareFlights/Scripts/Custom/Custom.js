$(function () {

    var letters = /^[A-Z]/;

    $(document).ready(function () {
        $("[id='number-of-passangers']").attr("required", true);
        $("[id='origin-airport']").attr("required", true);
        $("[id='arrival-airport']").attr("required", true);
        $("[id='departure-date']").attr("required", true);
        
        $("#departure-date").datepicker({
            minDate: 0,
            onSelect: function (date) {
                var date2 = $('#return-date').datepicker('getDate');
                var date = new Date(Date.parse(date2));
                date.setDate(date.getDate() - 1);
                var newDate = date.toDateString();
                newDate = new Date(Date.parse(newDate));
                $('#departure-date').datepicker('setDate',newDate);
            }
        });
        $("#return-date").datepicker({
            onSelect: function (date) {
                var date1 = $('#departure-date').datepicker('getDate');
                var date = new Date(Date.parse(date1));
                date.setDate(date.getDate() + 1);
                var newDate = date.toDateString();
                newDate = new Date(Date.parse(newDate));
                $('#return-date').datepicker("option", "minDate", newDate);
            }           
        });
    });

    $(".other").click(function () {

        var passangersValue = $("#number-of-passangers").val();
        if (event.target.id != "number-of-passangers" && $.isNumeric(passangersValue) == false){       
            $("span#numeric-warning").addClass("hidden").removeClass("hidden");
        }

        var originValue = $("#origin-airport").val();
        if (event.target.id != "origin-airport" && (!originValue.match(letters) || originValue.length != 3)) {   
            $("span#origin-warning").addClass("hidden").removeClass("hidden");        
        }

        var arrivalValue = $("#arrival-airport").val();
        if (event.target.id != "arrival-airport" && (!arrivalValue.match(letters) || arrivalValue.length != 3)) {
            $("span#arrival-warning").addClass("hidden").removeClass("hidden");
        }

        var departureDateValue = $("#departure-date").val();
        if (event.target.id != "departure-date" && (departureDateValue == "" || departureDateValue == null)) {
            $("span#date-warning").addClass("hidden").removeClass("hidden");
        }

        var currencyValue = $("#currency").val();
        if (event.target.id != "currency" && currencyValue != "USD" && currencyValue != "HRK" && currencyValue != "EUR") {
                $("span#currency-warning").addClass("hidden").removeClass("hidden");
        }
    });

    $("#number-of-passangers").mouseout(function () {
        var passangersValue = $("#number-of-passangers").val();
        if ($.isNumeric(passangersValue) == true) {
            $("span#numeric-warning").removeClass("hidden").addClass("hidden");
        }
    });

    $("#origin-airport").mouseout(function () {
        var originValue = $("#origin-airport").val();
        if (originValue.match(letters) && originValue.length == 3) {
            $("span#origin-warning").removeClass("hidden").addClass("hidden");
        }
    });

    $("#arrival-airport").mouseout(function () {
        var arrivalValue = $("#arrival-airport").val();
        if (arrivalValue.match(letters) && arrivalValue.length == 3) {
            $("span#arrival-warning").removeClass("hidden").addClass("hidden");
        }
    });

    $("#departure-date").mouseout(function () {
        var departureDateValue = $("#arrival-airport").val();
        if (departureDateValue != "" || departureDateValue != null) {
            $("span#date-warning").removeClass("hidden").addClass("hidden");
        }
    });

    $("#currency").mouseout(function () {
        var currencyValue = $("#currency").val();
        if (currencyValue == "USD" || currencyValue == "HRK" || currencyValue == "EUR") {
            $("span#currency-warning").removeClass("hidden").addClass("hidden");
        }
    });
    

    $(document).on('submit', '#filter-form', function (e) {
        e.preventDefault();
        var form = $(this);
        var dataAjax = $(document).find(".data-ajax");

        var adults = $("#number-of-passangers").val();
        var origin = $("#origin-airport").val();
        var destination = $("#arrival-airport").val();
        var departureDate = $("#departure-date").val();
        var returnDate = $("#return-date").val(); 
        var currency = $("#currency").val();

        if ($.isNumeric(adults) == false)
            return;
        else if (!origin.match(letters) || origin.length != 3)
            return;
        else if (!destination.match(letters) || destination.length != 3) 
            return;
        else if (departureDate == "" || departureDate == null)
            return;
        else if (currency != "USD" && currency != "HRK" && currency != "EUR") 
            return; 

        $.ajax({
            url: form.attr("data-action"),
            method: form.attr("data-method"),
            data: { origin: origin, destination: destination, departureDate: departureDate, returnDate: returnDate, adults: adults, currency: currency },
            async: true,
            success: function (response) {
                dataAjax.html(response);
            }
            
        });
    });


});
$(function () {
    
    var source = [
                    "Sunday",
                    "Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday"
		        ];
    

    // Create a jqxDropDownList
    $("#DayList").jqxDropDownList({ source: source, selectedIndex: 0, width: '200', height: '25' });
    var selected = 'Sunday';
    $("#DayList").on('select', function (event) {
        if (event.args) {
            var item = event.args.item;
            if (item) {
                selected = item.value;
                $.post("/Preference/Test", { Day: item.value }).done(function(data) {
                    alert('JSON.stringify(data)');
                });
            }
        }
    });
    function saveDayPreferences() {
        var firstTimeFrom = $("#TimeFrom1").val();
        var firstTimeTo = $("#TimeTo1").val();
        var secondTimeFrom = $("#TimeFrom2").val();
        var secondTimeTo = $("#TimeTo2").val();
        $.post("/Preference/SaveDayTimePreference", { Day: selected, FirstFrom: firstTimeFrom, FirstTo: firstTimeTo, SecondFrom: secondTimeFrom, SecondTo: secondTimeTo}).done(function (data) {
            alert('success');
        });
    }

    $('#btnSave').click(function () {
        if(validate())
        saveDayPreferences();
    });

    function validate() {
        alert('validate fn');
        var timeRegEx = new RegExp(/^([01]?[0-9]|2[0-3]):[0-5][0-9]$/);
        if ($("#TimeTo1").val().match(timeRegEx) && $("#TimeTo2").val().match(timeRegEx) && $("#TimeFrom1").val().match(timeRegEx) && $("#TimeFrom2").val().match(timeRegEx)) {
            $("#lblValidate").text('');
            return true;
        } else {
            $("#lblValidate").text('Incorrect time format');
            return false;
        }    
    }
});
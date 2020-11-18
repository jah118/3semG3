var $datePicker = $("div#datepicker");

var $datePicker = $("div");

$datePicker.datepicker({
    changeMonth: true,
    changeYear: true,
    inline: true,
    altField: "#datep",
}).change(function (e) {
    setTimeout(function () {
        $datePicker
            .find('.ui-datepicker-current-day').parent().after('<tr><td colspan="8"><div><button>8:00 am – 9:00 am</button></div><button>9:00 am – 10:00 am</button></div><button>10:00 am – 11:00 am</button></div></td></tr>')

    });
});



$('.date-picker-2').popover({
    html: true,
    content: function () {
        return $("#example-popover-2-content").html();
    },
    title: function () {
        return $("#example-popover-2-title").html();
    }
});
$(".date-picker-2").datepicker({
    onSelect: function (dateText) {
        $('#example-popover-2-title').html('<b>Avialable Appiontments</b>');
        var html = '<button  class="btn btn-success">8:00 am – 9:00 am</button><br><button  class="btn btn-success">10:00 am – 12:00 pm</button><br><button  class="btn btn-success">12:00 pm – 2:00 pm</button>';
        $('#example-popover-2-content').html('Avialable Appiontments On <strong>' + dateText + '</strong><br>' + html);
        $('.date-picker-2').popover('show');
    }
});
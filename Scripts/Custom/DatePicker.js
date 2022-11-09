$(document).ready(function () {
    $('.datepicker').datepicker({
        dateFormat: 'dd-M-yy',
        changeMonth: true,
        changeYear: true,
        maxDate: '0',
        yearRange: '-60:+60'

    });
});
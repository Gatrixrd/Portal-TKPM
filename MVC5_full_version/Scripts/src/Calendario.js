//var e = jQuery.noConflict();
var imagenCalendario = "../../Images/bafar/calendario.png";
$.datepicker.regional['es'] = {
    closeText: 'Cerrar',
    prevText: '<Ant',
    nextText: 'Sig>',
    currentText: 'Hoy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};
$.datepicker.setDefaults($.datepicker.regional['es']);
$(function () {
    $(".fecha1").datepicker({
        onClose: function (selectedDate) {
            $(".fecha2").datepicker("option", "minDate", selectedDate);
        }
    });
    $(".fecha2").datepicker({
        onClose: function (selectedDate) {
            $(".fecha1").datepicker("option", "maxDate", selectedDate);
        }
    });
    $(".fecha1_boton").datepicker({
        onClose: function (selectedDate) {
            $(".fecha2_boton").datepicker("option", "minDate", selectedDate);
        },
        showOn: "button",
        buttonImageOnly: true,
        buttonImage: imagenCalendario,
        buttonText: "Fecha inicial"
    });
    $(".fecha2_boton").datepicker({
        onClose: function (selectedDate) {
            $(".fecha1_boton").datepicker("option", "maxDate", selectedDate);
        },
        showOn: "button",
        buttonImageOnly: true,
        buttonImage: imagenCalendario,
        buttonText: "Fecha final"
    });    
    $(".fechas").datepicker({
        maxDate: "+0D",
        showOn: "button",
        buttonImage: imagenCalendario,
        buttonImageOnly: true,
        buttonText: "Fecha",
    });
    $(".fechas2").datepicker({
        maxDate: "+0D",
        showOn: "button",
        buttonImage: imagenCalendario,
        buttonText: "Fecha",
    });

    $(".fechaIIndicador").datepicker({
        onClose: function (selectedDate) {
            $(".fechaFIndicador").datepicker("option", "minDate", selectedDate);
        },
        onSelect: function (fecha) {
            Inicio();
        },
        showOn: "button",
        buttonImageOnly: true,
        buttonImage: imagenCalendario,
        buttonText: "Fecha inicial"
    });
    $(".fechaFIndicador").datepicker({
        onClose: function (selectedDate) {
            $(".fechaIIndicador").datepicker("option", "maxDate", selectedDate);
        },
        onSelect: function (fecha) {
            Inicio();
        },
        showOn: "button",
        buttonImageOnly: true,
        buttonImage: imagenCalendario,
        buttonText: "Fecha final"
    });
});
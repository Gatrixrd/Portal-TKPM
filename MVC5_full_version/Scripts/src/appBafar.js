var url_dir = '';
var loading = url_dir + "/Images/bafar/ajax-loader-obs.gif";

var appBafar = function () {
    function CerrarSesion() {
        $.ajax({
            type: "post",
            url: url_dir + '/Dashboard/CerrarSesion',
            cache: false,
            success: function (event, xhr, settings) {
                var jsonResult = JSON.parse(settings.responseText);
                if (jsonResult != undefined && jsonResult.NewUrl)
                    window.location = jsonResult.NewUrl;
            },
            error: function (e) {
                if (typeof (e) == "string") {
                }
                else {
                }
                return;
            }
        });
        $(document)
            .ajaxStart(
            $.blockUI({
                message: '<h1><img src="' + loading + '" /> Cerrando sesión...</h1>',
                css: {
                    border: 'none',
                    padding: '15px',
                    opacity: '0',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            }))
            .ajaxError($.unblockUI)
            .ajaxComplete($.unblockUI)
            .ajaxStop($.unblockUI);
    }

    $(function () {
        Salir();
        MenuAdmonPerfilPaquete();
    });

    var Salir = function (e) {//, #cerrarSesion, #cerrarSesion2
        $('#cerrarSesion').on('click', function () {
            CerrarSesion();
        });
        $('#cerrarSesion2').on('click', function () {
            CerrarSesion();
        });
        $('#cerrarSesion3').on('click', function () {
            CerrarSesion();
        });
    };
    
    var MenuAdmonPerfilPaquete = function (e) {

        $("#MenuPerfilPaquete").click(function (event) {
            event.preventDefault();
            
            $.ajax({
                type: "post",
                url: url_dir + '/Administracion/AdmonPerfilesPaquetes',
                cache: false,
                success: function (lista) {
                    if (lista.length == 0) {

                    }
                    else {
                        window.location = url_dir + '/Administracion/AdmonPerfilesPaquetes';
                    }
                }
            });


            $(document).ajaxStart(
                        $.blockUI({
                            message: '<h1><img src="' + loading + '" /> Espere...</h1>',
                            css: {
                                border: 'none',
                                padding: '15px',
                                backgroundColor: '#000',
                                '-webkit-border-radius': '10px',
                                '-moz-border-radius': '10px',
                                opacity: .5,
                                color: '#fff'
                            }
                        }))
                        .ajaxSend($.blockUI({
                            message: '<h1><img src="' + loading + '" /> Cargando...</h1>',
                            css: {
                                border: 'none',
                                padding: '15px',
                                backgroundColor: '#000',
                                '-webkit-border-radius': '10px',
                                '-moz-border-radius': '10px',
                                opacity: .5,
                                color: '#fff'
                            }
                        }))
                        .ajaxSuccess($.blockUI({
                            message: '<h1><img src="' + loading + '" /> Espere...</h1>',
                            css: {
                                border: 'none',
                                padding: '15px',
                                backgroundColor: '#000',
                                '-webkit-border-radius': '10px',
                                '-moz-border-radius': '10px',
                                opacity: .5,
                                color: '#fff'
                            }
                        }))
                        .ajaxError(function (xhr, status, p3, p4) {
                            $.blockUI({
                                message: '<h1><img src="' + loading + '" />Espere...</h1>',
                                css: {
                                    border: 'none',
                                    padding: '15px',
                                    backgroundColor: '#000',
                                    '-webkit-border-radius': '10px',
                                    '-moz-border-radius': '10px',
                                    opacity: .5,
                                    color: '#fff'
                                }
                            });
                            setTimeout(function () {
                                $.unblockUI;
                            }, 2000);
                            return;

                        })
                        .ajaxComplete(function () {
                            
                                $.blockUI({
                                    message: '<h1><img src="' + loading + '" /> Actualizando información... </h1>',
                                    css: {
                                        border: 'none',
                                        padding: '15px',
                                        backgroundColor: '#000',
                                        '-webkit-border-radius': '10px',
                                        '-moz-border-radius': '10px',
                                        opacity: .5,
                                        color: '#fff'
                                    }
                                });
                                setTimeout(function () {
                                    $.unblockUI({
                                        onUnblock: function () {
                                            
                                        }
                                    });
                                }, 5000);
                            
                        })
                        .ajaxStop($.unblockUI);

        });



    };//fin function


    //return functions
    return {
        Salir: Salir,
        MenuAdmonPerfilPaquete:MenuAdmonPerfilPaquete
    };
}();

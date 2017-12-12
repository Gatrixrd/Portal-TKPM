
var txtFreight;
var txtNumProv;
var app = function () {

    $(function () {
        toggleSettings();
        switchTheme();
        navToggleRight();
        navToggleLeft();
        navToggleSub();
        profileToggle();
        widgetToggle();
        widgetClose();
        widgetFlip();
        tooltips();
        switcheryToggle();
        fullscreenWidget();
        fullscreenMode();
    });

    var toggleSettings = function () {
        $('.config-link').click(function () {
            if ($(this).hasClass('open')) {
                $('#config').animate({
                    "right": "-205px"
                }, 150);
                $(this).removeClass('open').addClass('closed');
            } else {
                $("#config").animate({
                    "right": "0px"
                }, 150);
                $(this).removeClass('closed').addClass('open');
            }
        });
    };

    var switchTheme = function () {
        $('.theme-style-wrapper').click(function () {
            $('#main-wrapper').attr('class', '');
            var themeValue = $(this).data('theme');
            $('#main-wrapper').addClass(themeValue);
        });
    };


    var navToggleRight = function () {
        $('#toggle-right').on('click', function () {
            $('#sidebar-right').toggleClass('sidebar-right-open');
            $("#toggle-right .fa").toggleClass("fa-indent fa-dedent");

        });
    };

    var customCheckbox = function () {
        $('input.icheck').iCheck({
            checkboxClass: 'icheckbox_flat-grey',
            radioClass: 'iradio_flat-grey'
        });
    }

    var formMask = function () {
        $("#input1").mask("99/99/9999");
        $("#input2").mask('(999) 999-9999');
        $("#input3").mask("(999) 999-9999? x99999");
        $("#input4").mask("99-9999999");
        $("#input5").mask("999-99-9999");
        $("#input6").mask("a*-999-a999");
    }

    var weather = function () {
        var icons = new Skycons({
            "color": "#27B6AF"
        });

        icons.set("clear-day", Skycons.CLEAR_DAY);
        icons.set("clear-night", Skycons.CLEAR_NIGHT);
        icons.set("partly-cloudy-day", Skycons.PARTLY_CLOUDY_DAY);
        icons.set("partly-cloudy-night", Skycons.PARTLY_CLOUDY_NIGHT);
        icons.set("cloudy", Skycons.CLOUDY);
        icons.set("rain", Skycons.RAIN);
        icons.set("sleet", Skycons.SLEET);
        icons.set("snow", Skycons.SNOW);
        icons.set("wind", Skycons.WIND);
        icons.set("fog", Skycons.FOG);

        icons.play();
    }

    var formWizard = function () {
        $('#myWizard').wizard()
    }

    var navToggleLeft = function () {
        $('#toggle-left').on('click', function () {
            var bodyEl = $('#main-wrapper');
            ($(window).width() > 767) ? $(bodyEl).toggleClass('sidebar-mini') : $(bodyEl).toggleClass('sidebar-opened');
        });
    };

    var navToggleSub = function () {
        var subMenu = $('.sidebar .nav');
        $(subMenu).navgoco({
            caretHtml: false,
            accordion: true
        });

    };

    var profileToggle = function () {
        $('#toggle-profile').click(function () {
            $('.sidebar-profile').slideToggle();
        });
    };

    var widgetToggle = function () {
        $(".actions > .fa-chevron-down").click(function () {
            $(this).parent().parent().next().slideToggle("fast"), $(this).toggleClass("fa-chevron-down fa-chevron-up")
        });
    };

    var widgetClose = function () {
        $(".actions > .fa-times").click(function () {
            $(this).parent().parent().parent().fadeOut()
        });
    };

    var widgetFlip = function () {
        $(".actions > .fa-cog").click(function () {
            $(this).closest('.flip-wrapper').toggleClass('flipped')
        });
    };

    var dateRangePicker = function () {
        $('.reportdate').daterangepicker({
            format: 'YYYY-MM-DD',
            startDate: '2014-01-01',
            endDate: '2014-06-30'
        });
    };


    //tooltips
    var tooltips = function () {
        $('.tooltip-wrapper').tooltip({
            selector: "[data-toggle=tooltip]",
            container: "body"
        })
    };

    //Sliders
    var sliders = function () {
        $('.slider-span').slider()
    };


    //Chart.js LineChart, BarChart, DoughnutChart
    var chartJs = function () {
        //Line Charts
        var randomScalingFactor = function () {
            return Math.round(Math.random() * 100)
        };
        var lineChartData = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [{
                label: 'Network Usage',
                fillColor: 'rgba(26,188,156,0.5)',
                strokeColor: 'rgba(26,188,156,1)',
                pointColor: 'rgba(220,220,220,1)',
                pointStrokeColor: '#fff',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
            }, {
                label: 'CPU Load',
                fillColor: 'rgba(31,123,182,0.5)',
                strokeColor: 'rgba(31,123,182,1)',
                pointColor: 'rgba(151,187,205,1)',
                pointStrokeColor: '#fff',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(151,187,205,1)',
                data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
            }]

        }
        //Bar Charts
        var randomScalingFactor = function () {
            return Math.round(Math.random() * 100)
        };
        var barChartData = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [{
                fillColor: 'rgba(26,188,156,0.5)',
                strokeColor: 'rgba(255,255,255,0.8)',
                highlightFill: 'rgba(26,188,156,1)',
                highlightStroke: 'rgba(255,255,255,0.8)',
                data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
            }, {
                label: 'CPU Load',
                fillColor: 'rgba(31,123,182,0.5)',
                strokeColor: 'rgba(255,255,255,0.8)',
                highlightFill: 'rgba(31,123,182,1)',
                highlightStroke: 'rgba(255,255,255,0.8)',
                data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
            }]

        }

        //DoughnutChart
        var doughnutData = [{
            value: 300,
            color: "#1ABC9C",
            highlight: "#1ABC9C",
            label: "Chrome"
        }, {
            value: 50,
            color: "#556B8D",
            highlight: "#556B8D",
            label: "IE"
        }, {
            value: 100,
            color: "#EDCE8C",
            highlight: "#EDCE8C",
            label: "Safari"
        }, {
            value: 40,
            color: "#CED1D3",
            highlight: "#1F7BB6",
            label: "Other"
        }, {
            value: 120,
            color: "#1F7BB6",
            highlight: "#1F7BB6",
            label: "Firefox"
        }];

        window.onload = function () {
            var ctx1 = document.getElementById("canvas1").getContext("2d");
            window.myLine = new Chart(ctx1).Line(lineChartData, {
                responsive: true
            });

            var ctx2 = document.getElementById("canvas2").getContext("2d");
            window.myBar = new Chart(ctx2).Bar(barChartData, {
                responsive: true
            });

            var ctx3 = document.getElementById("doughnut-chart-area").getContext("2d");
            window.myDoughnut = new Chart(ctx3).Doughnut(doughnutData, {
                responsive: true
            });

        };

    };
    var KPI_Grafica_Dona = function (datos, nombreElemento, nombreLeyenda, porcentaje) {
        var graficos = Array(datos.length);

        for (var i = 0; i < datos.length; i++) {
            var d = new Object();
            var elemento = datos[i];
            d.value = elemento.Valor;
            d.label = elemento.Etiqueta;
            switch (elemento.Tipo) {
                case 1:
                    d.color = "#1ABC9C";
                    d.highlight = "#1ABC9C";
                    break;
                case 2:
                    d.color = "#556B8D";
                    d.highlight = "#556B8D";
                    break;
                case 3:
                    d.color = "#EDCE8C";
                    d.highlight = "#EDCE8C";
                    break;
                case 4:
                    d.color = "#1F7BB6";
                    d.highlight = "#1F7BB6";
                    break;
                default:
                    break;
            }
            graficos[i] = d;
        }
        var options = {
            segmentShowStroke: true,
            animateRotate: true,
            animateScale: false,
            percentageInnerCutout: 50,
            responsive: true,
            title: {
                display: true,
                text: 'Custom Chart Title',
                position: top
            }
        }
        if (porcentaje) {
            options.tooltipTemplate = "<%= value %>%";
        }
        var ctx3 = document.getElementById(nombreElemento).getContext("2d");
        window.myDoughnut = new Chart(ctx3).Doughnut(graficos, options);
        var leyenda = $(nombreLeyenda);
        leyenda.html(window.myDoughnut.generateLegend());
    };

    var nestedSortable = function () {
        var updateOutput = function (e) {
            var list = e.length ? e : $(e.target),
                output = list.data('output');
            if (window.JSON) {
                output.val(window.JSON.stringify(list.nestable('serialize'))); //, null, 2));
            } else {
                output.val('JSON browser support required for this demo.');
            }
        };

        // activate Nestable for list 1
        $('#nestable').nestable({
            group: 1
        })
            .on('change', updateOutput);

        // activate Nestable for list 2
        $('#nestable2').nestable({
            group: 1
        })
            .on('change', updateOutput);

        // output initial serialised data
        updateOutput($('#nestable').data('output', $('#nestable-output')));
        updateOutput($('#nestable2').data('output', $('#nestable2-output')));

        $('#nestable-menu').on('click', function (e) {
            var target = $(e.target),
                action = target.data('action');
            if (action === 'expand-all') {
                $('.dd').nestable('expandAll');
            }
            if (action === 'collapse-all') {
                $('.dd').nestable('collapseAll');
            }
        });
    };

    var ValidacionFreight = function () {
        $('#formBuscar').validate({
            errorElement: "span",
            validClass: "has-success has-feedback",
            errorClass: "has-error has-feedback",
            rules: {
                freightOrder: {
                    required: true,
                    digits: true
                },
                numProveedor: {
                    required: false,
                }
            },
            messages: {
                freightOrder: {
                    required: "",
                    digits: ""
                },
                numProveedor: {
                    required: "",
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(validClass).addClass(errorClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-ok form-control-feedback").addClass("glyphicon glyphicon-remove form-control-feedback").css("display", "block");
                $("#btnFreightOrder").attr("disabled", "disabled");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(errorClass).addClass(validClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-remove form-control-feedback").addClass("glyphicon glyphicon-ok form-control-feedback").css("display", "block");
                $("#btnFreightOrder").removeAttr("disabled");
            }
        });
    }

    var ValidacionFreight2 = function () {
        $('#formBuscar').validate({
            errorElement: "span",
            validClass: "has-success has-feedback",
            errorClass: "has-error has-feedback",
            rules: {
                freightOrder: {
                    required: true,
                    digits: true
                }
            },
            messages: {
                freightOrder: {
                    required: "",
                    digits: ""
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(validClass).addClass(errorClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-ok form-control-feedback").addClass("glyphicon glyphicon-remove form-control-feedback").css("display", "block");
                $("#btnFreightOrder").attr("disabled", "disabled");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(errorClass).addClass(validClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-remove form-control-feedback").addClass("glyphicon glyphicon-ok form-control-feedback").css("display", "block");
                $("#btnFreightOrder").removeAttr("disabled");
            }
        });
    }

    var ValidacionDatosPDF = function () {
        $('#formPDF').validate({
            errorElement: "span",
            validClass: "has-success has-feedback",
            errorClass: "has-error has-feedback",
            onclick: true,
            rules: {
                folio: {
                    required: true
                },
                fecha: {
                    required: true,
                    date: true
                },
                total: {
                    required: true,
                    number: true
                },
                cargaPDF: {
                    required: true,
                    accept: "application/pdf"
                }
            },
            messages: {
                folio: {
                    required: ""
                },
                fecha: {
                    required: "",
                    digits: ""
                },
                total: {
                    required: "",
                    number: ""
                },
                cargaPDF: {
                    required: "El archivo PDF es requerido.",
                    accept: "No es archivo PDF"
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(validClass).addClass(errorClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-ok form-control-feedback").addClass("glyphicon glyphicon-remove form-control-feedback").css("display", "block");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).closest('.form-group').removeClass(errorClass).addClass(validClass);
                $(element.form).find("span[for=" + element.id + "]")
                  .removeClass("glyphicon glyphicon-remove form-control-feedback").addClass("glyphicon glyphicon-ok form-control-feedback").css("display", "block");
            }
        });
    }

    var ValidacionDatosXML = function () {
        $('#formXML').validate({
            rules: {
                cargaXML: {
                    required: true,
                    accept: "text/xml"
                }
            },
            messages: {
                cargaXML: {
                    required: "El archivo XML es requerido.",
                    accept: "No es archivo XML"
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group').removeClass('success').addClass('error');
                //$("#btnFreightOrder").attr('disabled', 'disabled');
                //for (var i = 0; i < element.length; i++) {
                //    //if (element[i].innerText == "Correcto") {
                //    if (element[i].htmlFor == "txtFreightOrder") {
                //        txtFreight = false;
                //    }
                //    else {
                //        txtNumProv = false;
                //    }
                //    //}
                //}
            },
            success: function (element) {
                element.text('Correcto').addClass('valid')
                    .closest('.form-group').removeClass('error').addClass('success');
                //for (var i = 0; i < element.length; i++) {
                //    //if (element[i].innerText == "Correcto") {
                //    if (element[i].htmlFor == "txtFreightOrder") {
                //        txtFreight = true;
                //    }
                //    else {
                //        txtNumProv = true;
                //    }
                //    //}
                //}
                //if (txtFreight && txtNumProv) {
                //    $("#btnFreightOrder").removeAttr('disabled');
                //}
                //else {
                //    $("#btnFreightOrder").attr('disabled', 'disabled');
                //}
            },
            submitHandler: function () {
                $("#btnAnterior").attr('disabled', 'disabled');
                //$("#btnAnterior").click();
                ////$("#btnFreightOrder").removeAttr('disabled');
            }
        });
    }

    var formValidation = function () {
        $('#form').validate({
            rules: {
                input1: {
                    required: true
                },
                input2: {
                    minlength: 5,
                    required: true
                },
                input3: {
                    maxlength: 5,
                    required: true
                },
                input4: {
                    required: true,
                    minlength: 4,
                    maxlength: 8
                },
                input5: {
                    required: true,
                    min: 5
                },
                input6: {
                    required: true,
                    range: [5, 50]
                },
                input7: {
                    minlength: 5
                },
                input8: {
                    required: true,
                    minlength: 5,
                    equalTo: "#input7"
                },
                input9: {
                    required: true,
                    email: true
                },
                input10: {
                    required: true,
                    url: true
                },
                input11: {
                    required: true,
                    digits: true
                },
                input12: {
                    required: true,
                    phoneUS: true
                },
                input13: {
                    required: true,
                    minlength: 5
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group').removeClass('success').addClass('error');
            },
            success: function (element) {
                element.text('Correcto').addClass('valid')
                    .closest('.form-group').removeClass('error').addClass('success');
            }
        });
    }

    var spinStart = function (spinOn) {
        var spinFull = $('<div class="preloader"><div class="iconWrapper"><i class="fa fa-circle-o-notch fa-spin"></i></div></div>');
        var spinInner = $('<div class="preloader preloader-inner"><div class="iconWrapper"><i class="fa fa-circle-o-notch fa-spin"></i></div></div>');
        if (spinOn === undefined) {
            $('body').prepend(spinFull);
        } else {
            $(spinOn).prepend(spinInner);
        };

    };

    var spinStop = function () {
        $('.preloader').remove();
    };

    var switcheryToggle = function () {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elems.forEach(function (html) {
            var switchery = new Switchery(html, {
                size: 'small',
                color: '#27B6AF',
                secondaryColor: '#B3B8C3'
            });
        });
    };

    var fullscreenWidget = function () {
        $('.panel .fa-expand').click(function () {
            var panel = $(this).closest('.panel');
            panel.toggleClass('widget-fullscreen');
            $(this).toggleClass('fa-expand fa-compress');
            $('body').toggleClass('fullscreen-widget-active')

        })
    };


    var fullscreenMode = function () {
        $('#toggle-fullscreen.expand').on('click', function () {
            $(document).toggleFullScreen()
            $('#toggle-fullscreen .fa').toggleClass('fa-expand fa-compress');
        });
    };

    var zonaCargaDoc = function (IdElemento, extension, url, multiple) {        
        Dropzone.autoDiscover = false;
        var zona1 = new Dropzone(IdElemento, { // Make the whole body a dropzone
            paramName: "files",
            acceptedFiles: extension,
            url: url_dir + url,
            addRemoveLinks: true,
            uploadMultiple: multiple,
            dictFallbackMessage: "De click para cargar sus archivos.",
            dictDefaultMessage: "Su navegador no soporta la carga de archivos con arrastrar y soltar.",
            dictFallbackText: "Por favor, utilice el formulario de abajo para cargar sus archivos.",
            dictInvalidFileType: "No se puede cargar este tipo de archivo.",
            dictFileTooBig: "El archivo {{filesize}} es demasiado grande {{maxFilesize}}",
            dictResponseError: "Error en servidor: {{statusCode}}",
            dictCancelUpload: "Cancelar la carga.",
            dictCancelUploadConfirmation: "¿Esta seguro de cancelar la carga de archivos?",
            dictRemoveFile: "Eliminar archivo",
            dictMaxFilesExceeded: "Ya no puede cargar mas archivos.",
        });
        return zona1;        
    };

    //return functions
    return {
        dateRangePicker: dateRangePicker,
        sliders: sliders,
        nestedSortable: nestedSortable,
        chartJs: chartJs,
        customCheckbox: customCheckbox,
        formValidation: formValidation,
        ValidacionFreight: ValidacionFreight,
        ValidacionFreight2: ValidacionFreight2,
        ValidacionDatosPDF: ValidacionDatosPDF,
        ValidacionDatosXML: ValidacionDatosXML,
        formMask: formMask,
        formWizard: formWizard,
        weather: weather,
        spinStart: spinStart,
        spinStop: spinStop,
        KPI_Grafica_Dona: KPI_Grafica_Dona,
        zonaCargaDoc: zonaCargaDoc
    };
}();


$(window).resize(function () {
    app.chartJs();

});

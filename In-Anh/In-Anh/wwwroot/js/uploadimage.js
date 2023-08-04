
$(document).ready( async function () {
    await uploader.initScript();
    uploader.selected();
    $('.image-cointainer').removeClass('hidden')
})
var lstinit = [];
var uploader = {
    initScript: async function () {
        await $.getScript("https://code.jquery.com/jquery.min.js");
        await $.getScript("../lib/ssi-uploader-master/dist/ssi-uploader/js/ssi-uploader.min.js");
        await $('<link>')
            .appendTo('head')
            .attr({
                type: 'text/css',
                rel: 'stylesheet',
                href: '../lib/ssi-uploader-master/dist/ssi-uploader/styles/ssi-uploader.min.css'
            });
        await $('<link>')
            .appendTo('head')
            .attr({
                type: 'text/css',
                rel: 'stylesheet',
                href: '../css/uploadimage.css'
            });
       
    },
    init0: function () {
        
        $('.ssi-uploadInput0').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init1: function () {
        
        $('.ssi-uploadInput1').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init2: function () {
        
        $('.ssi-uploadInput2').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init3: function () {
        
        $('.ssi-uploadInput3').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init4: function () {
        
        $('.ssi-uploadInput4').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init5: function () {
        
        $('.ssi-uploadInput5').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },
    init6: function () {
        
        $('.ssi-uploadInput6').ssi_uploader({

            // allows duplicates
            allowDuplicates: false,

            // The utl to which the ajax request is sent.
            url: 'http://ssinput.com/php/upload.php',

            // Sends extra data with the request.
            data: {},

            // en, gr
            locale: 'en',

            // Enables/disables the file preview.
            preview: true,

            // Enables/disables drag and drop.
            dropZone: true,

            // How many files are allowed per upload.
            maxNumberOfFiles: '',

            // Setting up the response validation, ssi-uploader will be able to handle erros and display them.
            // validationKey: {String||Object} Defines the validation key of the response. For
            // resultKey: {String} Defines the validation key of the response.
            // success: {String} Defines the success key of the response.
            // error: {String} Defines the error key of the response.
            response: false,

            // If true the upload will continue normally even if there is an error in a callback function. 
            // If false the upload will aborted, if it's possible, and will console.log the errors.
            ignoreCallbackErrors: false,

            // Boolean or a function
            // Transform file to form data
            transformFile: true,

            // The maximum size of each file.
            maxFileSize: "40",

            // is in form
            inForm: false,

            // Extends the default options of $.ajax function:
            // https://api.jquery.com/jquery.ajax/
            ajaxOptions: {},

            // The files allowed to be uploaded. 
            allowed: ['jpg', 'jpeg', 'png', 'bmp', 'gif'],

            // The method that will be used to display the messages.
            errorHandler: {
            method: function (msg) {
                alert(msg);
            },
            success: 'success',
            error: 'error'
        }
  
});

    },

    selected: function () {
        $('.image-size-select').on('change', function () {
            var optionsl = $(this).find(":selected").val() * 1;
            console.log(optionsl);
            switch (optionsl) {
                case 0:
                    console.log(123);
                    if (lstinit.some(x => x == 0)) {
                        break;
                    }
                    lstinit.push(0);
                    uploader.init0();
                    $('.ssi-uploadInput0').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 4x6')
                    $('.ssi-uploadInput0').removeClass('hidden');

                    break;
                case 1:
                    if (lstinit.some(x => x == 1)) {
                        break;
                    }
                    lstinit.push(1);
                    uploader.init1();
                    $('.ssi-uploadInput1').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 6x9')
                    $('.ssi-uploadInput1').removeClass('hidden');
                    break;
                case 2:
                    if (lstinit.some(x => x == 2)) {
                        break;
                    }
                    uploader.init2();
                    $('.ssi-uploadInput2').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 9x12')
                    $('.ssi-uploadInput2').removeClass('hidden');
                    lstinit.push(2);
                   
                    break;
                case 3: 
                    if (lstinit.some(x => x == 3)) {
                        break;
                    }
                    uploader.init3();
                    $('.ssi-uploadInput3').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 10x15')
                    $('.ssi-uploadInput3').removeClass('hidden');
                    lstinit.push(3);
                    break;
                case 4: 
                    if (lstinit.some(x => x == 4)) {
                        break;
                    }
                    uploader.init4();
                    $('.ssi-uploadInput4').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 13x18')
                    $('.ssi-uploadInput4').removeClass('hidden');
                    lstinit.push(4);
                    break;
                case 5: 
                    if (lstinit.some(x => x ==5)) {
                        break;
                    }
                    uploader.init5();
                    $('.ssi-uploadInput5').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 15x21')
                    $('.ssi-uploadInput5').removeClass('hidden');
                    lstinit.push(5);
                    break;
                case 6: 
                    if (lstinit.some(x => x == 6)) {
                        break;
                    }
                    uploader.init6();
                    $('.ssi-uploadInput6').parents('.ssi-uploader').find('#ssi-DropZoneBack').html('Size: 20x30')
                    $('.ssi-uploadInput6').removeClass('hidden');
                    lstinit.push(6);
                    break;
                default:
                    break;
            }
        });
    }
}
var keyword = "";
var page = 0;

var admin = {
    init: function () {

    },
    paging: function (index) {
        $.ajax({
            url: "/Admin/GetData",
            type: "get",
            beforeSend: function () {
                $.LoadingOverlay("show");
            },
            data: { pageIndex: index, keyw: keyword },
            success: function (result) {
                $.LoadingOverlay("hide");
                $('.container-tb').html(result)
                

            }
        });
    },
    viewDetail: function () {
        $(document.querySelector('#exampleModalAdmin')).modal('show');
    }, removeDetail: function () {

    },print: function () {

    }

    }
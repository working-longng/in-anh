var keyword = "";
var page = 0;
var admin = {
    init: function () {

    },
    paging: function (index) {
        $.ajax({
            url: "/Admin/GetData",
            type: "get",
            data: { pageIndex: index, keyw: keyword },
            success: function (result) {
                page = pageIndex
                data: fd

            }
        });
    }

    }
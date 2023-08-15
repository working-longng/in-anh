
var isGetData = true;
var page = 0;
$(window).on("load", function () {
    historyinanh.init();
});

var historyinanh = {
    init: function () {
        $(window).on('scroll', function () {
            var scrollHeight = $('.data-history-list').height();
            //scroll position
            var scrollPos = $(window).height() + $(window).scrollTop();
            // fire if the scroll position is 300 pixels above the bottom of the page
            if (((scrollHeight + 130) >= scrollPos) / scrollHeight == 0) {
                if (isGetData) {
                    isGetData = false;
                    page += 1;
                    $.ajax({
                        url: "/History/GetData",
                        type: "get",
                        beforeSend: function () {
                            $.LoadingOverlay("show");
                        },
                        data: { page: page },
                        success: function (result) {
                            
                            $.LoadingOverlay("hide");
                          
                            
                            if (result.code == 200) {
                                isGetData = true;
                                $('.data-history-list').append(result.data.html);
                            }
                           


                        }, error: function () {

                        }
                    });
                    
                }
                
            }
        });
    }
}
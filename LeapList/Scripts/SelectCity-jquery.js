$(document).ready(function(){
    $('input').click(function() {
        var d = $(this).attr('value');
        $.post("/Login/SelectCity", { city: d })
    });
});

//function Redirect(url) {
//    var ua = naviator.userAgent.toLowerCase(),
//        isIE = ua.indexOf('msie') !== -1,
//        version = parseInt(ua.substr(4, 2), 10);

//    // Internet Explorer 8 and lower
//    if (isIE && version < 9) {
//        var link = document.createElement('a');
//        link.href = url;
//        document.body.appendChild(link);
//        link.click();
//    }

//        // All other browsers
//    else { window.location.href = url; }
//}
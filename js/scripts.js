/*----------------------------------------------------
' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
----------------------------------------------------*/

$(function() {
    //Smooth Scroll
    //---------------------------------------------------
    $('a[href*="#"]:not([href="#"])').click(function() {
        if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//, "") &&
            location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $("[name=" + this.hash.slice(1) + "]");
            if (target.length) {
                $("html, body").animate({
                        scrollTop: target.offset().top
                    },
                    1000);
                return false;
            }
        }
    });
    //---------------------------------------------------

    //Scroll to top
    //---------------------------------------------------
    $(document).ready(function() {
        $(window).scroll(function() {
            if ($(this).scrollTop() > 50) {
                $("#back-to-top").fadeIn();
            } else {
                $("#back-to-top").fadeOut();
            }
        });
        // scroll body to 0px on click
        $("#back-to-top").click(function() {
            $("#back-to-top").tooltip("hide");
            $("body,html").animate({
                    scrollTop: 0
                },
                1200);
            return false;
        });
        $("#back-to-top").tooltip("show");
    });
    //---------------------------------------------------

    //Init the wow animation
    //---------------------------------------------------
    new WOW().init();
    //---------------------------------------------------

    
});
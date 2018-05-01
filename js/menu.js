/*----------------------------------------------------
' Package: Facebook F8
' Version: 1.0
' Date Created: 23-04-2018
' Last Modified: 23-04-2018
' Authors: Rami Zebian <rami.zebian@lelabodigital.com>
----------------------------------------------------*/
(function() {
    "use strict";

    var fullscreenmenu = document.querySelector(".fullscreenmenu");

    var strokes = document.querySelectorAll(".strokes"),
        icon = document.querySelector(".menu"),
        fullscreenmenu = document.querySelector(".fullscreenmenu");

    function transformStart() {

        strokes[0].classList.toggle("animate0");
        strokes[1].classList.toggle("hide");
        strokes[2].classList.toggle("animate2");
        fullscreenmenu.classList.toggle("show");

    }

    icon.addEventListener("click", transformStart);

})();
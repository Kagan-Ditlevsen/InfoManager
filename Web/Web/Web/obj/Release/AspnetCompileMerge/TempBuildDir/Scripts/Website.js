/* Skd Admin Template */
window.addEventListener("resize", screenResize);
window.addEventListener("load", screenResize);
screen.orientation.addEventListener("change", function (e) { screenResize(); });

function screenResize() {
    let d = Skd.DeviceInformation();
    let topBtnHtml = "<span class='" + d.Icon + "'></span> " + d.Breakpoint.Short + " <span class='fas " + d.ScreenOrientationIcon + "'></span><br />" + d.X + "x" + d.Y;

    if (document.getElementById("skd-device-info") != null) {
        document.getElementById("skd-device-info").innerHTML = d.Html;
    }
    if (document.getElementById("skd-device-info-top") != null) {
        document.getElementById("skd-device-info-top").innerHTML = topBtnHtml;
    }
}
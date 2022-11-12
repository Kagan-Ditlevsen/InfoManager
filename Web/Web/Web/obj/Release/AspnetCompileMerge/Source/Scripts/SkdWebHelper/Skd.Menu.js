// title = "Skd Menu"
// version = 1.0
// date = 2021-09-16
// author = "Stig Kagan-Ditlevsen"

if (typeof SkdModule === 'undefined') {
    console.error("Skd.js is missing");
    console.warn("Skd.js is required by this function module to function correctly. Please include the \"Skd.js\" from your project directory. Every further error could derieve from this.");
} else {
    new SkdModule("Skd.Menu");

    window.addEventListener("load", function (e) {
        if (!_skd_menu_init_executed) {
            if (skd_debug) console.log("-- autorun");
            skd_menu_init();
        }
    });

    /* setup variables */
    if (typeof _skd_menu_init_executed === 'undefined') {
        var _skd_menu_init_executed = false;
    }
    if (typeof skd_menu_url === 'undefined') {
        var skd_menu_url = false;
    }
}

function skd_menu_init(forcedReload) {
    if (!forcedReload) { forcedReload = false; }
    if (_skd_menu_init_executed && forcedReload == false) {
        if (skd_debug) console.log("skd_menu_init() is already loaded");
        return;
    }
    if (skd_debug) console.log("skd_menu_init(forcedReload: " + forcedReload + ")");

    /***** Module specific *****/
    $(".skd-menu-item[data-url]:not([data-url=''])").on("click", function (e) {
        url = $(this).attr("data-url");

        // loading-message
        loadingText = "Fetching page...";
        var loadingTextAttr = $(this).attr("loading-message");
        if (typeof loadingTextAttr !== 'undefined' && loadingTextAttr !== "") {
            loadingText = loadingTextAttr;
        }

        Skd.PageLoading(loadingText, 500);
        location.href = url;
        return false;
    });

    _skd_menu_init_executed = true;
}
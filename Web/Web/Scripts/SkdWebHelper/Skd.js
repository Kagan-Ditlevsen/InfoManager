// #ToRead: https://stackoverflow.com/questions/48737886/how-to-define-a-public-property-in-the-javascript-class
/// 2021-09-10 07:20    1.00
///     Initial version
/// 2021-09-25 17:55
///     Added Device information
/// 2021-10-20
///     Added Skd.PageLoading
///     Added Skd.Message

if (typeof skd_debug === 'undefined') {
    var skd_debug = false;
}

class SkdModule {
    constructor(title, initFunction = "", author = "Stig Kagan-Ditlevsen", createDateTime = "2000-01-01 00:00") {

        // check for jQuery
        if (typeof jQuery === 'undefined') {
            console.error("jQuery (1.8 => *) is required to run the functionality of the Skd Library.");
        }

        this.Title = title;
        if (!initFunction || typeof initFunction === 'undefined' || initFunction === "") {
            this.InitFunction = title + ".Init";
        } else {
            this.InitFunction = initFunction;
        }
        this.Author = author;
        this.CreateDateTime = new Date(createDateTime);

        return this;
    }
}

class Skd {
    #modules = [];
    constructor() { }

    ModuleAdd(title, initFunction = "", author = "Stig Kagan-Ditlevsen", createDateTime = "2000-01-01 00:00") {
        // check if module already exists
        if (this.#modules.length > 0 && ModuleGet(title) === 'undefined') {
            if (skd_debug) console.warn("Module \"" + title + "\" are already loaded.");
            return;
        }

        if (skd_debug) console.log("Adding " + title);
        let m = new SkdModule(title, initFunction, author, createDateTime);
        this.#modules.push(m);

        return m;
    }

    ModuleGet(moduleName) {
        moduleName = moduleName.toLowerCase();
        if (skd_debug) console.log("searching for '" + moduleName + "' in the Skd.ModuleList. List contains " + this.#modules.length + " items.");
        for (let i = 0; i < this.#modules.length; i++) {
            if (skd_debug) console.log("searching through Skd.ModuleList: " + this.#modules[i].Title);
            if (modules[i].Title.toLowerCase() == moduleName.toLowerCase()) {
                return this.#modules[i];
                break;
            }
        }
    }

    static BootstrapBreakpoint() {
        this.Short;
        this.Name;
        this.Icon = "fas fa-exclamation-triangle";

        let width = window.innerWidth;
        switch (true) {
            case (width >= 1400):
                this.Short = "XXL";
                this.Name = "Large Desktop";
                this.Icon = "fas fa-desktop";
                break;
            case (width >= 1200):
                this.Short = "XL";
                this.Name = "Large Desktop";
                this.Icon = "fas fa-desktop";
                break;
            case (width >= 992):
                this.Short = "LG";
                this.Name = "Small Desktop";
                this.Icon = "fas fa-laptop";
                break;
            case (width >= 768):
                this.Short = "MD";
                this.Name = "Tablet";
                this.Icon = "fas fa-tablet-alt";
                break;
            case (width >= 576):
                this.Short = "SM";
                this.Name = "Phone";
                this.Icon = "fas fa-mobile-alt";
                break;
            case (width < 576):
                this.Short = "XS";
                this.Name = "Phone";
                this.Icon = "fas fa-mobile-alt";
                break;
        }
        return this;
    }

    static DeviceInformation() {
        if (skd_debug) console.warn("Initializing Skd.DeviceInformation()");

        this.isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
        this.isTablet = /(ipad|tablet|(android(?!.*mobile))|(windows(?!.*phone)(.*touch))|kindle|playbook|silk|(puffin(?!.*(IP|AP|WP))))/.test(navigator.userAgent.toLowerCase());
        this.isDesktop = !this.isMobile && !this.isTablet;
        this.isLandscape = window.matchMedia("(orientation: landscape)").matches;

        this.Breakpoint = this.BootstrapBreakpoint();
        this.Type = this.Breakpoint.Name;
        this.X = window.innerWidth;
        this.Y = window.innerHeight;
        this.ScreenOrientation = this.isLandscape ? "Landscape" : "Portrait";
        this.ScreenOrientationIcon = this.isLandscape ? "fa-arrows-alt-h" : "fa-arrows-alt-v";
        this.AgentIcon = "fas fa-exclamation-triangle";
        switch (true) {
            case this.isMobile:
                this.AgentIcon = "fas fa-mobile-alt";
                if (this.isLandscape) {
                    this.AgentIcon += " fa-rotate-90";
                }
                break;
            case this.isTablet:
                this.AgentIcon = "fas fa-tablet-alt";
                if (this.isLandscape) {
                    this.AgentIcon += " fa-rotate-90";
                }
                break;
            case this.isDesktop:
                if (this.X <= 992) {
                    this.AgentIcon = "fas fa-laptop";
                } else {
                    this.AgentIcon = "fas fa-desktop";
                }
                break;
        };
        this.Icon = this.Breakpoint.Icon;

        this.Title = this.X + "x" + this.Y + " " + this.Type + " (" + this.Breakpoint.Short + ") as " + (this.isLandscape ? "landscaped" : "portraited") + ".";
        this.Html = "<skd title='" + this.Title + "'>" + this.X + "x" + this.Y + " <span class='" + this.Icon + "'></span> " + this.Breakpoint.Short + "</skd>";

        return this;
    }
}

/* ContentLoading */
var _skd_contentLoading_html = `<div id="skd_contentloader_template"><img class="skd-content-loader" src="/Content/Images/loader-circle.gif" /></div>`;
Skd.ContentLoading = function (elm) {
    if ($("#skd_contentloader_template").length == 0) {
        $("body").prepend(_skd_contentLoading_html);
    }
    elm = AsJQuery(elm);
    elm.addClass("skd-content-loader-show");
    let temp = $("#skd_contentloader_template").clone();

    elm.prepend(temp.children());

    return elm.children(0);
}
Skd.ContentLoadingHide = function (elm) {
    if (!elm) {
        $(".skd-content-loader").each(function (e) {
            if ($(this).parent().attr("id") != "skd_contentloader_template") {
                $(this).parent().removeClass("skd-content-loader-show");
                $(this).remove();
            }
        });
    } else {
        elm = AsJQuery(elm);
        elm.find(".skd-content-loader").each(function (e) {
            if ($(this).parent().attr("id") != "skd_contentloader_template") {
                $(this).parent().removeClass("skd-content-loader-show");
                $(this).remove();
            }
        });
    }
}
/* PageLoading */
new SkdModule("Skd.PageLoading");

var _skd_pageLoadingShown = false;
var _skd_pageLoadingTimer = false;
var _skd_pageLoading_html = "<div id=\"skd_pageLoading\" style=\"display: none!important;\"><div></div><div><span class=\"fas fa-spinner fa-5x fa-spin\"></span><p>Loading content...</p></div></div>";

Skd.PageLoading = function (text = "Loading content...", miliseconds = 500) {
    if ($("#skd_pageLoading").length == 0) {
        $("body").prepend(_skd_pageLoading_html);
    }

    if (_skd_pageLoadingShown) {
        return;
    }

    _skd_pageLoadingShown = true;
    _skd_pageLoadingTimer = setTimeout(
        function () {
            $("#skd_pageLoading p").html(text);
            $("#skd_pageLoading").show();
        }, miliseconds);
}

Skd.PageLoadingHide = function () {
    clearTimeout(_skd_pageLoadingTimer);
    _skd_pageLoadingShown = false;
    $("#skd_pageLoading").hide();
}

/* Message */
Skd.Message = new SkdModule("Skd.Message", null, null, new Date(2021, 06, 01));

var _skd_message_container = "<div id=\"skd-message-container\" class=\"toast-container position-fixed end-0 p-3\"></div>";
var _skd_message_template = "<div data-bs-delay='*msg-delay*' id=\"skd-message-*id*\" class=\"skd-message toast\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\"><div class=\"toast-header *type*\"><span class=\"fas fa-*icon*\"></span><strong class=\"ms-2 me-auto\">*text*</strong><small class=\"text-nowarp\">*createDT*</small><button type=\"button\" class=\"btn-close ms-1\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button></div><div class=\"toast-body\">*subtext*</div></div>";
var _skd_message_delay = 5000;
//_skd_message_delay = 50000000;
var _skd_message_deleteOnDelay = true;
Skd.Message.Show = function () {
    $("#skd-message-container .toast").each(function (e) {
        var elm = document.getElementById($(this).attr("id"));
        var msg = new bootstrap.Toast(elm);
        msg.show();
        if (_skd_message_deleteOnDelay) {
            elm.addEventListener('hidden.bs.toast', function () { // if one is removed, all is removed
                $.each(AsJQuery(elm.id).parent(), function () {
                    $(this).remove();
                })
                //elm.remove();
            })
        }
    });
    return false;
}
Skd.Message.Clear = function () {
    $(".skd-message").remove();
}
Skd.Message.Delete = function (msgId) {
    $(msgId).remove();
}
Skd.Message.Add = function (text, type = "info", subText = "", id = Guid()) {
    let html = _skd_message_template;
    let icon = "";
    let typeText = type.toLowerCase();

    switch (type.toUpperCase()) {
        case "INFO":
            icon = "info";
            typeText = "bg-info text-black";
            break;
        case "WARNING":
            icon = "exclamation-triangle";
            typeText = "bg-warning text-black";
            break;
        case "ERROR":
            icon = "bomb";
            typeText = "bg-danger text-black";
            break;
        case "SUCCESS":
            icon = "check";
            typeText = "bg-success text-light";
            break;
        case "DARK":
            icon = "info";
            typeText = "bg-dark text-white";
            break;
        default:
            icon = "info";
            typeText = "bg-dark text-white";
            if (subText != "") {
                subText += "<br />";
            }
            subText += "<small>Message.Type <code>" + type.toUpperCase() + "</code> where not recognized.";
            break;
    }

    html = html.replace("*type*", typeText);
    html = html.replace("*icon*", icon);
    html = html.replace("*text*", text);
    html = html.replace("*subtext*", (subText == "" ? text : subText));
    html = html.replace("*id*", id);
    html = html.replace("*createDT*", new Date().Format("HH:mm:ss"));

    html = html.replace("*msg-delay*", _skd_message_delay);

    if ($("#skd-message-container").length == 0) { // add message container if not existing
        $("body").prepend(_skd_message_container);
    }

    $("#skd-message-container").append(html);
}

/********************************************** Skd - Common Events */
Skd.SetEvents = function (e) {
    /* skd-sidebar */
    $("[skd-sidebar-url]:not([skd-sidebar-url=''])").off("click").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();

        if ($("[skd-sidebar]").hasClass("show") && $(this).attr("data-sidebar-url") == skd_sidebar_url) {
            return skd_sidebar_close();
        }
        skd_sidebar_load_content($(this).attr("skd-sidebar-url"), $(this).attr("skd-sidebar-data"));

        $(this).blur();
    });
    $("[skd-sidebar] .nav a").on("click", function (e) {
        skd_sidebar_set_active_tab(this);
    });
    /* skd-sidebar END */
}
/********************************************** Skd - Common Events END */

/* sidebar fnc */
var _skd_sidebar_modal_html = "<div class='skd-sidebar-modal' onclick='return skd_sidebar_close(); ' style='display: block;'></div>";
function skd_sidebar_set_active_tab(tabId) {
    $("[skd-sidebar] .nav a").removeClass("active");
    $("[skd-sidebar] .skd-tab").hide();

    $(tabId).addClass("active");
    $("[skd-sidebar] #sidebarTab" + $(tabId).attr("tab-id")).show();
}
function skd_sidebar_load_content(url, inputData) {
    Skd.PageLoading("Fetching sidebar content...", 250);
    if ($(".skd-sidebar-modal").length == 0) {
        $("body").prepend(_skd_sidebar_modal_html)
    }

    $("body").css("--skd-sidebar-width", "");
    $("[skd-sidebar]").removeClass("show");
    $(".skd-sidebar-modal").hide();

    $.ajax({
        url: url,
        method: "POST",
        data: inputData
    })
        .done(function (response) {
            $("[skd-sidebar]").addClass("show");
            $(".skd-sidebar-modal").show();
            skd_sidebar_url = url;
            $("[skd-sidebar]").html(response);
        })
        .always(function () {
            Skd.PageLoadingHide();
        })
        .fail(function (jqXHR, textStatus) {
        });
}

function skd_sidebar_setWidth(width) {
    $("body").css("--skd-sidebar-width", width + "px");
}

function skd_sidebar_close(confirmClose) {
    if (!confirmClose) confirmClose = false;

    if (confirmClose) {
        if (!confirm("Are you sure you want to close the sidebar. ALL data you may have entered will be lost.")) {
            return false;
        }
    }

    skd_sidebar_url = "";
    $("[skd-sidebar]").html("Loading content...").removeClass("show");
    $("[skd-sidebar] .tabs").html("");
    $(".skd-sidebar-modal").hide();
    $("body").css("--skd-sidebar-width", "200px");
    return false;
}
/* sidebar fnc END */

/* regex */
var regExISODate = /^([\+-]?\d{4}(?!\d{2}\b))((-?)((0[1-9]|1[0-2])(\3([12]\d|0[1-9]|3[01]))?|W([0-4]\d|5[0-2])(-?[1-7])?|(00[1-9]|0[1-9]\d|[12]\d{2}|3([0-5]\d|6[1-6])))([T\s]((([01]\d|2[0-3])((:?)[0-5]\d)?|24\:?00)([\.,]\d+(?!:))?)?(\17[0-5]\d([\.,]\d+)?)?([zZ]|([\+-])([01]\d|2[0-3]):?([0-5]\d)?)?)?)?$/;
var regExTime24leadingZero = /^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/;
var regExNewLine = /(\r\n|\r|\n)/;
/* regex END */
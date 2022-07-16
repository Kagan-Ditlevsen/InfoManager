// title = "Skd JavascriptExtend"
// version = 1.0
// date = ????-??-??
// author = "Stig Kagan-Ditlevsen"

if (typeof SkdModule !== 'undefined') {
    new SkdModule("Skd.JavascriptExtend");
}

/***************** jQuery extension */
Window.prototype.AsJQuery = function (obj) {
    if (obj instanceof jQuery) {
        if (skd_debug) console.log("AsJquery: Already jQuery");
    } else {
        if (obj.indexOf("#") != 0) {
            if (skd_debug) console.log("AsJquery: Adding #");
            obj = "#" + obj;
        }
        if (skd_debug) console.log("AsJquery: Transform to jQuery: " + obj);
        obj = $(obj);
    }
    return obj;
}
$.prototype.hasAttr = function (attrName) {
    var attr = $(this).attr(attrName);
    return (typeof attr !== 'undefined' && attr !== false);
}
/* Content Spinner */
$.prototype.SkdContentSpinner = function () {
    $.each(this, function (e) {
        //let oldContent = $(this).html();
        if ($(this).find(".skd-content-spinner-content").length == 0) {
            // add placeholder
            newContent = "<span class='fas fa-spinner fa-spin'></span><span style='display: none;'>" + $(this).html() + "</span>";
        }

        $(this).addClass("skd-content-spinner").width($(this).width()).height($(this).height()).html(newContent).attr("disabled", "disabled");
    });
    return $(this);
}
$.prototype.SkdContentSpinnerReset = function (resetAll = false) {
    let elm = this;
    if (resetAll) {
        elm = $(".skd-content-spinner");
    }

    $.each(elm, function (e) {

        //$(this).children(1).remove();
        //let oldContent = $(this).html().substring(4);
        //oldContent = oldContent.substring(0, oldContent.length - 6);

        //$(this).html(oldContent).width("").height("").attr("disabled", "");

        //$(this);
        $(this).width("").height("").removeAttr("disabled").html($(this).find("span:nth-child(2)").html()).removeClass("skd-content-spinner");
    });
    return $(this);
}

/***************** QueryString */
Window.prototype.GetQueryStringValue = function (name = "", defaultValue = "") {
    if (name == "") return defaultValue;

    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
    results = regex.exec(window.location.search);
    if (results != null && typeof results[2] !== "undefined") {
        return results[2];
    } else {
        return defaultValue;
    }
}

// inspired from: https://stackoverflow.com/questions/901115/how-can-i-get-query-string-values-in-javascript
Window.prototype.QueryString = function (name = "", value, queryString = window.location.search) {
    if (name == "") {
        return queryString;
    }
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
    results = regex.exec(queryString);

    if (results && value == "") {
        if (skd_debug) console.warn("removing key");
        queryString = queryString.replace(name + "=" + results[2], "");
        queryString = queryString.replace("?&", "?").replace("&&", "&").replace("??", "").replace("&&", "");
        return queryString;
    }
    if (results && results[2] && !value) { // key found and not to be set
        if (skd_debug) console.warn("return-value");
        return results[2];
    }
    if (!results && value == "") { // not found
        if (skd_debug) console.warn("found, removing key");
        return queryString;
    }
    if (!results && !value) { // not found
        if (skd_debug) console.warn("not-found");
        return null;
    }
    if (results && results[2]) { // found and setting
        if (skd_debug) console.warn("found-setting");
        return queryString.replace(name + "=" + results[2], name + "=" + value);
    }

    // not found and setting value
    if (skd_debug) console.warn("not found and setting value");
    if (!queryString.startsWith("?")) {
        queryString = "?";
    } else {
        queryString += "&";
    }
    queryString += name + "=" + value;

    queryString = queryString.replace("?#", "").replace("&#", "");
    if (queryString.endsWith("?") || queryString.endsWith("&")) {
        queryString = queryString.substring(0, queryString.length - 2);
    }

    return decodeURIComponent(queryString);
}
/***************** QueryString END */

/***************** Ajax */
/* NOT COMPLETE */
SkdAjaxFormPost = function (url, data, postIntoElement, returnDataType = "html", async = false) {
    $.ajax({
        async: async,
        method: "POST",
        cache: false,
        url: url,
        data: data,
        dataType: returnDataType,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data, status, xhr) {
            if (skd_debug) console.log("SkdAjaxFormPost: success");
            //Skd.PageLoadingHide();

            if (postIntoElement) {
                postIntoElement.html(data);
            }

            return data;
        },
        error: function (xhr, status, error) {
            //
        },
        complete: function () {
            if (skd_debug) console.log("SkdAjaxFormPost: complete");
            //Skd.PageLoadingHide();
        }
    });
}
/***************** Ajax END */

/***************** RegEx */
/***************** RegEx END */

/***************** String */
Window.prototype.LeadingZero = function (val, qty) {
    if (!qty) {
        qty = 2;
    }
    tmp = "0".repeat(qty) + val;
    return tmp.substr(tmp.length - qty, qty);
}

// Author: https://www.tutorialspoint.com/how-to-create-guid-uuid-in-javascript
Window.prototype.Guid = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

/***************** Date */
Date.prototype.DateTimeStringIso = function () {
    return this.Format("yyyy-MM-ddTHH:mm:ss.fffZ");
}

Date.prototype.DateString = function () {
    var month = LeadingZero(this.getMonth() + 1);
    var day = LeadingZero(this.getDate());

    return [
        this.getFullYear(), month, day].join('-');
};
Date.prototype.DateTimeString = function () {
    return this.Format();
};
Date.prototype.TimeString = function () {
    var hour = LeadingZero(this.getHours());
    var minute = LeadingZero(this.getMinutes());
    var second = LeadingZero(this.getSeconds());

    return [hour, minute, second].join(':');
};
Date.prototype.Format = function (format) {
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss.ffff";
    }

    var year = this.getFullYear();
    var month = LeadingZero(this.getMonth() + 1);
    var day = LeadingZero(this.getDate());
    var hour = LeadingZero(this.getHours());
    var minute = LeadingZero(this.getMinutes());
    var second = LeadingZero(this.getSeconds());
    var millisecond = this.getMilliseconds();

    return format
        .replace(/yyyy/gi, year)
        .replace(/MM/g, month)
        .replace(/dd/gi, day)
        .replace(/HH/gi, hour)
        .replace(/hh/gi, this.getHours())
        .replace(/mm/g, minute)
        .replace(/ss/gi, second)
        .replace(/ffff/gi, (this.getMilliseconds() + "0000").substring(0, 4))
        .replace(/fff/gi, (this.getMilliseconds() + "000").substring(0, 3))
        .replace(/f/gi, millisecond)
    ;
};

/***************** Other */
/* #TODO Get CSS styles/rules: https://stackoverflow.com/questions/64251663/document-stylesheets-get-all-styles-on-an-element-with-pseudo-selector */

$(function (e) {
    /* Highlight rows */
    if (typeof highlightRowIds !== 'undefined') {
        $.each(highlightRowIds, function (index, obj) {
            var blinkCss = $("[data-id='" + obj + "']").attr("skd-blink");
            if (!blinkCss || blinkCss == "") {
                blinkCss = "green";
            }
            $("[data-id='" + obj + "']").addClass("blink-" + blinkCss);
            window.setTimeout(function (e) {
                $("[data-id='" + obj + "']").removeClass("blink-" + blinkCss);
            }, 5100);
        });
    }
});

/***************** NEW_AREA */
/***************** NEW_AREA END */
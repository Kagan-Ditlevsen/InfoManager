// title = "Skd Form"
// version = 1.0
// date = 2021-10-10 01:35
// author = "Stig Kagan-Ditlevsen"

if (typeof Skd === 'undefined') {
    console.error("Skd.js is missing");
    console.warn("Skd.js is required by this function module to function correctly. Please include the \"Skd.js\" from your project directory. Every further error could derieve from this.");
}

class SkdForm_Class {
    #_init = false;

    constructor() {
        new SkdModule("Skd.Form");

        this.Init();
    }

    Init(forceInit) {
        if (!forceInit) { forceInit = false; }
        if (this.#_init && forceInit == false) {
            if (skd_debug) console.log("Skd.Form.Init() is already loaded");
            return;
        }
        this.#_init = true;
        if (skd_debug) console.warn("Skd.Form.Init(forceInit: " + forceInit + ")");

        /* debug formular */
        if ($("#skd_debug_posted_values").length == 0) {
            $("body").prepend("<div id=\"skd_debug_posted_values\"></div>");
        }
        /* debug formular END */

        /* dropdown-customtext */
        $("[skd-cmd='dropdown-customtext']").on("blur", function (e) {
            let id = $(this).closest("[id$=_container]").attr("id").replace("_container", "");
            $("#" + id + "_text").val($(this).find("option[value='" + $(this).val() + "']").text());
        });
        $("[skd-cmd='dropdown-customtext-btn']").on("click", function (e) {
            let id = $(this).closest("[id$=_container]").attr("id").replace("_container", "");
            let elm = $("#" + id);
            let customText = prompt(elm.attr("data-prompt"), elm.find("option[value='-1']").first().text());

            if (customText == "") {
                elm.find("option[value='-1']").remove();
            } else if (!customText) { // cancel were pressed - do nothing
                return false;
            } else {
                elm.find("option[value='-1']").remove();
                elm.find("option").removeAttr("selected").removeClass("active");

                let search = elm.find("option").text().toLowerCase().includes(customText.toLowerCase());
                let searchResult = elm.find("option[value!='-1'][value!='']").filter(function () { if ($(this).val() == "*") { return false; } return $(this).text().toLowerCase().includes(customText); });
                if (searchResult.length > 0) {
                    if (searchResult.length == 1) {
                        let searchText = prompt(
                            "Found a result matching your custom text. Would you like to use this istead?",
                            searchResult.first().text()
                        );

                        if (searchText && searchText != customText) {
                            searchResult.first().attr("selected", "selected");
                            return false;
                        }
                    } else {
                        if (confirm("Multible existing values containing your custom text exists. Perhaps the item already exists?\n\nContinue with you custom text - or - narrow down your search - or - find the item in the dropdown list\n\nWould you like to re-search [Ok] or use your custom text [Cancel] ?")) {
                            console.error("redo search");
                        }
                    }
                }

                elm.prepend("<option value='-1' selected='selected'>" + customText + "</option>");
                $("#" + id + "_text").val(customText);
            }
            skd_form_valid($(this).closest("form"));
            return false;
        })
        /* dropdown-customtext END */

        // jQuery defaults: https://jqueryvalidation.org/category/plugin/
        $.validator.setDefaults({
            //debug: true,
            validClass: "is-valid",
            errorClass: "is-invalid",
            highlight: function (element, errorClass, validClass) {
                $(element).addClass(errorClass).removeClass(validClass);
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass(errorClass).addClass(validClass);
            },
            invalidHandler: function (event, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    if (validator.errorList.length > 0) {
                        for (let x = 0; x < validator.errorList.length; x++) {
                            let elm = AsJQuery(validator.errorList[x].element.id);
                            if (skd_debug) console.log("error: " + elm.attr("id"));
                            $(validator.errorList[x].element.id).addClass("is-invalid");
                        }
                    }
                }
            }
        });
        $.validator.addMethod("formDate", function (value, element) {
            if (!$(element).hasAttr("required") && value == "") {
                return $(element).is("input[type=date]");
            }

            return ($(element).is("input[type=date]") && regExISODate.test(value));
        }, "This value does not match the ISO8601 form specification");
        $.validator.addMethod("formTime", function (value, element) {
            if (!$(element).hasAttr("required") && value == "") {
                return $(element).is("input[type=time]");
            }

            return ($(element).is("input[type=time]") && regExTime24leadingZero.test(value));
        }, "This value does not match the 00:00 to 23:59 specification");
        var rulesOriginal = $.prototype.rules;
        $.prototype.rules = function () {
            if (arguments.length === 0 && this.is("input[type=date]")) {
                var originalRules = rulesOriginal.apply(this);
                delete originalRules["date"];
                originalRules["formDate"] = true;

                return originalRules;
            }
            if (arguments.length === 0 && this.is("input[type=time]")) {
                var originalRules = rulesOriginal.apply(this);
                delete originalRules["time"];
                originalRules["formTime"] = true;

                return originalRules;
            }

            return rulesOriginal.apply(this, arguments);
        }
        jQuery.extend(jQuery.validator.messages, {
            hidden: "Are you kidding?!?",
            required: "This field is required.",
            remote: "Please fix this field.",
            email: "Please enter a valid email address.",
            url: "Please enter a valid URL.",
            date: "[Obsolete] Please enter a valid date.",
            dateISO: "Please enter a valid date (ISO).",
            number: "Please enter a valid number.",
            digits: "Please enter only digits.",
            creditcard: "Please enter a valid credit card number.",
            equalTo: "Please enter the same value again.",
            accept: "Please enter a value with a valid extension.",
            maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
            minlength: jQuery.validator.format("Please enter at least {0} characters."),
            rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
            range: jQuery.validator.format("Please enter a value between {0} and {1}."),
            max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
            min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
        });
        /* jQuery END */

        $(".skd-form button").attr("tabindex", "-1");
        $(".skd-form button:not([type=submit]").on("click", function (e) { /* prevent unintended submit */
            return false;
        });
        $(".skd-form .form-control, skd-form .form-select").on("change", function (e) {
            let frm = $(this).closest("form");
            $("#" + frm.attr("id") + " .form-control[disabled]:not([disabled=''])").removeClass("valid error");
            skd_form_valid($(frm));
        });

        /* datetime event */
        $("[skd-cmd='datetime_clear']").off("click").on("click", function (e) {
            let id = $(this).closest("[id*=_container]").attr("id")
            id = id.substring(0, id.lastIndexOf("_"));
            if (skd_debug) console.log("datetime_clear");
            $(this).blur();

            $("#" + id).val("");
            $("#" + id + "_datetime").val("");
            $("#" + id + "_date").val("");
            $("#" + id + "_date_second").val("");
            $("#" + id + "_time").val("");
            $("#" + id + "_time_second").val("");
            skd_form_valid(id);

            return false;
        })
            .attr("tabindex", "-1");
        $("[skd-cmd='datetime_set_now']").off("click").on("click", function (e) {
            let id = $(this).closest("[id*=_container]").attr("id")
            id = id.substring(0, id.lastIndexOf("_"));
            let ldt = new Date().DateTimeStringIso();
            if (skd_debug) console.log("datetime_set_now");
            $(this).blur();

            $("#" + id).val(ldt.substr(0, 16).replace("T", " "));
            $("#" + id + "_datetime").val(ldt.substr(0, 16));
            $("#" + id + "_date").val(ldt.substr(0, 10));
            $("#" + id + "_date_second").val(ldt.substr(0, 10));
            $("#" + id + "_time").val(ldt.substr(12, 16));
            $("#" + id + "_time_second").val(ldt.substr(12, 16));
            skd_form_valid(id);

            return false;
        })
            .attr("tabindex", "-1");
        $("[type=date], [type=time]").off("blur").on("blur", function (e) {
            if ($(this).parent().parent().attr("control-type") == "Skd.Web.Form+DateTime.DateDate"
                || $(this).parent().parent().attr("control-type") == "Skd.Web.Form+DateTime.TimeTime"
                || $(this).parent().parent().attr("control-type") == "Skd.Web.Form+DateTime.DateTimeTime") {
                return false;
            }
            skd_datetime_setValue($(this));
            return false;
        });
        $("[type=datetime-local]").off("blur").on("blur", function (e) {
            let id = $(this).attr("id").substring(0, $(this).attr("id").lastIndexOf("_"));
            $("#" + id).val($(this).val().replace("T", " "));
        });
        $("[type=date]").off("keydown").on("keydown", function (e) {
            if (e.which == 9) return true; // tab
            let dt = new Date($(this).val());
            if (String.fromCharCode(e.which) == "C") {
                $(this).val("");
                return true;
            } else if (String.fromCharCode(e.which) == "N") { dt = new Date(); }
            else if (e.which == 187 || e.which == 107) { dt.setDate(dt.getDate() + 1); }
            else if (e.which == 189 || e.which == 109) { dt.setDate(dt.getDate() - 1); }
            else {
                return true;
            }

            $(this).val(dt.Format("yyyy-MM-dd"));
            return true;
        });
        $("[type=time]").off("keydown").on("keydown", function (e) {
            if (e.which == 9) return true; // tab
            let dt = new Date("2000-01-01 " + $(this).val());
            if (String.fromCharCode(e.which) == "C") {
                $(this).val("");
                return true;
            } else if (String.fromCharCode(e.which) == "N") { dt = new Date(); }
            else if (e.which == 187 || e.which == 107) { dt.setTime(dt.getTime() + (1000 * 60)); }
            else if (e.which == 189 || e.which == 109) { dt.setTime(dt.getTime() - 1000 * 60); }
            else {
                return true;
            }

            $(this).val(dt.Format("HH:mm"));
            return true;
        })
        /* datetime event END */

        /* dropdown */
        $("[skd-cmd=dropdown]").off("change").on("change", function (e) {
            let id = $(this).attr("id");
            $("#" + id + "_text").val($("#" + id + " option:selected").text());
            $(this).find("option").removeClass("active");
            $(this).find("option:selected").addClass("active");

            let frm = $(this).closest("form");
            skd_form_valid($(frm));
            return false;
        });
        /* dropdown END */

        /* skd-search */
        $("[skd-cmd='search']").off("keyup").on("keyup", function (e) { skd_search($(this), e); });

        /* button-toggle */
        $("[skd-cmd=button-toggle]").off("click").on("click", function (e) {
            let id = $(this).attr("id");
            let on = $(this).attr("skd-on");
            let onCss = $(this).attr("skd-on-css");
            let off = $(this).attr("skd-off");
            let offCss = $(this).attr("skd-off-css");
            let isTrue = $(this).val() != "true"; // backward logic if == "true", then true BEFORE click. Should therefore be != "true"
            let elmHtml = "";

            let paddingSize = $(this).hasClass("btn-sm") ? 1 : $(this).hasClass("btn-lg") ? 3 : 2
            if (isTrue) {
                elmHtml = "<div class='d-flex flex-row align-items-center'><span class='col-auto p-0 fas " + $(this).attr("skd-on-icon") + "'></span><span class='col'>" + on + "</span></div>";
            } else {
                elmHtml = "<div class='d-flex flex-row align-items-center'><span class='col'>" + off + "</span><span class='col-auto p-0 fas " + $(this).attr("skd-off-icon") + "'></span></div>";
            }
            $(this).html(elmHtml);
            $(this).toggleClass(onCss).toggleClass(offCss).val(isTrue);
            $("#" + id.substring(0, id.lastIndexOf("_"))).val(isTrue);

            return false;
        });

        this.#_init = true;

        return {
            IsInitialized: this.#_init
        };
    }

    IsInitialized() { return this.#_init; }

    GetId(elm) {
        elm = AsJQuery(elm);
        let id = elm.closest("[id$=_container]").attr("id");
        id = id.substring(0, id.lastIndexOf("_"));

        let rtn = {};
        rtn["id"] = $("#" + id);
        $("[id^=" + id + "_]").each(function (e) {
            let name = $(this).attr("id").substring($(this).attr("id").lastIndexOf("_") + 1);
            rtn[name] = $(this);
        });

        return rtn;
    }

    ShowHiddenFields(id = "") {
        let orgId = id;
        if (id === "") {
            id = $("body");
            orgId = "body";
        } else {
            id = AsJQuery(id);
            orgId = id.attr("id");
        }

        let elms = id.find("input[type=hidden]");
        elms.attr("type", "text").addClass("form-control form-control-sm me-2").css("max-width", "100px");

        return {
            "Original Id": orgId, "jQuery element": id, "Affected elements": elms
        };
    }
}
Skd.Form = new SkdForm_Class();

class SkdForm_Button_Confirm_Class {
    interval;
    confirmSeconds = 3;

    constructor() {
        this.Init();
    }

    Init() {
        $("[skd-cmd=button-confirm]").off("click").on("click", function (e) {
            let elm = $(this);

            if (elm.hasClass("confirm")) {
                // action is confirmed
                let elmData = Skd.Form.ButtonConfirm.Paint(elm);

                window.clearInterval(Skd.Form.ButtonConfirm.interval);
                elm
                    .html(elmData.Html)
                    .toggleClass(elmData.Background + " " + elmData.ConfirmBackground)
                    .removeClass("confirm")
                    .addClass("confirmed");
                if (elm.attr("type") == "submit") {
                    elm.attr("disabled", true);
                }

                eval($(elm).attr("skd-cmd-data"));
            } else {
                if (elm.attr("type") == "submit" && !skd_form_valid(elm)) {
                    alert("form is not valid, so impossible to confirm");
                    return false;
                }

                let elmData = Skd.Form.ButtonConfirm.Paint(elm);
                // start count down
                let confirmCounter = Skd.Form.ButtonConfirm.confirmSeconds;
                elm
                    .html(elmData.ConfirmHtml.replace("*counter*", Number(confirmCounter)))
                    .toggleClass(elmData.Background + " " + elmData.ConfirmBackground)
                    .addClass("confirm");

                window.clearInterval(Skd.Form.ButtonConfirm.interval);
                Skd.Form.ButtonConfirm.interval = window.setInterval(function (e) {
                    confirmCounter--;
                    elm.html(elmData.ConfirmHtml.replace("*counter*", Number(confirmCounter)))
                    if (confirmCounter <= 0) {
                        window.clearInterval(Skd.Form.ButtonConfirm.interval);
                        elm
                            .html(elmData.ConfirmHtml.replace("*counter*", Number(confirmCounter)))
                            .toggleClass(elmData.Background + " " + elmData.ConfirmBackground)
                            .removeClass("confirm")
                            .attr("disabled", false);
                        elm.blur();
                        Skd.Form.ButtonConfirm.Reset(elm);
                    }
                }, 1000);
            }
            return false;
        });
    }

    Reset = function (elm) {
        elm.each(function (e) {
            let elmData = Skd.Form.ButtonConfirm.Paint($(this));
            $(this).html(elmData.Html);
        })
    }

    Paint = function (elm, state = "normal") {
        let elmIcon = elm.attr("skd-icon");
        let elmIconLocation = elm.attr("skd-icon-location");
        let elmPadding = elm.hasClass("btn-sm") ? 1 : elm.hasClass("btn-lg") ? 3 : 2;
        let elmHtml = elm.attr("skd-text");
        let elmConfirmHtml = elm.attr("skd-confirm-text") + "&nbsp;(*counter*)";
        if (elmIconLocation == "right") {
            elmHtml = "<div class='d-flex flex-row align-items-center'><span class='col p-0 pe-" + elmPadding + "'>" + elmHtml + "</span><span class='col-auto p-0 fas " + elmIcon + "'></span></div>";
            elmConfirmHtml = "<div class='d-flex flex-row align-items-center'><span class='col p-0 pe-" + elmPadding + "'>" + elmConfirmHtml + "</span><span class='col-auto p-0 fas " + elm.attr("skd-confirm-icon") + "'></span></div>";
        } else if (elmIconLocation == "left") {
            elmHtml = "<div class='d-flex flex-row align-items-center'><span class='col-auto p-0 fas " + elmIcon + "'></span><span class='col p-0 ps-" + elmPadding + "'>" + elmHtml + "</span></div>";
            elmConfirmHtml = "<div class='d-flex flex-row align-items-center'><span class='col-auto p-0 fas " + elm.attr("skd-confirm-icon") + "'></span><span class='col p-0 ps-" + elmPadding + "'>" + elmConfirmHtml + "</span></div>";
        }
        let elmBackground = "btn-" + elm.attr("skd-background");
        let elmConfirmBackground = "btn-" + elm.attr("skd-confirm-background");

        return {
            Html: elmHtml,
            ConfirmHtml: elmConfirmHtml,
            Background: elmBackground,
            ConfirmBackground: elmConfirmBackground
        };
    }
}
Skd.Form.ButtonConfirm = new SkdForm_Button_Confirm_Class();

class SkdForm_Dropdown_Class {
    timeout;

    constructor() {
        this.Init();

        $("form").each(function (e) { $(this).validate(); });
    }
    Init() {
        $.validator.addMethod("formDropDownAdv", function (value, element) {
            if (!$(element).hasAttr("required") && value == "") {
                return $(element).is("input[skd-cmd=dropdownadv]");
            }

            if ($(element).hasAttr("required")) {
                let ids = Skd.Form.GetId($(element));
                if ($(element).attr("skd-multible") == "true" && ids.id.val() == "") {
                    return false;
                }
                if ($(element).attr("skd-select-only") == "true" && ids.id.val() == "") {
                    return false;
                }
            }

            return $(element).is("input[skd-cmd=dropdownadv]");
        }, "You are required to select an item from the provided list. Custom text is not allowed.");

        var rulesOriginal = $.prototype.rules;
        $.prototype.rules = function () {
            if (arguments.length === 0 && this.is("input[skd-cmd=dropdownadv]")) {
                var originalRules = rulesOriginal.apply(this);
                originalRules["formDropDownAdv"] = true;

                return originalRules;
            }

            return rulesOriginal.apply(this, arguments);
        }

        $("[skd-cmd=dropdownadv]").off("focus").on("focus", function (e) {
            if ($(this).val() != "") {
                Skd.Form.DropDown.Search($(this), $(this).val());
            }
            Skd.Form.DropDown.Show($(this), true);
        });
        $("[skd-cmd=dropdownadv]").off("blur").on("blur", function (e) {
            let ids = Skd.Form.GetId($(this));
            let callerElm = $(this);
            let idText = ids.text.val();

            Skd.Form.DropDown.timeout = window.setTimeout(function (e) {
                // find out if text exists in list and replace values if it do
                ids.list.find(".skd-table-row").each(function (e) {
                    if (idText.replace(/[\s\,\.\']/gm, "").toLowerCase() == $(this).attr("data-text").replace(/[\s\,\.\']/gm, "").toLowerCase()) {
                        ids.id.val($(this).attr("data-value"));
                        ids.text.val($(this).attr("data-text"));
                    }
                });

                skd_form_valid(callerElm);

                Skd.Form.DropDown.Hide(callerElm);
            }, 500);
        });
        $("[skd-cmd=dropdownadv-save-custom]").off("click").on("click", function (e) {
            let ids = Skd.Form.GetId($(this));

            ids.list.find(".skd-table-row").removeClass("active");
            $(ids.id).val("");
            Skd.Form.DropDown.Hide($(this));
            $(this).blur();
            skd_form_valid($(this));
            return false;
        });
        $("[skd-cmd=dropdownadv-clear]").off("click").on("click", function (e) {
            return Skd.Form.DropDown.Clear($(this));
        });
        $("[skd-cmd=dropdownadv-toggle]").off("click").on("click", function (e) {
            return Skd.Form.DropDown.Show($(this));
        });
        $("[skd-cmd=dropdownadv-list] .skd-table-row").off("click").on("click", function (e) {
            let ids = Skd.Form.GetId($(this));

            if (ids.text.attr("skd-multible") == "true") {
                window.clearInterval(Skd.Form.DropDown.timeout);
                $(this).find("[type=checkbox]").prop("checked", !$(this).find("[type=checkbox]").prop("checked"));
                let count = ids.list.find("[type=checkbox]:checked").length;
                if (count == 0) {
                    ids.text.val("");
                } else if (count == 1) {
                    ids.text.val(ids.list.find("[type=checkbox]:checked").first().closest(".skd-table-row").attr("data-text"));
                } else {
                    ids.text.val("Multible values (" + count + ") ...");
                }
                let idValue = "";
                ids.list.find("[type=checkbox]:checked").each(function (e) {
                    idValue += $(this).val() + ",";
                });
                if (idValue.endsWith(",")) {
                    idValue = idValue.substring(0, idValue.lastIndexOf(","));
                }
                ids.id.val(idValue);
                skd_form_valid($(this));
            } else {
                ids.id.val($(this).attr("data-value"));
                ids.text.val($(this).attr("data-text"));
                ids.list.find(".skd-table-row").removeClass("active");
                $(this).addClass("active");
                Skd.Form.DropDown.Hide($(this));
                skd_form_valid($(this));
            }

            return true;
        });
        $("[skd-cmd=dropdownadv]").off("keyup").on("keyup", function (e) {
            if (e.keyCode == 27 || $(this).val() == "") {
                return Skd.Form.DropDown.Clear($(this));
            }
            Skd.Form.GetId($(this)).id.val("");

            Skd.Form.DropDown.Search($(this), $(this).val());
            Skd.Form.DropDown.Show($(this), true);
            return true;
        });
    }

    static ResetList(dropdownObj) {
        let ids = Skd.Form.GetId(dropdownObj);

        ids.list.find(".skd-form-header, .skd-form-row").each(function (e) {
            $(this).removeClass("hide blink-green active").addClass("shade");
        });
        if (ids.text.attr("skd-multible") == "true") {
            ids.list.find(".skd-table-row [type=checkbox]").each(function (e) {
                $(this).prop("checked", false);
            });
        }
        //ids.list.find(".skd-table-row:visible:even").css("background-color", "var(--row-shade-color)");
    }
    ResetList(dropdownObj) { return SkdForm_Dropdown_Class.ResetList(dropdownObj); }

    static RowCount(dropdownObj) {
        this.ResetList(dropdownObj);
        let ids = Skd.Form.GetId(dropdownObj);
        ids.list.find(".card-header").first().find("[name='search-count']").text(ids.list.find(".skd-table-row:not(.hide)").length);

        let headers = ids.list.find(".skd-table-header");
        headers.show();
        headers.each(function (e) {
            // search-count-header
            let groupId = $(this).attr("data-group-id");
            let groupCount = Number($(ids.list).find(".skd-table-row:not(.hide)[data-group-id=" + groupId + "]").length);
            if (groupCount == 0) {
                $(this).hide();
            } else {
                $(this).find("[skd-cmd=search-count-header]").text("Count: " + groupCount);
            }
        });
    }
    RowCount(dropdownObj) { return SkdForm_Dropdown_Class.RowCount(dropdownObj); }

    static Clear(dropdownObj) {
        window.clearInterval(Skd.Form.DropDown.timeout);
        Skd.Form.DropDown.ResetList(dropdownObj);

        let ids = Skd.Form.GetId(dropdownObj);

        ids.id.val("");
        ids.text.val("");
        ids.list.find(".skd-table-row").removeClass("active");
        SkdForm_Dropdown_Class.Search(dropdownObj, "");
        skd_form_valid(dropdownObj);
        ids.text.focus();
        return false;
    }
    Clear(dropdownObj) { return SkdForm_Dropdown_Class.Clear(dropdownObj); }

    static Search(dropdownObj, searchText) {
        let ids = Skd.Form.GetId(dropdownObj);
        let rows = ids.list.find(".skd-table-row");

        if (searchText == "") {
            rows
                .removeClass("hide blink-green")
                .css("background-color", "")
                .addClass("shade");
        } else {
            rows
                .addClass("hide")
                .css("background-color", "")
                .removeClass("shade");
            rows.each(function (e) {
                if ($(this).text().toLowerCase().includes(searchText.toLowerCase())) {
                    $(this).removeClass("hide");
                }
            });
        }
        ids.list.find(".skd-table-row:visible:even").css("background-color", "var(--row-shade-color)");
        Skd.Form.DropDown.RowCount(dropdownObj);
    }
    Search(dropdownObj, searchText) { return SkdForm_Dropdown_Class.Search(dropdownObj, searchText); }

    static Show(dropdownObj, forceShow = false) {
        let ids = Skd.Form.GetId(dropdownObj);
        $("[skd-cmd=dropdownadv]").each(function (e) { // hide all open dropdowns but current
            let hideIds = Skd.Form.GetId($(this));
            if (hideIds.id.attr("id") != ids.id.attr("id")) {
                Skd.Form.DropDown.Hide($(this));
            }
        })

        let eWidth = ids.list.parent().width();
        let eHeight = ids.list.parent().height();

        ids.list.find(".card-header").first().find("[name='search-count']").text(ids.list.find(".skd-table-row:not(.hide)").length);
        ids.list
            .css("top", "calc(" + eHeight + "px + 0.25rem")
            .css("width", (Math.ceil(eWidth) + 1) + "px");
        if (forceShow) {
            ids.list.addClass("shown").show();
            ids.list.parent().find("[skd-cmd=dropdownadv-toggle]").addClass("shown");
        } else {
            ids.list.toggle().toggleClass("shown");
            ids.list.parent().find("[skd-cmd=dropdownadv-toggle]").toggleClass("shown");
        }
        ids.list.parent().find("[skd-cmd=dropdownadv-toggle]").blur();
        Skd.Form.DropDown.RowCount(dropdownObj);
        return false;
    }
    Show(dropdownObj, searchText) { return SkdForm_Dropdown_Class.Show(dropdownObj, searchText); }

    static Hide(dropdownObj) {
        let ids = Skd.Form.GetId(dropdownObj);

        ids.id.trigger("change");
        ids.text.trigger("change");

        ids.list.removeClass("shown").hide();
        ids.list.parent().find("[skd-cmd=dropdownadv-toggle]").removeClass("shown");
        skd_form_valid(dropdownObj.closest("form"));
        //this.Search(dropdownObj, "");
    }
    Hide(dropdownObj, forceShow = false) { return SkdForm_Dropdown_Class.Hide(dropdownObj, forceShow = false); }
}
Skd.Form.DropDown = new SkdForm_Dropdown_Class();

/* skd-search fnc */
function skd_search_updateCount() {
    $("[skd-field='search-count']").each(function () {
        /* todo getting the list to count from */
        $(this).text($(this).closest(".skd-table").find("[class*='card-body'] .skd-table-row:not(.hide):not(.skd-table-header)").length);
    });
}
function skd_search(elm, e) {
    if (typeof e !== "undefined" && e.keyCode == 27) {
        elm.val("");
    }
    if (skd_debug) {
        console.log("skd_search(elm, e): Search: " + elm.val());
        debugger;
    }

    var searchText = elm.val();
    var elms = elm.closest(".skd-table").find(".skd-table-row");
    if (searchText == "") {
        elms.removeClass("hide blink-green").addClass("shade");
    } else {
        elms.addClass("hide").removeClass("shade");
        elms.each(function (e) {
            if ($(this).text().toLowerCase().includes(searchText.toLowerCase())) {
                $(this).removeClass("hide").addClass("shade");
            }
        });
    }

    skd_search_updateCount();
}

/* datetime fnc */
function skd_datetime_setValue(callerElm, value) {
    callerElm = AsJQuery(callerElm);
    if (typeof value === "undefined") {
        value = "";
    }
    let formId = callerElm.closest("form").attr("id");
    if (typeof formId === "undefined") {
        formId = "";
    }
    let id = "#" + callerElm.attr("id");
    if (formId != "") {
        id = "#" + formId + " " + id;
    }
    let elmType = callerElm.attr("type").toLowerCase();
    if (elmType == "" || elmType == "hidden") {
        elmType = "text";
    }
    let masterId = id.substring(0, id.lastIndexOf("_"));
    let masterValue = "";
    let dateTimeId = masterId + "_datetime";
    let dateTimeValue = $(dateTimeId).val();
    let dateId = masterId + "_date";
    let dateValue = $(dateId).val();
    let timeId = masterId + "_time";
    let timeValue = $(timeId).val();
    if (masterId == "") {
        masterId = id;
        dateTimeId = id + "_datetime";
        dateId = id + "_date";
        timeId = id + "_time";
    }

    if (elmType == "text" && value != "") {
        // assume it's a call from [skd-cmd=datetime_set_now] or [skd-cmd=datetime_clear]
        masterValue = value;
        if (value.length == 16) {
            if (regExISODate.test(value.substring(0, 10)) && regExTime24leadingZero.test(value.substring(11, 17))) {
                if ($(dateId).length == 1) {
                    dateValue = value.substring(0, 10);
                    if ($(timeId).length == 0) {
                        masterValue = dateValue;
                    }
                }
                if ($(timeId).length == 1) {
                    timeValue = value.substring(11, 17);
                    if ($(dateId).length == 0) {
                        masterValue = timeValue;
                    }
                }
            }
        } else
            if (value.length == 10) {
                if (regExISODate.test(value)) {
                    dateValue = value.substring(0, 10);
                    masterValue = dateValue;
                }
            } else
                if (value.length == 5) {
                    if (regExTime24leadingZero.test(value)) {
                        timeValue = value.substring(11, 17);
                        masterValue = timeValue;
                    }
                }
    } else if (elmType == "date") {
        masterValue = dateValue;
        if ($(timeId).length == 1) {
            masterValue += " " + timeValue;
        }
    } else if (elmType == "time") {
        masterValue = timeValue;
        if ($(dateId).length == 1) {
            masterValue = dateValue + " " + timeValue;
        }
    }

    if (skd_debug) {
        console.warn("skd_datetime_setValue");
        console.log("-- formId: " + formId);
        console.log("-- id: " + id);
        console.log("-- value: " + value);
        console.log("-- value: " + value + " (length: " + (value != "" ? value.length : 0) + ")");
        console.log("-- element type: " + elmType);
        console.log("-- masterId: " + masterId + " (" + ($(masterId).length == 1) + ")");
        console.log("-- masterValue: " + masterValue + " (length: " + masterValue.length + ")");
        console.log("-- dateTimeId: " + dateTimeId + " (" + ($(dateTimeId).length == 1) + ")");
        console.log("-- dateId: " + dateId + " (" + ($(dateId).length == 1) + ")");
        console.log("-- timeId: " + timeId + " (" + ($(timeId).length == 1) + ")");
        console.log("-- date: " + dateValue);
        console.log("-- time: " + timeValue);
    }

    if ($(masterId).length == 1) {
        $(masterId).val(masterValue);
    }
    if ($(dateId).length == 1) {
        $(dateId).val(dateValue);
    }
    if ($(timeId).length == 1) {
        $(timeId).val(timeValue);
    }

    if (formId != "") {
        skd_form_valid(formId);
    } else {
        // callerElm.valid(); // form element is required to make jQuery Validation work
    }

    return false;
}
/* datetime fnc END */

function skd_form_debug(formElm, postIntoElement, url = "/Developer/PostData") {
    formElm = AsJQuery(formElm);
    if (!skd_form_valid(formElm)) {
        if (skd_debug) console.error("Form is invalid: " + formElm.attr("id"));
        Skd.PageLoadingHide();
        return false;
    }
    if (typeof postIntoElement === 'undefined' || postIntoElement == null) {
        postIntoElement = AsJQuery("#skd_debug_posted_values");
    }

    //formElm.find("[type='submit']").SkdContentSpinner();
    //Skd.PageLoadingHide();
    if (!_skd_pageLoadingShown) {
        Skd.PageLoading("Posting debug form...");
    }

    $.ajax({
        async: false,
        method: "POST",
        cache: false,
        url: url,
        data: $(formElm).serialize(),
        dataType: "HTML",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data, status, xhr) {
            if (skd_debug) console.log("skd_form_debug: success");
            $("#skd_debug_posted_values").css("visibility", "visible");
            $("#skd_debug_posted_values").off("click").on("click", function (e) { Skd.PageLoadingHide(); $(this).html("").css("visibility", "hidden"); })

            if (postIntoElement) {
                postIntoElement.html(data);
            }

            if (skd_debug) console.log("skd_form_debug: success");

            formElm.find("[skd-cmd='button-confirm']").removeClass("confirmed confirm");
            return data;
        },
        fail: function () {
            Skd.PageLoadingHide();
        },
        always: function () {
            if (skd_debug) console.log("skd_form_debug: always");
        },
        complete: function (event, xhr, options) {
            if (skd_debug) console.log("skd_form_debug: complete");
        }
    });

    return false;
}

function skd_form_valid(formElm) {
    if (skd_debug) console.warn("skd_form_valid");

    formElm = AsJQuery(formElm);
    form = formElm.closest("form");
    if (!formElm.is("form")) {
        if (skd_debug) console.log("-- finding nearst form");
        formElm = formElm.closest("form");
        if (!formElm.is("form")) {
            if (skd_debug) console.warn("skd_form_valid: submit action are not contained in a form control. If the submit action is handled otherwise, please dismiss this message.");
            return false;
        }
    }

    if (skd_debug) console.log("-- íd: " + formElm.attr("id"));

    // loop items
    form.find("[control-type]").each(function (e) {
        //console.warn("skd_form_valid(" + formElm + "):" + $(this).attr("id"));
        //if($(this).attr("[control-type]")=="form.text.")
        //STIG
    });
    form.find("[id$='_container']").each(function (e) {
        $(this).find("label .label-valid-status").css("display", ($(this).find("[required]").length > 0 ? "inline" : "none"));
    });

    var isError = !formElm.valid();

    if (skd_debug) console.log("-- isError: " + isError);

    skd_form_valid_setbuttons(form, isError);

    return !isError;
}
function skd_form_valid_setbuttons(form, isError) {
    form.find("[skd-cmd=submit] .fas, [skd-cmd=button-confirm][type=submit] .fas").each(function (e) {
        $(this).removeClass("fa-spinner fa-spin");
        $(this).addClass($(this).parent().attr("skd-icon"));
    });

    if (isError) { // form invalid
        form.find("[skd-cmd=submit], [skd-cmd=button-confirm][type=submit]").removeClass("btn-success btn-warning confirm confirmed").addClass("btn-danger").attr("disabled", true);
    } else { // form invalid
        form.removeClass("error"); // FIX: jQuery Validation
        form.find("[skd-cmd=submit], [skd-cmd=button-confirm][type=submit]").addClass("btn-success").removeClass("btn-danger btn-warning").attr("disabled", false);
    }
}
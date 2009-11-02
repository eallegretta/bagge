var onLoadFunctions = new Array();

function pageLoad() {
    $(document).ready(function() {
        $("input[type=text],input[type=password],input[type=file],textarea").focus(function() {
            $(this).addClass("selected");
        }).blur(function() {
            $(this).removeClass("selected");
        });

        $("button,input[type=submit]").mouseover(function() {
            $(this).addClass("buttonHover");
        }).mouseout(function() {
            $(this).removeClass("buttonHover");
        });

        $(".collapsible h3").click(function() {
            $(".collapsible table").toggle("fast");
            $(this).toggleClass("collapsed");
        });

        $(".numeric").numeric();

        if (jQuery.fn.dialog) {
            if ($(".failureText span").text() != "")
                $(".failureText").dialog();
        }

        attachStyleToValidators();

        for (var funcName in onLoadFunctions)
            onLoadFunctions[funcName]();

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
    });
}

function beginRequestHandler() {
    showLoadingAnimation();
}
function endRequestHandler() {
    hideLoadingAnimation();
}


function showLoadingAnimation() {
    //	var content = $("body");
    var loading = $("#loadingAnimation");
    //	var top = (content.position().top + content.height() / 2) - (loading.height() / 2);
    //	var left = (content.position().left + content.width() / 2) - (loading.width() / 2);

    var dimensions = getWindowDimensions();
    var top = (dimensions.height / 2) - (loading.height() / 2) + dimensions.scrollY;
    var left = (dimensions.width / 2) - (loading.width() / 2) + dimensions.scrollX;

    loading.css("left", left + "px");
    loading.css("top", top + "px");
    loading.css("display", "");
}

function hideLoadingAnimation() {
    var loading = $("#loadingAnimation");
    loading.css("display", "none");
}


function getWindowDimensions() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }

    var scrOfX = 0, scrOfY = 0;
    if (typeof (window.pageYOffset) == 'number') {
        //Netscape compliant
        scrOfY = window.pageYOffset;
        scrOfX = window.pageXOffset;
    } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
        //DOM compliant
        scrOfY = document.body.scrollTop;
        scrOfX = document.body.scrollLeft;
    } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
        //IE6 standards compliant mode
        scrOfY = document.documentElement.scrollTop;
        scrOfX = document.documentElement.scrollLeft;
    }

    return { width: myWidth, height: myHeight, scrollX: scrOfX, scrollY: scrOfY };
}



function attachStyleToValidators() {
    try {
        $(Page_Validators).each(function() {
            setValidationTargetCssClass(this);
            $(this)
        .bind("DOMAttrModified propertychange", function(e) {
            // Exit early if IE because it throws this event lots more
            if (e.originalEvent.propertyName && e.originalEvent.propertyName != "isvalid") return;

            setValidationTargetCssClass(this);
        });
        });
    }
    catch (ex) {
    }
}

function setValidationTargetCssClass(validator) {
    var controlToValidate = $("#" + validator.controltovalidate);
    var validators = controlToValidate.attr("Validators");
    if (validators == null) return;

    var isValid = true;
    $(validators).each(function() {
        if (this.isvalid !== true) {
            isValid = false;
        }
    });

    if (isValid) {
        controlToValidate.removeClass("error");
    } else {
        controlToValidate.addClass("error");
    }
}
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
	var content = $("#content");
	var loading = $("#loadingAnimation");
	var top = (content.position().top + content.height() / 2) - (loading.height() / 2);
	var left = (content.position().left + content.width() / 2) - (loading.width() / 2);

	loading.css("left", left + "px");
	loading.css("top", top + "px");
	loading.css("display", "");
}

function hideLoadingAnimation() {
	var loading = $("#loadingAnimation");
	loading.css("display", "none");
}

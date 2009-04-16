function addSelectedItem(tableId, hdnId, itemId, priceId, itemType)
{
	var id = $("#" + itemId).value;
	var text = $("#" + itemId).text;
	var price = $("#" + priceId).value;

	var row = $("<tr></tr>").append(
					$("<td></td>").text(text)
				).append(
					$("<td></td>").text(price)
				);
	$("#" + tableId).append(row);

	var hdnValue = jQuery.trim($("#" + hdnId).value);

	if (hdnValue.length == 0) {
		hdnValue = "[]";
	}

	var elements = JSON.parse(hdnValue);
	if (itemType == "Product") {
		elements[elements.length] = {
			"Product": {
				"Id": id,
				"Name": text
			},
			"Price": price
		}
	}
	else {
		elements[elements.length] = {
			"Provider": {
				"Id": id,
				"Name": text
			},
			"Price": price
		}
	}
}
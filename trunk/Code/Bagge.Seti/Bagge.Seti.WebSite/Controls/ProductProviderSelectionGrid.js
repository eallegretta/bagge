// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function(from, to) {
  var rest = this.slice((to || from) + 1 || this.length);
  this.length = from < 0 ? this.length + from : from;
  return this.push.apply(this, rest);
};

function ProductProviderSelectionGrid(tableId, hdnId, itemId, priceId, deleteIconUrl) {

	this.table = $("#" + tableId);
	this.hdn = $("#" + hdnId);
	this.items = $("#" + itemId);
	this.price = $("#" + priceId);
	this.deleteIconUrl = deleteIconUrl;

	this.getSelectedOption = function() {
		return $("option:selected", this.items);
	}

	this.getSelecteItem = function() {
		var option = this.getSelectedOption();
		return this.createItem(option.val(), option.text(), this.price.val());
	}
	
	this.isValidPrice = function(price) {
		var regex = /\d+(?:\.\d*)?$/;
		return regex.test(price);
	}

	this.addSelectedItem = function() {
		var item = this.getSelecteItem();
		if (item.Name.length > 0 && this.isValidPrice(item.Price)) {
			this.getSelectedOption().remove();
			this.addItem(item.id, item.text, item.price);
			this.price.val("");
		}
	}
	this.addItem = function(id, text, price) {
		var item = this.createItem(id, text, price);
		this.appendItem(item);
		this.appendRow(item);
	}

	this.refresh = function() {
		var firstRow = $("tr:first-child", this.table);
		$(this.table).children().remove();
		$(this.table).append(firstRow);
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		for (var index = 0; index < items.length; index++) {
			var item = items[index];
			this.appendRow(item);
		}
	}
	
	

	this.removeSelectedItem = function(img) {
		var row = $(img).parent().parent();
		var item = row.data("item");
		var itemId = item["Id"];
		var itemName = item["Name"];
		this.removeItem(item);
		row.remove();
		this.items.append("<option value='" + itemId + "'>" + itemName + "</option>");
	}

	this.createItem = function (id, text, price) {
		return { "Id": id, "Name": text, "Price": price };
	}

	this.appendItem = function(item) {
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		items[items.length] = item;
		this.hdn.val(JSON.stringify(items));
	}

	this.removeItem = function(item) {
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		for (var index = 0; index < items.length; index++) {
			if (items[index]["Id"] == item["Id"]) {
				items.remove(index);
				break;
			}
		}
		this.hdn.val(JSON.stringify(items));
	}

	this.appendRow = function(item) {
		var img = document.createElement("img");
		img.src = this.deleteIconUrl;
		var row = $("<tr></tr>").append(
						$("<td></td>").text(item.Name)
					).append(
						$("<td style='text-align:right'></td>").text(item.Price)
				).append(
					$("<td style='text-align:center'></td>").append(img)
				).data("item", item);
		img.behaviour = this;
		$(img).click(function() {
			this.behaviour.removeSelectedItem(this);
		});
		this.table.append(row);
	}
	
	this.getHiddenValue = function(){
		var hdnValue = jQuery.trim(this.hdn.val());
		if (hdnValue.length == 0) {
			hdnValue = "[]";
		}
		return hdnValue;
	}
}
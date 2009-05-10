// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function(from, to) {
  var rest = this.slice((to || from) + 1 || this.length);
  this.length = from < 0 ? this.length + from : from;
  return this.push.apply(this, rest);
};

function ProductProviderSelectionGrid(tableId, btnId, hdnId, itemId, priceId, deleteIconUrl, isReadOnly) {

	this.table = $("#" + tableId);
	this.addButton = $("#" + btnId);
	this.addButton[0].behavior = this;
	this.addButton.click(function() {
		this.behavior.addSelectedItem();
	});
	
	this.hdn = $("#" + hdnId);
	this.items = $("#" + itemId);
	this.price = $("#" + priceId);
	this.deleteIconUrl = deleteIconUrl;
	this.isReadOnly = isReadOnly;


	this.getSelectedOption = function() {
		return $("option:selected", this.items);
	}

	this.getSelecteItem = function() {
		var option = this.getSelectedOption();
		return this.createItem(option.val(), option.text(), this.price.val());
	}
	
	this.isValidPrice = function(price) {
		var regex = /^\d+(?:\.\d*)?$/;
		return regex.test(price);
	}

	this.addSelectedItem = function() {
		var item = this.getSelecteItem();
		if (item.Name.length > 0 && this.isValidPrice(item.Price)) {
			this.getSelectedOption().remove();
			this.addItem(item.Id, item.Name, item.Price);
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

	this.updatePrice = function(item) {
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		for (var index = 0; index < items.length; index++) {
			if (items[index]["Id"] == item["Id"]) {
				items[index]["Price"] = item["Price"];
				break;
			}
		}
		this.hdn.val(JSON.stringify(items));
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

		var cssClass = ($("tr", this.table).length % 2 == 1) ? "gridRow" : "gridRowAlternate";

		var priceBox = (this.isReadOnly) ? document.createElement("span") : document.createElement("input");
		if (this.isReadOnly)
			$(priceBox).text(item.Price);
		else {
			priceBox.behaviour = this;

			$(priceBox)
				.focus(function() {
					this.lastValue = this.value;
				})
				.val(item.Price)
				.change(function() {
					if (this.behaviour.isValidPrice(this.value)) {
						var item = $(this).parent().parent().data("item");
						item.Price = this.value;
						this.behaviour.updatePrice(item);
					}
					else
						this.value = this.lastValue;
				}
			);
		}
		$(priceBox).css("text-align", "right");

		var row = $("<tr class='" + cssClass + "'></tr>").append(
						$("<td></td>").text(item.Name)
					).append(
						$("<td style='text-align:right'></td>").append(priceBox)
				);
		if (!this.isReadOnly)
			row.append($("<td style='text-align:center'></td>").append(img));
		row.data("item", item);
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
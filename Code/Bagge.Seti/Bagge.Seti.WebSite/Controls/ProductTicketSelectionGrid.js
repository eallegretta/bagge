// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function(from, to) {
	var rest = this.slice((to || from) + 1 || this.length);
	this.length = from < 0 ? this.length + from : from;
	return this.push.apply(this, rest);
};

function ProductTicketSelectionGrid(tableId, btnId, hdnId, productId, providerId, quantityId, deleteIconUrl, isReadOnly, totalQuantityId) {

	this.table = $("#" + tableId);
	this.addButton = $("#" + btnId);
	this.addButton[0].behavior = this;
	this.addButton.click(function() {
		this.behavior.addSelectedItem();
	});

	this.hdn = $("#" + hdnId);

	this.products = $("#" + productId);
	
	this.products.selected = function() {
		return $("option:selected", this);
	}


	this.providers = $("#" + providerId);
	this.providers.selected = function() {
		return $("option:selected", this);
	}
	this.providers.removeAll = function() {
		$("option", this).remove();
	}
	
	this.quantity = $("#" + quantityId);

	this.deleteIconUrl = deleteIconUrl;

	this.isReadOnly = isReadOnly;

	this.hdnTotalQuantity = $("#" + totalQuantityId);

	this.totalPrice = $(".budget");
	
	if (this.totalPrice == null)
		this.totalPrice = {};

	this.totalPrice.setValue = function(value) {
	if (this instanceof jQuery) {
			if (this[0].tagName.toLowerCase() == "input")
				this.val(value);
			else
				this.text(value);
		}
		$("#totalPrice").text(value);
	};


	this.getSelectedItem = function() {
		var product = this.products.selected();
		var provider = this.providers.selected();

		if (provider.val() == null)
			return null;

		var providerData = JSON.parse(provider.val());

		var id = providerData.Id;
		var productId = product.val();
		var productName = product.text();
		var providerId = providerData.ProviderId;
		var providerName = providerData.Name;
		var quantity = this.quantity.val();
		var price = providerData.Price;

		return this.createItem(id, productId, productName, providerId, providerName, quantity, price);
	}

	this.isValidQuantity = function(quantity) {
		var regex = /^\d+$/;
		return regex.test(quantity);
	}

	this.addSelectedItem = function() {
		var item = this.getSelectedItem();
		if (item != null && this.isValidQuantity(item.Quantity)) {
			this.products.selected().remove();
			this.providers.removeAll();
			this.quantity.val("");
			this.addItem(item);
		}
	}
	this.addItem = function(item) {
		this.appendRow(item);
		this.calculateTotalQuantity();
		this.calculateTotalPrice();
		this.appendItem(item);
	}

	this.refresh = function() {
		var firstRow = $("tr:first-child", this.table);
		var lastRow = $("tr:last-child", this.table);
		$(this.table).children().remove();
		$(this.table).append(firstRow);
		$(this.table).append(lastRow);
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		for (var index = 0; index < items.length; index++) {
			var item = items[index];
			this.appendRow(item);
		}
		this.calculateTotalQuantity();
		this.calculateTotalPrice();
		
	}



	this.removeSelectedItem = function(img) {
		var row = $(img).parent().parent();
		var item = row.data("item");
		var itemId = item.ProductId;
		var itemName = item.Product;
		this.removeItem(item);
		row.remove();
		this.products.append("<option value='" + itemId + "'>" + itemName + "</option>");
		this.calculateTotalQuantity();
		this.calculateTotalPrice();

		//refresh styles
		$("tr.productProvider:odd", this.table).removeClass("gridRow").removeClass("gridRowAlternate").addClass("gridRowAlternate");
		$("tr.productProvider:even", this.table).removeClass("gridRow").removeClass("gridRowAlternate").addClass("gridRow");
	}

	this.createItem = function(id, productId, product, providerId, provider, quantity, unitaryPrice) {
		return { "Id": id, "ProductId": productId, "Product": product, "ProviderId": providerId, "Provider": provider, "Quantity": quantity, "UnitaryPrice": unitaryPrice };
	}

	this.updateQuantity = function(item) {
		var hdnValue = this.getHiddenValue();
		var items = JSON.parse(hdnValue);
		for (var index = 0; index < items.length; index++) {
			if (items[index]["Id"] == item["Id"]) {
				items[index]["Quantity"] = item["Quantity"];
				break;
			}
		}
		this.hdn.val(JSON.stringify(items));
		this.calculateTotalQuantity();
		this.calculateTotalPrice();
	}
	this.calculateTotalPrice = function() {
		var total = 0;
		$(".price", this.table).each(function() {
			total += parseFloat($(this).text().replace("$", ""));
		});

		this.totalPrice.setValue(total);
	}

	this.calculateTotalQuantity = function() {
		var total = 0;
		$(".quantity", this.table).each(function() {
			total += parseInt(this.value);
		});
		this.hdnTotalQuantity.value = total;
		$("#totalQuantity", this.table).text(total);
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

		var cssClass = ($("tr", this.table).length % 2 == 0) ? "gridRow" : "gridRowAlternate";

		var quantityBox = (this.isReadOnly) ? document.createElement("span") : document.createElement("input");
		if (this.isReadOnly)
			$(quantityBox).text(item.Quantity);
		else {
			quantityBox.behaviour = this;

			$(quantityBox)
				.addClass("quantity")
				.focus(function() {
					this.lastValue = this.value;
				})
				.val(item.Quantity)
				.change(function() {
					if (this.behaviour.isValidQuantity(this.value)) {
						var item = $(this).parent().parent().data("item");
						item.Quantity = this.value;
						$("#price_" + item.Id, this.behaviour.table).html("$" + (item.Quantity * item.UnitaryPrice));
						this.behaviour.updateQuantity(item);
					}
					else
						this.value = this.lastValue;
				}
			);
		}
		$(quantityBox).css("text-align", "right");
		
		var row = $("<tr class='productProvider " + cssClass + "'></tr>")
					.append($("<td></td>").text(item.Product))
					.append($("<td></td>").text(item.Provider))
					.append($("<td style='text-align:right'></td>").append(quantityBox))
					.append("<td style='text-align:right' class='price' id='price_" + item.Id + "' >$" + (item.Quantity * item.UnitaryPrice) + "</td>")
		;
		if (!this.isReadOnly)
			row.append($("<td style='text-align:center'></td>").append(img));
		row.data("item", item);
		img.behaviour = this;
		$(img).click(function() {
			this.behaviour.removeSelectedItem(this);
		});
		$("tr:last-child", this.table).before(row);
	}

	this.getHiddenValue = function() {
		var hdnValue = jQuery.trim(this.hdn.val());
		if (hdnValue.length == 0) {
			hdnValue = "[]";
		}
		return hdnValue;
	}


}
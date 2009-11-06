function Controls_Calendar(id, txtId, imgId, imgDisabledId, compareToId, compareOp) {
	this._txt = document.getElementById(txtId);
	this._txt.behaviorId = id;
	this._img = document.getElementById(imgId);
	this._imgDisabled = document.getElementById(imgDisabledId);
	this._compareToControl = document.getElementById(compareToId);
	this._compareOp = compareOp;


	this.enable = function() {
		this._txt.disabled = false;
		this._img.style.display = "";
		this._imgDisabled.style.display = "none";
	}
	this.disable = function() {
		this._txt.disabled = true;
		this._img.style.display = "none";
		this._imgDisabled.style.display = "";
	}
	this.setOnChange = function(handler) {
		this._txt.onchange = handler;
	}
	this.setDate = function(date) {
		this._txt.value = date;
	}
	this.getSelectedDate = function() {
		var date = Date.parseLocale(this._txt.value);
		if (date != null) {
			if (date.getFullYear() < 1950 || date.getFullYear() > 2030)
				return null;
		}
		return date;
	}

	this.isValid = function() {
		if ($.trim(this._txt.value).length == 0)
			return true;

		if (this.getSelectedDate() == null)
			return false;

		return true;
	}

	this.isValidCompare = function() {
		if ($.trim(this._txt.value).length == 0)
			return true;

		if (this.getSelectedDate() == null)
			return false;

		if (this._compareToControl == null || $.trim(this._compareToControl.value).length == 0)
			return true;

		var date = Date.parseLocale(this._compareToControl.value);
		switch (this._compareOp) {
			case "Equal": return this.getSelectedDate() == date;
			case "LessThan": return this.getSelectedDate() < date;
			case "LessThanEqual": return this.getSelectedDate() <= date;
			case "GreaterThan": return this.getSelectedDate() > date;
			case "GreaterThanEqual": return this.getSelectedDate() >= date;
			case "NotEqual": return this.getSelectedDate() != date;
			default: throw "Invalid Comparisson Operator";
		}
	}

	return this;
}

function ValidateDateCalendar(sender, args) {
	var behavior = window[sender.id.replace("__invalidDateVal", "")];
	args.IsValid = behavior.isValid();
}

function ValidateCompareCalendar(sender, args) {
	var behavior = window[sender.id.replace("__calendarCompare", "")];
	args.IsValid = behavior.isValidCompare();
}

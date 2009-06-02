function Controls_Calendar(txtId, imgId, imgDisabledId)
{
	this._txt = document.getElementById(txtId);
	this._img = document.getElementById(imgId);
	this._imgDisabled = document.getElementById(imgDisabledId);
	
	
	this.enable = function()
	{
		this._txt.disabled = false;
		this._img.style.display = "";
		this._imgDisabled.style.display = "none";
	}
	this.disable = function()
	{
		this._txt.disabled = true;
		this._img.style.display = "none";
		this._imgDisabled.style.display = "";
	}
	this.setOnChange = function(handler)
	{
		this._txt.onchange = handler;
	}
	this.setDate = function(date)
	{
		this._txt.value = date;
	}
	this.getSelectedDate = function()
	{
		return Date.parseLocale(this._txt.value);
	}
	return this;
}

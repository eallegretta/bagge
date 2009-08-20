//Fix for MaskedEdit for es-AR culture to allow
//using the . as decimal separator only works with 99.99 format

function applyMaskedEditFix(textBox){
	/*$(textBox).keypress(function(e){
		var isValidMask = this.MaskedEditBehavior._MaskType == AjaxControlToolkit.MaskedEditType.Number &&
		(this.MaskedEditBehavior._InputDirection == AjaxControlToolkit.MaskedEditInputDirections.LeftToRight ||
		this.MaskedEditBehavior._InputDirection == AjaxControlToolkit.MaskedEditInputDirections.RightToLeft); 
		
		var isRTL = this.MaskedEditBehavior._InputDirection == AjaxControlToolkit.MaskedEditInputDirections.RightToLeft;
		
		if(isValidMask && String.fromCharCode(e.which) == ".")
		{
			if(isRTL)
				this.MaskedEditBehavior._AdjustElementDecimalRTL();
			else
				this.MaskedEditBehavior._AdjustElementDecimalLTR();
		}
	});*/
}




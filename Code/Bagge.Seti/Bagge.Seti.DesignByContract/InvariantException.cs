using System;
using System.Collections.Generic;
using System.Text;

namespace Bagge.Seti.DesignByContract
{
	/// <summary>
	/// Exception raised when an invariant fails.
	/// </summary>
	public class InvariantException : DesignByContractException
	{
	
		/// <summary>
		/// Invariant Exception.
		/// </summary>
		public InvariantException() { }
		
		/// <summary>
		/// Invariant Exception.
		/// </summary>
		public InvariantException(string message) : base(message) { }
		
		/// <summary>
		/// Invariant Exception.
		/// </summary>
		public InvariantException(string message, Exception inner) :
			base(message, inner) { }
	}
}

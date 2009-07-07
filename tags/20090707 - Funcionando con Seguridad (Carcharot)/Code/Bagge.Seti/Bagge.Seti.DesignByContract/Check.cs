#if DEBUG
	#define DBC_CHECK_ALL
#else
	#define DBC_CHECK_PRECONDITION
#endif

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace Bagge.Seti.DesignByContract
{
	public sealed class Check
	{
		#region Interface

		/// <summary>

		/// Precondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION"),
		Conditional("DBC_CHECK_PRECONDITION")]
		public static void Require(bool assertion, string message)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PreconditionException(message);
			}
			else
			{
				Trace.Assert(assertion, "Precondition: " + message);
			}
		}

		/// <summary>

		/// Precondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION"),
		Conditional("DBC_CHECK_PRECONDITION")]
		public static void Require(bool assertion, string message, Exception inner)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PreconditionException(message, inner);
			}
			else
			{
				Trace.Assert(assertion, "Precondition: " + message);
			}
		}

		/// <summary>

		/// Precondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION"),
		Conditional("DBC_CHECK_PRECONDITION")]
		public static void Require(bool assertion)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PreconditionException("Precondition failed.");
			}
			else
			{
				Trace.Assert(assertion, "Precondition failed.");
			}
		}

		/// <summary>

		/// Postcondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Ensure(bool assertion, string message)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PostconditionException(message);
			}
			else
			{
				Trace.Assert(assertion, "Postcondition: " + message);
			}
		}

		/// <summary>

		/// Postcondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Ensure(bool assertion, string message, Exception inner)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PostconditionException(message, inner);
			}
			else
			{
				Trace.Assert(assertion, "Postcondition: " + message);
			}
		}

		/// <summary>

		/// Postcondition check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION")]
		public static void Ensure(bool assertion)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new PostconditionException("Postcondition failed.");
			}
			else
			{
				Trace.Assert(assertion, "Postcondition failed.");
			}
		}

		/// <summary>

		/// Invariant check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT")]
		public static void Invariant(bool assertion, string message)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new InvariantException(message);
			}
			else
			{
				Trace.Assert(assertion, "Invariant: " + message);
			}
		}

		/// <summary>

		/// Invariant check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT")]
		public static void Invariant(bool assertion, string message, Exception inner)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new InvariantException(message, inner);
			}
			else
			{
				Trace.Assert(assertion, "Invariant: " + message);
			}
		}

		/// <summary>

		/// Invariant check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT")]
		public static void Invariant(bool assertion)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new InvariantException("Invariant failed.");
			}
			else
			{
				Trace.Assert(assertion, "Invariant failed.");
			}
		}

		/// <summary>

		/// Assertion check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion, string message)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new AssertionException(message);
			}
			else
			{
				Trace.Assert(assertion, "Assertion: " + message);
				//Trace.Assert(assertion, "Assertion: " + message);

			}
		}

		/// <summary>

		/// Assertion check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion, string message, Exception inner)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new AssertionException(message, inner);
			}
			else
			{
				Trace.Assert(assertion, "Assertion: " + message);
			}
		}

		/// <summary>

		/// Assertion check.

		/// </summary>

		[Conditional("DBC_CHECK_ALL")]
		public static void Assert(bool assertion)
		{
			if (UseExceptions)
			{
				if (!assertion) throw new AssertionException("Assertion failed.");
			}
			else
			{
				Trace.Assert(assertion, "Assertion failed.");
			}
		}

		/// <summary>

		/// Set this if you wish to use Trace Assert statements

		/// instead of exception handling.

		/// (The Check class uses exception handling by default.)

		/// </summary>

		public static bool UseAssertions
		{
			get
			{
				return useAssertions;
			}
			set
			{
				useAssertions = value;
			}
		}

		#endregion // Interface


		#region Implementation

		// No creation

		private Check() { }

		/// <summary>

		/// Is exception handling being used?

		/// </summary>

		private static bool UseExceptions
		{
			get
			{
				return !useAssertions;
			}
		}

		// Are trace assertion statements being used?

		// Default is to use exception handling.

		private static bool useAssertions = false;

		#endregion // Implementation


		#region Obsolete

		/// <summary>

		/// Precondition check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Require")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION"),
		Conditional("DBC_CHECK_PRECONDITION")]
		public static void RequireTrace(bool assertion, string message)
		{
			Trace.Assert(assertion, "Precondition: " + message);
		}


		/// <summary>

		/// Precondition check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Require")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION"),
		Conditional("DBC_CHECK_PRECONDITION")]
		public static void RequireTrace(bool assertion)
		{
			Trace.Assert(assertion, "Precondition failed.");
		}

		/// <summary>

		/// Postcondition check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Ensure")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION")]
		public static void EnsureTrace(bool assertion, string message)
		{
			Trace.Assert(assertion, "Postcondition: " + message);
		}

		/// <summary>

		/// Postcondition check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Ensure")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT"),
		Conditional("DBC_CHECK_POSTCONDITION")]
		public static void EnsureTrace(bool assertion)
		{
			Trace.Assert(assertion, "Postcondition failed.");
		}

		/// <summary>

		/// Invariant check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Invariant")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT")]
		public static void InvariantTrace(bool assertion, string message)
		{
			Trace.Assert(assertion, "Invariant: " + message);
		}

		/// <summary>

		/// Invariant check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Invariant")]
		[Conditional("DBC_CHECK_ALL"),
		Conditional("DBC_CHECK_INVARIANT")]
		public static void InvariantTrace(bool assertion)
		{
			Trace.Assert(assertion, "Invariant failed.");
		}

		/// <summary>

		/// Assertion check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Assert")]
		[Conditional("DBC_CHECK_ALL")]
		public static void AssertTrace(bool assertion, string message)
		{
			Trace.Assert(assertion, "Assertion: " + message);
		}

		/// <summary>

		/// Assertion check.

		/// </summary>

		[Obsolete("Set Check.UseAssertions = true and then call Check.Assert")]
		[Conditional("DBC_CHECK_ALL")]
		public static void AssertTrace(bool assertion)
		{
			Trace.Assert(assertion, "Assertion failed.");
		}
		#endregion // Obsolete


	} // End Check

} // End Design By Contract


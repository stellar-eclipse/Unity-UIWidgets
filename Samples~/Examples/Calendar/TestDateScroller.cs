﻿namespace UIWidgets.Examples
{
	using UnityEngine;

	/// <summary>
	/// Test DateScroller.
	/// </summary>
	public class TestDateScroller : MonoBehaviour
	{
		/// <summary>
		/// DateScroller.
		/// </summary>
		[SerializeField]
		protected UIWidgets.DateScroller DateScroller;

		/// <summary>
		/// Start this instance.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0603:Delegate allocation from a method group", Justification = "Required")]
		protected void Start()
		{
			DateScroller.OnDateChanged.AddListener(ProcessDate);

			// change culture
			DateScroller.Culture = new System.Globalization.CultureInfo("en-US");

			// change calendar
			// DateScroller.Culture = new System.Globalization.CultureInfo("ja-JP");
			// DateScroller.Culture.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();
			// DateScroller.UpdateCalendar();

			// scroll +5 years
			// DateScroller.YearsScrollBlock.Scroll(5);

			// scroll -2 days
			// DateScroller.DaysScrollBlock.Scroll(-2);
		}

		/// <summary>
		/// Process the destroy event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0603:Delegate allocation from a method group", Justification = "Required")]
		protected void OnDestroy()
		{
			DateScroller.OnDateChanged.RemoveListener(ProcessDate);
		}

		void ProcessDate(System.DateTime dt)
		{
			Debug.Log(dt.ToString());
		}
	}
}
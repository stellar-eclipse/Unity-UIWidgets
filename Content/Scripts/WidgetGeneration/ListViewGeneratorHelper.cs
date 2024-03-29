﻿namespace UIWidgets.WidgetGeneration
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// ListView generator helper.
	/// </summary>
	[HelpURL("https://ilih.name/unity-assets/UIWidgets/docs/generator.html")]
	public class ListViewGeneratorHelper : MonoBehaviour
	{
		/// <summary>
		/// Main object.
		/// </summary>
		public GameObject Main;

		/// <summary>
		/// Container.
		/// </summary>
		public RectTransform Container;

		/// <summary>
		/// ScrollRect.
		/// </summary>
		public ScrollRect ScrollRect;

		/// <summary>
		/// Viewport.
		/// </summary>
		public RectTransform Viewport;

		/// <summary>
		/// Default item.
		/// </summary>
		public GameObject DefaultItem;
	}
}
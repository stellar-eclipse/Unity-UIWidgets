﻿// <auto-generated/>
// Auto-generated added to suppress names errors.

namespace UIWidgets
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	/// <summary>
	/// Deselect listener.
	/// </summary>
	[HelpURL("https://ilih.name/unity-assets/UIWidgets/docs/components/listeners/select.html")]
	public class SelectListener : MonoBehaviour, ISelectHandler, IDeselectHandler
	{
		/// <summary>
		/// The OnSelect event.
		/// </summary>
		public SelectEvent onSelect = new SelectEvent();

		/// <summary>
		/// The OnDeselect event.
		/// </summary>
		public SelectEvent onDeselect = new SelectEvent();

		/// <summary>
		/// Process the select event.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnSelect(BaseEventData eventData)
		{
			onSelect.Invoke(eventData);
		}

		/// <summary>
		/// Process the deselect event.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnDeselect(BaseEventData eventData)
		{
			onDeselect.Invoke(eventData);
		}
	}
}
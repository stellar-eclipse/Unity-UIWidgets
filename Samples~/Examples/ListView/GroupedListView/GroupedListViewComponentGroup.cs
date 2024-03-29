﻿namespace UIWidgets.Examples
{
	using UIWidgets;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// GroupedListViewComponentGroup.
	/// </summary>
	public class GroupedListViewComponentGroup : GroupedListViewComponent
	{
		/// <summary>
		/// Name.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[System.Obsolete("Replaced with NameAdapter.")]
		public Text Name;

		/// <summary>
		/// Name.
		/// </summary>
		[SerializeField]
		public TextAdapter NameAdapter;

		/// <inheritdoc/>
		public override void UpdateView()
		{
			NameAdapter.text = Item.Name;
		}

		/// <summary>
		/// Upgrade this instance.
		/// </summary>
		public override void Upgrade()
		{
			base.Upgrade();

#pragma warning disable 0612, 0618
			Utilities.RequireComponent(Name, ref NameAdapter);
#pragma warning restore 0612, 0618
		}
	}
}
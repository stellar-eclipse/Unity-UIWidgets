﻿namespace UIWidgets
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// ListViewString component.
	/// </summary>
	public class ListViewStringItemComponent : ListViewItem, IViewData<string>
	{
		/// <summary>
		/// The Text component.
		/// </summary>
		[SerializeField]
		public TextAdapter Text;

		/// <summary>
		/// Item.
		/// </summary>
		public string Item
		{
			get;
			protected set;
		}

		/// <summary>
		/// Init graphics foreground.
		/// </summary>
		protected override void GraphicsForegroundInit()
		{
			if (GraphicsForegroundVersion == 0)
			{
				#pragma warning disable 0618
				Foreground = new Graphic[] { UtilitiesUI.GetGraphic(Text), };
				#pragma warning restore
				GraphicsForegroundVersion = 1;
			}

			base.GraphicsForegroundInit();
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="item">Text.</param>
		public virtual void SetData(string item)
		{
			Item = item;
			Text.Value = item.Replace("\\n", "\n");
		}
	}
}
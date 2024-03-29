﻿namespace UIWidgets.Examples.Tasks
{
	using UIWidgets;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Task component.
	/// </summary>
	public class TaskComponent : ListViewItem, IViewData<Task>
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

		/// <summary>
		/// Progressbar.
		/// </summary>
		#pragma warning disable 0618
		public Progressbar Progressbar;
		#pragma warning restore

		/// <summary>
		/// Init graphics foreground.
		/// </summary>
		protected override void GraphicsForegroundInit()
		{
			if (GraphicsForegroundVersion == 0)
			{
				#pragma warning disable 0618
				Foreground = new Graphic[] { UtilitiesUI.GetGraphic(NameAdapter), };
				#pragma warning restore
				GraphicsForegroundVersion = 1;
			}

			base.GraphicsForegroundInit();
		}

		Task currentItem;

		/// <summary>
		/// Current task.
		/// </summary>
		public Task Item
		{
			get
			{
				return currentItem;
			}

			set
			{
				if (currentItem != null)
				{
					currentItem.OnProgressChange -= UpdateProgressbar;
				}

				currentItem = value;
				if (currentItem != null)
				{
					NameAdapter.text = currentItem.Name;
					Progressbar.Value = currentItem.Progress;

					currentItem.OnProgressChange += UpdateProgressbar;
				}
			}
		}

		/// <summary>
		/// Set data.
		/// </summary>
		/// <param name="item">Item.</param>
		public void SetData(Task item)
		{
			Item = item;
		}

		void UpdateProgressbar()
		{
			Progressbar.Animate(currentItem.Progress);
		}

		/// <summary>
		/// Reset current item.
		/// </summary>
		protected override void OnDestroy()
		{
			Item = null;
		}

		/// <summary>
			/// Upgrade this instance.
			/// </summary>
		public override void Upgrade()
		{
#pragma warning disable 0612, 0618
			Utilities.RequireComponent(Name, ref NameAdapter);
#pragma warning restore 0612, 0618
		}
	}
}
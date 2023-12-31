﻿namespace UIWidgets
{
	using UIWidgets.Styles;
	using UnityEngine;

	/// <summary>
	/// Picker for the ListViewCustom.
	/// </summary>
	/// <typeparam name="TListView">Type of the ListView.</typeparam>
	/// <typeparam name="TListViewComponent">Type of the ListView component.</typeparam>
	/// <typeparam name="TValue">Type of the value.</typeparam>
	/// <typeparam name="TPicker">Type of the this picker.</typeparam>
	public class PickerListViewCustom<TListView, TListViewComponent, TValue, TPicker> : PickerOptionalOK<TValue, TPicker>
		where TListView : ListViewCustom<TListViewComponent, TValue>
		where TListViewComponent : ListViewItem
		where TPicker : Picker<TValue, TPicker>
	{
		/// <summary>
		/// ListView.
		/// </summary>
		[SerializeField]
		public TListView ListView;

		/// <inheritdoc/>
		protected override void AddListeners()
		{
			base.AddListeners();
			ListView.OnSelectInternal.AddListener(ListViewCallback);
		}

		/// <inheritdoc/>
		protected override void RemoveListeners()
		{
			base.RemoveListeners();
			ListView.OnSelectInternal.RemoveListener(ListViewCallback);
		}

		/// <summary>
		/// Prepare picker to open.
		/// </summary>
		/// <param name="defaultValue">Default value.</param>
		public override void BeforeOpen(TValue defaultValue)
		{
			base.BeforeOpen(defaultValue);
			ListView.SelectedIndex = ListView.DataSource.IndexOf(defaultValue);
		}

		void ListViewCallback(int index)
		{
			Value = ListView.DataSource[index];

			if (Mode == PickerMode.CloseOnSelect)
			{
				Selected(Value);
			}
		}

		#region IStylable implementation

		/// <inheritdoc/>
		public override bool SetStyle(Style style)
		{
			base.SetStyle(style);
			ListView.SetStyle(style);

			return true;
		}

		/// <inheritdoc/>
		public override bool GetStyle(Style style)
		{
			base.GetStyle(style);
			ListView.GetStyle(style);

			return true;
		}
		#endregion
	}
}
﻿namespace UIWidgets.Examples
{
	using UIWidgets;
	using UnityEngine;

	/// <summary>
	/// GroupedListView
	/// </summary>
	public class GroupedListView : ListViewCustomHeight<GroupedListViewComponent, IGroupedListItem>
	{
		/// <summary>
		/// Template selector.
		/// </summary>
		protected class Selector : IListViewTemplateSelector<GroupedListViewComponent, IGroupedListItem>
		{
			/// <summary>
			/// Group template.
			/// </summary>
			public GroupedListViewComponent GroupTemplate;

			/// <summary>
			/// Item template.
			/// </summary>
			public GroupedListViewComponent ItemTemplate;

			/// <inheritdoc/>
			public GroupedListViewComponent[] AllTemplates()
			{
				return new[] { GroupTemplate, ItemTemplate };
			}

			/// <inheritdoc/>
			public GroupedListViewComponent Select(int index, IGroupedListItem item)
			{
				if (item is GroupedListGroup)
				{
					return GroupTemplate;
				}

				return ItemTemplate;
			}
		}

		/// <summary>
		/// Grouped data.
		/// </summary>
		public GroupedItems GroupedData = new GroupedItems();

		/// <summary>
		/// GroupTemplate.
		/// </summary>
		[SerializeField]
		protected GroupedListViewComponent GroupTemplate;

		/// <summary>
		/// ItemTemplate.
		/// </summary>
		[SerializeField]
		protected GroupedListViewComponent ItemTemplate;

		bool isGroupedListViewInited;

		/// <inheritdoc/>
		public override void Init()
		{
			if (isGroupedListViewInited)
			{
				return;
			}

			isGroupedListViewInited = true;

			base.Init();

			GroupedData.GroupComparison = (x, y) => UtilitiesCompare.Compare(x.Name, y.Name);
			GroupedData.Data = DataSource;

			CanSelect = IsItem;
		}

		/// <inheritdoc/>
		protected override IListViewTemplateSelector<GroupedListViewComponent, IGroupedListItem> CreateTemplateSelector()
		{
			return new Selector()
			{
				GroupTemplate = GroupTemplate,
				ItemTemplate = ItemTemplate,
			};
		}

		bool IsItem(int index)
		{
			return DataSource[index] is GroupedListItem;
		}
	}
}
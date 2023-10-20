﻿namespace UIWidgets.Examples
{
	using UIWidgets;
	using UnityEngine;

	/// <summary>
	/// GroupedTileView.
	/// </summary>
	public class GroupedTileViewV2 : GroupedTileViewBase
	{
		/// <summary>
		/// Header template.
		/// </summary>
		[SerializeField]
		protected GroupedTileViewComponentHeader HeaderTemplate;

		/// <summary>
		/// Empty header template.
		/// </summary>
		[SerializeField]
		protected GroupedTileViewComponentEmpty HeaderEmptyTemplate;

		/// <summary>
		/// Item template.
		/// </summary>
		[SerializeField]
		protected GroupedTileViewComponentItem ItemTemplate;

		/// <summary>
		/// Empty item template;
		/// </summary>
		[SerializeField]
		protected GroupedTileViewComponentEmpty ItemEmptyTemplate;

		class Selector : IListViewTemplateSelector<GroupedTileViewComponentBase, Photo>
		{
			GroupedTileViewComponentBase headerTemplate;

			GroupedTileViewComponentBase headerEmptyTemplate;

			GroupedTileViewComponentBase itemTemplate;

			GroupedTileViewComponentBase itemEmptyTemplate;

			GroupedTileViewComponentBase[] templates;

			public Selector(
				GroupedTileViewComponentHeader headerTemplate,
				GroupedTileViewComponentEmpty headerEmptyTemplate,
				GroupedTileViewComponentItem itemTemplate,
				GroupedTileViewComponentEmpty itemEmptyTemplate)
			{
				this.headerTemplate = headerTemplate;
				this.headerEmptyTemplate = headerEmptyTemplate;
				this.itemTemplate = itemTemplate;
				this.itemEmptyTemplate = itemEmptyTemplate;

				templates = new[] { this.headerTemplate, this.headerEmptyTemplate, this.itemTemplate, this.itemEmptyTemplate, };
			}

			public GroupedTileViewComponentBase[] AllTemplates() => templates;

			public GroupedTileViewComponentBase Select(int index, Photo item)
			{
				if (item.IsGroup)
				{
					return item.IsEmpty ? headerEmptyTemplate : headerTemplate;
				}
				else
				{
					return item.IsEmpty ? itemEmptyTemplate : itemTemplate;
				}
			}
		}

		bool isGroupedListViewInited;

		/// <inheritdoc/>
		public override void Init()
		{
			if (isGroupedListViewInited)
			{
				return;
			}

			isGroupedListViewInited = true;

			TemplateSelector = new Selector(HeaderTemplate, HeaderEmptyTemplate, ItemTemplate, ItemEmptyTemplate);

			base.Init();

			GroupedData.GroupComparison = (x, y) => x.Created.CompareTo(y.Created);
			GroupedData.Data = DataSource;

			GroupedData.EmptyGroupItem = new Photo() { IsGroup = true, IsEmpty = true };
			GroupedData.EmptyItem = new Photo() { IsEmpty = true };
			GroupedData.ItemsPerBlock = ListRenderer.GetItemsPerBlock();
		}

		/// <inheritdoc/>
		public override void UpdateItems()
		{
			base.UpdateItems();

			GroupedData.ItemsPerBlock = ListRenderer.GetItemsPerBlock();
		}

		/// <inheritdoc/>
		public override void Resize()
		{
			base.Resize();

			GroupedData.ItemsPerBlock = ListRenderer.GetItemsPerBlock();
		}
	}
}
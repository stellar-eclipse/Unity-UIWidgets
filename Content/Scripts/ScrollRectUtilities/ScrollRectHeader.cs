﻿namespace UIWidgets
{
	using UnityEngine;

	/// <summary>
	/// ScrollRectHeader.
	/// </summary>
	[HelpURL("https://ilih.name/unity-assets/UIWidgets/docs/components/scrollrect/scrollrect-header.html")]
	public class ScrollRectHeader : ScrollRectBlock
	{
		/// <inheritdoc/>
		protected override float RevealPosition(float rate)
		{
			if (IsHorizontal)
			{
				return Mathf.Lerp(-MaxSize.x, 0, rate);
			}
			else
			{
				return Mathf.Lerp(MaxSize.y, 0, rate);
			}
		}

		/// <inheritdoc/>
		protected override void InitReveal()
		{
			if (DisplayType != ScrollRectHeaderType.Reveal)
			{
				return;
			}

			if (IsHorizontal)
			{
				Block.anchorMin = new Vector2(1, 0);
				Block.anchorMax = new Vector2(1, 1);
				Block.pivot = new Vector2(1, 0);
				Block.anchoredPosition = Vector2.zero;
			}
			else
			{
				Block.anchorMin = new Vector2(0, 1);
				Block.anchorMax = new Vector2(1, 1);
				Block.pivot = new Vector2(0, 1);
				Block.anchoredPosition = Vector2.zero;
			}
		}

		/// <inheritdoc/>
		protected override void InitLayout()
		{
			if (Layout == null)
			{
				return;
			}

			if (BaseMargin == -1f)
			{
				BaseMargin = IsHorizontal
					? Layout.MarginLeft - MaxSize.x
					: Layout.MarginTop - MaxSize.y;
			}
		}

		/// <inheritdoc/>
		protected override void UpdateLayout(float size)
		{
			if (Layout == null)
			{
				return;
			}

			if (IsHorizontal)
			{
				Layout.MarginLeft = size + BaseMargin;
			}
			else
			{
				Layout.MarginTop = size + BaseMargin;
			}
		}
	}
}
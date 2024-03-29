﻿namespace UIWidgets
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// ScrollRectFooter.
	/// </summary>
	[HelpURL("https://ilih.name/unity-assets/UIWidgets/docs/components/scrollrect/scrollrect-footer.html")]
	public class ScrollRectFooter : ScrollRectBlock
	{
		/// <summary>
		/// Change layout.
		/// </summary>
		[SerializeField]
		protected bool ChangeLayout = false;

		/// <inheritdoc/>
		protected override Vector2 GetPosition()
		{
			var content = ScrollRect.content;
			var result = content.rect.size - (content.anchoredPosition + ScrollRectTransform.rect.size);
			return result;
		}

		/// <inheritdoc/>
		protected override float RevealPosition(float rate)
		{
			if (IsHorizontal)
			{
				return Mathf.Lerp(0, MaxSize.x, rate);
			}
			else
			{
				return Mathf.Lerp(0, MaxSize.y, rate);
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
				Block.anchorMin = new Vector2(0, 0);
				Block.anchorMax = new Vector2(0, 1);
				Block.pivot = new Vector2(1, 0);
				Block.anchoredPosition = Vector2.zero;
			}
			else
			{
				Block.anchorMin = new Vector2(0, 0);
				Block.anchorMax = new Vector2(1, 0);
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
					? Layout.MarginRight - MaxSize.x
					: Layout.MarginBottom - MaxSize.y;
			}
		}

		/// <inheritdoc/>
		protected override void UpdateLayout(float size)
		{
			if (!ChangeLayout)
			{
				return;
			}

			if (Layout == null)
			{
				return;
			}

			if (IsHorizontal)
			{
				Layout.MarginRight = size + BaseMargin;
			}
			else
			{
				Layout.MarginBottom = size + BaseMargin;
			}
		}
	}
}
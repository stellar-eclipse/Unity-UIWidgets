namespace UIWidgets
{
	using System.Collections.Generic;
	using UIWidgets.Attributes;
	using UIWidgets.Styles;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// ColorPicker.
	/// </summary>
	[DataBindSupport]
	[HelpURL("https://ilih.name/unity-assets/UIWidgets/docs/widgets/input/colorpicker.html")]
	public class ColorPicker : MonoBehaviour, IStylable
	{
		/// <summary>
		/// IDs of ColorPicker shader properties.
		/// </summary>
		public static class ShaderIDs
		{
			/// <summary>
			/// Left color ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Left = Shader.PropertyToID("_ColorLeft");

			/// <summary>
			/// Right color ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Right = Shader.PropertyToID("_ColorRight");

			/// <summary>
			/// Top color ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Top = Shader.PropertyToID("_ColorTop");

			/// <summary>
			/// Bottom color ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Bottom = Shader.PropertyToID("_ColorBottom");

			/// <summary>
			/// Quality ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Quality = Shader.PropertyToID("_Quality");

			/// <summary>
			/// Value ID.
			/// </summary>
			[DomainReloadExclude]
			public static readonly int Value = Shader.PropertyToID("_Value");
		}

		/// <summary>
		/// Value limit in HSV gradients.
		/// </summary>
		public const int ValueLimit = 80;

		[SerializeField]
		ColorPickerRGBPalette rgbPalette;

		/// <summary>
		/// Gets or sets the RGB palette.
		/// </summary>
		/// <value>The RGB palette.</value>
		public ColorPickerRGBPalette RGBPalette
		{
			get => rgbPalette;

			set
			{
				if (rgbPalette != null)
				{
					rgbPalette.OnChangeRGB.RemoveListener(ColorChanged);
				}

				rgbPalette = value;

				if (rgbPalette != null)
				{
					rgbPalette.OnChangeRGB.AddListener(ColorChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerRGBBlock rgbBlock;

		/// <summary>
		/// Gets or sets the RGB sliders block.
		/// </summary>
		/// <value>The RGB sliders block.</value>
		public ColorPickerRGBBlock RGBBlock
		{
			get => rgbBlock;

			set
			{
				if (rgbBlock != null)
				{
					rgbBlock.OnChangeRGB.RemoveListener(ColorChanged);
				}

				rgbBlock = value;

				if (rgbBlock != null)
				{
					rgbBlock.OnChangeRGB.AddListener(ColorChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerHSVPalette hsvPalette;

		/// <summary>
		/// Gets or sets the HSV palette.
		/// </summary>
		/// <value>The HSV palette.</value>
		public ColorPickerHSVPalette HSVPalette
		{
			get => hsvPalette;

			set
			{
				if (hsvPalette != null)
				{
					hsvPalette.OnChangeHSV.RemoveListener(ColorHSVChanged);
				}

				hsvPalette = value;

				if (hsvPalette != null)
				{
					hsvPalette.OnChangeHSV.AddListener(ColorHSVChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerHSVBlock hsvBlock;

		/// <summary>
		/// Gets or sets the HSV sliders block.
		/// </summary>
		/// <value>The HSV sliders block.</value>
		public ColorPickerHSVBlock HSVBlock
		{
			get => hsvBlock;

			set
			{
				if (hsvBlock != null)
				{
					hsvBlock.OnChangeHSV.RemoveListener(ColorHSVChanged);
				}

				hsvBlock = value;

				if (hsvBlock != null)
				{
					hsvBlock.OnChangeHSV.AddListener(ColorHSVChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerABlock aBlock;

		/// <summary>
		/// Gets or sets Alpha slider block.
		/// </summary>
		/// <value>Alpha slider block.</value>
		public ColorPickerABlock ABlock
		{
			get => aBlock;

			set
			{
				if (aBlock != null)
				{
					aBlock.OnChangeAlpha.RemoveListener(ColorAlphaChanged);
				}

				aBlock = value;

				if (aBlock != null)
				{
					aBlock.OnChangeAlpha.AddListener(ColorAlphaChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerHexBlockBase hexBlock;

		/// <summary>
		/// Gets or sets Alpha slider block.
		/// </summary>
		/// <value>Alpha slider block.</value>
		public ColorPickerHexBlockBase HexBlock
		{
			get => hexBlock;

			set
			{
				if (hexBlock != null)
				{
					hexBlock.OnChangeRGB.RemoveListener(ColorChanged);
				}

				hexBlock = value;

				if (hexBlock != null)
				{
					hexBlock.OnChangeRGB.AddListener(ColorChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerColorBlock colorView;

		/// <summary>
		/// Gets or sets the color view.
		/// </summary>
		/// <value>The color view.</value>
		public ColorPickerColorBlock ColorView
		{
			get => colorView;

			set
			{
				colorView = value;
				if (colorView != null)
				{
					colorView.SetColor(color);
				}
			}
		}

		[SerializeField]
		ColorPickerImagePalette imagePalette;

		/// <summary>
		/// Gets or sets the image palette.
		/// </summary>
		/// <value>The image palette.</value>
		public ColorPickerImagePalette ImagePalette
		{
			get => imagePalette;

			set
			{
				if (imagePalette != null)
				{
					imagePalette.OnChangeRGB.RemoveListener(ColorChanged);
				}

				imagePalette = value;

				if (imagePalette != null)
				{
					imagePalette.OnChangeRGB.AddListener(ColorChanged);
				}
			}
		}

		[SerializeField]
		ColorPickerInputMode inputMode;

		/// <summary>
		/// Gets or sets the input mode.
		/// </summary>
		/// <value>The input mode.</value>
		public ColorPickerInputMode InputMode
		{
			get => inputMode;

			set
			{
				inputMode = value;
				if (rgbPalette != null)
				{
					rgbPalette.InputMode = inputMode;
				}

				if (hsvPalette != null)
				{
					hsvPalette.InputMode = inputMode;
				}

				if (imagePalette != null)
				{
					imagePalette.InputMode = inputMode;
				}

				if (rgbBlock != null)
				{
					rgbBlock.InputMode = inputMode;
				}

				if (hsvBlock != null)
				{
					hsvBlock.InputMode = inputMode;
				}

				if (aBlock != null)
				{
					aBlock.InputMode = inputMode;
				}

				if (hexBlock != null)
				{
					hexBlock.InputMode = inputMode;
				}
			}
		}

		[SerializeField]
		ColorPickerPaletteMode paletteMode;

		/// <summary>
		/// Gets or sets the palette mode.
		/// </summary>
		/// <value>The palette mode.</value>
		public ColorPickerPaletteMode PaletteMode
		{
			get => paletteMode;

			set
			{
				paletteMode = value;

				if (rgbPalette != null)
				{
					rgbPalette.PaletteMode = paletteMode;
				}

				if (hsvPalette != null)
				{
					hsvPalette.PaletteMode = paletteMode;
				}

				if (imagePalette != null)
				{
					imagePalette.PaletteMode = paletteMode;
				}

				if (rgbBlock != null)
				{
					rgbBlock.PaletteMode = paletteMode;
				}

				if (hsvBlock != null)
				{
					hsvBlock.PaletteMode = paletteMode;
				}

				if (aBlock != null)
				{
					aBlock.PaletteMode = paletteMode;
				}

				if (hexBlock != null)
				{
					hexBlock.PaletteMode = paletteMode;
				}
			}
		}

		[SerializeField]
		Color32 color = new Color32(255, 255, 255, 255);

		/// <summary>
		/// Gets or sets the color32.
		/// </summary>
		/// <value>The color32.</value>
		public Color32 Color32
		{
			get => color;

			set
			{
				var is_changed = (Color)color != (Color)value;

				color = value;
				UpdateBlocks(color);

				if (is_changed)
				{
					OnChange.Invoke(color);
				}
			}
		}

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		[DataBindField]
		public Color Color
		{
			get => Color32;

			set => Color32 = value;
		}

		/// <summary>
		/// OnChange color event.
		/// </summary>
		[DataBindEvent("Color", "Color32")]
		public ColorRGBChangedEvent OnChange = new ColorRGBChangedEvent();

		/// <summary>
		/// Start this instance.
		/// </summary>
		public virtual void Start()
		{
			Init();
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		public virtual void Init()
		{
			if (rgbPalette != null)
			{
				RGBPalette = rgbPalette;
				rgbPalette.Init();
			}

			if (hsvPalette != null)
			{
				HSVPalette = hsvPalette;
				hsvPalette.Init();
			}

			if (imagePalette != null)
			{
				ImagePalette = imagePalette;
				imagePalette.Init();
			}

			if (rgbBlock != null)
			{
				RGBBlock = rgbBlock;
				rgbBlock.Init();
			}

			if (hsvBlock != null)
			{
				HSVBlock = hsvBlock;
				hsvBlock.Init();
			}

			if (aBlock != null)
			{
				ABlock = aBlock;
				aBlock.Init();
			}

			if (hexBlock != null)
			{
				HexBlock = hexBlock;
				hexBlock.Init();
			}

			InputMode = inputMode;
			PaletteMode = paletteMode;

			UpdateBlocks(color);
		}

		/// <summary>
		/// Update color blocks after enabling.
		/// </summary>
		protected virtual void OnEnable()
		{
			UpdateBlocks(color);
		}

		/// <summary>
		/// Update color and blocks with specified color.
		/// </summary>
		/// <param name="newColor">New color.</param>
		protected virtual void ColorChanged(Color32 newColor)
		{
			color = newColor;
			UpdateBlocks(color);

			OnChange.Invoke(color);
		}

		/// <summary>
		/// Update color and blocks with specified color.
		/// </summary>
		/// <param name="newColor">New color.</param>
		protected virtual void ColorHSVChanged(ColorHSV newColor)
		{
			color = newColor;
			UpdateBlocks(newColor);

			OnChange.Invoke(color);
		}

		/// <summary>
		/// Update color and blocks with specified alpha.
		/// </summary>
		/// <param name="alpha">Alpha.</param>
		protected virtual void ColorAlphaChanged(byte alpha)
		{
			color.a = alpha;

			UpdateBlocks(color);

			OnChange.Invoke(color);
		}

		/// <summary>
		/// Update blocks with specified color.
		/// </summary>
		/// <param name="newColor">New color.</param>
		protected virtual void UpdateBlocks(Color32 newColor)
		{
			if (colorView != null)
			{
				colorView.SetColor(newColor);
			}

			if (rgbPalette != null)
			{
				rgbPalette.SetColor(newColor);
			}

			if (hsvPalette != null)
			{
				hsvPalette.SetColor(newColor);
			}

			if (imagePalette != null)
			{
				imagePalette.SetColor(newColor);
			}

			if (rgbBlock != null)
			{
				rgbBlock.SetColor(newColor);
			}

			if (hsvBlock != null)
			{
				hsvBlock.SetColor(newColor);
			}

			if (aBlock != null)
			{
				aBlock.SetColor(newColor);
			}

			if (hexBlock != null)
			{
				hexBlock.SetColor(newColor);
			}
		}

		/// <summary>
		/// Update blocks with specified color.
		/// </summary>
		/// <param name="newColor">New color.</param>
		protected virtual void UpdateBlocks(ColorHSV newColor)
		{
			if (colorView != null)
			{
				colorView.SetColor(newColor);
			}

			if (rgbPalette != null)
			{
				rgbPalette.SetColor(newColor);
			}

			if (hsvPalette != null)
			{
				hsvPalette.SetColor(newColor);
			}

			if (imagePalette != null)
			{
				imagePalette.SetColor(newColor);
			}

			if (hsvBlock != null)
			{
				hsvBlock.SetColor(newColor);
			}

			if (rgbBlock != null)
			{
				rgbBlock.SetColor(newColor);
			}

			if (aBlock != null)
			{
				aBlock.SetColor(newColor);
			}

			if (hexBlock != null)
			{
				hexBlock.SetColor(newColor);
			}
		}

		/// <summary>
		/// Toggles the input mode.
		/// </summary>
		public void ToggleInputMode()
		{
			InputMode = (InputMode == ColorPickerInputMode.RGB)
				? ColorPickerInputMode.HSV
				: ColorPickerInputMode.RGB;
		}

		[DomainReloadExclude]
		static readonly List<ColorPickerPaletteMode> RGBPaletteModes = new List<ColorPickerPaletteMode>()
		{
			ColorPickerPaletteMode.Red,
			ColorPickerPaletteMode.Green,
			ColorPickerPaletteMode.Blue,
		};

		[DomainReloadExclude]
		static readonly List<ColorPickerPaletteMode> HSVPaletteModes = new List<ColorPickerPaletteMode>()
		{
			ColorPickerPaletteMode.Hue,
			ColorPickerPaletteMode.Saturation,
			ColorPickerPaletteMode.Value,
			ColorPickerPaletteMode.HSVCircle,
		};

		[DomainReloadExclude]
		static readonly List<ColorPickerPaletteMode> ImagePaletteModes = new List<ColorPickerPaletteMode>()
		{
			ColorPickerPaletteMode.Image,
		};

		/// <summary>
		/// Toggles the palette mode.
		/// </summary>
		public void TogglePaletteMode()
		{
			var paletteModes = new List<ColorPickerPaletteMode>();
			if (rgbPalette != null)
			{
				paletteModes.AddRange(RGBPaletteModes);
			}

			if (hsvPalette != null)
			{
				paletteModes.AddRange(HSVPaletteModes);
			}

			if ((imagePalette != null) && (imagePalette.Image.sprite != null))
			{
				paletteModes.AddRange(ImagePaletteModes);
			}

			if (paletteModes.Count == 0)
			{
				return;
			}

			var next_index = paletteModes.IndexOf(PaletteMode) + 1;
			if (next_index == paletteModes.Count)
			{
				next_index = 0;
			}

			PaletteMode = paletteModes[next_index];
		}

		/// <summary>
		/// Remove listeners.
		/// </summary>
		protected virtual void OnDestroy()
		{
			RGBPalette = null;
			HSVPalette = null;
			ImagePalette = null;

			RGBBlock = null;
			HSVBlock = null;

			ABlock = null;
			HexBlock = null;

			ColorView = null;
		}

		#region IStylable implementation

		/// <inheritdoc/>
		public virtual bool SetStyle(Style style)
		{
			style.ColorPicker.Background.ApplyTo(GetComponent<Image>());

			if (rgbPalette != null)
			{
				rgbPalette.SetStyle(style.ColorPicker, style);
			}

			if (rgbBlock != null)
			{
				rgbBlock.SetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.ApplyTo(rgbBlock.transform.Find("R/Text"));
				style.ColorPicker.InputChannelLabel.ApplyTo(rgbBlock.transform.Find("G/Text"));
				style.ColorPicker.InputChannelLabel.ApplyTo(rgbBlock.transform.Find("B/Text"));
			}

			if (hsvPalette != null)
			{
				hsvPalette.SetStyle(style.ColorPicker, style);
			}

			if (hsvBlock != null)
			{
				hsvBlock.SetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.ApplyTo(hsvBlock.transform.Find("H/Text"));
				style.ColorPicker.InputChannelLabel.ApplyTo(hsvBlock.transform.Find("S/Text"));
				style.ColorPicker.InputChannelLabel.ApplyTo(hsvBlock.transform.Find("V/Text"));
			}

			if (aBlock != null)
			{
				aBlock.SetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.ApplyTo(aBlock.transform.Find("A/Text"));
			}

			if (imagePalette != null)
			{
				imagePalette.SetStyle(style.ColorPicker, style);
			}

			if (hexBlock != null)
			{
				hexBlock.SetStyle(style.ColorPicker, style);
			}

			style.ColorPicker.PaletteToggle.ApplyTo(transform.Find("PaletteToggle/Toggle"));
			style.ColorPicker.InputToggle.ApplyTo(transform.Find("InputToggle/Toggle"));

			return true;
		}

		/// <inheritdoc/>
		public virtual bool GetStyle(Style style)
		{
			style.ColorPicker.Background.GetFrom(GetComponent<Image>());

			if (rgbPalette != null)
			{
				rgbPalette.GetStyle(style.ColorPicker, style);
			}

			if (rgbBlock != null)
			{
				rgbBlock.GetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.GetFrom(rgbBlock.transform.Find("R/Text"));
				style.ColorPicker.InputChannelLabel.GetFrom(rgbBlock.transform.Find("G/Text"));
				style.ColorPicker.InputChannelLabel.GetFrom(rgbBlock.transform.Find("B/Text"));
			}

			if (hsvPalette != null)
			{
				hsvPalette.GetStyle(style.ColorPicker, style);
			}

			if (hsvBlock != null)
			{
				hsvBlock.GetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.GetFrom(hsvBlock.transform.Find("H/Text"));
				style.ColorPicker.InputChannelLabel.GetFrom(hsvBlock.transform.Find("S/Text"));
				style.ColorPicker.InputChannelLabel.GetFrom(hsvBlock.transform.Find("V/Text"));
			}

			if (aBlock != null)
			{
				aBlock.GetStyle(style.ColorPicker, style);

				style.ColorPicker.InputChannelLabel.GetFrom(aBlock.transform.Find("A/Text"));
			}

			if (imagePalette != null)
			{
				imagePalette.GetStyle(style.ColorPicker, style);
			}

			if (hexBlock != null)
			{
				hexBlock.GetStyle(style.ColorPicker, style);
			}

			style.ColorPicker.PaletteToggle.GetFrom(transform.Find("PaletteToggle/Toggle"));
			style.ColorPicker.InputToggle.GetFrom(transform.Find("InputToggle/Toggle"));

			return true;
		}
		#endregion
	}
}
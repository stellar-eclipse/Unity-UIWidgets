namespace UIWidgets.Examples
{
	using UIWidgets;
	using UnityEngine;

	/// <summary>
	/// Test filter.
	/// </summary>
	public class TestFilter : MonoBehaviour
	{
		/// <summary>
		/// ListView.
		/// </summary>
		[SerializeField]
		public ListViewIcons ListView;

		/// <summary>
		/// InputField.
		/// </summary>
		[SerializeField]
		public InputFieldAdapter InputField;

		ObservableListFilter<ListViewIconsItemDescription> Filter;

		/// <summary>
		/// Process the start event.
		/// </summary>
		protected void Start()
		{
			Filter = new ObservableListFilter<ListViewIconsItemDescription>(ListView.DataSource, Predicate);
			ListView.DataSource = Filter.Output;
			InputField.onValueChanged.AddListener(InputFieldChanged);
		}

		/// <summary>
		/// Process the destroy event.
		/// </summary>
		protected void OnDestroy()
		{
			if (InputField != null)
			{
				InputField.onValueChanged.RemoveListener(InputFieldChanged);
			}
		}

		void InputFieldChanged(string ignore) => Filter.Refresh();

		bool Predicate(ListViewIconsItemDescription item)
		{
			var name = item.LocalizedName ?? item.Name;
			return UtilitiesCompare.Contains(name, InputField.Value, false);
		}

		/// <summary>
		/// Add new item
		/// </summary>
		public void Add()
		{
			var name = string.Format("Item {0}", Filter.Input.Count.ToString());
			Filter.Input.Add(new ListViewIconsItemDescription() { Name = name });
		}
	}
}
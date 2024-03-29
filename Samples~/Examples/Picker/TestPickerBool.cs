﻿namespace UIWidgets.Examples
{
	using System;
	using UIWidgets;
	using UnityEngine;
	using UnityEngine.Serialization;
	using UnityEngine.UI;

	/// <summary>
	/// Test PickerBool.
	/// </summary>
	public class TestPickerBool : MonoBehaviour, IUpgradeable
	{
		/// <summary>
		/// PickerBool template.
		/// </summary>
		[SerializeField]
		protected PickerBool PickerTemplate;

		/// <summary>
		/// Text component to display selected value.
		/// </summary>
		[SerializeField]
		[HideInInspector]
		[Obsolete("Replaced with InfoAdapter.")]
		protected Text Info;

		/// <summary>
		/// Text component to display selected value.
		/// </summary>
		[SerializeField]
		protected TextAdapter InfoAdapter;

		/// <summary>
		/// Current value.
		/// </summary>
		[NonSerialized]
		[FormerlySerializedAs("currentValue")]
		protected bool CurrentValue;

		/// <summary>
		/// Show picker and log selected value.
		/// </summary>
		public async void Test()
		{
			// create picker from template
			var picker = PickerTemplate.Clone();

			picker.SetMessage("Confirmation text");

			// show picker
			var result = await picker.ShowAsync(CurrentValue);
			if (result)
			{
				ValueSelected(result);
			}
			else
			{
				Canceled();
			}
		}

		/// <summary>
		/// Show picker and log selected value.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0603:Delegate allocation from a method group", Justification = "Required")]
		public void TestCallbacks()
		{
			// create picker from template
			var picker = PickerTemplate.Clone();

			picker.SetMessage("Confirmation text");

			// show picker
			picker.Show(CurrentValue, ValueSelected, Canceled);
		}

		void ValueSelected(bool value)
		{
			CurrentValue = value;
			Debug.Log(string.Format("value: {0}", value.ToString()));
		}

		void Canceled()
		{
			Debug.Log("canceled");
		}

		/// <summary>
		/// Show picker and display selected value.
		/// </summary>
		public async void TestShow()
		{
			// create picker from template
			var picker = PickerTemplate.Clone();

			picker.SetMessage("Confirmation text");

			// show picker
			var result = await picker.ShowAsync(CurrentValue);
			if (result.Success)
			{
				CurrentValue = result;
				InfoAdapter.text = string.Format("Value: {0}", result.ToString());
			}
			else
			{
				InfoAdapter.text = "Canceled";
			}
		}

		/// <summary>
		/// Upgrade this instance.
		/// </summary>
		public virtual void Upgrade()
		{
#pragma warning disable 0612, 0618
			Utilities.RequireComponent(Info, ref InfoAdapter);
#pragma warning restore 0612, 0618
		}

#if UNITY_EDITOR
		/// <summary>
		/// Validate this instance.
		/// </summary>
		protected virtual void OnValidate()
		{
			Compatibility.Upgrade(this);
		}
#endif
	}
}
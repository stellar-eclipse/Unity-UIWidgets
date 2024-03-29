#if UNITY_EDITOR && UIWIDGETS_TMPRO_SUPPORT
namespace UIWidgets
{
	using System;
	using System.Collections.Generic;
	using UIWidgets.Pool;
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// Converter functions to replace component with another component.
	/// </summary>
	public partial class ConverterTMPro
	{
		/// <summary>
		/// Replacement info.
		/// </summary>
		/// <typeparam name="T">Component type.</typeparam>
		protected class ReplacementInfo<T>
			where T : Component
		{
			/// <summary>
			/// Property with reference to the deleted component.
			/// </summary>
			public readonly struct PropertyReference
			{
				/// <summary>
				/// Target.
				/// </summary>
				readonly Component Target;

				/// <summary>
				/// Property path.
				/// </summary>
				readonly string PropertyPath;

				/// <summary>
				/// Initializes a new instance of the <see cref="PropertyReference"/> struct.
				/// </summary>
				/// <param name="target">Serialized component.</param>
				/// <param name="propertyPath">Property path.</param>
				public PropertyReference(Component target, string propertyPath)
				{
					Target = target;
					PropertyPath = propertyPath;
				}

				/// <summary>
				/// Set new reference to the target field.
				/// </summary>
				/// <param name="newReference">New reference.</param>
				/// <param name="cache">Cache.</param>
				public readonly void Set(Component newReference, SerializedObjectCache cache)
				{
					var so = cache.Get(Target);
					so.FindProperty(PropertyPath).objectReferenceValue = newReference;
					UtilitiesEditor.ApplyModifiedProperties(so);
				}
			}

			/// <summary>
			/// Count of the components to convert.
			/// </summary>
			public int Count
			{
				get
				{
					return Components.Count - CannotReplace.Count;
				}
			}

			/// <summary>
			/// Components.
			/// </summary>
			public readonly List<T> Components = new List<T>();

			/// <summary>
			/// Components which cannot be replaced.
			/// </summary>
			public readonly HashSet<T> CannotReplace = new HashSet<T>();

			/// <summary>
			/// Components to delete.
			/// </summary>
			public readonly Dictionary<Component, T> Deleted = new Dictionary<Component, T>();

			/// <summary>
			/// References.
			/// </summary>
			public readonly Dictionary<Component, List<PropertyReference>> References = new Dictionary<Component, List<PropertyReference>>();

			/// <summary>
			/// Components to exclude from processing.
			/// </summary>
			public readonly HashSet<T> Exclude = new HashSet<T>();

			/// <summary>
			/// Errors.
			/// </summary>
			public readonly List<Message> Errors = new List<Message>();

			/// <summary>
			/// Warnings.
			/// </summary>
			public readonly List<Message> Warnings = new List<Message>();

			/// <summary>
			/// Game objects to find references.
			/// </summary>
			public List<GameObject> GameObjectsWithReferences;

			/// <summary>
			/// Fill replaces actions.
			/// </summary>
			public void FillReplaces()
			{
				foreach (var key in Deleted.Keys)
				{
					References[key] = new List<PropertyReference>();
				}
			}

			/// <summary>
			/// Find references to the components and create replacement actions.
			/// </summary>
			/// <typeparam name="TSecondary">Component type.</typeparam>
			/// <param name="cache">Cache.</param>
			public void FindReferences<TSecondary>(SerializedObjectCache cache)
				where TSecondary : Component
			{
				using var _ = ListPool<Component>.Get(out var required);
				using var __ = ListPool<Component>.Get(out var go_components);

				var types_equal = typeof(T) == typeof(TSecondary);

				foreach (var go in GameObjectsWithReferences)
				{
					go.GetComponents(go_components);

					GetMatchedComponents<T, T>(go_components, Deleted, required);
					if ((required.Count > 0) && FindTypeRequiredUsage<T>(go_components))
					{
						foreach (var r in required)
						{
							CannotReplace.Add(Deleted[r]);
						}
					}

					if (!types_equal)
					{
						GetMatchedComponents<T, TSecondary>(go_components, Deleted, required);
						if ((required.Count > 0) && FindTypeRequiredUsage<TSecondary>(go_components))
						{
							foreach (var r in required)
							{
								CannotReplace.Add(Deleted[r]);
							}
						}
					}

					foreach (var component in go_components)
					{
						if ((component == null) || Deleted.ContainsKey(component))
						{
							continue;
						}

						FindReferencesInComponent(component, cache);
					}

					go_components.Clear();
				}
			}

			/// <summary>
			/// Check is source type is equal to target type or derived from it.
			/// </summary>
			/// <param name="source">source type.</param>
			/// <param name="target">Target type.</param>
			/// <returns>true if types are similar; otherwise false.</returns>
			protected static bool IsSimilarTypes(Type source, Type target)
			{
				if (target == null)
				{
					return false;
				}

				return (target == source) || source.IsSubclassOf(target);
			}

			/// <summary>
			/// Get all component of the TCheck type and contained in the referenceComponents.
			/// </summary>
			/// <typeparam name="TComponent">Component type.</typeparam>
			/// <typeparam name="TCheck">Checked type.</typeparam>
			/// <param name="components">Components.</param>
			/// <param name="referenceComponents">Reference components.</param>
			/// <param name="matched">Matched components.</param>
			protected static void GetMatchedComponents<TComponent, TCheck>(List<Component> components, Dictionary<Component, TComponent> referenceComponents, List<Component> matched)
				where TComponent : Component
				where TCheck : Component
			{
				matched.Clear();
				foreach (var component in components)
				{
					var typed = component as TCheck;
					if ((typed != null) && referenceComponents.ContainsKey(typed))
					{
						matched.Add(typed);
					}
				}
			}

			/// <summary>
			/// Find type usage by RequireComponent attribute.
			/// </summary>
			/// <typeparam name="TRequire">Type.</typeparam>
			/// <param name="components">Components with attributes to check.</param>
			/// <returns>true if any components has RequireComponent attribute of the specified type.</returns>
			protected bool FindTypeRequiredUsage<TRequire>(List<Component> components)
				where TRequire : Component
			{
				var type = typeof(TRequire);
				var template = "<b>{1}:</b> found <i>[RequireComponent(typeof({0}))]</i> use.\n{2} cannot be replaced automatically.\n<i>Recommendation:</i> temporarily comment out a <i>[RequireComponent(typeof({0})]</i> line.";

				var has_required = false;
				foreach (var component in components)
				{
					if (component == null)
					{
						continue;
					}

					var attrs = component.GetType().GetCustomAttributes(typeof(RequireComponent), true);
					foreach (var attr in attrs)
					{
						if (attr is RequireComponent r)
						{
							if (IsSimilarTypes(type, r.m_Type0))
							{
								var message = string.Format(template, r.m_Type0.Name, Utilities.GameObjectPath(component.gameObject), type.Name);
								Warnings.Add(new Message(message, component));
								has_required = true;
							}

							if (IsSimilarTypes(type, r.m_Type1))
							{
								var message = string.Format(template, r.m_Type1.Name, Utilities.GameObjectPath(component.gameObject), type.Name);
								Warnings.Add(new Message(message, component));
								has_required = true;
							}

							if (IsSimilarTypes(type, r.m_Type2))
							{
								var message = string.Format(template, r.m_Type2.Name, Utilities.GameObjectPath(component.gameObject), type.Name);
								Warnings.Add(new Message(message, component));
								has_required = true;
							}
						}
					}
				}

				return has_required;
			}

			/// <summary>
			/// Find references in the specified component to any reference components and create actions to replace references.
			/// </summary>
			/// <param name="component">Component with references.</param>
			/// <param name="cache">Cache.</param>
			protected void FindReferencesInComponent(Component component, SerializedObjectCache cache)
			{
				var template = "<b>{0}:</b> reference cannot be replaced automatically;\n<b>Component:</b> {1}; <b>Property:</b> {2}.\n<i>Recommendation:</i> replace the type of the referenced field with TextAdapter or InputFieldAdapter\n or temporarily remove the reference.";

				if (component == null)
				{
					return;
				}

				var serialized = cache.Get(component);
				var property = serialized.GetIterator();

				while (property.NextVisible(true))
				{
					if (property.propertyType != SerializedPropertyType.ObjectReference)
					{
						continue;
					}

					var reference = property.objectReferenceValue as Component;
					if (reference == null)
					{
						continue;
					}

					if (Deleted.ContainsKey(reference))
					{
						var type = component.GetType();
						var field = new FieldData(component, type, property.propertyPath);
						if (field.IsObsolete)
						{
							continue;
						}

						if (!field.TypeReplaceable<T>())
						{
							var message = string.Format(template, Utilities.GameObjectPath(component.gameObject), component.GetType().Name, property.propertyPath);
							CannotReplace.Add(Deleted[reference]);
							Warnings.Add(new Message(message, component));
							continue;
						}

						References[reference].Add(new PropertyReference(component, property.propertyPath));
					}
				}
			}

			/// <summary>
			/// Replace old components with the new components.
			/// </summary>
			/// <typeparam name="TNew">New type.</typeparam>
			/// <param name="replace">Replace function.</param>
			/// <param name="progress">Progress function.</param>
			/// <param name="cache">Cache.</param>
			public void Convert<TNew>(Func<T, TNew> replace, Action progress, SerializedObjectCache cache)
				where TNew : Component
			{
				foreach (var old_component in Components)
				{
					if (CannotReplace.Contains(old_component))
					{
						continue;
					}

					var replaces = References[old_component];

					var new_component = replace(old_component);

					for (int i = 0; i < replaces.Count; i++)
					{
						replaces[i].Set(new_component, cache);
					}

					progress();
				}
			}
		}
	}
}
#endif
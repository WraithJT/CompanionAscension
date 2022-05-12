using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using Owlcat.QA.Validation;
using UnityEngine;

namespace CompanionAscension.NewContent.Components
{
	[TypeId("a7dbf6fa022442099fa9ecdd9bfc2664")]
	class CompanionSpellbookMerge : BlueprintFeature, IFeatureSelection
	{
		public ReferenceArrayProxy<BlueprintSpellbook, BlueprintSpellbookReference> AllowedSpellbooks
		{
			get
			{
				return this.m_AllowedSpellbooks;
			}
		}

		public BlueprintSpellList MythicSpellList
		{
			get
			{
				return this.m_MythicSpellList;
			}
		}

		public BlueprintSpellsTable SpellKnownForSpontaneous
		{
			get
			{
				return this.m_SpellKnownForSpontaneous;
			}
		}

		public IEnumerable<IFeatureSelectionItem> Items
		{
			get
			{
				List<IFeatureSelectionItem> result;
				if ((result = this.m_CachedItems) == null)
				{
					result = (this.m_CachedItems = this.CollectItems());
				}
				return result;
			}
		}

		public FeatureGroup GetGroup()
		{
			return FeatureGroup.SelectMythicSpellbook;
		}

		public bool IsIgnorePrerequisites()
		{
			return false;
		}

		public bool IsObligatory()
		{
			return true;
		}

		public bool IsSelectionProhibited(UnitDescriptor unit)
		{
			return false;
		}

		public IEnumerable<IFeatureSelectionItem> ExtractSelectionItems(UnitDescriptor beforeLevelUpUnit, UnitDescriptor previewUnit)
		{
			foreach (IFeatureSelectionItem featureSelectionItem in this.Items)
			{
				BlueprintSpellbook blueprintSpellbook = (BlueprintSpellbook)featureSelectionItem.Param.Blueprint;
				if (blueprintSpellbook == null || previewUnit.GetSpellbook(blueprintSpellbook) != null)
				{
					yield return featureSelectionItem;
				}
			}
			IEnumerator<IFeatureSelectionItem> enumerator = null;
			yield break;
			yield break;
		}

		public bool CanSelect(UnitDescriptor unit, LevelUpState state, FeatureSelectionState selectionState, IFeatureSelectionItem item)
		{
			BlueprintSpellbook blueprintSpellbook = (BlueprintSpellbook)item.Param.Blueprint;
			return blueprintSpellbook == null || (unit.GetSpellbook(blueprintSpellbook) != null && this.AllowedSpellbooks.HasReference(blueprintSpellbook));
		}

		private List<IFeatureSelectionItem> CollectItems()
		{
			List<IFeatureSelectionItem> list = new List<IFeatureSelectionItem>();
			list.Add(new FeatureUIData(this, null));
			foreach (BlueprintSpellbook blueprintSpellbook in this.AllowedSpellbooks)
			{
				if (blueprintSpellbook != null)
				{
					list.Add(new FeatureUIData(this, blueprintSpellbook));
				}
			}
			list = list.Distinct<IFeatureSelectionItem>().ToList<IFeatureSelectionItem>();
			return list;
		}

		public override void ApplyValidation(ValidationContext context, int parentIndex)
		{
			base.ApplyValidation(context, parentIndex);
			if (this.MythicSpellList != null && !this.MythicSpellList.IsMythic)
			{
				context.AddError("MythicSpellList: must be mythic", Array.Empty<object>());
			}
		}

		private List<IFeatureSelectionItem> m_CachedItems;

		[SerializeField]
		public BlueprintSpellbookReference[] m_AllowedSpellbooks;

		[SerializeField]
		[ValidateNotNull]
		public BlueprintSpellListReference m_MythicSpellList;

		[SerializeField]
		[ValidateNotNull]
		[InfoBox("Table should be based on mythic level (not caster level!)")]
		public BlueprintSpellsTableReference m_SpellKnownForSpontaneous;
	}
}

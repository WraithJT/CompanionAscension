//using System;
//using Kingmaker.Blueprints;
//using Kingmaker.Blueprints.Classes;
//using Kingmaker.Blueprints.Classes.Spells;
//using Kingmaker.Blueprints.JsonSystem;
//using Kingmaker.UnitLogic;
//using Owlcat.QA.Validation;
//using UnityEngine;
//using System.Collections.Generic;
//using System.Linq;
//using Kingmaker.Blueprints.Classes.Selection;
//using Kingmaker.UnitLogic.Class.LevelUp;
//using Kingmaker.Utility;

//namespace Kingmaker.Designers.Mechanics.Facts
//{
//	[AllowMultipleComponents]
//	[AllowedOn(typeof(BlueprintFeature), false)]
//	[TypeId("6D471395-1CC2-45C2-A456-04C0BC36D737")]
//	class AddAdditionalSpellbookLevel : UnitFactComponentDelegate
//	{
//		//public BlueprintSpellbook Spellbook
//		//{
//		//	get
//		//	{
//		//		BlueprintSpellbookReference spellbook = this.m_Spellbook;
//		//		if (spellbook == null)
//		//		{
//		//			return null;
//		//		}
//		//		return spellbook.Get();
//		//	}
//		//}

//		//public override void OnActivate()
//		//{
//		//	base.Owner.DemandSpellbook(this.Spellbook).AddBaseLevel();
//		//}

//		//public override void ApplyValidation(ValidationContext context, int parentIndex)
//		//{

//		//	base.ApplyValidation(context, parentIndex);
//		//	if (this.Spellbook != null && this.Spellbook.IsMythic)
//		//	{
//		//		context.AddError("AddSpellbookLevel can't add level to mythic spellbook", Array.Empty<object>());
//		//	}
//		//}

//		//public IEnumerable<IFeatureSelectionItem> Items
//		//{
//		//	get
//		//	{
//		//		List<IFeatureSelectionItem> result;
//		//		if ((result = this.m_CachedItems) == null)
//		//		{
//		//			result = (this.m_CachedItems = this.CollectItems());
//		//		}
//		//		return result;
//		//	}
//		//}

//		//private List<IFeatureSelectionItem> CollectItems()
//		//{
//		//	List<IFeatureSelectionItem> list = new List<IFeatureSelectionItem>();
//		//	list.Add(new FeatureUIData(this, null));
//		//	foreach (BlueprintSpellbook blueprintSpellbook in this.AllowedSpellbooks)
//		//	{
//		//		if (blueprintSpellbook != null)
//		//		{
//		//			list.Add(new FeatureUIData(this, blueprintSpellbook));
//		//		}
//		//	}
//		//	list = list.Distinct<IFeatureSelectionItem>().ToList<IFeatureSelectionItem>();
//		//	return list;
//		//}

//		//[SerializeField]
//		//[ValidateNotNull]
//		//private BlueprintSpellbookReference m_Spellbook;

//		//private List<IFeatureSelectionItem> m_CachedItems;
//	}
//}
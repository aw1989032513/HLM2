﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgRoleInfoSystem
    {

        public static void RegisterUIEvent(this DlgRoleInfo self)
        {
            self.RegisterCloseEvent<DlgRoleInfo>(self.View.E_CloseButton);
            self.View.ES_AttributeItem.RegisterEvent((int)NumericType.Power);
            self.View.ES_AttributeItem1.RegisterEvent((int)NumericType.PhysicalStrength);
            self.View.ES_AttributeItem2.RegisterEvent((int)NumericType.Agile);
            self.View.ES_AttributeItem3.RegisterEvent((int)NumericType.Spirit);
            self.View.E_AttributesLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
            { self.OnAttributeItemRefreshHandler(transform, index); });
        }

        public static void ShowWindow(this DlgRoleInfo self, Entity contextData = null)
        {
            self.Refresh();
        }
        public static void OnAttributeItemRefreshHandler(this DlgRoleInfo self, Transform transform, int index)
        {
            Scroll_Item_attribute scroll_Item_Attribute = self.ScrollItemAttributes[index].BindTrans(transform);
            PlayerNumericConfig config = PlayerNumericConfigCategory.Instance.GetConfigByIndex(index);
            scroll_Item_Attribute.E_attributeNameText.text = config.Name + ":";
            scroll_Item_Attribute.E_attributeValueText.text = config.isPrecent == 0 ?
                UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene()).GetAsLong(config.Id).ToString() :
                $"{UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene()).GetAsFloat(config.Id).ToString("0.00")}%";
        }
        public static void Refresh(this DlgRoleInfo self)
        {
            self.View.ES_AttributeItem.Refresh((int)NumericType.Power);
            self.View.ES_AttributeItem1.Refresh((int)NumericType.PhysicalStrength);
            self.View.ES_AttributeItem2.Refresh((int)NumericType.Agile);
            self.View.ES_AttributeItem3.Refresh((int)NumericType.Spirit);

            NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
            if (numericComponent != null)
            {
                self.View.E_CombatEffectivenessText.text = "战力值:" + numericComponent.GetAsLong((int)NumericType.CombatEffectiveness).ToString();
                self.View.E_AttributePointText.text = numericComponent.GetAsInt((int)NumericType.AttributePoint).ToString();
            }
            
            int count = PlayerNumericConfigCategory.Instance.GetShowConfigCount();
            self.AddUIScrollItems(ref self.ScrollItemAttributes, count);
            self.View.E_AttributesLoopVerticalScrollRect.SetVisible(true, count);
        }

    }
}
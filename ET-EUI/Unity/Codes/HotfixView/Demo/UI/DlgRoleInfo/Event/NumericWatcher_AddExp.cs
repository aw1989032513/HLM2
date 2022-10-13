using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [NumericWatcher((int)NumericType.AttributePoint)]
    [NumericWatcher((int)NumericType.Exp)]
    public class NumericWatcher_AddExp : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }

            unit = args.Parent as Unit;

            if (args.NumericType == (int)NumericType.Exp)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                int level = numericComponent.GetAsInt((int)NumericType.Level);
                if (PlayerLevelConfigCategory.Instance.Contain(level))
                {
                    long needXp = PlayerLevelConfigCategory.Instance.Get(level).NeedExp;
                    if (args.New >= needXp)
                    {
                        RedDotHelper.ShowRedDotNode(unit.ZoneScene(), "UpLevelButton");
                    }
                    else
                    {
                        if (RedDotHelper.IsLogicAlreadyShow(unit.ZoneScene(), "UpLevelButton"))
                        {
                            RedDotHelper.HideRedDotNode(unit.ZoneScene(), "UpLevelButton");
                        }
                    }
                }



            }
            if (args.NumericType == (int)NumericType.AttributePoint)
            {
                if (args.New > 0)
                {
                    RedDotHelper.ShowRedDotNode(unit.ZoneScene(), "AddAttribute");
                }
                else
                {
                    if (RedDotHelper.IsLogicAlreadyShow(unit.ZoneScene(), "AddAttribute"))
                    {
                        RedDotHelper.HideRedDotNode(unit.ZoneScene(), "AddAttribute");
                    }
                }
            }

            //刷新界面
            unit.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.Refresh();
        }
    }
}

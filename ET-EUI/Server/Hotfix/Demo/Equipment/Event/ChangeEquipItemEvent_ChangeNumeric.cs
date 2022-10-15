using ET.EventType;

namespace ET
{
    public class ChangeEquipItemEvent_ChangeNumeric : AEvent<ChangeEquipItem>
    {
        protected override async ETTask Run(ChangeEquipItem args)
        {
            EquipInfoComponent equipInfoComponent = args.item.GetComponent<EquipInfoComponent>();
            if (equipInfoComponent == null)
            {
                Log.Error("equipInfoComponent is null");
                return;
            }
            NumericComponent numericComponent = args.unit.GetComponent<NumericComponent>();
            foreach (var entry in equipInfoComponent.EntryList)
            {
                int numericTypeKey = entry.Key * 10 + 2;
                if (args.equipOp == EquipOp.Load)
                {
                    numericComponent[numericTypeKey] += entry.Value;
                }
                else if (args.equipOp == EquipOp.Unload)
                {
                    numericComponent[numericTypeKey] -= entry.Value;
                }
            }
            await ETTask.CompletedTask;
        }
    }
}

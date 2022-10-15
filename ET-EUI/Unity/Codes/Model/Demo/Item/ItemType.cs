

namespace ET
{
    public enum ItemType
    {
        Weapon = 0, 
        Armor = 1,
        Ring = 2,
        /// <summary>
        /// 道具
        /// </summary>
        Prop = 3,
    }
    public enum ItemOp
    {
        /// <summary>
        /// 增加物品
        /// </summary>
        Add = 0,
        /// <summary>
        /// 移除物品
        /// </summary>
        Remove,
    }
    public enum ItemContainerType
    {
        Bag = 0,//背包容器
        RoleInfo = 1,//角色装备容器
    }

}

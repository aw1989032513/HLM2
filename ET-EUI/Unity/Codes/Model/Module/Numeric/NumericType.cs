namespace ET
{
    public enum NumericType
    {
        Max = 10000,

        Speed = 1000,
        SpeedBase = Speed * 10 + 1,
        SpeedAdd = Speed * 10 + 2,
        SpeedPct = Speed * 10 + 3,//百分比加成 必须是10000+
        SpeedFinalAdd = Speed * 10 + 4,
        SpeedFinalPct = Speed * 10 + 5,


        MaxHp = 1002,
        MaxHpBase = MaxHp * 10 + 1,
        MaxHpAdd = MaxHp * 10 + 2,
        MaxHpPct = MaxHp * 10 + 3,
        MaxHpFinalAdd = MaxHp * 10 + 4,
        MaxHpFinalPct = MaxHp * 10 + 5,


        AOI = 1003,
        AOIBase = AOI * 10 + 1,
        AOIAdd = AOI * 10 + 2,
        AOIPct = AOI * 10 + 3,
        AOIFinalAdd = AOI * 10 + 4,
        AOIFinalPct = AOI * 10 + 5,


        MaxMp = 1004,
        MaxMpBase = MaxMp * 10 + 1,
        MaxMpAdd = MaxMp * 10 + 2,
        MaxMpPct = MaxMp * 10 + 3,
        MaxMpFinalAdd = MaxMp * 10 + 4,
        MaxMpFinalPct = MaxMp * 10 + 5,


        //伤害
        DamageValue = 1011,
        DamageValueBase = DamageValue * 10 + 1,
        DamageValueAdd = DamageValue * 10 + 2,
        DamageValuePct = DamageValue * 10 + 3,
        DamageValueFinalAdd = DamageValue * 10 + 4,
        DamageValueFinalPct = DamageValue + 5,

        //伤害追加
        AdditoinalDdamage = 1012,


        Hp = 1013,  // 生命值
        HpBase = Hp * 10 + 1,
        HpAdd = Hp * 10 + 2,
        HpPct = Hp * 10 + 3,
        HpFinalAdd = Hp * 10 + 4,
        HpFinalPct = Hp * 10 + 5,

        MP = 1014, //法力值
        MPBase = MP * 10 + 1,
        MPAdd = MP * 10 + 2,
        MPPct = MP * 10 + 3,
        MPFinalAdd = MP * 10 + 4,
        MPFinalPct = MP * 10 + 5,


        Armor = 1015, //护甲
        ArmorBase = Armor * 10 + 1,
        ArmorAdd = Armor * 10 + 2,
        ArmorPct = Armor * 10 + 3,
        ArmorFinalAdd = Armor * 10 + 4,
        ArmorFinalPct = Armor * 10 + 5,

        ArmorAddition = 1016, //护甲追加

        Dodge = 1017,           //闪避
        DodgeBase = Dodge * 10 + 1,
        DodgeAdd = Dodge * 10 + 2,
        DodgePct = Dodge * 10 + 3,
        DodgeFinalAdd = Dodge * 10 + 4,
        DodgeFinalPct = Dodge * 10 + 5,

        DodgeAddition = 1018,   // 闪避追加

        CriticalHitRate = 1019, //暴击率
        CriticalHitRateBase = CriticalHitRate * 10 + 1,
        CriticalHitRateAdd = CriticalHitRate * 10 + 2,
        CriticalHitRatePct = CriticalHitRate * 10 + 3,
        CriticalHitRateFinalAdd = CriticalHitRate * 10 + 4,
        CriticalHitRateFinalPct = CriticalHitRate * 10 + 5,


        Power = 3001, //力量
        /// <summary>
        /// 体力
        /// </summary>
        PhysicalStrength = 3002, 

        Agile = 3003, //敏捷值

        Spirit = 3004, //精神

        AttributePoint = 3005, //属性点

        CombatEffectiveness = 3006, //战力值

        Level = 3007,

        Gold = 3008,

        Exp = 3009,

        /// <summary>
        /// 关卡冒险状态
        /// 设置关卡状态AdventureState为levelId，因为关卡状态设为0 表示不在关卡状态
        /// 同时还能从AdventureState获得正在哪个关卡中
        /// </summary>
        AdventureState = 3010,

        /// <summary>
        /// 垂死状态
        /// </summary>
        DyingState = 3011,      

        AdventureStartTime = 3012,   //关卡开始冒险的时间

        IsAlive = 3013,    //存活状态  0为死亡 1为活着


        BattleRandomSeed = 3014,    //战斗随机数种子

        /// <summary>
        /// 背包最大负重
        /// </summary>
        MaxBagCapacity = 3015,

        /// <summary>
        /// 铁矿石
        /// </summary>
        IronStone = 3016,

        /// <summary>
        /// 皮毛
        /// </summary>
        Fur = 3017, 
    }
}

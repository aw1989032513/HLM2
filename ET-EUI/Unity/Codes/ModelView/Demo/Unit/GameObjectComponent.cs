using UnityEngine;

namespace ET
{
    /// <summary>
    /// ÏÔÊ¾GameObject×é¼þ
    /// </summary>
    public class GameObjectComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject;
        public SpriteRenderer SpriteRenderer { get; set; }
    }
}
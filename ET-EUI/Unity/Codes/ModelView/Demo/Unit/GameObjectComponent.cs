using UnityEngine;

namespace ET
{
    /// <summary>
    /// ��ʾGameObject���
    /// </summary>
    public class GameObjectComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject;
    }
}
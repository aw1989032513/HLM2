using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ET
{
    public class HeadHpViewComponent:Entity,IAwake,IDestroy
    {
        public GameObject HpBarGroup = null;
        public SpriteRenderer HpBar = null;
        public TextMeshPro HpText = null;
    }
}

﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    public static class EUIHelper
    {
        
  #region UI辅助方法
       

        public static void AddListenerAsync(this Button button, Func<ETTask> action)
        {
            button.onClick.RemoveAllListeners();
       
            button.onClick.AddListener(()=>
            {
                if (UIEventComponent.Instance==null || UIEventComponent.Instance.IsClicked)
                {
                    return;
                }
                clickActionAsync().Coroutine();
            });
            async ETTask clickActionAsync()//异步内部方法
            {
               UIEventComponent.Instance.SetUIClicked(true);
                await action();
                UIEventComponent.Instance.SetUIClicked(false);
            }
        }

            public static void SetText(this Text Label, string content )
        {
            if (null == Label)
            {
                Log.Error("label is null");
                return;
            }
            Label.text = content;
        }
        
        public static void SetVisible(this UIBehaviour uiBehaviour, bool isVisible)
        {
            if (null == uiBehaviour)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == uiBehaviour.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }
            
            if (uiBehaviour.gameObject.activeSelf == isVisible)
            {
                return;
            }
            uiBehaviour.gameObject.SetActive(isVisible);
        }
        
        
        public static void SetVisible(this LoopScrollRect loopScrollRect,bool isVisible,int count = 0)
        {
            loopScrollRect.gameObject.SetActive(isVisible);
            loopScrollRect.totalCount = count;
            loopScrollRect.RefillCells();
        }

        public static void SetVisible(this Transform transform, bool isVisible)
        {
            if (null == transform)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == transform.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }

            if (transform.gameObject.activeSelf == isVisible)
            {
                return;
            }
            transform.gameObject.SetActive(isVisible);
        }

        public  static void SetTogglesInteractable(this ToggleGroup toggleGroup, bool isEnable)
        {
           var toggles = toggleGroup.transform.GetComponentsInChildren<Toggle>();
           foreach (var toggle in toggles)
           {
               toggle.interactable = isEnable;
           }
        }
        

        public static (int,Toggle) GetSelectedToggle(this ToggleGroup toggleGroup)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                if (togglesList[i].isOn)
                {
                    return (i,togglesList[i]);
                }
            }
            Log.Error("none Toggle is Selected");
            return (-1,null);
        }
        
        
        public static void SetToggleSelected(this ToggleGroup toggleGroup, int index)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                if (i != index)
                {
                    continue;
                }
                togglesList[i].IsSelected(true);
            }
        }
        
        
        public static void IsSelected(this Toggle toggle, bool isSelected)
        {
            toggle.isOn = isSelected;
            toggle.onValueChanged?.Invoke(isSelected);
        }
        
        
        public static void AddUIScrollItems<T>(this Entity self, ref Dictionary<int, T> prefabDictionary, int count)  where T : Entity,IAwake
        {
            if (prefabDictionary == null)
            {
                prefabDictionary = new Dictionary<int, T>();
            }
            
            if (count <= 0)
            {
                return;
            }
            
            foreach (var item in prefabDictionary)
            {
                item.Value.Dispose();
            }
            prefabDictionary.Clear();
            for (int i = 0; i <= count; i++)
            {
                T itemServer = self.AddChild<T>(true);
                prefabDictionary.Add(i , itemServer);
            }
        }
        
        
        public static void RemoveUIScrollItems<T>(this Entity self, ref Dictionary<int, T> dictionary)  where T : Entity
        {
            if (dictionary == null)
            {
                return;
            }
            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }
            dictionary.Clear();
            dictionary = null;
        }
        
        public static void GetUIComponent<T>(this ReferenceCollector rf, string key, ref T t) where T : class
        {
            GameObject obj = rf.Get<GameObject>(key);

            if (obj == null)
            {
                t = null;
                return;
            }

            t = obj.GetComponent<T>();
        }

        #endregion

        #region UI按钮事件
        public static void AddListenerAsyncWithId(this Button button, Func<int, ETTask> action, int id)
        {
            button.onClick.RemoveAllListeners();

            async ETTask clickActionAsync()
            {
                UIEventComponent.Instance?.SetUIClicked(true);
                await action(id);
                UIEventComponent.Instance?.SetUIClicked(false);
            }

            button.onClick.AddListener(() =>
            {
                if (UIEventComponent.Instance == null)
                {
                    return;
                }

                if (UIEventComponent.Instance.IsClicked)
                {
                    return;
                }

                clickActionAsync().Coroutine();
            });
        }
        public static void AddListener(this ToggleGroup toggleGroup, UnityAction<int> selectEventHandler)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                int index = i;
                togglesList[i].AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        selectEventHandler(index);
                    }
                });
            }
        }
        public static void AddListener(this Toggle toggle, UnityAction<bool> selectEventHandler)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(selectEventHandler);
        }
        
        public static void AddListener(this Button button,UnityAction clickEventHandler )
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(clickEventHandler);
        }

        public static void AddListenerWithId(this Button button,Action<int> clickEventHandler ,int id)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(id);  });
        }
        
        public static void AddListenerWithId(this Button button,Action<long> clickEventHandler ,long id)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(id);  });
        }

        public static void AddListenerWithParam<T>(this Button button, Action<T> clickEventHandler, T param)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(param);  });
        }
        
        public static void AddListenerWithParam<T,A>(this Button button, Action<T,A> clickEventHandler, T param1 , A param2)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(param1 , param2);  });
        }
        
        /// <summary>
        /// 注册窗口关闭事件
        /// </summary>
        /// <OtherParam name="self"></OtherParam>
        /// <OtherParam name="closeButton"></OtherParam>
        public static void RegisterCloseEvent(this Entity self,Button closeButton) 
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(self.GetParent<UIBaseWindow>().WindowID); });
        }


        /// <summary>
        /// 注册窗口关闭事件
        /// </summary>
        /// <OtherParam name="self"></OtherParam>
        /// <OtherParam name="closeButton"></OtherParam>
        public static void RegisterCloseEvent<T>(this Entity self, Button closeButton, bool isClose = false) where T : Entity, IAwake, IUILogic
        {
            closeButton.onClick.RemoveAllListeners();
            if (isClose)
            {
                closeButton.onClick.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().CloseWindow(self.GetParent<UIBaseWindow>().WindowID); });

            }
            else
            {
                closeButton.onClick.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(self.GetParent<UIBaseWindow>().WindowID); });
            }
        }
        #endregion

    }
}


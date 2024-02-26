using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace EntityCreator
{
    public class ButtonController : MonoBehaviour
    {
        #region Properties

        public Image border;
        public EventTrigger eventTrigger;

        #endregion

        #region Unity Metods

        private void OnEnable()
        {
            EventDispatcher.RegisterObserver<OnButtonSelectStruct>(OnButtonSelect);
        }
        private void OnDisable()
        {
            EventDispatcher.UnregisterObserver<OnButtonSelectStruct>(OnButtonSelect);
        }
        private void Awake()
        {
            SelectEvent();
            OnPointerEnterEvent();
            OnPointerExitEvent();
        }

        #endregion

        #region Observers

        private void OnButtonSelect(OnButtonSelectStruct obj)
        {
            if (obj.myInstance == gameObject)
                return;

            OnPointerExit(new BaseEventData(EventSystem.current));
        }

        #endregion

        #region Core Metods

        private void SetEventTriggerType(EventTriggerType eventTriggerType, UnityAction<BaseEventData> baseEventData)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventTriggerType;
            entry.callback.AddListener(baseEventData);
            eventTrigger.triggers.Add(entry);
        }
        private void SetSelectedGameObject(GameObject gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        public void SetAsFirstButtonSelected()
        {
            OnPointerEnter(new BaseEventData(EventSystem.current));
        }

        public void ButtonClick(UnityAction<BaseEventData> onClick)
        {
            SetEventTriggerType(EventTriggerType.PointerClick, onClick);
            SetEventTriggerType(EventTriggerType.Submit, onClick);
        }

        private void SelectEvent()
        {
            SetEventTriggerType(EventTriggerType.Select, Select);
        }
        private void Select(BaseEventData baseEventData)
        {
            border.gameObject.SetActive(true);
            EventDispatcher.Dispatch<OnButtonSelectStruct>(new OnButtonSelectStruct { myInstance = gameObject });
        }

        private void OnPointerEnterEvent()
        {
            SetEventTriggerType(EventTriggerType.PointerEnter, OnPointerEnter);
        }
        private void OnPointerEnter(BaseEventData baseEventData)
        {
            border.gameObject.SetActive(true);
            SetSelectedGameObject(gameObject);
        }

        private void OnPointerExitEvent()
        {
            SetEventTriggerType(EventTriggerType.PointerExit, OnPointerExit);
        }
        private void OnPointerExit(BaseEventData baseEventData)
        {
            border.gameObject.SetActive(false);
        }


        #endregion
    }
}


public struct OnButtonSelectStruct
{
    public GameObject myInstance;
}
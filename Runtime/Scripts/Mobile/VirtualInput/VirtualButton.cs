using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Elysium.Input;
using Elysium.Utils.Attributes;

namespace Elysium.Input.Mobile
{
    public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInputBroadcaster<bool>
    {
        [Separator("Output", true)]
        public UnityEvent<bool> buttonStateOutputEvent;
        public UnityEvent buttonClickOutputEvent;
        public event UnityAction<bool> OnBroadcast = delegate { };

        public void OnPointerDown(PointerEventData eventData)
        {
            OutputButtonStateValue(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OutputButtonStateValue(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OutputButtonClickEvent();
        }

        void OutputButtonStateValue(bool buttonState)
        {
            buttonStateOutputEvent.Invoke(buttonState);
            OnBroadcast?.Invoke(buttonState);
        }

        void OutputButtonClickEvent()
        {
            buttonClickOutputEvent.Invoke();
        }

    }
}
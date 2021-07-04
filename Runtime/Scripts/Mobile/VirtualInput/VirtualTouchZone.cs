using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Elysium.Utils.Attributes;

namespace Elysium.Input.Mobile
{
    public class VirtualTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IInputBroadcaster<Vector2>
    {
        [Separator("Rect References", true)]
        [SerializeField] private RectTransform containerRect = default;
        [SerializeField] private RectTransform handleRect = default;

        [Separator("Settings", true)]
        [SerializeField] private bool clampToMagnitude = default;
        [SerializeField] private float magnitudeMultiplier = 1f;
        [SerializeField] private bool invertXOutputValue = default;
        [SerializeField] private bool invertYOutputValue = default;        

        [Separator("Events", true)]
        public UnityEvent<Vector2> touchZoneOutputEvent;
        public event UnityAction<Vector2> OnBroadcast = delegate { };

        //Stored Pointer Values
        private Vector2 pointerDownPosition = default;
        private Vector2 currentPointerPosition = default;

        void Start()
        {
            SetupHandle();
        }

        private void SetupHandle()
        {
            if (handleRect)
            {
                SetObjectActiveState(handleRect.gameObject, false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

            if (handleRect)
            {
                SetObjectActiveState(handleRect.gameObject, true);
                UpdateHandleRectPosition(pointerDownPosition);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);

            Vector2 positionDelta = GetDeltaBetweenPositions(pointerDownPosition, currentPointerPosition);

            Vector2 clampedPosition = ClampValuesToMagnitude(positionDelta);

            Vector2 outputPosition = ApplyInversionFilter(clampedPosition);

            OutputPointerEventValue(outputPosition * magnitudeMultiplier);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pointerDownPosition = Vector2.zero;
            currentPointerPosition = Vector2.zero;

            OutputPointerEventValue(Vector2.zero);

            if (handleRect)
            {
                SetObjectActiveState(handleRect.gameObject, false);
                UpdateHandleRectPosition(Vector2.zero);
            }
        }

        void OutputPointerEventValue(Vector2 pointerPosition)
        {
            touchZoneOutputEvent.Invoke(pointerPosition);
            OnBroadcast?.Invoke(pointerPosition);
        }

        void UpdateHandleRectPosition(Vector2 newPosition)
        {
            handleRect.anchoredPosition = newPosition;
        }

        void SetObjectActiveState(GameObject targetObject, bool newState)
        {
            targetObject.SetActive(newState);
        }

        Vector2 GetDeltaBetweenPositions(Vector2 firstPosition, Vector2 secondPosition)
        {
            return secondPosition - firstPosition;
        }

        Vector2 ClampValuesToMagnitude(Vector2 position)
        {
            return Vector2.ClampMagnitude(position, 1);
        }

        Vector2 ApplyInversionFilter(Vector2 position)
        {
            if (invertXOutputValue)
            {
                position.x = InvertValue(position.x);
            }

            if (invertYOutputValue)
            {
                position.y = InvertValue(position.y);
            }

            return position;
        }

        float InvertValue(float value)
        {
            return -value;
        }

    }
}
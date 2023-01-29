using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public float dampingSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;

    private RectTransform draggingObjectRectTransform;
    private Image cardArtwork;

    void Start() {
        cardArtwork = GetComponent<Image>();
    }

    void Awake() {
        draggingObjectRectTransform = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData) {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition)) {
            draggingObjectRectTransform.position = Vector3.SmoothDamp(draggingObjectRectTransform.position, globalMousePosition, ref velocity, dampingSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        HandleState(CardState.Dragging);

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition)) {
            initialPosition = draggingObjectRectTransform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        HandleState(CardState.Idle);

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition)) {
            draggingObjectRectTransform.position = initialPosition;
        }
    }

    private void HandleState(CardState state) {
        switch (state) {
            case CardState.Idle:
                Opaque();
                break;
            case CardState.Dragging:
                SeeThru();
                break;
        }
    }

    private void SeeThru() {
        Color color = cardArtwork.color;
        color.a = 0.5f;
        cardArtwork.color = color;
    }

    private void Opaque() {
        Color color = cardArtwork.color;
        color.a = 1f;
        cardArtwork.color = color;
    }

    private enum CardState {
        Idle,
        Dragging
    }
}

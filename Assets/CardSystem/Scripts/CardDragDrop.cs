using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CardDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public GameObject gameTilemap;

    public float dampingSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;

    private RectTransform draggingObjectRectTransform;
    private Image cardArtwork;

    private MainController mainController;

    void Start() {
        cardArtwork = GetComponent<Image>();
        GameObject mainControllerObj = GameObject.Find("MainController");
        mainController = mainControllerObj.GetComponent<MainController>();
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

        GridHelper gridHelper = gameTilemap.GetComponent<GridHelper>();

        Vector3Int tilePosition = gridHelper.GetGameTilePosition(globalMousePosition);
        Vector3 tileCenter = gridHelper.GetTileWorldPosition(tilePosition);
        
        TileBase tile = gridHelper.GetTileAt(tilePosition);
        mainController.CreateTurret(tileCenter, tile, GetComponent<Card>().turretObj);
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

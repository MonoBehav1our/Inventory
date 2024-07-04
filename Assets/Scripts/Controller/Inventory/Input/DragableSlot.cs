using System.Collections.Generic;
using UnityEngine.EventSystems;
using Model.Inventory;
using UnityEngine.UI;
using UnityEngine;

namespace Controller.Inventory
{
    public class DragableSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Space]
        [SerializeField] private RectTransform _dragableImage;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private PopupWindow _popupWindow;

        [SerializeField] private EventSystem _eventSystem;
        private PointerEventData _pointerEventData;
        private GraphicRaycaster _raycaster;

        private RectTransform _rectTransform;
        private Vector2 _startPos;

        private InventoryGrid _inventory;
        private int _thisNubmer;

        public void Init(InventoryGrid grid, int thisSlotNumber)
        {
            _inventory = grid;
            _thisNubmer = thisSlotNumber;

            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _raycaster = _canvas.gameObject.GetComponent<GraphicRaycaster>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = _dragableImage.localPosition;
            _dragableImage.parent = _rectTransform.parent;
            _dragableImage.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) => _dragableImage.position = eventData.position;

        public void OnEndDrag(PointerEventData eventData)
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            _raycaster.Raycast(_pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out DragableSlot targetSlot))
                {
                    if (result.gameObject == gameObject) continue;

                    targetSlot.SwapSlots(_thisNubmer);
                }
            }
            _dragableImage.parent = _rectTransform;
            _dragableImage.localPosition = _startPos;
        }

        public void SwapSlots(int secondSlotNubmer) => _inventory.SwapItems(_thisNubmer, secondSlotNubmer);

        public void OnPointerClick(PointerEventData eventData)
        {
            int i = _thisNubmer / _inventory.MainSlotsGrid.GetLength(1);
            int j = _thisNubmer % _inventory.MainSlotsGrid.GetLength(1);

            _popupWindow.Open(_inventory.MainSlotsGrid[i, j]);
        }
    }
}
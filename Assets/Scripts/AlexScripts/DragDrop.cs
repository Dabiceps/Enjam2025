using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private RectTransform canvasRectTransform;

    private Transform originalParent; // Dossier d'origine
    private Vector3 originalPosition; // Position d'origine



    private void Awake()
    {
        // Trouver automatiquement le Canvas si ce n'est pas assign�
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("Pas trouvé le canva, l'objet est-il bien dans un canva?");
            }
        }

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();


    }

    private void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {


        Vector2 localPointerPosition;
        if (canvas != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, canvas.worldCamera, out localPointerPosition))
        {
            // On recup la taille de l'objet et du canvas
            Vector2 objectSize = rectTransform.rect.size;
            Vector2 canvasSize = canvasRectTransform.rect.size;

            // calculer des limites du canva (utile si l'objet est plus grand que le canvas)
            float minX = -canvasSize.x / 2 + objectSize.x / 2;
            float maxX = canvasSize.x / 2 - objectSize.x / 2;
            float minY = -canvasSize.y / 2 + objectSize.y / 2;
            float maxY = canvasSize.y / 2 - objectSize.y / 2;

            // on limite la position de l'objet dans le canvas
            localPointerPosition.x = Mathf.Clamp(localPointerPosition.x, minX, maxX);
            localPointerPosition.y = Mathf.Clamp(localPointerPosition.y, minY, maxY);

            // on applique la position limitée
            rectTransform.anchoredPosition = localPointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }


    public void StartDragDrop ()
    {
        
    }

}
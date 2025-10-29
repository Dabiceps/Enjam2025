using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<GameObject> DraggableItems;
    [SerializeField] private List<RectTransform> ItemsPosition;

    private List<Vector2> initialPositions = new List<Vector2>();

    private bool win = false;
    private bool IsDropZoneActive = false;

    private void Awake()
    {
        // Stocker les positions initiales des éléments déplaçables
        foreach (var item in DraggableItems)
        {
            RectTransform rect = item.GetComponent<RectTransform>();
            initialPositions.Add(rect.anchoredPosition);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (IsDropZoneActive)
        {
            GameObject droppedFile = eventData.pointerDrag;
            if (droppedFile != null)
            {
                bool isGood = droppedFile.CompareTag("gooditem");

                Debug.Log((isGood ? "Fichier accepté: " : "Fichier refusé: ") + droppedFile.name + " de type " + droppedFile.tag);

                droppedFile.GetComponent<DragDrop>().enabled = false;
                droppedFile.GetComponent<CanvasGroup>().alpha = 1f;

                win = isGood;
                IsDropZoneActive = false;
            }
        }
    }

    public bool EndDragDropGame()
    {
        this.gameObject.SetActive(false);
        foreach (var item in DraggableItems)
        {
            item.SetActive(false);
        }
        return win;
    }

    public void StartDragDropGame(int difficultyLevel)
    {
        this.gameObject.SetActive(true);
        IsDropZoneActive = true;
        win = false;

        for (int i = 0; i < DraggableItems.Count; i++)
        {
            GameObject item = DraggableItems[i];
            item.SetActive(true);

            // Réinitialiser la position à celle d'origine
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition = initialPositions[i];

            // Réactiver le drag et l'affichage
            item.GetComponent<DragDrop>().enabled = true;
            item.GetComponent<CanvasGroup>().alpha = 1f;

            // Définir le tag selon la difficulté
            item.tag = (i == difficultyLevel-1) ? "gooditem" : "baditem";
            Debug.Log("Item " + item.name + " set as " + item.tag);
        }
    }
}
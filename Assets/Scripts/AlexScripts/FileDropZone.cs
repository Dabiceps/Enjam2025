using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileDropZone : MonoBehaviour, IDropHandler
{
    [Header("Items draggables")] 
    [SerializeField] private List<GameObject> DraggableItems;
    [SerializeField] private List<RectTransform> ItemsPosition;

    [Header("Images")]
    [SerializeField] private List<GameObject> Images;
    private int image;

    [Header("Positions DropZone")]
    [SerializeField] private List<RectTransform> Positions;

    private List<Vector2> initialPositions = new List<Vector2>();

    private bool win = false;
    private bool IsDropZoneActive = true;

    private void Awake()
    {
        // Stocker les positions initiales des �l�ments d�pla�ables
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
                Debug.Log("test");
                Debug.Log((isGood ? "Fichier accept�: " : "Fichier refus�: ") + droppedFile.name + " de type " + droppedFile.tag);
                droppedFile.GetComponent<CanvasGroup>().alpha = 1f;

                win = isGood;
                IsDropZoneActive = false;
            }
        }
    }

    public bool EndDragDropGame()
    {
        this.gameObject.SetActive(false);
        GameObject img = Images[image];
        img.SetActive(false);
        foreach (var item in DraggableItems)
        {
            item.SetActive(false);
        }
        return win;
    }

    public void StartDragDropGame(int difficultyLevel)
    {
        
        IsDropZoneActive = true;
        win = false;
        image = difficultyLevel - 1;
        GameObject img = Images[image];
        img.SetActive(true);
        

        RectTransform rectTransform = Positions[image];
        this.transform.position = rectTransform.position;
        this.gameObject.SetActive(true);

        for (int i = 0; i < DraggableItems.Count; i++)
        {
            GameObject item = DraggableItems[i];
            item.SetActive(true);


            // R�initialiser la position � celle d'origine
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.anchoredPosition = initialPositions[i];

            // R�activer le drag et l'affichage
            item.GetComponent<DragDrop>().enabled = true;
            item.GetComponent<CanvasGroup>().alpha = 1f;

            // D�finir le tag selon la difficult�
            item.tag = (i == difficultyLevel-1) ? "gooditem" : "baditem";
            Debug.Log("Item " + item.name + " set as " + item.tag);
        }
        
    }
}
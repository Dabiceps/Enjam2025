using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class FileDropZone : MonoBehaviour, IDropHandler
{
    public List<string> acceptedTags; // Tags acceptés

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedFile = eventData.pointerDrag;
        if (droppedFile != null)
        {
            for (int i = 0; i < acceptedTags.Count; i++)
            {
                acceptedTags[i] = acceptedTags[i].ToLower().Trim();
                if (droppedFile.CompareTag(acceptedTags[i]))
                {
                    Debug.Log("Fichier accepte: " + droppedFile.name + " de type " + acceptedTags[i]);
                    Destroy(droppedFile);
                }
                else
                {
                    Debug.Log("Fichier refuse: " + droppedFile.name + " de type " + acceptedTags[i]);
                }
            }

        }
    }
}
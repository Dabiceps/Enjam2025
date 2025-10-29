using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PointAndClickController1 : MonoBehaviour
{
    [SerializeField] private Button bouton1;
    [SerializeField] private Button bouton2;
    [SerializeField] private Button bouton3;
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    public bool pointAndClickVictory;



    private void Awake()
    {
        bouton1.enabled = true;
        bouton2.enabled = false;
        bouton3.enabled = false;
        line1.SetActive(false);
        line2.SetActive(false);
        pointAndClickVictory = false;
        PlayPaC1();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void PlayPaC1()
    {
        bouton1.onClick.AddListener(() =>
        {
            bouton2.enabled = true;
            bouton1.enabled = false;
            bouton1.gameObject.SetActive(false);
        });
        bouton2.onClick.AddListener(() =>
        {
            bouton3.enabled = true;
            bouton2.enabled = false;
            line1.SetActive(true);
            bouton2.gameObject.SetActive(false);
        });
        bouton3.onClick.AddListener(() =>
        {
            bouton3.enabled = false;
            line2.SetActive(true);
            bouton3.gameObject.SetActive(false);
            line1.SetActive(false);
            line2.SetActive(false);
            pointAndClickVictory = true;
        });

    }
}

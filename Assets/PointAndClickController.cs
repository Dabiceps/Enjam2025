using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PointAndClickController : MonoBehaviour
{
    [SerializeField] private Button bouton1;
    [SerializeField] private Button bouton2;
    [SerializeField] private Button bouton3;

    private void Awake()
    {
        bouton1.enabled = true;
        bouton2.enabled = false;
        bouton3.enabled = false;
    }
    private void Init()
    {
        bouton1.enabled = true;
        bouton2.enabled = false;
        bouton3.enabled = false;
        bouton1.gameObject.SetActive(true);
        bouton1.gameObject.SetActive(true);
        bouton1.gameObject.SetActive(true);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
            bouton2.gameObject.SetActive(false);
        });
        bouton3.onClick.AddListener(() =>
        {
            bouton3.enabled = false;
            bouton3.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateButton(int buttonToActivate)
    {
        bouton2.enabled = true;
    }
}

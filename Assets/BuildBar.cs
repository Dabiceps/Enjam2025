using UnityEngine;
using UnityEngine.UI;

public class BuildBar : MonoBehaviour
{
    [SerializeField] private GameObject buildBarEnBasla;
    [SerializeField] private GameObject buildFullBar;
    [SerializeField] private GameObject[] barSucess;
    [SerializeField] private GameObject[] barEchec;
    [SerializeField] private Sprite[] spritesBar;
    [SerializeField] private Image barSpriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // buildBar(0,true);
        // buildBar(1,true);
        // buildBar(2,true);
        // buildBar(3,false);
        // buildBar(4,false);
        // buildBar(5,false);
        // buildBar(6,false);
        // buildBar(7,true);
        // buildBar(8,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildBar(int level, bool sucess, bool full = false)
    {
        if (full == true && sucess == false)
        {
            buildFullBar.SetActive(true);
            buildBarEnBasla.SetActive(false);
        }
        else
        {
            buildFullBar.SetActive(false);
            buildBarEnBasla.SetActive(true);
            barSpriteRenderer.sprite = spritesBar[level];
            if (sucess == true)
            {
                barSucess[level].SetActive(true);
            }
            else
            {
                barEchec[level].SetActive(true);
            }
        }
    }

    public void closeBuildFullBar()
    {
        buildFullBar.SetActive(false);
        buildBarEnBasla.SetActive(true);
    }
    
    public void closeBuildBar()
    {
        buildBarEnBasla.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            barSucess[i].SetActive(false);
            barEchec[i].SetActive(false);
        }
    }
    
}

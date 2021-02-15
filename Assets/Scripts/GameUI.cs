using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject panelOfKnifes;
    [SerializeField]
    private GameObject knifeUI;

    public GameObject restartButton;

    public void CreateKnifesUI(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(knifeUI, panelOfKnifes.transform);
        }
    }

    private int indexOfKnifeUsed = 0;
    public void KnifeUsed()
    {
        panelOfKnifes.transform.GetChild(indexOfKnifeUsed++).GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
    }
}

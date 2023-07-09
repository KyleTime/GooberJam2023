using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> buttonList;

    public GameObject levelSelectMenu;
    GameObject mainMenu;

    int currentButton = 0;

    List<Vector3> initialButtonSizes = new List<Vector3>();
    public float hoverSizeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        levelSelectMenu.SetActive(false);

        //mainMenu = GameObject.Find("MainMenu");

        foreach (GameObject curButtonObj in buttonList)
        {
            initialButtonSizes.Add(curButtonObj.transform.localScale);
        }
        SizeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown("down"))
            {
                currentButton = (currentButton + 1) % buttonList.Count;
                SizeButtons();
            }
            if (Input.GetKeyDown("up"))
            {
                currentButton = (buttonList.Count + currentButton - 1) % buttonList.Count;
                SizeButtons();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string nameOfButton = buttonList[currentButton].name;
                if (nameOfButton == "LevelSelect")
                {
                    levelSelectMenu.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (nameOfButton == "Quit")
                {
                    Application.Quit();
                }
            }
        }
    }

    void SizeButtons()
    {
        foreach (GameObject curButtonObj in buttonList)
        {
            int pos = buttonList.IndexOf(curButtonObj);
            if (pos == currentButton)
            {
                curButtonObj.transform.localScale = initialButtonSizes[pos] * hoverSizeIncrease;
            }
            else
            {
                curButtonObj.transform.localScale = initialButtonSizes[pos];
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public List<GameObject> levelList;

    public GameObject mainMenu;

    int selectedLevel = 0;

    public GameObject backButton;
    bool backSelected = false;
    Vector3 initialBackSize;

    List<Vector3> initialButtonSizes = new List<Vector3>();
    public float hoverSizeIncrease;

    void OnEnable()
    {

        foreach (GameObject curButtonObj in levelList)
        {
            initialButtonSizes.Add(curButtonObj.transform.localScale);
        }

        initialBackSize = backButton.transform.localScale;

        SizeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!backSelected)
        {
            if (Input.GetKeyDown("right"))
            {
                selectedLevel = (selectedLevel + 1) % levelList.Count;
                SizeButtons();
            }
            if (Input.GetKeyDown("left"))
            {
                selectedLevel = (levelList.Count + selectedLevel - 1) % levelList.Count;
                SizeButtons();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelList[selectedLevel].name);
            }
            if (Input.GetKeyDown("up"))
            {
                selectedLevel = 0;
                foreach (GameObject curButtonObj in levelList)
                {
                    int pos = levelList.IndexOf(curButtonObj);
                    curButtonObj.transform.localScale = initialButtonSizes[pos];
                }

                backSelected = true;
                backButton.transform.localScale = initialBackSize * hoverSizeIncrease;
            }
        }

        else
        {
            if (Input.GetKeyDown("down")) {
                selectedLevel = 0;
                SizeButtons();
                backSelected = false;
                backButton.transform.localScale = initialBackSize;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                backSelected = false;
                backButton.transform.localScale = initialBackSize;
                mainMenu.SetActive(true);
                gameObject.SetActive(false);
            }
           
        }

    }

    void SizeButtons()
    {
        foreach (GameObject curButtonObj in levelList)
        {
            int pos = levelList.IndexOf(curButtonObj);
            if (pos == selectedLevel)
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

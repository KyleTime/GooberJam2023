using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> buttonList;

    float currentButton = 0;

    List<Vector3> initialButtonSizes = new List<Vector3>();
    public float hoverSizeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject curButtonObj in buttonList)
        {
            initialButtonSizes.Add(curButtonObj.transform.localScale);
        }
        SizeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down"))
        {
            currentButton = (currentButton + 1) % buttonList.Count;
            SizeButtons();
            print(currentButton);
        }
        if (Input.GetKeyDown("up"))
        {
            currentButton = (buttonList.Count + currentButton - 1) % buttonList.Count;
            SizeButtons();
            print(currentButton);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print("hi");
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

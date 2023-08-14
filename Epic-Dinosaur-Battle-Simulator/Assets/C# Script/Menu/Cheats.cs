using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Cheats : MonoBehaviour
{
    [SerializeField] private GameObject cheatBox;
    [SerializeField] private Sprite RedFace;
    [SerializeField] private Sprite GreenFace;
    [SerializeField] private Image Face;
    [SerializeField] private GameObject FaceObj;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        FaceObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cheat(string cheat)
    {
        string assist = cheat.ToUpper();
        if (assist is not null && assist == "PROGRAMMERS")
        {
            FaceObj.SetActive(true);
            gameObject.SetActive(true);
            cheatBox.SetActive(false);
            Face.sprite = GreenFace;
        }
        else if (assist is not null && assist != "")
        {
            FaceObj.SetActive(true);
            Face.sprite = RedFace;
        }
        else
            FaceObj.SetActive(false);
    }
}

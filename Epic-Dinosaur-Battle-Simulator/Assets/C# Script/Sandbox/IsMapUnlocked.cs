using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMapUnlocked : MonoBehaviour
{
    [SerializeField] private int indexOfMap;
    [SerializeField] private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.dynamicData.Terrain[indexOfMap])
            gameObject.SetActive(false);
        else
            button.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButton : MonoBehaviour
{
    [SerializeField] private int idOfButton;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.dynamicData.UnlockLvls[idOfButton - 1] < 40)
            this.gameObject.SetActive(false);
    }

}

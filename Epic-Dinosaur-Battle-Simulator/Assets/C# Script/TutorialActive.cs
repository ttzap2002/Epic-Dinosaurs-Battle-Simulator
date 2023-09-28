using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialActive : MonoBehaviour
{
    [SerializeField] private GameObject tutorialOutput;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject Hide;
    [SerializeField] private SceneTutorial scene;
    private TextMeshProUGUI textMesh;


    private delegate void MakeAction(params object[] args);

    private class ActionWithParams
    {
        public MakeAction Action { get; }
        public object[] Args { get; }

        public ActionWithParams(MakeAction action, params object[] args)
        {
            Action = action;
            Args = args;
        }
    }

    private Stack<ActionWithParams> stosDelegatow = new Stack<ActionWithParams>();

    void Start()
    {
        if (IsTutorialMade())
        {
            gameObject.SetActive(false);
        }
        else
        {
            textMesh = tutorialOutput.transform.Find("TEXT").GetComponent<TextMeshProUGUI>();
            switch ((SceneTutorial)scene)
            {
                case SceneTutorial.continentChoice:
                    SetForContinentChoice();
                    break;
                case SceneTutorial.lvlChoice:
                    SetForLevelChoice();
                    break;
                case SceneTutorial.lvl:
                    SetForLvl();
                    break;
            }
        }
    }

    private bool IsTutorialMade()
    {
        return GameManager.Instance.dynamicData.isShowTutorialOnScene[(int)scene] == true;
    }

    private void SetForLevelChoice()
    {
        stosDelegatow.Push(new ActionWithParams(DisactiveGameObject));
        stosDelegatow.Push(new ActionWithParams(DisactiveOneActiveAnother, tutorialOutput, arrow, Hide));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Learning how to code efficiently..."));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "To œa levele ugabuga"));
    }

    private void SetForContinentChoice()
    {
        stosDelegatow.Push(new ActionWithParams(DisactiveGameObject));
        stosDelegatow.Push(new ActionWithParams(DisactiveOneActiveAnother, tutorialOutput, arrow, Hide));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Learning how to code efficiently..."));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Witaj dzielny wojowniku to sa lvl i po prostu se w nie graj"));
    }


    private void SetForLvl()
    {
        
        stosDelegatow.Push(new ActionWithParams(DisactiveGameObject));
        
       

        stosDelegatow.Push(new ActionWithParams(MoveArrow, arrow, -1167.234f, 800f, -36.71281f));
        stosDelegatow.Push(new ActionWithParams(DisactiveOneActiveAnother, tutorialOutput, arrow, Hide));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        stosDelegatow.Push(new ActionWithParams(ChangeText, textMesh, "the easier it will become to write"));
    }

    void ChangeText(params object[] args)
    {
        TextMeshProUGUI txt = args[0] as TextMeshProUGUI;
        string text = (string)(args[1]);

        txt.text = text;

        // Dodaj logikê dla 'isDisactive' i 'obj', jeœli potrzebujesz
    }


    void DisactiveOneActiveAnother(params object[] args)
    {
        GameObject objToDisative = args[0] as GameObject;
        GameObject objToActive = args[1] as GameObject;
        GameObject imgHide = args[2] as GameObject;
        imgHide.SetActive(false);
        objToDisative.SetActive(false);
        objToActive.SetActive(true);
        // Dodaj logikê dla 'isDisactive' i 'obj', jeœli potrzebujesz
    }


    void MoveArrow(params object[] args)
    {
        GameObject arrow = args[0] as GameObject;
        RectTransform rectTransform = arrow.GetComponent<RectTransform>();
        //rectTransform.transform.position = new Vector3((float)args[1], (float)args[2], (float)args[3]);
        rectTransform.localPosition = new Vector3((float)args[1], (float)args[2], (float)args[3]);
    }




    void DisactiveGameObject(params object[] args)
    {
        GameManager.Instance.dynamicData.isShowTutorialOnScene[(int)scene] = true;
        GameManager.Instance.dynamicData.Save();
    }





    public void MakeActionFromStack()
    {
        if (stosDelegatow.Count > 0 /* && inne warunki */)
        {
            ActionWithParams actionWithParams = stosDelegatow.Pop();
            actionWithParams.Action(actionWithParams.Args);
        }
    }
}


public enum SceneTutorial
{
    continentChoice,
    lvlChoice,
    lvl
}

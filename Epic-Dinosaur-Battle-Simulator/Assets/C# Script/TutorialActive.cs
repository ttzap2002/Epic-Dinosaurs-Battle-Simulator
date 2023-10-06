using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialActive : MonoBehaviour
{
    [SerializeField] private GameObject tutorialOutput;
    [SerializeField] private GameObject JoystickHideVertical;
    [SerializeField] private GameObject JoystickHideHorizontal;
    [SerializeField] private GameObject MapHide;
    [SerializeField] private GameObject ClearButtonHide;
    [SerializeField] private GameObject StartButtonHide;
    [SerializeField] private GameObject WarriorButton;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject Hide;
    private Button OutputButton;
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
    private Stack<ActionWithParams> ActionStack = new Stack<ActionWithParams>();

    void Start()
    {
        if (IsTutorialMade())
        {
            gameObject.SetActive(false);
            if (scene == SceneTutorial.lvl)
            {
                JoystickHideHorizontal.SetActive(false);
                JoystickHideVertical.SetActive(false);
            }
        }
        else
        {
            textMesh = tutorialOutput.transform.Find("TEXT").GetComponent<TextMeshProUGUI>();
            OutputButton = tutorialOutput.GetComponent<Button>();   
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
        ActionStack.Push(new ActionWithParams(DisactiveGameObject));
        ActionStack.Push(new ActionWithParams(AciveOneDisactiveAnother, tutorialOutput, arrow, Hide));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Learning how to code efficiently..."));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "To œa levele ugabuga"));
    }
    private void SetForContinentChoice()
    {
        ActionStack.Push(new ActionWithParams(DisactiveGameObject));
        ActionStack.Push(new ActionWithParams(AciveOneDisactiveAnother, tutorialOutput, arrow, Hide));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Learning how to code efficiently..."));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Witaj dzielny wojowniku to sa lvl i po prostu se w nie graj"));
    }
    private void SetForLvl()
    { 
        ActionStack.Push(new ActionWithParams(DisactiveGameObject));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeTextActiveGameObject, arrow, new Vector3(1000f, 680f, 0f), new Quaternion(0, 0, 0.25F, 0), textMesh, "Now Put your troops and click start button", StartButtonHide, false));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeTextActiveGameObject, arrow, new Vector3(1000f, 780f, 0f), new Quaternion(0, 0, 0.25F,0), textMesh, "Now. Lets click the clear button", ClearButtonHide, false));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "BRAWOOOOOO", true));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeTextActiveGameObject, arrow, new Vector3(-1167.234f, 800f, -36.71281f), new Quaternion(0, 0, 45, 0), textMesh, "COS", MapHide, false));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeTextActiveGameObject, arrow, new Vector3(- 1167.234f, 800f, -36.71281f), new Quaternion(0, 0, 45, 0), textMesh, "3", WarriorButton));
        //stosDelegatow.Push(new ActionWithParams(MoveArrow, arrow, -1167.234f, 800f, -36.71281f));
        //stosDelegatow.Push(new ActionWithParams(DisactiveOneActiveAnother, tutorialOutput, arrow, Hide));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeText, arrow, new Vector3(250f, -250f, -36.71281f), new Quaternion(0, 0, 0, 0), textMesh, "Here you have information about money and number of enemy troops"));
        ActionStack.Push(new ActionWithParams(MoveArrowChangeText, arrow, new Vector3(-250f, -250f, -36.71281f), new Quaternion(0,0,0,0), textMesh, "Here you have information about money and number of your troops"));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Gówno"));
        ActionStack.Push(new ActionWithParams(ChangeText, textMesh, "Hello brave surviver. Are you ready for epic dinosaurs battle."));
    }
    void ChangeText(params object[] args)
    {
        TextMeshProUGUI txt = args[0] as TextMeshProUGUI;
        string text = (string)(args[1]);
        txt.text = text;
        if(args.Length > 2) 
        {
            ActiveDisactiveButton(args[2]);
        }
        // Dodaj logikê dla 'isDisactive' i 'obj', jeœli potrzebujesz
    }
    void AciveOneDisactiveAnother(params object[] args)
    {
        GameObject objToActive = args[0] as GameObject;
        for(int i=1;i<args.Length-1; i++) 
        {
            GameObject obj = args[i] as GameObject;
            obj.SetActive(false);
        }
        objToActive.SetActive(true);
        // Dodaj logikê dla 'isDisactive' i 'obj', jeœli potrzebujesz
    }
    void MoveArrow(params object[] args)
    {
        GameObject arrow = args[0] as GameObject;
        RectTransform rectTransform = arrow.GetComponent<RectTransform>();
        Vector3 arrowposition = (Vector3)args[1];
        Quaternion arrowrotation = (Quaternion)args[2];
        rectTransform.localPosition = arrowposition;
        rectTransform.localRotation = arrowrotation;
    }
    void MoveArrowChangeText(params object[] args) 
    {
        MoveArrow(args[0], args[1], args[2]);
        ChangeText(args[3], args[4]);
    }

    void MoveArrowChangeTextActiveGameObject(params object[] args)
    {
        MoveArrow(args[0], args[1], args[2]);
        ChangeText(args[3], args[4]);
        ActiveGameObject(args[5]);
        if (args.Length > 6)
        {
            ActiveDisactiveButton(args[args.Length-1]);
        }
    }

    void MoveArrowChangeTextActiveGameObjectActiveDisactiveButton(params object[] args)
    {
        MoveArrow(args[0], args[1], args[2]);
        ChangeText(args[3], args[4]);
        AciveOneDisactiveAnother(args[5]);
        if(args.Length > 6) 
        {
            ActiveDisactiveButton(args[6]);
        }
    }


    void DisactiveGameObject(params object[] args)
    {
        GameManager.Instance.dynamicData.isShowTutorialOnScene[(int)scene] = true;
        GameManager.Instance.dynamicData.Save();
        gameObject.SetActive(false);
    }
    void ActiveGameObject(params object[] args) 
    {
        GameObject objToDisactive = args[0] as GameObject;
        objToDisactive.SetActive(false);
    }

    void ActiveDisactiveButton(params object[] args)
    { 
        OutputButton.enabled = (bool)args[0];
    }
    public void MakeActionFromStack()
    {
        if (ActionStack.Count > 0 /* && inne warunki */)
        {
            ActionWithParams actionWithParams = ActionStack.Pop();
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

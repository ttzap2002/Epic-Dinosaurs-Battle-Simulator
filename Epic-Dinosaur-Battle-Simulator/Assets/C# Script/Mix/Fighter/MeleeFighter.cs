using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeFighter : Fighter
{
    // Start is called before the first frame update

    public bool isFighting = false;
    int a = 1;
    private CreatureStats myStats = null;
    private FighterPlacement fighter = null;



    private void Start()
    {
        //GameManager.Instance.Awake();
        //GetFirstTarget();
        myStats = gameObject.GetComponent<CreatureStats>();
        fighter = gameObject.GetComponent<FighterPlacement>();
    }



    void Update()
    {
        if (a%3==0)
        { 
            if (target != null)
            {
            
                if (isChangeOfSquare())
                {
                    if (tag == "Blue")
                    {
                        GameManager.Instance.battleManager.React(false, gameObject,this);
                    }
                    else if(tag=="Enemy")
                    {
                        GameManager.Instance.battleManager.React(true, gameObject,this);
                    }
                }
                if (!isFighting)
                {
                    SecondMove(); if (Vector3.Distance(transform.position, target.transform.position) < myStats.radius) { isFighting = true;  }
                }
                else { Hit(target); }

            }
            else if (target == null) { GetFirstTarget(); }
        }

            //transform.position += moveDirection;
            a++;
    }

    public void GetFirstTarget() {
        if (tag == "Blue") { target=FindNearestEnemy("Enemy",transform.position); }
        else { target=FindNearestEnemy("Blue",transform.position);}}
    public void setdisactive()
    {
        gameObject.SetActive(false);
    }
  
    private void Hit(GameObject myEnemy) 
    {
        if (myEnemy == this.gameObject)
        {
            isFighting = false;
            GetFirstTarget();
        }
        else
        {
            if (myEnemy != null)
            {
                CreatureStats m = myEnemy.GetComponent<CreatureStats>();
                m.hp -= myStats.attack;
                if (m.hp <= 0) { isFighting = false;GameManager.Instance.battleManager.RemoveFromList(m.gameObject.GetComponent<FighterPlacement>()); Destroy(m.gameObject); }
            }
        }
    }
 

    private void SecondMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * myStats.speed);
 
    }

    private bool isChangeOfSquare()
    {

        if (fighter.CheckWhichSquare()[0]!=fighter.row || fighter.CheckWhichSquare()[1] != fighter.col) 
        {
            fighter.row = fighter.CheckWhichSquare()[0];
            fighter.col = fighter.CheckWhichSquare()[1];
            return true;
        }
        return false;
    }


    private GameObject FindNearestEnemy(string tag,Vector3 vectorOfMyObj)
    {
        float min = Mathf.Infinity;
        GameObject ObjectReturn = null;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag)) 
        {
         
            float distance = Vector3.Distance(obj.transform.position, vectorOfMyObj);
            if (distance < min)
            {
                min = distance;
                ObjectReturn = obj;
                if (min <= myStats.radius)
                {

                    isFighting = true;
                }
            }
          
        }
        return ObjectReturn;
    }
}

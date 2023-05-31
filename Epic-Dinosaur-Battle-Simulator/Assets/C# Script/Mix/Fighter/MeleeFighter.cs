using System;
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
    private LongNeckFighting longNeckFighter = null;
    private float timer = 0.0f; // Zmienna do œledzenia czasu
    [SerializeField] private float rotationSpeed = 5f;
    private float basicx;

    private void Start()
    {
        //GameManager.Instance.Awake();

        basicx = transform.rotation.x;

        myStats = gameObject.GetComponent<CreatureStats>();
        fighter = gameObject.GetComponent<FighterPlacement>();
       
        if (target is null) { GetFirstTarget(); }
        if (myStats.fightingScript == FightScript.LongNeck && longNeckFighter is null)
        {
            longNeckFighter = gameObject.GetComponent<LongNeckFighting>();

            longNeckFighter.onNoTargets += SetFightingStatusToFalse;
            longNeckFighter.IsReady=true;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (a%3==0)
        { 
            if (target != null && target!=gameObject)
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
                    Move(); if (Vector3.Distance(transform.position, target.transform.position) <= myStats.radius && Vector3.Distance(transform.position, target.transform.position)!=0) { isFighting = true;  }
                }
                else 
                {
                    if (Vector3.Distance(transform.position, target.transform.position) > myStats.radius) 
                    {
                        isFighting = false;
                    }
                    if (timer >= myStats.interval)
                    {
                        if (myStats.fightingScript == FightScript.Traditional)
                        {
                            Hit(target);
                        }
                        else
                        {
                            if (!longNeckFighter.HitAllEnemies(myStats.attack))
                            {
                                isFighting = false;
                            }
                        }
                        timer = 0f;
                    }
                }
         

            }
            else { GetFirstTarget(); }
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

    public void SetFightingStatusToFalse()
    {
        isFighting = false;
        Debug.Log("Done");
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
 

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * myStats.speed);
        Vector3 directionToEnemy = (transform.position- target.transform.position).normalized;
        directionToEnemy = directionToEnemy.normalized;
        
        if (!myStats.IsObligatoryToRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //rotation.x = transform.rotation.x;
            //rotation.z = transform.rotation.z;
            transform.rotation = rotation;
        
        }
        /*
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            rotation.x = transform.rotation.x;
            rotation.y = -transform.rotation.y;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
        }*/
        
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

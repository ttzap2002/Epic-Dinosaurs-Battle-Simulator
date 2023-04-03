using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeFighter : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isFighting = false;
    int a = 1;
    public GameObject target = null;
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

        //float horizontal = Input.GetAxis("Horizontal"); // pobierz wartoœæ wciœniêtego klawisza w poziomie (A/D lub strza³ki)
        //float vertical = Input.GetAxis("Vertical"); // pobierz wartoœæ wciœniêtego klawisza w pionie (W/S lub strza³ki)
        // oblicz wektor ruchu na podstawie wciœniêtych klawiszy i prêdkoœci
        //Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // przesuñ obiekt o wektor ruch
      
        //Move(tag);
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
                    else
                    {
                        GameManager.Instance.battleManager.React(true, gameObject,this);
                    }

                }
            
                if (!isFighting)
                {
                    SecondMove(); if (Vector3.Distance(transform.position, target.transform.position) < myStats.radius) { isFighting = true; }
                }

                else if (isFighting) { Hit(target); }

            }
            else if (target == null) { GetFirstTarget(); }
        }

            //transform.position += moveDirection;
            a++;
    }

    public void GetFirstTarget() { if (tag == "Blue") { target=FindNearbiestEnemy("Enemy",transform.position); }
        else { target=FindNearbiestEnemy("Blue",transform.position);}}
    public void setdisactive()
    {
        gameObject.SetActive(false);
    }
    void DoDamage()
    {
        GameObject myEnemy;
        if (tag == "Enemy") { myEnemy = FindNearbiestEnemy("Blue", transform.position); Hit(myEnemy); }
        else if (tag == "Blue") { myEnemy = FindNearbiestEnemy("Enemy", transform.position); Hit(myEnemy); }
    }
    private void Hit(GameObject myEnemy) 
    {
        if (myEnemy != null)
        {
            CreatureStats m = myEnemy.GetComponent<CreatureStats>();
            m.hp -= myStats.attack;
            if (m.hp <= 0) { isFighting = false; GameManager.Instance.battleManager.RemoveFromList(m.gameObject.GetComponent<FighterPlacement>()); Destroy(m.gameObject); }
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


    private GameObject FindNearbiestEnemy(string tag,Vector3 vectorOfMyObj)
    {
        float min = Mathf.Infinity;
        GameObject ObjectReturn = null;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag)) 
        {
            float distance = Vector3.Distance(obj.transform.position,vectorOfMyObj);
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

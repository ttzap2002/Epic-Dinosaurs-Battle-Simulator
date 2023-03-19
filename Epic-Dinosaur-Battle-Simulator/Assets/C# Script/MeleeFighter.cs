using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeFighter : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // pr�dko�� poruszania si� obiektu
    public float hp = 100f;
    public float attack = 20f;
    public float radius = 5f;
    public bool isFighting = false;
    public int a = 0;
   

    private void Start()
    {
        //GameManager.Instance.Awake();
     
      
        
    }

    void Update()
    {
        
        //float horizontal = Input.GetAxis("Horizontal"); // pobierz warto�� wci�ni�tego klawisza w poziomie (A/D lub strza�ki)
        //float vertical = Input.GetAxis("Vertical"); // pobierz warto�� wci�ni�tego klawisza w pionie (W/S lub strza�ki)
        // oblicz wektor ruchu na podstawie wci�ni�tych klawiszy i pr�dko�ci
        //Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // przesu� obiekt o wektor ruch

        //Move(tag);
        if (!isFighting){ Move(tag); }
        else if(isFighting){ DoDamage(); }
        
        if (a==100) { GameManager.Instance.GameResume(); }
        
        //transform.position += moveDirection;
    }

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
        MeleeFighter m=null;
        if (myEnemy != null)
        {
            m = myEnemy.GetComponent<MeleeFighter>();
            m.hp -= attack;
            if (m.hp <= 0) { 
                GameObject.Destroy(myEnemy);
                isFighting = false;
            }
        }
    }
    private void Move(string tag) 
    {
        if (tag == "Enemy")
        {
            GameObject myEnemy = FindNearbiestEnemy("Blue", transform.position);
            if (myEnemy != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, myEnemy.transform.position, Time.deltaTime * speed);
            }
        }
        else if (tag == "Blue")
        {

            GameObject myEnemy = FindNearbiestEnemy("Enemy", transform.position);
            if (myEnemy!=null)
            {
                transform.position = Vector3.MoveTowards(transform.position, myEnemy.transform.position, Time.deltaTime * speed);
            }
         }
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
                Debug.Log(min);
                if (min <= radius) 
                {
                    isFighting = true;
                }
            }
        }
        return ObjectReturn;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeFighter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 25f;
    [SerializeField] private float attack = 20f;
    private bool isFighting = false;
    int a = 1;
    public GameObject target = null;
    public float radius = 5f;
    public float hp = 100f;



    private void Start()
    {
        //GameManager.Instance.Awake();
        GetFirstTarget();
        Debug.Log("pies");
    }

    

    void Update()
    {

        //float horizontal = Input.GetAxis("Horizontal"); // pobierz wartoœæ wciœniêtego klawisza w poziomie (A/D lub strza³ki)
        //float vertical = Input.GetAxis("Vertical"); // pobierz wartoœæ wciœniêtego klawisza w pionie (W/S lub strza³ki)
        // oblicz wektor ruchu na podstawie wciœniêtych klawiszy i prêdkoœci
        //Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // przesuñ obiekt o wektor ruch

        //Move(tag);
        if (a % 3 == 0)
        {
            if (target != null)
            {
                
                if (isChangeOfSquare())
                {
                    if (tag == "Blue")
                    {
                        GameManager.Instance.battleManager.React(false, gameObject);
                    }
                    else
                    {
                        GameManager.Instance.battleManager.React(true, gameObject);
                    }
          
                }
                if (!isFighting) { SecondMove();  if (Vector3.Distance(transform.position, target.transform.position) < radius) { isFighting = true; }
                }
                else if (isFighting) { DoDamage(); }

            }
            else if (target == null) { GetFirstTarget(); }
            //else { GetFirstTarget(); }
           
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

    private void SecondMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }


  

    private bool isChangeOfSquare()
    {
        WhichSquare mySquare = gameObject.GetComponent<WhichSquare>();
        if (mySquare.CheckWhichSquare()[0]!=mySquare.row || mySquare.CheckWhichSquare()[1] != mySquare.col) 
        {
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

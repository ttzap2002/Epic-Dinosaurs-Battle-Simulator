using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public float _speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(_speed * Time.deltaTime, transform.position.y, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(_speed * Time.deltaTime, transform.position.y, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, _speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, _speed * Time.deltaTime, 0);
        }

    }
}

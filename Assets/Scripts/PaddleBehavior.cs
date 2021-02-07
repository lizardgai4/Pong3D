using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    float speed = 5f;
    public string upValue;
    public string downValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //temporary variable to store positions
        Vector3 pos = transform.position;

        //move up
        if (Input.GetKey(upValue) && pos.z <= 4)
        {
            pos.z += speed * Time.deltaTime;
        }

        //move down
        if (Input.GetKey(downValue) && pos.z >= -4)//or max calues aren't reached || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}

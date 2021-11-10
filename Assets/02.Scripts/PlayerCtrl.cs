using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h=" + h);
        Debug.Log("v=" + v);

        // Transform 컴포넌트의 position 속성값을 변경
        transform.position += new Vector3(0, 0, 1);
    }
}

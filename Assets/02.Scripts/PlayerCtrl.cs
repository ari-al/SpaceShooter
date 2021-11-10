using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 함수
    private Transform tr;
    // 이동 속도 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    public float moveSpeed = 10.0f;
    // 회전 속도 변수
    public float turnSpeed = 80.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Transform 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        Debug.Log("h=" + h);
        Debug.Log("v=" + v);

        // Transform 컴포넌트의 position 속성값을 변경
        // transform.position += new Vector3(0, 0, 1);

        
        // 정규화 벡터를 사용한 코드
        // tr.position += Vector3.forward * 1;

        // Translate 함수르 사용한 이동 로직

        // 프레임마다 10유닛씩 이동 ex) 30fps 300유닛 이동
        // tr.Translate(Vector3.forward * 10);
        // 매 초 10유닛씩 이동
        //tr.Translate(Vector3.forward * Time.deltaTime * 10);

       // tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);

       // 전후좌우 이동 방향 벡터 계산
       Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
       // Translate(이동 방향 * 속력 * Time.deltaTime) normalized는 정규화 벡터값을 사용하도록 한다. -> 대각선 이동도 길이가 1인 벡터로 변환
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

       // Vector3.up 축을 기준으로 turnSpeed만큼의 속도로 회전 
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
    }
}

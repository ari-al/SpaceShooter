using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야 할 대상을 연결할 변수
    public Transform targetTr;
    // Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    // 따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)] //[Ranget(min, max)] 어트리뷰트를 사용하면 다음라인에 선언한 변수의 입력 번위를 제한하여 인스펙터 뷰에 슬라이드바를 표시
    public float distance = 10.0f;

    
    // Y축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // 반응 속도
    public float damping = 10.0f;

    // 카메라 LookAt의 Offset 값
    public float targetOffset = 2.0f;

    // SmoothDamp에 사용할 변수
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 추적해야 할 대상의 뒤쪽으로 distance만큼 이동
        // 높이를 height만큼 이동
        // camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);
        Vector3 pos = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);
       
        // 구면 선형 보간 함수를 사용해 부드럽게 위치를 변경
        // camTr.position = Vector3.Slerp(camTr.position,              // 시작 위치
        //                                 pos,                        // 목표 위치
        //                                 Time.deltaTime * damping);  // 시간 t

        // SmoothDamp을 이용한 위치 보간
        camTr.position = Vector3.SmoothDamp(camTr.position,         // 시작 위치
                                            pos,                    // 목표 위치
                                            ref velocity,           // 현재 속도
                                            damping);               // 목표 위치까지 도달하 시간

        // Camera를 피벗 좌표를 향해 회전
        // camTr.LookAt(targetTr.position);

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 스파크 파티클 프리팹을 연결할 변수
    public GameObject sparkEffect;
    
    // 충돌이 시작할 때 발생하는 이벤트
    void OnCollisionEnter(Collision coll){
        // 충돌한 게임오브젝트의 태그값 비교
        if(coll.collider.CompareTag("BULLET")){ // coll.collier.tag == "문자열" 은 실행 시 해당 문자열의 복사본을 생성한다. 
        // 이 복사본은 특정 메모리에 할당되며 가비지 컬레션의 대상이 되며, 가바지 컬렉션의 대상이 많아질 수록 가비지 컬렉션을 해제하는 시점에서 랙 현상이 발생한다.
        // coll.collider.tag 또한 정상적으로 작봉하는 코드이지만 가비지 컬렉션이 발생하지 않는 CompareTag 함수를 사용하는 방법을 적극 권장한다.
        
            // 첫 번째 충돌 지점의 정보 추출
            ContactPoint cp = coll.GetContact(0); // coll.contacts 속성으로도 알 수 있지만 가바지 컬레션 발생
            // 충돌한 총알의 법선 벡터를 쿼터니언 타입으로 변환
            Quaternion rot =Quaternion.LookRotation(-cp.normal);

            // 스파크 파티클을 동적으로 생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            // 충돌한 게임오브젝트 삭제
            Destroy(coll.gameObject);

            // 충돌한 게임오브젝트 삭제
            Destroy(spark, 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

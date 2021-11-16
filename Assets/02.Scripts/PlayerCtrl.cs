using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 함수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수
    private Animation anim;

    // 이동 속도 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    public float moveSpeed = 10.0f;
    // 회전 속도 변수
    public float turnSpeed = 80.0f;

    // 초기 생명 값
    private readonly float initHp = 100.0f;
    // 현재 생명 값
    public float currHp;
    // Hpbar 연결할 변수
    private Image hpBar;

    // 델리게이트 선언
    public delegate void PlayerDieHandler();
    // 이벤트 선언
    public static event PlayerDieHandler OnPlayerDie;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Hpbar 연결
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        // Transform 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        // 애니메이션 실행;
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

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

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v){
        if (v >= 0.1f){
            anim.CrossFade("RunF", 0.25f); // 전진 애니메이션 실행
        }else if (v <= -0.1f){
            anim.CrossFade("RunB", 0.25f); // 후진 애니메이션 실행
        }else if (h >= 0.1f){
            anim.CrossFade("RunR", 0.25f); // 오른쪽 이동 애니메이션 실행
        }else if (h <= -0.1f){
            anim.CrossFade("RunL", 0.25f); // 왼쪽 이동 애니메이션 실행
        }else{
            anim.CrossFade("Idle", 0.25f); // 정지 시 Idle 애니메이션 실행
        }
    }

    // 충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생
    void OnTriggerEnter(Collider coll){
        // 충돌한 Collider가 몬스터의 PUNCH이면 Player의 HP 차감
        if (currHp >= 0.0f && coll.CompareTag("PUNCH")){
            currHp -= 10.0f;
            DisplayHealth();

            Debug.Log($"Player hp =  {currHp/initHp}");

            // Player의 생명이 0 이하이면 사망 처리
            if (currHp <= 0.0f){
                PlayerDie();
            }
        }
    }

     void PlayerDie(){
    //     Debug.Log("Player Die !");

    //     // Monster 태그를 가진 모든 게임오브젝트를 찾아옴
    //     GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

    //     // 모든 몬스터의 OnPlayerDie 함수를 순차적으로 호출
    //     foreach(GameObject monster in monsters){
    //         monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
    //     }
        // 주인공 사망 이벤트 호출(발생)
        OnPlayerDie();

        // GameManager 스크립트의 IsGameOver 프로퍼티 값을 변경
        // GameObject.Find("GameMgr").GetComponent<GameManager>().IsGameOver = true;
        GameManager.instance.IsGameOver = true;
    }

    void DisplayHealth(){
        hpBar.fillAmount = currHp/initHp;
    }
}

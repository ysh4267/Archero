using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour {
    //표시되는 HP바
    public Slider hpSlider;
    //피격시에만 표시되는 HP바
    public Slider BackHpSlider;
    //공격을 받았을때 뒤쪽 HP바가 줄어들도록 하기위한 불 값
    public bool backHpHit = false;

    public Transform enemy;
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position = enemy.position; //HP바가 대상 위를 따라다니기위한 위치값
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f); //선형 보간함수를 이용해 체력이 서서히 줄어들도록 설계
        if (backHpHit) {
            //맞아서 체력이 닳았는가
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f); //앞에 보이는 체력의 1/2의 속도로 선형 보간함수를 이용해 더 천천히 줄어들도록 설계
            if (hpSlider.value >= BackHpSlider.value - 0.01f) {
                //뒤쪽 HP바가 앞쪽 HP바와 0.01이내로 가까워지면 함수 중단 이후 값을 같은수치로 변경
                backHpHit = false;
                BackHpSlider.value = hpSlider.value;
            }
        }
    }
    //시험용 피격대미지 함수
    public void Dmg() {
        Invoke("BackHpFun", 0.5f);
    }

    void BackHpFun() {
        backHpHit = true;
    }
}

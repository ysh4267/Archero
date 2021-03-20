using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour {
    public Transform player;
    public Slider hpBar;
    public float maxHp;
    public float currentHp;

    public GameObject HpLineFolder;
    float unitHp = 200f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position = player.position;
        hpBar.value = currentHp / maxHp;
    }

    public void GetHpBoost() {
        maxHp += 150; //테스트용 체력증감식
        float scaleX = (1000f / unitHp) / (maxHp / unitHp); //체력이 1000일때가 1:1 비율이고, 체력단위 한칸을 unitHp라 했을때 전체 체력에 보이는 칸의 수를 계산하는 식
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);//오브젝트가 켜져있을때 이미지의 크기조정이 적용되지않아 끈 상태로 사이즈를 변경
        foreach (Transform child in HpLineFolder.transform) {
            //체력단위칸을 포함하고있는 부모오브젝트의 모든 child(체력칸들)
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1); //모든 체력칸의 스케일을 조정
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);//사이즈 변경후 오브젝트를 켬

    }
}
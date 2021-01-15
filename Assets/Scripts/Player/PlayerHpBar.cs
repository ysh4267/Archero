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
        maxHp += 150;
        float scaleX = (1000f / unitHp) / (maxHp / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach (Transform child in HpLineFolder.transform) {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);

    }
}
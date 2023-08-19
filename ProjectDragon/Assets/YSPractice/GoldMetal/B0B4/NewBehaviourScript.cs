using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int health = 30;
    int level = 5;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello Unity");

        // 1. 변수
        // 자료형 네 가지 // 변수의 타입과 이름=선언 // 값 부여=초기화
        
        float strength=15.5f;
        string playerName="나검사";
        bool isFullLevel=false;

        // 선언 > 초기화 > 호출
        Debug.Log("용사의 이름은?");
        Debug.Log(playerName);
        Debug.Log("용사의 레벨은?");
        Debug.Log(level);
        Debug.Log("용사의 힘은?");
        Debug.Log(strength);
        Debug.Log("용사는 만렙인가?");
        Debug.Log(isFullLevel);

        // 2. 그룹형 변수
        // 배열
        string[] monsters= { "슬라임", "사막뱀", "악마" };

        Debug.Log("맵에 존재하는 몬스터는?");
        Debug.Log(monsters[0]);
        Debug.Log(monsters[1]);
        Debug.Log(monsters[2]);

        int[] monsterLevel = new int[3];
        monsterLevel[0] = 1;
        monsterLevel[1] = 6;
        monsterLevel[2] = 20;
            //?// {}와 new 자료형[크기]의 차이 무엇?

        Debug.Log("맵에 존재하는 몬스터의 레벨은?");
        Debug.Log(monsterLevel[0]);
        Debug.Log(monsterLevel[1]);
        Debug.Log(monsterLevel[2]);

        // 리스트
        List<string> items = new List<string>();
        items.Add("생명물약30");
        items.Add("마나물약30");

        // items.RemoveAt(0);

        Debug.Log("가지고 있는 아이템은?");
        Debug.Log(items[0]);
        Debug.Log(items[1]);

        // 3. 연산자
        int exp = 1500;
        
        exp = 1500 + 320;
        exp = exp - 10;
        level = exp / 300;
        strength = level * 3.1f;

        Debug.Log("용사의 총 경험치는?");
        Debug.Log(exp);
        Debug.Log("용사의 레벨은?");
        Debug.Log(level);
        Debug.Log("용사의 힘은?");
        Debug.Log(strength);

        int nextExp = 300 - (exp % 300);
        Debug.Log("다음 레벨까지 남은 경험치는?");
        Debug.Log(nextExp);

        string title = "전설의";
        Debug.Log("용사의 이름은?");
        Debug.Log(title+" "+playerName);

        int fullLevel = 99;
        isFullLevel=level==fullLevel;
        Debug.Log("용사는 만렙입니까? " + isFullLevel);

        bool isEndTutorial = level > 10;
        Debug.Log("튜토리얼이 끝난 용사입니까? " + isEndTutorial);

        
        int mana = 25;
        mana = 20;
        bool isBadCondition = health <= 50 && mana <= 20;
        Debug.Log("용사의 상태가 나쁩니까? " + isBadCondition);

        //?// mana=20; 여기서 하고
        // Debug.Log 동일하게 물으면 여전히 False로 나옴

        health= 30;
        mana = 25;
        isBadCondition = health <= 50 || mana <= 20;
 
        string condition = isBadCondition ? "나쁨" : "좋음";
        Debug.Log("용사의 상태가 나쁩니까? " + condition);

        // 4. 키워드
        // int float string bool new List

        // 5. 조건문
        if (condition=="나쁨") 
        {
            Debug.Log("플레이어의 상태가 나쁘니 아이템을 사용하세요.");
        }
        else
        {
            Debug.Log("플레이어의 상태가 좋습니다.");
        }

        if (condition == "나쁨" && items[0] == "생명물약30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log("생명포션30을 사용하였습니다.");
        }
        else if (condition =="나쁨" && items[0] =="마나물약30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log("마나포션30을 사용하였습니다.");
        }

        switch (monsters[1])
        {
            case "슬라임":
            case "악마":
                Debug.Log("소형 몬스터가 출현!");
                break;
            case "골렘":
                Debug.Log("대형 몬스터가 출현!");
                break;
            default:
                Debug.Log("??? 몬스터가 출현!");
                break;
        }

        // 6. 반복문
        while (health>0)
        {
            health--;
            if(health > 0)
            Debug.Log("독 데미지를 입었습니다. " + health);
            else
            Debug.Log("사망하였습니다.");
            if(health==10)
            {
                Debug.Log("해독제를 사용합니다.");
                break;
            }
        }

        for (int count=0;count<10 ;count++)
        {
            health++;
            Debug.Log("붕대로 치료 중... " + health);
        }

        for(int index=0;index<monsters.Length;index++)
        {
            Debug.Log("이 지역에 있는 몬스터: " + monsters[index]);
        }

        foreach(string monster in monsters)
        {
            Debug.Log("이 지역에 있는 몬스터: " + monster);
        }

        // 7. 함수
        // health = Heal(health);
        Heal();
        
        for(int index = 0; index < monsters.Length; index++)
        { 
            Debug.Log("용사는 " + monsters[index]+"에게 "+Battle(monsterLevel[index]));
        }

        // 8. 클래스
        Player player = new Player();
        player.id = 0;
        player.name = "나법사";
        player.title = "현명한";
        player.strength = 2.4f;
        player.weapon = "나무 지팡이";
        Debug.Log(player.Talk());
        Debug.Log(player.HasWeapon());
        player.LevelUp();
        Debug.Log(player.name + "의 레벨은 "+player.level+"입니다.");
        Debug.Log(player.move());
    }

    // 7. 함수
    /* int Heal(int currentHealth)
    {
        currentHealth += 10;
        Debug.Log("힐을 받았습니다. " + currentHealth);
        return currentHealth;
    }
        //?// '함수의 매개변수명과 실제로 집어넣을 변수명과 같을 필요 없다' */


    void Heal()
    {
        health += 10;
        Debug.Log("힐을 받았습니다. " + health);
        // void Start 함수 안에 있는 지역변수 health 참조 불가 -> 필드로 위치 이동, 전역변수화
    }

    string Battle(int monsterLevel)
    {
        string result;
        if (level >= monsterLevel)
            result = "이겼습니다";
        else
            result = "졌습니다";
        return result;
    }

}

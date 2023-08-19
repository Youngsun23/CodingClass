using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor // Actor가 부모 Player가 자식 -> 부모 클래스 사용+자식 본인의 클래스도 사용
{
  public string move()
    {
        return "플레이어는 움직입니다.";
    }
}

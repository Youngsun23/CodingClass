using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int combo = 0;

    public void IncreaseCombo()
    {
        combo++;
        Debug.Log("���� �޺�:" + combo);
    }

    public void ResetCombo()
    {
        combo = 0;
    }

}
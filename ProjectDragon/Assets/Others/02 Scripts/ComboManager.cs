using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int combo = 0;

    public void IncreaseCombo()
    {
        combo++;
        Debug.Log("ÇöÀç ÄÞº¸:" + combo);
    }

    public void ResetCombo()
    {
        combo = 0;
    }

}
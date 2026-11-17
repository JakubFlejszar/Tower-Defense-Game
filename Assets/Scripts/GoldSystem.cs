using TMPro;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text goldAmountText;
    [SerializeField] private TMP_Text insufficientGoldText;
    [SerializeField] private int startingGold = 20;
    private int goldAmount;
    private float alertTime = 0;

    private void Start()
    {
        goldAmount = startingGold;
        UpdateGoldText();
        insufficientGoldText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (alertTime > 0)
        {
            alertTime -= Time.deltaTime;
            if (alertTime <= 0)
            {
                insufficientGoldText.gameObject.SetActive(false);
            }
        }
    }

    public bool TrySpend(int receivedAmount)
    {
        if (goldAmount < receivedAmount)
        {
            alertTime = 3;

            insufficientGoldText.gameObject.SetActive(true);

            return false;
        }
        goldAmount -= receivedAmount;
        UpdateGoldText();
        return true;
    }

    public void AddGold(int receivedAmount)
    {
        goldAmount += receivedAmount;
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldAmountText.SetText($"Gold: {goldAmount}");
    }
}
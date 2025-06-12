
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardInstance : MonoBehaviour
{
    public Card cardSO;
    public TMP_Text nameText;
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text defenseText;
    //public TMP_Text retreatCostText;
    public Image backgroundImage;
    public CardData cardData;
    void Start()
    {
        //// Auto-assign UI components if not set in Inspector
        //if (nameText == null || attackText == null || healthText == null)
        //{
        // TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        // foreach (TMP_Text text in texts)
        // {
        // if (text.name == "NameText") nameText = text;
        // else if (text.name == "AttackText") attackText = text;
        // else if (text.name == "HealthText") healthText = text;
        // else if (text.name == "DefenseText") defenseText = text;
        // }
        //}
        // Create runtime copy
        if (cardSO != null)
        {
            // Inicializa cardData copiando todos los campos desde cardSO.cardData
            var soData = cardSO.cardData;
            cardData = new CardData(
            soData.cardName,
            soData.attack,
            soData.defense,
            soData.energy,
            soData.energyAttackCost,
            soData.energyRegen,
            soData.retreatCost,
            soData.health
            );


            //backgroundImage.sprite = cardSO.cardImage;
            //backgroundImage.enabled = true;
            UpdateCardUI();
        }
    }
    public void UpdateCardUI()
    {
        if (cardData == null) return;
        nameText.text = cardData.cardName;
        attackText.text = cardData.attack.ToString();
        healthText.text = Mathf.Max(0, cardData.health).ToString();
        defenseText.text = cardData.defense.ToString();
    }
}
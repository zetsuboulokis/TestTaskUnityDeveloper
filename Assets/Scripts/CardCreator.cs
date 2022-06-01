using UnityEngine;

[RequireComponent(typeof(CardHand))]
public class CardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;

    private int _cardCountMin = 3;
    private int _cardCountMax = 7;

    private int _cardValueMin = 1;
    private int _cardValueMax = 6;

    public void CreateCards()
    {
        int cardCount = Random.Range(_cardCountMin, _cardCountMax);
        int cardValueTypeCount = (int) Card.ValueType.ValueTypeCount;
        var cardHand = GetComponent<CardHand>();

        for (int i = 0; i < cardCount; i++)
        {
            var inst = Instantiate(_cardPrefab, transform);
            var card = inst.GetComponent<Card>();
            card.Init();

            for (int j = 0; j < cardValueTypeCount; j++)
            {
                int value = Random.Range(_cardValueMin, _cardValueMax);
                card.SetValue((Card.ValueType) j, value);
            }

            cardHand.AddCard(card);
        }
    }
}

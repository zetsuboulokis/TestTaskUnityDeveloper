using UnityEngine;

[RequireComponent(typeof(CardHand))]
public class CardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;

    public void CreateCards()
    {
        int cardCount = Random.Range(3, 7);
        int cardValueTypeCount = (int) Card.ValueType.ValueTypeCount;
        var cardHand = GetComponent<CardHand>();

        for (int i = 0; i < cardCount; i++)
        {
            var inst = Instantiate(_cardPrefab, transform);
            var card = inst.GetComponent<Card>();
            card.Init();

            for (int j = 0; j < cardValueTypeCount; j++)
            {
                int value = Random.Range(1, 6);
                card.SetValue((Card.ValueType) j, value);
            }

            cardHand.AddCard(card);
        }
    }
}

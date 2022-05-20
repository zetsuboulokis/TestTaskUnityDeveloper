using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardCreator))]
public class CardHand : MonoBehaviour
{
    private List<Card> _cards;

    private float _cardWidth = 80;
    private float _cardsMaxHeight = 140;
    private float _cardArcHeightStep = 10;
    private float _cardArcAngleStep = 10;
    private float _distanceBetweenCards = 13;
    private float _openAnimationTime = 1;

    private int _changeCardIndex;
    private bool _isChangeCardAnimationActive;

    public bool IsOpenAnimationActive { get; private set; }

    private void Start()
    {
        _cards = new List<Card>();
        GetComponent<CardCreator>().CreateCards();
        StartCoroutine(OpenCards());
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    private IEnumerator OpenCards()
    {
        IsOpenAnimationActive = true;

        float time = 0;
        while (time < _openAnimationTime)
        {
            time += Time.deltaTime;
            time = Mathf.Min(time, _openAnimationTime);

            float positionFactor = 1;
            if (_openAnimationTime != 0)
                positionFactor = time/_openAnimationTime;

            for (int i = 0; i < _cards.Count; i++)
                SetArcPosition(_cards.Count * positionFactor, i * positionFactor, _cards[i].transform);

            yield return null;
        }

        IsOpenAnimationActive = false;
    }

    public void SetArcPosition(float cardCount, float cardNumber, Transform cardTransform)
    {
        float width = _cardWidth + _distanceBetweenCards;
        float x = -width * (0.5f * (cardCount - 1) - cardNumber);
        float positionFactor = (cardCount - 1) * 0.5f - cardNumber;
        float y = _cardsMaxHeight - positionFactor * positionFactor * _cardArcHeightStep;
        float angle = positionFactor * _cardArcAngleStep;

        cardTransform.localPosition = new Vector3(x, y, 0);
        cardTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void PressChangeCards()
    {
        if (_isChangeCardAnimationActive || IsOpenAnimationActive)
            return;

        StartChangeCards();
    }

    public void StartChangeCards()
    {
        _isChangeCardAnimationActive = true;
        _changeCardIndex = 0;
        StartChangeNextCard();
    }

    public void StartChangeNextCard()
    {
        if (_changeCardIndex >= _cards.Count)
        {
            _isChangeCardAnimationActive = false;
            return;
        }

        int cardValueNumber = Random.Range(0, (int)Card.ValueType.ValueTypeCount);
        int cardValue = Random.Range(-2, 10);
        _cards[_changeCardIndex++].SetValueWithAnimation((Card.ValueType) cardValueNumber, cardValue);
    }
}

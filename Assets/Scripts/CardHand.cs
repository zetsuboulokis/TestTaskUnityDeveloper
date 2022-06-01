using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardCreator))]
public class CardHand : MonoBehaviour
{
    private List<Card> _cards;
    private List<Card> _cardsToChange;

    private float _openAnimationTime = 1;

    private ICardPositionPattern _cardPositionPattern = new CardPositionPatternArc();

    public bool IsOpenAnimationActive { get; private set; }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        for (int i = 0; i < _cards.Count; i++)
            if (_cards[i] == card)
            {
                _cards.RemoveAt(i);
                UpdateCardsPositions();
                break;
            }
    }

    public void PressChangeCards()
    {
        if (_cardsToChange.Count != 0)
            return;

        StartChangeCards();
    }

    private void Awake()
    {
        _cards = new List<Card>();
        _cardsToChange = new List<Card>();
        GetComponent<CardCreator>().CreateCards();
        StartCoroutine(OpenCards());
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
                _cardPositionPattern.SetPosition(1 + (_cards.Count - 1) * positionFactor, i * positionFactor, _cards[i].transform);

            yield return null;
        }

        IsOpenAnimationActive = false;
    }

    private void StartChangeCards()
    {
        _cardsToChange = new List<Card>(_cards);
        StartChangeNextCard();
    }

    private void ClearAndStartChangeNextCard()
    {
        _cardsToChange.RemoveAt(0);
        StartChangeNextCard();
    }

    private void StartChangeNextCard()
    {
        if (_cardsToChange.Count == 0)
            return;

        var card = _cardsToChange[0];
        card.OnValueChangeAnimationFinish += ClearAndStartChangeNextCard;
        card.SetRandomValueWithAnimation();
    }

    private void UpdateCardsPositions()
    {
        for (int i = 0; i < _cards.Count; i++)
            _cardPositionPattern.SetPosition(_cards.Count, i, _cards[i].transform);
    }
}

using UnityEngine;

public class CardPositionPatternArc : ICardPositionPattern
{
    private float _cardWidth = 80;
    private float _cardsMaxHeight = 140;
    private float _cardArcHeightStep = 10;
    private float _cardArcAngleStep = 10;
    private float _distanceBetweenCards = 13;

    public void SetPosition(float cardCount, float cardNumber, Transform transform)
    {
        float width = _cardWidth + _distanceBetweenCards;
        float x = -width * (0.5f * (cardCount - 1) - cardNumber);
        float positionFactor = (cardCount - 1) * 0.5f - cardNumber;
        float y = _cardsMaxHeight - positionFactor * positionFactor * _cardArcHeightStep;
        float angle = positionFactor * _cardArcAngleStep;

        transform.localPosition = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

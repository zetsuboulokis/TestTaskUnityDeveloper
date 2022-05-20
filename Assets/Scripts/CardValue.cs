using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardValue : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private int _value;
    private float _changeValueTimeStep = 0.2f;

    public void Set(int value)
    {
        _value = value;
        _text.text = value.ToString();
    }

    public void SetWithAnimation(int value)
    {
        _value = value;
        StartCoroutine(ChangeValueAnimated());
    }

    private IEnumerator ChangeValueAnimated()
    {
        if (!int.TryParse(_text.text, out int textValue))
        {
            _text.text = _value.ToString();
            yield break;
        }

        while (textValue != _value)
        {
            textValue += (int) Mathf.Sign(_value - textValue);
            _text.text = textValue.ToString();

            yield return new WaitForSeconds(_changeValueTimeStep);
        }

        FindObjectOfType<CardHand>().StartChangeNextCard();
    }
}
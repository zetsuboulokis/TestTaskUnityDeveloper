using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class Card : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RawImage _art;
    [SerializeField]
    private GameObject _shinyEffect;
    [SerializeField]
    private Text _textTitle;
    [SerializeField]
    private Text _textDescription;
    [SerializeField]
    private CardValue[] _cardValues;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Animator _animator;
    private Vector3 _positionPreDrag;

    public enum ValueType
    {
        Attack,
        HP,
        Mana,

        ValueTypeCount
    }

    public void Init()
    {
        LoadArt();

        _rectTransform = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        _canvas = FindObjectOfType<Canvas>();
        _canvasGroup = FindObjectOfType<CanvasGroup>();
    }

    public void SetValue(ValueType valueType, int value)
    {
        _cardValues[(int)valueType].Set(value);
    }

    public void SetValueWithAnimation(ValueType valueType, int value)
    {
        _cardValues[(int)valueType].SetWithAnimation(value);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _shinyEffect.SetActive(true);
        _animator.SetInteger("State", 1);
        _positionPreDrag = transform.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _shinyEffect.SetActive(false);
        _animator.SetInteger("State", 2);

        var go = eventData.pointerCurrentRaycast.gameObject;
        if (go != null && go.tag == "Board")
            return;

        transform.localPosition = _positionPreDrag;
        _canvasGroup.blocksRaycasts = true;
    }

    private void LoadArt()
    {
        StartCoroutine(Utils.Dowloader.LoadImage("https://picsum.photos/70/60", _art));
    }
}
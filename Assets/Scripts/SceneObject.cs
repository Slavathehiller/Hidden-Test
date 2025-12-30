using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SceneObject : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction<SceneObject> OnClick;

    [SerializeField]
    private Vector3 _position;

    [SerializeField]
    private PolygonCollider2D _collider;

    [SerializeField]
    private SpriteRenderer _renderer;

    private Sprite _UISprite;
    private string _description;

    public Sprite UISprite => _UISprite;
    public string Description => _description;

    private void Awake()
    {
        transform.position = _position;
    }

    public void Init(Sprite sprite, string description)
    {
        _UISprite = sprite;
        _description = description;
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
        StopAllCoroutines();
        StartCoroutine(FadeOutCorutineRoutine());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    private IEnumerator FadeOutCorutineRoutine()
    {
        var color = _renderer.color;
        float elapsedTime = 0f;
        float _duration = 1;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _duration);
            _renderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        _renderer.color = new Color(color.r, color.g, color.b, 0f);
    }

    public void Reset()
    {
        var color = _renderer.color;
        _renderer.color = new Color(color.r, color.g, color.b, 1);
        _collider.enabled = false;
    }

}

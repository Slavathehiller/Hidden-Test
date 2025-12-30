using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [SerializeField]
    private Image _picture;
    [SerializeField]
    private TMP_Text _text;

    public void Init(Sprite sprite, string text)
    {
        _picture.sprite = sprite;
        _picture.SetNativeSize();
        _text.text = text;
    }
    public void SetDisplayType(DisplayType displayType)
    {
        _picture.enabled = displayType == DisplayType.Picture;
        _text.enabled = displayType == DisplayType.Text;
    }
}

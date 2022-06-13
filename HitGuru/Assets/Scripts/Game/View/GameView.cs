using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameInfoText;

    public void UpdateView(string text)
    {
        _gameInfoText.text = text;
    }
}

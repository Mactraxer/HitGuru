using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Slider _helthSlider;
    [SerializeField] private TextMeshProUGUI _healthCountText;

    public void DisplayHealthCount(string value)
    {
        _healthCountText.text = value;
    }

    public void DisplayHelthPercent(float percent)
    {
        _helthSlider.value = percent;
    }
   
    public void DisappearHealth()
    {
        _helthSlider.gameObject.SetActive(false);
    }
}

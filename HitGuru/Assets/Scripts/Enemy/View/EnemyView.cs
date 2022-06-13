using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Slider _helthSlider;
    [SerializeField] private TextMeshProUGUI _healthCountText;

    public void UpdateView(EnemyViewModel viewModel)
    {
        _healthCountText.text = viewModel._healthCountRemained;
        _helthSlider.value = viewModel._healthPercentRemained;
    }
   
    public void DisappearHealth()
    {
        _helthSlider.gameObject.SetActive(false);
    }
}

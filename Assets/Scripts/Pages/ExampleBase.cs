using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ExampleBase : MonoBehaviour
{
    [SerializeField] protected Button _runButton;
    [SerializeField] private TextMeshProUGUI _resultView;

    protected void Awake()
    {
        _runButton.onClick.AddListener(Run);
    }

    public abstract void Run();

    protected void PrintResult(string result)
    {
        if (!string.IsNullOrEmpty(result))
            _resultView.text = result.ToString();
    }
}

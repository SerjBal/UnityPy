using UnityEngine;
using UnityEngine.UI;

public class Page: MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    private void Awake()
    {
        _nextButton.onClick.AddListener(NextPage);
        _prevButton.onClick.AddListener(PrevPage);
    }

    private void NextPage() => DI.Get<IPages>().NextPage();

    private void PrevPage() => DI.Get<IPages>().PrevPage();
}

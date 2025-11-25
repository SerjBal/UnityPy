using UnityEngine;

public class Pages : MonoBehaviour, IPages
{
    [SerializeField] Transform pagesContainer;
    int _activePage = 0;

    public void Init() => pagesContainer.GetChild(_activePage).gameObject.SetActive(true);

    public void NextPage()
    {
        Leaf(+1);
    }

    public void PrevPage()
    {
        Leaf(-1);
    }

    private void Leaf(int value)
    {
        pagesContainer.GetChild(_activePage).gameObject.SetActive(false);
        _activePage = Mathf.Clamp(_activePage + value, 0, pagesContainer.childCount-1);
        pagesContainer.GetChild(_activePage).gameObject.SetActive(true);
    }
}

using UnityEngine;

public class AppServiceLocator : MonoBehaviour
{
    [SerializeField] private ScriptRunner _scriptRunner;
    [SerializeField] private Pages _pages;

    public IScriptRunner ScriptRunner { get => _scriptRunner; }
    
    private void Awake()
    {
        DI.Register<IScriptRunner>(_scriptRunner);
        DI.Register<IPages>(_pages);
        Initialize();
    }

    public void Initialize()
    {
        _scriptRunner.Init();
        _pages.Init();
    }
}
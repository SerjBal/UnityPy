using Python.Runtime;

public interface IScriptRunner : IService
{
    dynamic GetModule(string scriptName);

    T ExecuteCode<T>(string pythonCode, string variableName = null);
    void ExecuteCode(string pythonCode);
    dynamic ExecuteCodeWithScope(string pythonCode);
    PyObject ExecuteCodeRaw(string pythonCode, string variableName = null);
}

using System.IO;
using Python.Runtime;
using UnityEngine;

public class ScriptRunner : MonoBehaviour, IScriptRunner
{
    private readonly string _projectPath = Path.Combine(Application.streamingAssetsPath, "Py");
    private PyEngine _pyEngine;

    public void Init()
    {
        _pyEngine = new PyEngine(_projectPath);
        _pyEngine.Initialize();
    }

    /// <summary>
    /// Get Python module by script name
    /// </summary>
    public dynamic GetModule(string scriptName)
    {
        try
        {
            using (Py.GIL())
            {
                return Py.Import(scriptName);
            }
        } catch (PythonException e)
        {
            Debug.LogError($"Failed to import module: {e.Message}");
            Debug.LogError($"Python Stack Trace:\n{e.StackTrace}");
            return null;
        }
    }

    /// <summary>
    /// Execute Python code from string and return result
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="pythonCode">Python code to execute</param>
    /// <param name="variableName">Variable name to return (optional)</param>
    /// <returns>Result of execution</returns>
    public T ExecuteCode<T>(string pythonCode, string variableName = null)
    {
        try
        {
            using (Py.GIL())
            {
                using PyModule scope = Py.CreateScope();

                // Execute the Python code
                scope.Exec(pythonCode);

                // If variable name is specified, return that variable
                if (!string.IsNullOrEmpty(variableName))
                {
                    if (scope.Contains(variableName))
                    {
                        PyObject result = scope.Get(variableName);
                        return result.As<T>();
                    } else
                    {
                        Debug.LogError($"Variable '{variableName}' not found in executed code");
                        return default(T);
                    }
                }

                // If no variable specified, try to get the last expression result
                return default(T);
            }
        } catch (PythonException e)
        {
            Debug.LogError($"Python code execution failed: {e.Message}");
            Debug.LogError($"Python Stack Trace:\n{e.StackTrace}");
            return default(T);
        }
    }

    /// <summary>
    /// Execute Python code from string without return value
    /// </summary>
    public void ExecuteCode(string pythonCode)
    {
        try
        {
            using (Py.GIL())
            {
                using PyModule scope = Py.CreateScope();
                scope.Exec(pythonCode);
            }
        } catch (PythonException e)
        {
            Debug.LogError($"Python code execution failed: {e.Message}");
            Debug.LogError($"Python Stack Trace:\n{e.StackTrace}");
        }
    }

    /// <summary>
    /// Execute Python code and return dynamic scope for accessing multiple variables
    /// </summary>
    public dynamic ExecuteCodeWithScope(string pythonCode)
    {
        try
        {
            using (Py.GIL())
            {
                PyModule scope = Py.CreateScope();
                scope.Exec(pythonCode);
                return scope;
            }
        } catch (PythonException e)
        {
            Debug.LogError($"Python code execution failed: {e.Message}");
            Debug.LogError($"Python Stack Trace:\n{e.StackTrace}");
            return null;
        }
    }

    /// <summary>
    /// Execute Python code from string and return PyObject for advanced operations
    /// </summary>
    /// <param name="pythonCode">Python code to execute</param>
    /// <param name="variableName">Variable name to return</param>
    /// <returns>PyObject result</returns>
    public PyObject ExecuteCodeRaw(string pythonCode, string variableName = null)
    {
        try
        {
            using (Py.GIL())
            {
                using PyModule scope = Py.CreateScope();
                scope.Exec(pythonCode);

                if (!string.IsNullOrEmpty(variableName))
                {
                    return scope.Get(variableName);
                }

                return null;
            }
        } catch (PythonException e)
        {
            Debug.LogError($"Python code execution failed: {e.Message}");
            Debug.LogError($"Python Stack Trace:\n{e.StackTrace}");
            return null;
        }
    }



    void OnDestroy() => _pyEngine.Dispose();
}
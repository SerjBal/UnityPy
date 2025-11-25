using Python.Runtime;
using System;
using System.IO;
using UnityEngine;

public class PyEngine
{
    private readonly string _projectPath;
    private string _pyLibPath;

    public PyEngine(string yourProjectPath)
    {
        CheckPath(yourProjectPath);
        _projectPath = yourProjectPath;
    }

    public void Initialize()
    {
        SetEnvironment();
        EngineInitialize();
    }

    private void SetEnvironment()
    {
        var pyPath = Path.Combine(Application.streamingAssetsPath, "py38");
        var pyPackagesPath = Path.Combine(pyPath, "Lib", "site-packages");
        CheckPath(pyPackagesPath);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        var libName = "libpython3.8.dylib";
        var pyLibPath = Path.Combine(pyPath, libName);

        CheckPath(pyLibPath);
        Environment.SetEnvironmentVariable("DYLD_LIBRARY_PATH", pyPath, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{_projectPath}:{pyPackagesPath}", EnvironmentVariableTarget.Process);

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        var libName = "python38.dll";
        var pyLibPath = Path.Combine(pyPath, libName);

        CheckPath(pyLibPath);
        Environment.SetEnvironmentVariable("PYTHONHOME", pyLibPath);
        Environment.SetEnvironmentVariable("PATH", pyLibPath, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{_projectPath};{pyPackagesPath}", EnvironmentVariableTarget.Process);

#endif
        _pyLibPath = pyLibPath;
    }

    private void EngineInitialize()
    {
        Runtime.PythonDLL = _pyLibPath;

        try
        {
            PythonEngine.Initialize();

            Debug.Log("Python initialized successfully.");
        } catch (Exception ex)
        {
            throw new Exception($"Python initialization failed: {ex.Message}", ex);
        }
    }

    private void CheckPath(string path)
    {
        if (!File.Exists(path))
            new Exception($"The path {path} is not exists!");
    }

    public void Dispose()
    {
        PythonEngine.Shutdown();
    }
}
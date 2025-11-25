using UnityEngine;
using UnityEngine.UI;

public class ExampleA : ExampleBase
{
    /// <summary>
    /// Call specific methods without return value
    /// </summary>
    public override void Run()
    {
        var scriptRunner = DI.Get<IScriptRunner>();

        dynamic testScript = scriptRunner.GetModule("ExampleA"); // Get script module in StreamingAssetsPath/Py/ExampleA.py
        testScript.my_method();  // Use simple method
    }
}
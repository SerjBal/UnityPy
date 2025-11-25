import clr
clr.AddReference("UnityEngine")
from UnityEngine import Debug


counter = 0

def my_method():
    global counter
    
    counter += 1
    Debug.Log(f"Py: Message from python: counter = {counter}")


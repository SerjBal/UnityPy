import numpy as np

import clr
clr.AddReference("UnityEngine")
from UnityEngine import Debug


class SqrtCalculator:
    def __init__(self, initial_value = 0):
        self.result = initial_value
        Debug.Log(f"Py: Calculator initialized with: {initial_value}")
    
    def add(self, x):
        self.result += x
        
    def calc_sqrt_numpy(self):
        result = np.sqrt(self.result)
        Debug.Log(f"Py: Calculated square root using numpy of {self.result} = {result}")
        return result


debug_message = "Py: Script loaded successfully"
Debug.Log(debug_message)

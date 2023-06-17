using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOpstionsWindow : MonoBehaviour
{
   
   public  void CloseWindow() {
        WindowList.Instance.OptionsWindow.SetActive(false);
    }
}

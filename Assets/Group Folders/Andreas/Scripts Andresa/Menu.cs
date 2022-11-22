using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   
     public Canvas MenuCanvas;    // now you have to drag and drop your canvas in the editor in the script component
     private bool MenuActive = false; // do we have to display the canvas (true) or not (false)
     
     void Update () 
     {
         if (OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.Hands))
         { 
             MenuActive = !MenuActive; // change the state of your bool
             MenuCanvas.gameObject.SetActive(MenuActive); // display or not the canvas (following the state of the bool)
         }
     }
}

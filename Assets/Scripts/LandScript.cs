using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandScript : MonoBehaviour {

	public void Click()
    {
        GameManager.Instance.currentLand = this.gameObject;
        this.gameObject.GetComponent<Button>().interactable = false;
        GameManager.Instance.LandColor = this.gameObject.GetComponent<Image>().color;
        GameManager.Instance.StartGame();
    }
}

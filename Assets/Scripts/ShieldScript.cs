using UnityEngine;

public class ShieldScript : MonoBehaviour
{
   public void SetActive(bool value)
    {

        var Shield = GameObject.FindWithTag("Shield");
        if (value)
        {
            Shield.GetComponent<PlayerCollision>().enabled = false;
        }
        else
        {
            Shield.GetComponent<PlayerCollision>().enabled = true;


        }
    }
}

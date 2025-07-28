using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeZone : MonoBehaviour
{
    public Tilemap techo;
    public Tilemap pared;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "FeetPlayer")
        {
            var a = techo.gameObject.activeSelf;
            var b = pared.gameObject.activeSelf;

            techo.gameObject.SetActive(!a);
            pared.gameObject.SetActive(!b);
        }
    }
    
    
}

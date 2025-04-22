using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Life: MonoBehaviour
{
    public float life=100;
    public float lifemax=100;
    public Image healthbar;
    public Text numberlife;
    void Update(){
        _interface();
    }

    void _interface()
    {
        healthbar.fillAmount = life/lifemax;
        //numberlife.text="Life: " + life.ToString("f0");
    }
}
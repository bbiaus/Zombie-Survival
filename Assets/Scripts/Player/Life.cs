using UnityEngine;
using UnityEngine.UI;
public class Life: MonoBehaviour
{
    public float Lifee=100;
    public float LifeMax=100;
    
    public Image HealthBar;
    void Update(){
        updatehealth();
    }

    void updatehealth(){
        HealthBar.fillAmount=Lifee/LifeMax;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyMaus : MonoBehaviour
{

    private GameObject Player;
   public GameObject Maus;
    private float Minapeardistance =20f;
    private Vector3 von;
    private bool Mtrigger = false;
    private float Lerpmove;
    public GameObject spawn;
    // Start is called before the first frame update
    private  void Start()
    {
        Player = GameObject.Find("Player 1");
        von = new Vector3(8.23f, -3.16f, 41.55f);
    }

    // Update is called once per frame
   private void Update()
    {
        
       // von = spawn.transform.position;
        if (Vector3.Distance(transform.position,Player.transform.position) <= Minapeardistance)
        {
            Mtrigger = true;
        }
        if (Mtrigger == true)
        {
          transform.position=  Vector3.Lerp(von, new Vector3(von.x, von.y, von.z + 5),Lerpmove);
            Lerpmove += Time.deltaTime*0.3f;
            if(transform.position == new Vector3(von.x, von.y, von.z + 5))
            {
                Destroy(gameObject);
           }
        }
    }

    private void OnDestroy()
    {
        Instantiate(Maus,new Vector3(8.46f,0.88f,40.89f), Maus.transform.rotation);

        for(int i = 1; i < 8; i++)
        {
            FindObjectOfType<AudioManager>().Play("Mousehole" + i);
        }
    }
}

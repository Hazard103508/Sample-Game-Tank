using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rosso
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image CoolingProgress;
        [SerializeField] private Text BulletCount;
        [SerializeField] private Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            Camera.main.transform.parent = player.transform;
        }

        // Update is called once per frame
        void Update()
        {
            BulletCount.text = $"X {player.Bullets}";
            CoolingProgress.fillAmount = player.CoolingProgress;
        }
    }
}
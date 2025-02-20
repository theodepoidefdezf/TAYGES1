using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Assets;
using TMPro;
namespace Assets
{

    public class NewBehaviourScript : MonoBehaviour
    {
        [SerializeField] private Button serverBtn;
        [SerializeField] private Button hostBtn;
        [SerializeField] private Button clientBtn;
        [SerializeField] private Button TAYGES;
        [SerializeField] private Image Logo;
        [SerializeField] private Button Pause;

        [SerializeField] private Button Continue;
        [SerializeField] private Button Quit;

        [SerializeField] private Image Pointeur;
        [SerializeField] private TMP_InputField Name;

        public static bool InPause = false;
        public static string Name1 = "";

        private void Awake()
        {
     
            serverBtn.gameObject.SetActive(true);
            hostBtn.gameObject.SetActive(true);
            clientBtn.gameObject.SetActive(true);
            TAYGES.gameObject.SetActive(true);
            Logo.gameObject.SetActive(true);

            Pause.gameObject.SetActive(false);
            Continue.gameObject.SetActive(false);
            Quit.gameObject.SetActive(false);
            Pointeur.gameObject.SetActive(false);

            serverBtn.onClick.AddListener(() =>
            {
                Name.gameObject.SetActive(false);
                Name1 = Name.text;
                NetworkManager.Singleton.StartServer();
                HideButtons();
                Pause.gameObject.SetActive(true);
                Pointeur.gameObject.SetActive(true);
            });

            hostBtn.onClick.AddListener(() =>
            {
                Name.gameObject.SetActive(false);
                Name1 = Name.text;
                NetworkManager.Singleton.StartHost();
                HideButtons();
                Pause.gameObject.SetActive(true);
                Pointeur.gameObject.SetActive(true);
            });

            clientBtn.onClick.AddListener(() =>
            {
                Name.gameObject.SetActive(false);
                Name1 = Name.text;
                NetworkManager.Singleton.StartClient();
                HideButtons();
                Pause.gameObject.SetActive(true);
                Pointeur.gameObject.SetActive(true);
            });

            Pause.onClick.AddListener(() =>
            {
                InPause = true;
                Pause.gameObject.SetActive(false);
                Pointeur.gameObject.SetActive(false);
                Continue.gameObject.SetActive(true);
                Quit.gameObject.SetActive(true);
                Quit.onClick.AddListener(() =>
                {
                    if (NetworkManager.Singleton.IsHost)
                    {
                        NetworkManager.Singleton.Shutdown();
                    }
                    else if (NetworkManager.Singleton.IsClient)
                    {
                        NetworkManager.Singleton.Shutdown();
                    }
                    InPause = false;
                    ShowButtons();
                });

                Continue.onClick.AddListener(() =>
                {
                    InPause = false;
                    Pause.gameObject.SetActive(true);
                    Pointeur.gameObject.SetActive(true);
                    Continue.gameObject.SetActive(false);
                    Quit.gameObject.SetActive(false);
                });

            });
        }

        private void HideButtons()
        {
            serverBtn.gameObject.SetActive(false);
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
            TAYGES.gameObject.SetActive(false);
            Logo.gameObject.SetActive(false);
        }
        private void ShowButtons()
        {
            Name.gameObject.SetActive(true);
            serverBtn.gameObject.SetActive(true);
            hostBtn.gameObject.SetActive(true);
            clientBtn.gameObject.SetActive(true);
            TAYGES.gameObject.SetActive(true);
            Logo.gameObject.SetActive(true);

            Pause.gameObject.SetActive(false);
            Continue.gameObject.SetActive(false);
            Quit.gameObject.SetActive(false);
        }

    }
}
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Data : MonoBehaviour
    {
        public static Data instance { get; private set; }

        string playerName;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                SetPlayerName(GeneratePlayerName());
            }
            else
            {
                Destroy(gameObject);
            }
        }

        string GeneratePlayerName()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            System.Random random = new System.Random();
            string randomChars = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)]).ToArray());
            return "Player_" + randomChars;
        }

        public string GetPlayerName() => playerName;
        public void SetPlayerName(string name) => playerName = name.ToUpper();
    }
}

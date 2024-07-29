﻿using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
      
            if(bootstrapper != null) return;

            Instantiate(_bootstrapperPrefab);
        }
    }
}
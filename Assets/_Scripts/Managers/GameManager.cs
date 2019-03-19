using Gamekit3D;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ioc.IOCStudents.Core
{
    public enum GameState
    {
        splash,
        mainMenu,
        inGame,
        gameOver,
        exit,
        transition2
    }

    public class GameManager : PersistentSingleton<GameManager>
    {
        protected GameState m_gameState;
        protected float m_playedTime;

        protected override void Awake()
        {
            m_gameState = GameState.splash;
            m_playedTime = 0.0f;
            base.Awake();
        }

        protected void Update()
        {
            if (m_gameState == GameState.inGame)
            {
                m_playedTime += Time.deltaTime;
            }
        }

        public void ToTransitionScene2()
        {
            ChangeState(GameState.transition2);
        }

        public void ToManinMenu()
        {
            ChangeState(GameState.mainMenu);
        }

        public void ToGame()
        {
            ChangeState(GameState.inGame);
        }
        public void ToGameOver()
        {
            ChangeState(GameState.gameOver);
        }

        public void ToExit()
        {
            ChangeState(GameState.exit);
        }

        private void ChangeState(GameState newState)
        {
            switch (newState)
            {
                case GameState.splash:
                    break;
                case GameState.mainMenu:
                    StartCoroutine(LoadGame("MainMenu"));
                    break;
                case GameState.inGame:
                    StartCoroutine(LoadGame("Level01"));
                    break;
                case GameState.gameOver:
                    StartCoroutine(LoadGame("EndGame"));
                    break;
                case GameState.transition2:
                    StartCoroutine(LoadGame("SceneTransition2"));
                    break;
                case GameState.exit:
                    Application.Quit();
                    break;
            }

            m_gameState = newState;
        }

        private IEnumerator LoadGame(string sceneName)
        {
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return StartCoroutine(ScreenFader.FadeSceneIn());
        }
    }
}
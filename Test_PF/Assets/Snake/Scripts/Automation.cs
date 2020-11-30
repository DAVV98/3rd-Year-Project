using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlanZucconi.Snake
{
    public class Automation : MonoBehaviour
    {
        [Header("Game Parameteres")]
        [EditorOnly]
        public SnakeGame Snake;
        [Space]
        [Range(0f,1f)]
        public float Delay = 0f;

        [Header("Simulation Parameteres")]
        [Range(1,1000)]
        public int TestsPerAI = 100;
        public bool Rendering = true;
        [EditorOnly]
        public bool ClearData = true;
        [Space]
        public List<SnakeAI> AIs;


        // Use this for initialization
        //void Start()
        [Button(Editor=false)]
        void Run ()
        {
            Snake.DeathCallback.AddListener(SimulationDone);

            StartCoroutine(Automate());
        }
        
        IEnumerator Automate ()
        {
            foreach (SnakeAI ai in AIs)
            {
                Debug.Log("Testing AI: [" + ai.AIName + "]...");

                if (ClearData)
                    ai.PlotData.Data.Clear();

                for (int i = 0; i < TestsPerAI; i++)
                {
                    Debug.Log("\tSimulation " + i + "\tof " + TestsPerAI + "...");

                    Reset(ai);
                    Snake.StartGame();

                    StartSimulation();
                    yield return new WaitWhile(() => Running); // Wait until simulation done
                    Snake.StopGame();
                }
            }

            

            Debug.Log("DONE!");
        }


        public void Reset(SnakeAI ai)
        {
            Snake.AI = ai;

            Snake.Restart();

            Snake.Delay = Delay;
            Snake.Rendering = Rendering;
            Snake.PauseOnDeath = false;
        }



        private bool Running = false;
        public void StartSimulation ()
        {
            Running = true;
        }
        public void SimulationDone ()
        {
            CollectStats(Snake.AI);
            Running = false;
        }


        public void CollectStats (SnakeAI ai)
        {
            ai.PlotData.Add(new Vector2(Snake.Ticks, Snake.Body.Count));

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(ai);
#endif
        }
    }
}
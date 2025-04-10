using UnityEngine;
using DG.Tweening;

namespace AE
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance;

        [Header("Win Condition")]
        public int requiredLitCandles = 2;
        public int requiredSwordsPlaced = 2;

        private int candlesLit = 0;
        private int swordsPlaced = 0;

        [Header("Chest")]
        public GameObject chest;
        public Transform chestLid;
        public float chestOpenDuration = 1f;
        public float chestOpenAngle = 110f;

        private bool puzzleComplete = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void RegisterCandlesLit()
        {
            candlesLit++;
            Debug.Log($"Candles lit: {candlesLit}/{requiredLitCandles}");
            CheckPuzzleComplete();
        }

        public void RegisterSwordPlacement()
        {
            swordsPlaced++;
            Debug.Log($"Swords placed: {swordsPlaced}/{requiredSwordsPlaced}");
            CheckPuzzleComplete();
        }

        private void CheckPuzzleComplete()
        {
            if (puzzleComplete)
            {
                return;
            }

            if (candlesLit >= requiredLitCandles && swordsPlaced >= requiredSwordsPlaced)
            {
                puzzleComplete = true;
                Debug.Log("Puzzle Complete! Opening chest...");
                OpenChest();
            }
        }

        private void OpenChest()
        {
            if (chest != null)
            {
                chest.SetActive(true);
            }

            if (chestLid != null)
            {
                chestLid.DOLocalRotate(new Vector3(chestOpenAngle, 0, 0), chestOpenDuration)
                    .SetEase(Ease.OutCubic);
            }
        }
    }
}

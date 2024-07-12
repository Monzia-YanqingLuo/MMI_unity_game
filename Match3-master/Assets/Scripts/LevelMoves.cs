
using UnityEngine.Audio;
using UnityEngine;

namespace Match3
{
    public class LevelMoves : Level
    {
        public int numMoves;
        public int targetScore;

        private int _movesUsed = 0;
        public AudioClip movementSound; // Keep the AudioClip reference to specify the movement sound

        private void Start()
        {
            
            type = LevelType.Moves;
            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore);
            hud.SetRemaining(numMoves);
        }


        public override void OnMove()
        {
            _movesUsed++;
            hud.SetRemaining(numMoves - _movesUsed);

            // Play the move sound effect using the HUD's AudioSource
            if (hud.audioSource != null && movementSound != null)
            {
                hud.audioSource.PlayOneShot(movementSound);
            }

            if (numMoves - _movesUsed == 0)
            {
                if (currentScore >= targetScore)
                {
                    GameWin();
                }
                else
                {
                    GameLose();
                }
            }
        }
    }

}


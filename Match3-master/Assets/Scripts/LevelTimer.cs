using UnityEngine;
using UnityEngine.Audio;

namespace Match3
{
    public class LevelTimer : Level
    {

        public int timeInSeconds;
        public int targetScore;

        private float _timer;
        public AudioClip movementSound; // Keep the AudioClip reference to specify the movement sound

        private int _movesUsed = 0;

        private void Start()
        {
            type = LevelType.Timer;

            hud.SetLevelType(type);
            hud.SetScore(currentScore);
            hud.SetTarget(targetScore);
            hud.SetRemaining($"{timeInSeconds / 60}:{timeInSeconds % 60:00}");
        }

        public override void OnMove()
        {
            _movesUsed++;
            if (_movesUsed > 0)
            {
                if (hud.audioSource != null && movementSound != null)
                {
                    hud.audioSource.PlayOneShot(movementSound);
                }
            }
        }

        private void Update()
        {
            float previousTime = _timer;
            _timer += Time.deltaTime;
            int previousSeconds = (int)(timeInSeconds - previousTime);
            int currentSeconds = (int)(timeInSeconds - _timer);
            
            // Check if a new second has ticked
            if (previousSeconds != currentSeconds)
            {
                hud.SetRemaining(
                    $"{(int)Mathf.Max((timeInSeconds - _timer) / 60, 0)}:{(int)Mathf.Max((timeInSeconds - _timer) % 60, 0):00}");
                // Play the sound effect
                
            }

            if (_timer >= timeInSeconds)
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

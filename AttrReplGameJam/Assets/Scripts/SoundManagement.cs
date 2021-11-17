using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    [System.Serializable]
    public struct SoundEffect
    {
        public string name;
        public AudioClip sound;
    }

    [SerializeField] private List<SoundEffect> soundEffects;

    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioSource audioSrcLoop;

    // Start is called before the first frame update
    void Start()
    {
        Actions.OnRepelStar += OnRepelStar;
        Actions.OnAttractStar += OnAttractStar;
        Actions.OnStopAttractStar += OnStopAttractStar;
        Actions.OnStarReturn += OnStarReturn;
        Actions.OnStarHit += OnStarHit;

        Actions.OnEnemyHitPlayer += OnPlayerTakeDamage;
    }

    private void OnSoundEvent(string eventName)
    {
        foreach (SoundEffect se in soundEffects) 
        {
            if (se.name == eventName)
            {
                audioSrc.PlayOneShot(se.sound);
                break;
            }
        }
    }

    private void OnSoundEventLoop(string eventName, bool enable)
    {
        foreach (SoundEffect se in soundEffects) 
        {
            if (se.name == eventName)
            {
                audioSrcLoop.clip = se.sound;

                if (enable)
                    audioSrcLoop.Play();
                else
                    audioSrcLoop.Stop();

                break;
            }
        }
    }

    public void StopAllSoundEffects() 
    {
        audioSrc.Stop();
        audioSrcLoop.Stop();
    }

    private void OnRepelStar() { OnSoundEvent("OnRepelStar"); }
    private void OnAttractStar() { OnSoundEventLoop("OnAttractStar", true); }
    private void OnStopAttractStar() { OnSoundEventLoop("OnAttractStar", false); }
    private void OnStarReturn() { OnSoundEvent("OnStarReturn"); }
    private void OnStarHit() { OnSoundEvent("OnStarHit"); }

    private void OnPlayerTakeDamage() { OnSoundEvent("OnPlayerTakeDamage"); }

    void OnDestroy()
    {
        Actions.OnRepelStar -= OnRepelStar;
        Actions.OnAttractStar -= OnAttractStar;
        Actions.OnStopAttractStar -= OnStopAttractStar;
        Actions.OnStarReturn -= OnStarReturn;
        Actions.OnStarHit -= OnStarHit;

        Actions.OnEnemyHitPlayer -= OnPlayerTakeDamage;
    }
}

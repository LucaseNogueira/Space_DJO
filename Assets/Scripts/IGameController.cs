using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void LimparFalas();
    void LimparAudioSource();
    void InterromperFala(AudioClip clip);
    void AlterarScene(int idScene);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlatformLights : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] meshRenderer;
    [SerializeField] private Material[] materials;
    [SerializeField] private GameController gameController;


    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeToGreen()
    {
        bool changedLight = false;
        foreach (MeshRenderer mesh in meshRenderer)
        {
            Material[] mats = mesh.materials;
            if (mats[1] != materials[0])
            {
                changedLight = true;
                mats[1] = materials[0];
                mesh.materials = mats;
            }
        }

        if (changedLight)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(audioClips[0]);
            gameController.UpdateScore();
        }
    }

    public void ChangeToBlue()
    {
        foreach (MeshRenderer mesh in meshRenderer)
        {
            Material[] mats = mesh.materials;
            mats[1] = materials[1];
            mesh.materials = mats;
        }

        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(audioClips[1]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public Material Flash;
    public Material SkyFlash;
    public Material Bolt;
    public Material Spark;

    public Material DupeFlash;
    public Material DupeBolt;
    public Material DupeSpark;

    public Material SingleFlash;
    public Material SingleBolt;
    public Material SingleSpark;

    public ParticleSystem Lightning;
    public ParticleSystem LightningDupe;
    public ParticleSystem BigBolt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int numPartitions = 8;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[i] / (512 / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }

        var em = Lightning.emission;
        em.rateOverTime = 15 * aveMag[0];

        var main = Lightning.main;
        main.startSpeed = 90 * aveMag[1];

        if (aveMag[4] > .8f)
            LightningDupe.Play();

        if (aveMag[5] > .8f)
            BigBolt.Play();

        Flash.SetColor("_EmissiveColor", new Color(aveMag[1], aveMag[2], aveMag[0], .5f));
        SkyFlash.SetColor("_EmissiveColor", new Color(aveMag[1], aveMag[2], aveMag[0], .5f));
        Bolt.SetColor("_EmissiveColor", new Color(aveMag[1], aveMag[2], aveMag[0], 1.0f));
        Spark.SetColor("_EmissiveColor", new Color(aveMag[1], aveMag[2], aveMag[0], 1.0f));

        DupeFlash.SetColor("_EmissiveColor", new Color(aveMag[2], aveMag[0], aveMag[1], .5f));
        DupeBolt.SetColor("_EmissiveColor", new Color(aveMag[2], aveMag[0], aveMag[1], 1.0f));
        DupeSpark.SetColor("_EmissiveColor", new Color(aveMag[2], aveMag[0], aveMag[1], 1.0f));

        SingleFlash.SetColor("_EmissiveColor", new Color(aveMag[0], aveMag[1], aveMag[2], .5f));
        SingleBolt.SetColor("_EmissiveColor", new Color(aveMag[0], aveMag[1], aveMag[2], 1.0f));
        SingleSpark.SetColor("_EmissiveColor", new Color(aveMag[0], aveMag[1], aveMag[2], 1.0f));

        Bolt.SetFloat("_Emissiveness", aveMag[3]*.35f);
        SkyFlash.SetFloat("_Emissiveness", aveMag[3]*.07f);

        DupeBolt.SetFloat("_Emissiveness", aveMag[3] * .4f);
        DupeSpark.SetFloat("_Emissiveness", aveMag[3] * .5f);

        SingleBolt.SetFloat("_Emissiveness", aveMag[3] * .4f);
        SingleSpark.SetFloat("_Emissiveness", aveMag[3] * .5f);

    }
}

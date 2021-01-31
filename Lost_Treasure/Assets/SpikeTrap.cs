using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private enum State
    {
        Lowered,
        Lowering,
        Raising,
        Raised
    }
    public float interval = 2f;
    public float raiseWaitTime = .3f;
    public float lowerTime = .6f;
    public float raiseTime = .08f;
    public Transform spikeHolder;
    public GameObject hitboxGameObject;
    public GameObject colliderGameObject;


    private float lastSwitchTime = Mathf.NegativeInfinity;
    private State state = State.Lowered;
    private const float SpikeHeight = 3.6f;
    private const float LoweredSpikeHeight = 0.08f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartRaising", interval);
    }
    void StartRaising()
    {
        lastSwitchTime = Time.time;
        state = State.Raising;
        hitboxGameObject.SetActive(true);
    }
    void StartLowering()
    {
        lastSwitchTime = Time.time;
        state = State.Lowering;
    }
    // Update is called once per frame
    void Update()
    {
        if (state == State.Lowering)
        {
            Vector3 scale = spikeHolder.localScale;
            scale.y = Mathf.Lerp(SpikeHeight, LoweredSpikeHeight, (Time.time - lastSwitchTime) / lowerTime);
            spikeHolder.localScale = scale;
            if (scale.y == LoweredSpikeHeight)
            {
                Invoke("StartRaising", interval);
                colliderGameObject.SetActive(false);
                state = State.Lowered;
            }
        }
        else if (state == State.Raising)
        {
            Vector3 scale = spikeHolder.localScale;
            scale.y = Mathf.Lerp(LoweredSpikeHeight, SpikeHeight, (Time.time - lastSwitchTime) / raiseTime);
            spikeHolder.localScale = scale;
            if (scale.y == SpikeHeight)
            {
                Invoke("StartLowering", raiseWaitTime);
                colliderGameObject.SetActive(true);
                hitboxGameObject.SetActive(false);
                state = State.Raised;
            }
        }

    }
}

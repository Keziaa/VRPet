using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairomizerHandle : OVRGrabbable
{
    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private AudioSource slurpStart;

    [SerializeField]
    private AudioSource slurpEnd;

    public static HairomizerHandle Instance;

    public float HairVelocity { get; private set; }
    Vector3 InitialHandleEuler;
    Vector3 initialGrabberPosition;
    OVRGrabber grabbingHand = null;

    private bool isIdle = true;
    private bool firstPull = false;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;    
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Instance = null;
    }

    protected override void Start()
    {
        m_overrideGrabMovement = true;        
        base.Start();
        HairVelocity = 0;
        InitialHandleEuler = transform.localEulerAngles;
    }

    void Update()
    {
        //HairVelocity = Mathf.Sin(Time.time);

        //transform.localEulerAngles = InitialHandleEuler + Vector3.left * 60 * HairVelocity;

        if (m_grabbedBy) {
            HairVelocity = m_grabbedBy.transform.position.y - initialGrabberPosition.y;
            transform.localEulerAngles = 75 * (m_grabbedBy.transform.position.y - initialGrabberPosition.y) * new Vector3(1, 0, 0);
        }
        else {
            HairVelocity = 0;
            transform.localEulerAngles = Vector3.zero;
            //HairVelocity = 1 / 90.0f * ((transform.localEulerAngles.x - InitialHandleEuler.x) % 90);
        }

        if (GameLoop.Instance.GameStart && !firstPull)
        {
            arrow.gameObject.SetActive(true);
        }

        if (HairVelocity != 0)
        {
            isIdle = false;
            if (!slurpStart.isPlaying)
            {
                slurpStart.Play();
            }

            if(!firstPull && GameLoop.Instance.gameObject)
            {
                arrow.gameObject.SetActive(false);
                firstPull = true;
            }
        }
        else
        {
            if (isIdle) { return; }
            slurpEnd.Play();
            isIdle = true;
        }
        //Debug.Log("HairVelocity " + HairVelocity);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        initialGrabberPosition = hand.transform.position;
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        transform.localEulerAngles = InitialHandleEuler;
    }
}

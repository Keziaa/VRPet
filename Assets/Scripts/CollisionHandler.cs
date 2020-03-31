using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Obi;

[RequireComponent(typeof(ObiSolver))]
public class CollisionHandler : MonoBehaviour
{
    private const float COOLDOWN_TIME = 0.2f;

    ObiSolver solver;
    Obi.ObiSolver.ObiCollisionEventArgs collisionEvent;
    List<Component> colliders;

    private bool justCuthair = false;
    private bool justChangedColor = false;
    private bool justChangedTexture = false;

    void Awake()
    {
        solver = GetComponent<Obi.ObiSolver>();
    }

    void OnEnable()
    {
        solver.OnCollision += Solver_OnCollision;
    }

    void OnDisable()
    {
        solver.OnCollision -= Solver_OnCollision;
    }

    void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        colliders = new List<Component>();
        foreach (Oni.Contact contact in e.contacts)
        {
            // this one is an actual collision:
            if (contact.distance < 0.0001)
            {
                Component collider;
                if (ObiCollider.idToCollider.TryGetValue(contact.other, out collider))
                {
                    ObiSolver.ParticleInActor pa = solver.particleToActor[contact.particle];

                    ObiRope rope;
                    if (pa.actor.GetComponent<ObiRope>())
                    {
                        colliders.Add(collider);
                        rope = pa.actor.GetComponent<ObiRope>();
                        if (collider.GetComponent<Scissors>())
                        {
                            if (!justCuthair)
                            {
                                Debug.Log("Cutting hair at index: " + pa.indexInActor);
                                collider.GetComponent<Scissors>().CutHair(rope, pa.indexInActor);
                                justCuthair = true;
                                StartCoroutine(scissors_cooldown_cr());
                            }
                        }
                        else if (collider.GetComponent<Razor>())
                        {
                            collider.GetComponent<Razor>().CutHair(rope);
                        }
                        else if(collider.GetComponent<SprayCanAir>())
                        {
                            if (!justChangedColor)
                            {
                                collider.GetComponent<SprayCanAir>().SprayHair(rope);
                                justChangedColor = true;
                                StartCoroutine(color_cooldown_cr());
                            }
                        }
                        else if(collider.GetComponent<Beater>())
                        {
                            collider.GetComponent<Beater>().TwistHair(rope);
                        }
                        else if(collider.GetComponent<BlowDryerAir>())
                        {
                            if (!justChangedTexture)
                            {
                                collider.GetComponent<BlowDryerAir>().BlowHair(rope);
                                justChangedTexture = true;
                                StartCoroutine(texture_cooldown_cr());
                            }
                        }
                    }
                }
            }
        }

    }

    #region Cooldown coroutines
    private IEnumerator scissors_cooldown_cr()
    {
        while (justCuthair)
        {
            bool dontReady = false;
            foreach (Component collider in colliders)
            {
                if (collider.GetComponent<Scissors>())
                {
                    dontReady = true;
                    break;
                }
            }

            if (!dontReady)
            {
                yield return new WaitForSeconds(1.0f);
                justCuthair = false;
                break;
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator color_cooldown_cr()
    {
        while (justChangedColor)
        {
            bool dontReady = false;
            foreach (Component collider in colliders)
            {
                if (collider.GetComponent<SprayCan>())
                {
                    dontReady = true;
                    break;
                }
            }

            if (!dontReady)
            {
                yield return new WaitForSeconds(COOLDOWN_TIME);
                justChangedColor = false;
                break;
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator texture_cooldown_cr()
    {
        while (justChangedTexture)
        {
            bool dontReady = false;
            foreach (Component collider in colliders)
            {
                if (collider.GetComponent<SprayCan>())
                {
                    dontReady = true;
                    break;
                }
            }

            if (!dontReady)
            {
                yield return new WaitForSeconds(COOLDOWN_TIME);
                justChangedTexture = false;
                break;
            }
            yield return null;
        }
        yield return null;
    }
    #endregion
}

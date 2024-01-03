using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Helpers
{
    public static class MoverHelper
    {
        public static IEnumerator MoveOverSeconds(Transform transform, Vector3 end, float seconds,
            [CanBeNull] Action onEnd)
        {
            float elapsedTime = 0;
            Vector3 startingPos = transform.position;
            while (elapsedTime < seconds)
            {
                transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            transform.position = end;
            if (onEnd != null)
            {
                onEnd();
            }
        }

        public static IEnumerator RotateOverSeconds(Transform transform, Vector3 end, float seconds,
            [CanBeNull] Action onEnd)
        {
            float elapsedTime = 0;
            Vector3 starting = transform.eulerAngles;
            
            Debug.Log(starting +" "+ end);

            if (starting != end)
            {
                while (elapsedTime < seconds)
                {
                    transform.eulerAngles = Vector3.Lerp(starting, end, (elapsedTime / seconds));
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                transform.eulerAngles = end;
            }
            else
            {
                Debug.Log("===");
            }

            if (onEnd != null)
            {
                onEnd();
            }
        }
    }
}
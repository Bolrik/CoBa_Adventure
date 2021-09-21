using PlayerInteraction.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helper
{
    public static class VisualEditorAssistant
    {
        public static void Visualize(this InteractiveObject interactiveObject)
        {
            if (interactiveObject is ColorBox colorBox)
            {
                Gizmos.color = colorBox.ColorCode.GetColor();
                Gizmos.DrawSphere(colorBox.transform.position + (Vector3.left + Vector3.up) * .25f, .2f);
            }

            if (interactiveObject is ColorButton colorDetector)
            {
                Gizmos.color = colorDetector.ColorCode.GetColor();
                Gizmos.DrawSphere(colorDetector.transform.position + (Vector3.left + Vector3.up) * .25f, .2f);
            }

            if (interactiveObject is ColorDoor colorDoor)
            {
                Gizmos.color = colorDoor.ColorCode.GetColor();
                Gizmos.DrawSphere(colorDoor.transform.position + (Vector3.left + Vector3.up) * .25f, .2f);

                if (colorDoor.IsClosed)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawSphere(colorDoor.transform.position + (Vector3.right + Vector3.up) * .25f, .2f);
                }
            }

            if (interactiveObject is DetectorDoor detectorDoor)
            {
                if (detectorDoor.IsClosed)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawSphere(detectorDoor.transform.position + (Vector3.right + Vector3.up) * .25f, .2f);
                }
            }
        }
    }
}

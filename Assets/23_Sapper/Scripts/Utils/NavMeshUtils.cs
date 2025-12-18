using UnityEngine;
using UnityEngine.AI;

namespace Modul_23.Utils
{
    public class NavMeshUtils : MonoBehaviour
    {
        public static float GetPathLength(NavMeshPath path)
        {
            float length = 0;

            if (path.corners.Length > 1)
                for (int i = 1; i < path.corners.Length; i++)
                    length += Vector3.Distance(path.corners[i - 1], path.corners[i]);

            return length;
        }

        public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
        {
            if (NavMesh.CalculatePath(sourcePosition, targetPosition, queryFilter, pathToTarget) 
                && pathToTarget.status != NavMeshPathStatus.PathInvalid)
                return true;

            return false;
        }
    }
}
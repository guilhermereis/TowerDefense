using UnityEngine;
using System.Collections.Generic;
public class PropertyScript  {
    [System.Serializable]
    public struct Property
    {
        public string type;
        public UnitBlueprint unit;
        public GameObject builtGameObject;

        public Property(string _type)
        {
            type = _type;
            unit = null;
            builtGameObject = null;
        }

        public Property(UnitBlueprint _unit, ref List<GameObject> ListOfGameObjects, int indexOfBuiltGameObject,string _type = "Normal")
        {
            type = _type;
            unit = _unit;
            builtGameObject = ListOfGameObjects[indexOfBuiltGameObject];
        }
    }

    [System.Serializable]
    public struct StructureState
    {
        public string structureName;
        public Transform transform;
        public Vector3 position;
        public float currentHealth;
        public StructureState(string _structureName, Transform _transform, float _currentHealth)
        {
            structureName = _structureName;
            transform = _transform;
            currentHealth = _currentHealth;
            position = _transform.position;
        }
    }


    }

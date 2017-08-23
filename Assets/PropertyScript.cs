using UnityEngine;
public class PropertyScript  {
    [System.Serializable]
    public struct Property
    {
        public string type;
        public UnitBlueprint unit;
        public GameObject builtGameObject;

        public Property(UnitBlueprint _unit, ref GameObject _builtGameObject, string _type = "Normal")
        {
            type = _type;
            unit = _unit;
            builtGameObject = _builtGameObject;
        }
    }
}

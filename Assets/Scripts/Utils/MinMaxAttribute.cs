using UnityEngine;

public class MinMaxAttribute: PropertyAttribute{
    public readonly float minLimit;
    public readonly float maxLimit;
    public MinMaxAttribute(float minLimit, float maxLimit)
    {
        this.minLimit = minLimit;
        this.maxLimit = maxLimit;
    }
}

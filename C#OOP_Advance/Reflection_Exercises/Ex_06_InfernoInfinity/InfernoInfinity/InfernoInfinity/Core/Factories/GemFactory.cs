using System;
public class GemFactory : IGemFactory
{
    public IGem CreateGem(string clarity, string gemType)
    {
        GemClarity gemClarity =
            (GemClarity)Enum.Parse(typeof(GemClarity), clarity);

        var type = Type.GetType(gemType);

        IGem instance = (IGem)Activator.CreateInstance(type, new object[] { gemClarity });

        return instance;
    }
}
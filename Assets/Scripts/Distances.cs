/// <summary>
/// Classe para representar as distancias da parede para o player
/// </summary>
public class Distances
{
    public float horizontalDistance;
    public float upperWallDistance;
    public float lowerWallDistance;

    public float Sum()
    {
        return horizontalDistance + upperWallDistance + lowerWallDistance;
    }
}
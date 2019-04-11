namespace Travel.Entities.Airplanes
{
	public class MediumAirplane : Airplane
	{
	    private const int seats = 10;
	    private const int numberOfBags = 14;

		public MediumAirplane()
			: base(seats,numberOfBags)
		{
		}
	}
}
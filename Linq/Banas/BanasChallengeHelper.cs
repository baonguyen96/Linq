namespace Linq.Banas
{
	partial class BanasChallenge
	{
		private class Owner
		{
			public string Name { get; set; }
			public int OwnerId { get; set; }
		}

		private class Animal
		{
			public string Name { get; set; }
			public double Weight { get; set; }
			public double Height { get; set; }
			public int AnimalId { get; set; }

			public Animal(string name = "No Name",
				double weight = 0,
				double height = 0)
			{
				Name = name;
				Weight = weight;
				Height = height;
			}

			public override string ToString()
			{
				return $"{Name} weighs {Weight}lbs and is {Height} inches tall";
			}
		}
	}
}
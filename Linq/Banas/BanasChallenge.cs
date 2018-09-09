using System;
using System.Linq;

namespace Linq.Banas
{
	partial class BanasChallenge
	{
		private static readonly Animal[] Animals =
		{
			new Animal
			{
				Name = "German Shepherd",
				Height = 25,
				Weight = 77,
				AnimalId = 1
			},
			new Animal
			{
				Name = "Chihuahua",
				Height = 7,
				Weight = 4.4,
				AnimalId = 2
			},
			new Animal
			{
				Name = "Saint Bernard",
				Height = 30,
				Weight = 200,
				AnimalId = 3
			},
			new Animal
			{
				Name = "Pug",
				Height = 12,
				Weight = 16,
				AnimalId = 1
			},
			new Animal
			{
				Name = "Beagle",
				Height = 15,
				Weight = 23,
				AnimalId = 2
			}
		};

		private static readonly Owner[] Owners =
		{
			new Owner
			{
				Name = "Doug Parks",
				OwnerId = 1
			},
			new Owner
			{
				Name = "Sally Smith",
				OwnerId = 2
			},
			new Owner
			{
				Name = "Paul Brooks",
				OwnerId = 3
			}
		};


		public static void Problem1()
		{
			var animalNameHeight = Animals.Select(a => new {a.Name, a.Height});

			foreach (var a in animalNameHeight)
			{
				Console.WriteLine(a.ToString());
			}

			Console.WriteLine();
		}

		public static void Problem2()
		{
			var ownerAnimal = from animal in Animals
			                  join owner in Owners on animal.AnimalId equals owner.OwnerId
			                  orderby owner.Name
			                  select new
			                  {
				                  owner,
				                  animal
			                  };

			foreach (var o in ownerAnimal)
			{
				Console.WriteLine($"{o.owner.Name} owns {o.animal.Name}");
			}

			Console.WriteLine();
		}

		public static void Problem3()
		{
			var ownerAnimalGroup = from owner in Owners
			                       orderby owner.Name
			                       select new
			                       {
				                       owner = owner,
				                       animals = from animal in Animals
				                                 where animal.AnimalId == owner.OwnerId
				                                 select animal
			                       };

			foreach (var o in ownerAnimalGroup)
			{
				Console.WriteLine($"{o.owner.Name} owns:");
				foreach (var a in o.animals)
				{
					Console.WriteLine($"   {a.Name}");
				}
			}

			Console.WriteLine();
		}
	}
}
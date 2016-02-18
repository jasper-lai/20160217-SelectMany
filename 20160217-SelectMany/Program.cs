using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20160217_SelectMany
{
    class Program
    {
        static void Main(string[] args)
        {
            // CASE 1
            Console.WriteLine("====== CASE 1 ======");
            SelectManyEx1();
            // CASE 2
            Console.WriteLine("====== CASE 2 ======");
            SelectManyEx2();
            // CASE 3
            Console.WriteLine("====== CASE 3 ======");
            SelectManyEx3();

            Console.ReadLine();
        }


        public static void SelectManyEx1()
        {
            PetOwner[] petOwners =
					{ new PetOwner { Name="Higa, Sidney",
						  Pets = new List<string>{ "Scruffy", "Sam" } },
					  new PetOwner { Name="Ashkenazi, Ronen",
						  Pets = new List<string>{ "Walker", "Sugar" } },
					  new PetOwner { Name="Price, Vernette",
						  Pets = new List<string>{ "Scratches", "Diesel" } } };

            // Query using SelectMany().
            IEnumerable<string> query1 = petOwners.MySelectMany(petOwner => petOwner.Pets);     // 呼叫自行由 .NET Framework 複製過來的擴充方法
            //IEnumerable<string> query1 = petOwners.SelectMany(petOwner => petOwner.Pets);		// 呼叫 .NET Framework 的擴充方法

            Console.WriteLine("Using SelectMany():");

            // Only one foreach loop is required to iterate 
            // through the results since it is a
            // one-dimensional collection.
            // 只需要 1 個 foreach 迴圈, 就可以選取第2層 Pets 的元素資料
            foreach (string pet in query1)
            {
                Console.WriteLine(pet);
            }

            // This code shows how to use Select() 
            // instead of SelectMany().
            IEnumerable<List<String>> query2 =
                petOwners.Select(petOwner => petOwner.Pets);

            Console.WriteLine("\nUsing Select():");

            // Notice that two foreach loops are required to 
            // iterate through the results
            // because the query returns a collection of arrays.
            // 需要 2 個 foreach 迴圈 
            foreach (List<String> petList in query2)
            {
                foreach (string pet in petList)
                {
                    Console.WriteLine(pet);
                }
                Console.WriteLine();
            }

            //輸出:
            //Using SelectMany():
            //Scruffy
            //Sam
            //Walker
            //Sugar
            //Scratches
            //Diesel
            //
            //Using Select():
            //Scruffy
            //Sam
            //
            //Walker
            //Sugar
            //
            //Scratches
            //Diesel	
        }

        public static void SelectManyEx2()
        {
            PetOwner[] petOwners =
					{ new PetOwner { Name="Higa, Sidney",
						  Pets = new List<string>{ "Scruffy", "Sam" } },
					  new PetOwner { Name="Ashkenazi, Ronen",
						  Pets = new List<string>{ "Walker", "Sugar" } },
					  new PetOwner { Name="Price, Vernette",
						  Pets = new List<string>{ "Scratches", "Diesel" } },
					  new PetOwner { Name="Hines, Patrick",
						  Pets = new List<string>{ "Dusty" } } };

            // Project the items in the array by appending the index 
            // of each PetOwner to each pet's name in that petOwner's 
            // array of pets.
            //
            // IEnumerable<string> query =
            // 	petOwners.SelectMany((petOwner, index) =>
            // 							 petOwner.Pets.Select(pet => index + pet));
            //
            IEnumerable<string> query =
                petOwners.MySelectMany((petOwner, index) =>
                                         petOwner.Pets.Select(pet => index + pet));

            foreach (string pet in query)
            {
                Console.WriteLine(pet);
            }
            Console.WriteLine();

            //輸出:
            //
            // 0Scruffy
            // 0Sam
            // 1Walker
            // 1Sugar
            // 2Scratches
            // 2Diesel
            // 3Dusty 
        }

        public static void SelectManyEx3()
        {
            PetOwner[] petOwners =
					{ new PetOwner { Name="Higa",
						  Pets = new List<string>{ "Scruffy", "Sam" } },
					  new PetOwner { Name="Ashkenazi",
						  Pets = new List<string>{ "Walker", "Sugar" } },
					  new PetOwner { Name="Price",
						  Pets = new List<string>{ "Scratches", "Diesel" } },
					  new PetOwner { Name="Hines",
						  Pets = new List<string>{ "Dusty" } } };

            // Project the pet owner's name and the pet's name.
            //
            //var query =
            //	petOwners
            //	.SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName })
            //	.Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"))
            //	.Select(ownerAndPet =>
            //			new
            //			{
            //				Owner = ownerAndPet.petOwner.Name,
            //				Pet = ownerAndPet.petName
            //			}
            //	);
            //
            var query =
                petOwners
                .MySelectMany(petOwner => petOwner.Pets,
                                (petOwner, petName) => new { petOwner, petName }
                            )
                .Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.petOwner.Name,
                            Pet = ownerAndPet.petName
                        }
                );
            //
            //var query =
            //	petOwners
            //	.MySelectMany	(	petOwner => petOwner.Pets, 
            //					(petOwner, petName) => new { petOwner, petName }
            //				) ;
            //					

            // Print the results.
            foreach (var obj in query)
            {
                Console.WriteLine(obj);
            }

            //本例輸出: (without Where() condition)
            // {Owner=Higa, Pet=Scruffy}
            // {Owner=Higa, Pet=Sam}
            // {Owner=Ashkenazi, Pet=Walker}
            // {Owner=Ashkenazi, Pet=Sugar}
            // {Owner=Price, Pet=Scratches}
            // {Owner=Price, Pet=Diesel}
            // {Owner=Hines, Pet=Dusty}

            //MSDN範例輸出: (with Where() condition)
            // {Owner=Higa, Pet=Scruffy}
            // {Owner=Higa, Pet=Sam}
            // {Owner=Ashkenazi, Pet=Sugar}
            // {Owner=Price, Pet=Scratches}
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Basics
{
    class Program
    {
        static void Main( string[] args )
        {
            QueryStringArray();
            QueryIntArray();
            QueryArrayList();
            QueryCollection();
            QueryAnimalData();
            System.Console.ReadLine();
        }
        static void QueryStringArray()
        {
            string[] dogs = {"K 9","Brian Griffin","Scooby Doo", "Old Yeller", "Rin Tin Tin"
            ,"Benji","Charlie B. Barkin", "Lassie","Snoopy"};

            var dogspaces = from dog in dogs
                            where dog.Contains( " " )
                            orderby dog descending
                            select dog;

            foreach ( var i in dogspaces )
            {
                System.Console.WriteLine( i );
            }
            System.Console.WriteLine();

        }

        static int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };

            var gt20 = from num in nums
                       where num > 20
                       orderby num
                       select num;

            foreach ( var item in gt20 )
            {
                System.Console.WriteLine( item );
            }
            System.Console.WriteLine();

            System.Console.WriteLine( $"Get Type : {gt20.GetType()}" );
            var listGT20 = gt20.ToList<int>();
            var arrayGT20 = gt20.ToArray();

            nums[0] = 40;

            foreach ( var item in gt20 )
            {
                System.Console.WriteLine( item );
            }
            System.Console.WriteLine();
            return arrayGT20;
        }

        static void QueryArrayList()
        {
            ArrayList famAnimals = new ArrayList()
            {
                new Animal { Name= "Heidi",Height=.8,Weight =18},
                new Animal { Name= "Shrek",Height=4,Weight =130},
                new Animal { Name= "Napolean",Height=1.5,Weight =180},
                new Animal { Name= "Pika Chuu",Height=.5,Weight =10},
                new Animal { Name = "Ratatolie", Height = .1, Weight = 1.8 }

            };
            var famAnimalEnum = famAnimals.OfType<Animal>();

            var smAnimals = from animal in famAnimalEnum
                            where animal.Weight <= 90
                            orderby animal.Name
                            select animal;
            foreach ( var item in smAnimals )
            {
                System.Console.WriteLine( "{0} weighs {1} lbs", item.Name, item.Weight );

            }
        }

        static void QueryCollection()
        {
            var animalList = new List<Animal>() {
            new Animal { Name = "German Shepherd",Height=25,Weight = 77},
            new Animal { Name = "Chihuahua",Height=7,Weight = 4.4},
            new Animal { Name = "Pitbull",Height=30,Weight = 200}
            };

            var bigDogs = from dog in animalList
                          where ( dog.Weight > 70 ) && ( dog.Height > 25 )
                          orderby dog.Name
                          select dog;

            foreach ( var item in bigDogs )
            {
                System.Console.WriteLine( "A {0} weighs {1} lbs", item.Name, item.Weight );
            }
            System.Console.WriteLine();
        }

        static void QueryAnimalData()
        {
            Animal[] animals = new[] {
            new Animal { Name = "German Shepherd",Height=25,Weight = 77, AnimalID = 1},
            new Animal { Name = "Chihuahua",Height=7,Weight = 4.4 , AnimalID = 2},
            new Animal { Name = "Beagle",Height=73,Weight = 59 , AnimalID = 2},
            new Animal { Name = "Pug",Height=17,Weight = 9.4 , AnimalID = 1},
            new Animal { Name = "Pitbull",Height=30,Weight = 200 , AnimalID = 3}
            };

            Owner[] owners = new[]
            {
                new Owner{ Name = "Doug Parks", OwnerID= 1},
                new Owner{ Name = "Sally Smith", OwnerID= 2},
                new Owner{ Name = "Paul Brooks", OwnerID= 3}
            };

            var nameHeight = from a in animals
                             select new {
                                 a.Name, a.Height
                             };

            Array arrNameHeight = nameHeight.ToArray();

            foreach ( var i in arrNameHeight )
            {
                Console.WriteLine( i.ToString() );
            }
            Console.WriteLine();

            var innerJoin = from animal in animals
                            join owner in owners on animal.AnimalID
                            equals owner.OwnerID
                            select new { OwnerName = owner.Name, AnimalName = animal.Name };


            foreach ( var item in innerJoin )
            {
                Console.WriteLine( "{0} owns {1}", item.OwnerName, item.AnimalName );
            }
            Console.WriteLine();

            var groupJoin = from owner in owners
                            orderby owner.OwnerID
                            join animal in animals
                            on owner.OwnerID
                            equals animal.AnimalID
                            into ownerGroup
                            select new {
                                Owner = owner.Name,
                                Animals = from owner2 in ownerGroup
                                          orderby owner2.Name

                                          select owner2
                            };

            foreach ( var item in groupJoin )
            {
                Console.WriteLine( item.Owner );
                foreach ( var x in item.Animals )
                {
                    Console.WriteLine( "* {0}", x.Name );
                }
            }
        }



    }
}

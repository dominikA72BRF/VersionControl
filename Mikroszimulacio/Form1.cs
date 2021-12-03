using Mikroszimulacio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroszimulacio
{
    public partial class Form1 : Form
    {

        Random rng = new Random(1234);

        List<Person> Population = null;                     //new List<Person>();
        List<BirthProbability> BirthProbabilities = null;    //new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = null;    //new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            // Végigmegyünk a vizsgált éveken
            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                    SimStep(year, Population[1]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }

        }

        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }


        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    var p = new Person();
                    p.BirthYear = int.Parse(line[0]);
                    p.Gender = (Gender)Enum.Parse(typeof(Gender), line[1]);
                    p.NbrOfChildren = int.Parse(line[2]);
                    population.Add(p);

                    //VAGY
                    //population.Add(new Person()
                    //{
                    //    BirthYear = int.Parse(line[0]),
                    //    Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                    //    NbrOfChildren = int.Parse(line[2])
                    //});

                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    birthProbabilities.Add(new BirthProbability()
                    {
                        Age = byte.Parse(line[0]),
                        P = double.Parse(line[2]),
                        NbrOfChildren = int.Parse(line[1])
                    });

                }
            }

            return birthProbabilities;
        }



        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    deathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        Age = byte.Parse(line[0]),
                        P = double.Parse(line[2])

                    });

                }
            }

            return deathProbabilities;
        }

    }
}

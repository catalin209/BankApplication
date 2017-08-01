using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Http;

namespace BApp
{
    public class Client : InterfaceB
    {
        private List<ClientBase> clientList  = new List<ClientBase>();
        private List<ClientBase> rClientList = new List<ClientBase>();

        /// <summary>
        /// Writes the clients from the specified client list.
        /// </summary>
        /// <param name="clientL">The client list.</param>
        public void Write(List<ClientBase> clientList)
        {
            foreach (ClientBase cl in clientList)
            {
                    Console.WriteLine(cl.FirstName + " " + cl.Gender + " " + cl.Age);
            }
        }

        /// <summary>
        /// Gets or sets the client list.
        /// </summary>
        /// <value>
        /// The client list.
        /// </value>
        public List<ClientBase> ClientList
        {
            get { return clientList; }
            set { clientList = value; }
        }

        /// <summary>
        /// Gets or sets the r client list.
        /// </summary>
        /// <value>
        /// The r client list.
        /// </value>
        public List<ClientBase> RClientList
        {
            get { return rClientList; }
            set { rClientList = value; }
        }

        /// <summary>
        /// Generates the random clients.
        /// </summary>
        public void GenerateRandomClients()
        {
            string s = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string file = s + "\\name_list.txt";
            string[] lines = File.ReadAllLines(file);

            Random r = new Random(Guid.NewGuid().GetHashCode());

            int numberOfElement = r.Next(0, 105);

            if (lines.Length < numberOfElement)
            {
                Console.WriteLine("The list doesn't contain enough elements.");
                return;
            }
            else
            {
                List<int> a = new List<int>();
                int ok = 0, position = 0, i;

                for (i = 0; i < lines.Length; i++)
                {
                    if (IsAllLetters(lines[i].Trim()))
                    {
                        a.Add(i);
                    }
                    else
                    {
                        ok = 1;
                        position = i;
                        break;
                    }
                }

                if(ok == 1)
                {
                    Console.WriteLine("The file contains characters other than letters at line: "+ position);
                    return;
                }

                for (i = 0; i < numberOfElement; i++)
                {
                    int index = r.Next(0, a.Count);

                    Console.WriteLine(clientList[index].FirstName + " "+clientList[index].Gender);
                    ClientBase CL = new ClientBase(clientList[index].Age, clientList[index].FirstName, clientList[index].Gender);

                    rClientList.Add(CL);
                    a.RemoveAt(index);
                }
            }
            Console.WriteLine("b");
        }

        /// <summary>
        /// Gets the gender of the clients from the list.
        /// </summary>
        public void GetGender()
        {
            string s = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string file = s + "\\name_list.txt";
            string[] lines = File.ReadAllLines(file);

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://api.genderize.io/");

                foreach (string line in lines)
                {
                    int i = 21 + line.Length;

                    HttpResponseMessage response = client.GetAsync($"?name={line}").Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;

                    ClientBase CL = new ClientBase(0, line, result[i]);
                    clientList.Add(CL);
                }
            }
        }

        /// <summary>
        /// Gets the gender of the clients using a local list of names.
        /// </summary>
        /// <exception cref="ExceptionClass"></exception>
        public void GetGenderStatic()
        {
            string s = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string file = s + "\\name_list.txt";
            string[] lines = File.ReadAllLines(file);

            {
                try
                {
                    foreach (string line in lines)
                    {
                        if (IsAllLetters(line.Trim()) == false)
                        {
                            clientList.Clear();
                            throw new ExceptionClass(line);
                        }
                        else
                        {
                            int ok = 0, okp = 0 ;

                            string s1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                            string fileException = s1 + "\\NameList\\unisex_name_list.txt";
                            string[] namesToCompareException = File.ReadAllLines(fileException);

                            foreach (string nameToCompareException in namesToCompareException)
                            {
                                if (nameToCompareException.TrimEnd().ToUpper().Equals(line.TrimEnd().ToUpper()))
                                {
                                    ClientBase CL = new ClientBase(line, 'x');
                                    clientList.Add(CL);
                                    ok = 1;
                                    okp = 1;
                                    break;
                                }
                            }

                            if (ok == 0)
                            {                             
                                string s2 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                                string file2 = s2 + "\\NameList\\name_list_" + line.ToCharArray()[0] + ".txt";
                                string[] namesToCompare = File.ReadAllLines(file2);

                                foreach (string nameToCompare in namesToCompare)
                                {
                                    if (nameToCompare.TrimEnd().ToUpper().Equals(line.TrimEnd().ToUpper()))
                                    {
                                        ClientBase CL = new ClientBase(line, 'm');
                                        clientList.Add(CL);
                                        ok  = 1;
                                        okp = 1;
                                        break;
                                    }
                                }
                            }
                            if (okp == 0)
                            {
                                ClientBase CL = new ClientBase(line, 'f');
                                clientList.Add(CL);
                            }
                        }
                    }
                }

                catch (ExceptionClass e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Fisier inexistent");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Index depasit");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {

        }

        /// <summary>
        /// Determines whether string has only letters.
        /// </summary>
        /// <param name="s">The string s.</param>
        /// <returns>
        ///   <c>true</c> if string s has only letters; otherwise, <c>false</c>.
        /// </returns>
        public  bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Generates the age randomly for the clients in the list.
        /// </summary>
        public void GenerateRandomAge()
        {
            
            Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random();
                   
            foreach (ClientBase cl in clientList)
            {

                int ageM = rnd1.Next(-7, 7);
                ageM = ageM + 45;

                int ageF = rnd1.Next(-5, 5);
                ageF = ageF + 37;

                if (cl.Gender.Equals('m'))
                {
                    cl.Age = ageM;
                }
                else if (cl.Gender.Equals('f'))
                {
                    cl.Age = ageF;
                }
                else
                {
                    cl.Age = 0;
                }
            }
        }

        /// <summary>
        /// Counts how many men are in the random client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int MaleCount()
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Gender == 'm')
                    nr++;
            }
            return nr;
        }
        /// <summary>
        /// Counts how many men are in specific client list.
        /// </summary>
        /// <param name="CL">The cl.</param>
        /// <returns></returns>
        public int MaleCount(List<ClientBase> CL)
        {
            int nr = 0;

            foreach (ClientBase cl in CL)
            {
                if (cl.Gender == 'm')
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many women are in the Random client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int FemaleCount()
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Gender == 'f')
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many women are in the client list.
        /// </summary>
        /// <param name="CL">List of clients.</param>
        /// <returns></returns>
        public int FemaleCount(List<ClientBase> CL)
        {
            int nr = 0;

            foreach (ClientBase cl in CL)
            {
                if (cl.Gender == 'f')
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many uncatalogued clients are in the list.
        /// </summary>
        /// <returns></returns>
        public int UnisexCount()
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Gender == 'x')
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many uncatalogued clients are in the list.
        /// </summary>
        /// <param name="CL">The cl.</param>
        /// <returns></returns>
        public int UnisexCount(List<ClientBase> CL)
        {
            int nr = 0;

            foreach (ClientBase cl in CL)
            {
                if (cl.Gender == 'x')
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many men are in the client list with a specified age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns>The computed average.</returns>
        public int MaleCount(int age, List<ClientBase> CL)
        {
            int nr = 0;

            foreach (ClientBase cl in CL)
            {
                if (cl.Age.Equals(age))
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many men are in the client list with a specified age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns></returns>
        public int MaleCount(int age)
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Age.Equals(age))
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many women are in the client list with a specified age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns>The computed average.</returns>
        public int FemaleCount(int age)
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Age.Equals(age))
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Counts how many women are in the client list with a specified age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns></returns>
        public int FemaleCount(int age, List<ClientBase> CL)
        {
            int nr = 0;

            foreach (ClientBase cl in rClientList)
            {
                if (cl.Age.Equals(age))
                    nr++;
            }
            return nr;
        }

        /// <summary>
        /// Computes the average age of the men in the client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int MaleAverage()
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.Gender.Equals('m'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the men in the client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int MaleAverage(List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.Gender.Equals('m'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }
        
        /// <summary>
        /// Computes the average age of the women in the client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int FemaleAverage()
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the women in the client list.
        /// </summary>
        /// <returns>The computed average.</returns>
        public int FemaleAverage(List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the men in the client list whose names contain a specified string.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The computed average.</returns>
        public int MaleAverage(string pattern)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.FirstName.Contains(pattern) && cl.Gender.Equals('m'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the men in the client list whose names contain a specified string.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The computed average.</returns>
        public int MaleAverage(string pattern, List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.FirstName.Contains(pattern) && cl.Gender.Equals('m'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }
        /// <summary>
        /// Computes the average age of the women in the client list whose names contain a specified string.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The computed average.</returns>
        public int FemaleAverage(string pattern)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.FirstName.Contains(pattern) && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                if (nr == 0) throw new DivideByZeroException();
                return sum / nr;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the women in the client list whose names contain a specified string.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="CL">The cl.</param>
        /// <returns></returns>
        /// <exception cref="System.DivideByZeroException"></exception>
        public int FemaleAverage(string pattern, List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.FirstName.Contains(pattern) && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                if (nr == 0) throw new DivideByZeroException();
                return sum / nr;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the women in the client list who are younger than a given age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns>The computed average.</returns>
        public int FemaleAverageYoungerThan(int age)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.Age < age && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the women in the client list who are younger than a given age.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <returns></returns>
        public int FemaleAverageYoungerThan(int age, List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.Age < age && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                return sum / nr;
            }
            catch (Exception)
            {
                Console.WriteLine("Division by zero.");
            }

            return 0;
        }

        /// <summary>
        /// Computes the average age of the women in the client list who have the age between two specified parameters.
        /// </summary>
        /// <param name="ageMin">The minimum age.</param>
        /// <param name="ageMax">The maximum age.</param>
        /// <returns>The computed average.</returns>
        public int FemaleAverageRange(int ageMin, int ageMax)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in rClientList)
                {
                    if (cl.Age > ageMin && cl.Age < ageMax && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                if (nr == 0)
                {
                    throw new DivideByZeroException();
                }
                return sum / nr;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        /// <summary>
        /// Females the average range.
        /// </summary>
        /// <param name="ageMin">The age minimum.</param>
        /// <param name="ageMax">The age maximum.</param>
        /// <returns></returns>
        /// <exception cref="System.DivideByZeroException"></exception>
        public int FemaleAverageRange(int ageMin, int ageMax, List<ClientBase> CL)
        {
            try
            {
                int nr = 0;
                int sum = 0;

                foreach (ClientBase cl in CL)
                {
                    if (cl.Age > ageMin && cl.Age < ageMax && cl.Gender.Equals('f'))
                    {
                        nr++;
                        sum += cl.Age;
                    }
                }
                if (nr == 0)
                {
                    throw new DivideByZeroException();
                }
                return sum / nr;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
    }
}

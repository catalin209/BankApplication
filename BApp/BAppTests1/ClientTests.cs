using Microsoft.VisualStudio.TestTools.UnitTesting;
using BApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace BApp.Tests
{
    [TestClass()]
    public class ClientTests
    {
        
        [TestMethod()]
        public void ObjectEqualityTest()
        {
            Client c1 = new Client();
            Client c2 = new Client();
            c1.GetGenderStatic();
            c2.GetGenderStatic();

            for (int i = 0; i < c1.ClientList.Count; i++)
            {
                Assert.IsTrue(c1.ClientList[i].FirstName == c2.ClientList[i].FirstName && c1.ClientList[i].Gender.Equals(c2.ClientList[i].Gender));
            }
        }

        [TestMethod()]
        public void AgeDiferencesBetweenTwoObject()
        {
            Client l = new Client();
            Client c = new Client();

            l.GetGenderStatic();
            c.GetGenderStatic();

            l.GenerateRandomAge();
            c.GenerateRandomAge();

            int ok = 0;       

            for (int i = 0; i < c.ClientList.Count; i++)
            {

                if (!(c.ClientList[i].Age.Equals(l.ClientList[i].Age))) ok = 1;
            }

            Assert.IsTrue(ok == 1);
        }

        [TestMethod()]
        public void MaleCountTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(15, "Adam", 'm'));
            c.RClientList.Add(new ClientBase(15, "Adon", 'm'));

            Assert.IsTrue(2 == c.MaleCount());
        }

        [TestMethod()]
        public void ValidFemeleCountTest()
        {
            Client c = new Client(); 
              
            c.RClientList.Add(new ClientBase(15, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(15, "Alex", 'm'));

            Assert.IsTrue(1 == c.FemaleCount());
        }

        [TestMethod()]
        public void InvalidFemeleCountTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(15, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(15, "Alex", 'm'));

            Assert.IsFalse(5 == c.FemaleCount());
        }

        [TestMethod()]
        public void FemeleAvarageTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(15, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Alex", 'm'));

            Assert.IsTrue(c.FemaleAverage() == 15);
        }

        [TestMethod()]
        public void MaleAvarageTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(15, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "John", 'm'));
            c.RClientList.Add(new ClientBase(15, "Alex", 'm'));

            Assert.IsTrue(c.MaleAverage() == 14);
        }

        [TestMethod()]
        public void FemaleAverageYoungerThanTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "John", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(36, "Samanta", 'f'));

            Assert.IsTrue(c.FemaleAverageYoungerThan(35) == 32);
        }

        [TestMethod()]
        public void FemaleAverageRangeTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "John", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(36, "Samanta", 'f'));

            Assert.IsTrue(c.FemaleAverageRange(28,35) == 32);
        }

        [TestMethod()]
        public void ValidFemaleAveragePatternTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(61, "Antonia", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Andrey", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(36, "Samanta", 'f'));

            Assert.IsTrue(c.FemaleAverage("An") == 44);
        }

        [TestMethod()]
        public void InvalidFemaleAveragePatternTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(61, "Antonia", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Andrey", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(36, "Samanta", 'f'));

            Assert.IsFalse(c.FemaleAverage("ZX") == 44);
        }

        [TestMethod()]
        public void MaleAveragePatternTest()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(61, "Antonia", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Andrey", 'm'));
            c.RClientList.Add(new ClientBase(15, "Anenadile", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(36, "Samanta", 'f'));

            Assert.IsTrue(c.MaleAverage("An").Equals(14));
        }

        [TestMethod()]
        public void FemaleCountByAge()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(61, "Antonia", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Andrey", 'm'));
            c.RClientList.Add(new ClientBase(15, "Anenadile", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(30, "Samanta", 'f'));

            Assert.IsTrue(c.FemaleCount(30).Equals(2));
        }

        [TestMethod()]
        public void MaleCountByAge()
        {
            Client c = new Client();

            c.RClientList.Add(new ClientBase(27, "Anna", 'f'));
            c.RClientList.Add(new ClientBase(61, "Antonia", 'f'));
            c.RClientList.Add(new ClientBase(30, "Marry", 'f'));
            c.RClientList.Add(new ClientBase(13, "Andrey", 'm'));
            c.RClientList.Add(new ClientBase(13, "Anenadile", 'm'));
            c.RClientList.Add(new ClientBase(34, "Marie", 'f'));
            c.RClientList.Add(new ClientBase(30, "Samanta", 'f'));

            Assert.IsTrue(c.MaleCount(13).Equals(2));
        }

        [TestMethod()]
        public void ValidIsLetterTest()
        {
            Client c = new Client();
            Assert.IsTrue(c.IsAllLetters("Andrey"));         
        }

        [TestMethod()]
        public void NotValidIsLetterTest()
        {
            Client c = new Client();
            Assert.IsFalse(c.IsAllLetters("Andrey;"));
        }

        [TestMethod()]
        public void GetGenderTest()
        {
            int m = 0;
            int f = 0;
            int u = 0;

            Client c = new Client();
            c.GetGender();
            c.GenerateRandomClients();

            foreach(var t in c.RClientList)
            {
                if (t.Gender.Equals('m')) m++;
                else if (t.Gender.Equals('f')) f++;
                else if (t.Gender.Equals('x')) u++;
            }

            Assert.IsTrue(c.MaleCount() == m) ;
            Assert.IsTrue(c.FemaleCount() == f);
            Assert.IsTrue(c.UnisexCount() == u);
        }

        [TestMethod()]
        public void GetGenderStativTest()
        {
            int i = 0;        
            int ok = 1;

            Client c = new Client();
            c.GetGenderStatic();

            string[] lines = File.ReadAllLines("C:\\Users\\catalin.zevedei\\Documents\\visual studio 2015\\Projects\\BApp\\BApp\\name_list.txt");

            foreach (var item in c.ClientList)
            {
                if (!(item.FirstName.Equals(lines[i]))) { ok = 0; break; }
                else  i++;
            }

            Assert.IsTrue(ok == 1);
        }

        [TestMethod()]
        public void GenerateRandomAgeTest()
        {
            int ok = 1;

            Client c = new Client();
            c.GetGenderStatic();
            c.GenerateRandomAge();
            c.GenerateRandomClients();

            foreach (var item in c.RClientList)
            {

                if (item.Gender.Equals('m') && (item.Age < 38) || item.Age > 52) ok = 0;
                if (item.Gender.Equals('f') && (item.Age >= 43 || item.Age <= 27)) ok = 0;
                else if (item.Gender.Equals('x') && !(item.Age.Equals(0))) ok = 0;
            }

            Assert.IsTrue(ok == 1);
          
        }

        [TestMethod()]
        public void GenerateRandomClients()
        {
            Client c = new Client();
            c.GetGenderStatic();
            c.GenerateRandomClients();

            Assert.IsFalse(c.RClientList.Count.Equals(0));
        }

        [TestMethod()]
        public void GenderLetterTest()
        {
            int ok = 1;

            Client c = new Client();
            c.GetGenderStatic();
            c.GenerateRandomClients();

            foreach (var item in c.RClientList)
            {
                ok = 0;
                if ((item.Gender.Equals('f'))) ok = 1;
                if ((item.Gender.Equals('m'))) ok = 1;
                if ((item.Gender.Equals('x'))) ok = 1;
                if (ok == 0) break;
            }

            Assert.IsTrue(ok == 1);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "InvalidClientGenderException")]
        public void ExceptionTest()
        { 
         
            Client c = new Client();
            c.GetGenderStatic();
            c.GenerateRandomClients();
            c.RClientList.Add(new ClientBase(27, "ANNA", 'p'));

        }

        [TestMethod()]
        public void CheckAllVowelsTest()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("Aaaa") == true);
        }

        public void CheckAllVowelsTest2()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("Aaaba") == false);
        }

        [TestMethod()]
        public void CheckAllVowelsTest3()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("Uaeio") == true);
        }

        [TestMethod()]
        public void CheckAllConsonantesTest()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("Cndvf") == true);
        }

        [TestMethod()]
        public void CheckAllConsonantesTest2()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("GGGGG") == true);
        }

        [TestMethod()]
        public void CheckAllConsonantesTest3()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckAllVowelsOrAllConsonantes("GGGGaG") == false);
        }

        [TestMethod()]
        public void CheckConsecutiveVowels()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Aeibsoldufuyuh") == true);
        }

        [TestMethod()]
        public void CheckConsecutiveVowels2()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Abaeiiit") == true);
        }

        [TestMethod()]
        public void CheckConsecutiveVowels3()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Qiiiit") == false);
        }

        [TestMethod()]
        public void CheckConsecutiveConsonantes()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Tbsoldufuyuh") == true);
        }

        [TestMethod()]
        public void CheckConsecutiveConsonantes2()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Abbbbaeiiit") == true);
        }

        [TestMethod()]
        public void CheckConsecutiveConsonantes3()
        {
            ClientBase c = new ClientBase();
            Assert.IsTrue(c.CheckConsecutiveVowelsOrConsecutiveConsonantes("Abbbbeiiit") == false);
        }
    }
}
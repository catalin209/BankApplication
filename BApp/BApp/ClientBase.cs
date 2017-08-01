using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp
{
    public class ClientBase
    {
        /// <summary>
        /// The first name of a client.
        /// </summary>
        private string firstName;

        /// <summary>
        /// The age of a client.
        /// </summary>
        private int age;

        /// <summary>
        /// The gender of a client.
        /// </summary>
        private char gender;

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age of a client.
        /// </value>
        public int Age
        {
            get { return age; }
            set {
                try
                {
                    if ( value < 0 || value > 150 ) throw new Exception("InvalidClientGenderException");
                    else age = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public ClientBase() { }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public char Gender
        {
            get { return gender; }
            set
            {
                try
                {
                    if (!(value.Equals('m')) && !(value.Equals('f')) && !(value.Equals('x'))) throw new Exception("InvalidClientGenderException");
                    else gender = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value) ||
                        CheckAllVowelsOrAllConsonantes(value) || CheckConsecutiveVowelsOrConsecutiveConsonantes(value))
                    {
                        throw new Exception("Not a valid name");
                    }
                    else
                    {
                        firstName = value;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class.
        /// </summary>
        /// <param name="agee">The agee.</param>
        /// <param name="firstNamee">The first namee.</param>
        /// <param name="gendere">The gendere.</param>
        public ClientBase(int agee, string firstNamee, char gendere) : this(firstNamee, gendere)
        {
           Age = agee;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class.
        /// </summary>
        /// <param name="firstNamee">The first namee.</param>
        /// <param name="gender">The gender.</param>
        public ClientBase(string firstName, char gender)
        {
            FirstName = firstName;
            Gender    = gender;
        }

        public ClientBase(string firstName)
        {
            FirstName = firstName;
        }

        public string vowels = "aeiouAEIOU";

        /// <summary>
        /// Checks the vowels.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool CheckAllVowelsOrAllConsonantes(string name)
        {
            int check = 0;

            for(int i = 0; i < name.Length; i++)
            {
                if (vowels.Contains(name[i]))
                {
                    check++;
                }
            }

            if (check == name.Length)
            {
                return true;
            }
            else
            {
                if (check == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the consecutive vowels or consecutive consonantes.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool CheckConsecutiveVowelsOrConsecutiveConsonantes(string name)
        {
            for(int i = 0; i < name.Length - 2; i++)
            {
                if(vowels.Contains(name[i]) && vowels.Contains(name[i+1]) && vowels.Contains(name[i+2]))
                {
                    if ( (char.ToLower(name[i]) != char.ToLower(name[i + 1])) && (char.ToLower(name[i + 1]) != char.ToLower(name[i + 2])))
                    {
                        return true;
                    }
                }
                if ( !(vowels.Contains(name[i])) && !(vowels.Contains(name[i + 1])) && !(vowels.Contains(name[i + 2])))
                {
                    if (char.ToLower(name[i]) != char.ToLower(name[i + 1]) && char.ToLower(name[i + 1]) != char.ToLower(name[i + 2]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
      
    }
}

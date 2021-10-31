using System;
using System.Collections.Generic;
namespace Bot
{
    public class RoleAdmin : Role
    {
        public static List<string> globalRatingsList = new List<string>();
        public static Dictionary<string, Company> invitations = new Dictionary<string, Company>();

        public RoleAdmin(String name, int id) : base(name, id)
        {
        }
        public static string CodeGeneratortoUserCompany(Company company)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            List<string> CodeList = new List<string>();

            if (CodeList.Contains(resultString))
            {
                random = new Random();
            }
            else
            {
                CodeList.Add(resultString);
            }

            invitations.Add(resultString, company);
            return resultString;
        }

        public void AddRating(string rating)
        {
            globalRatingsList.Add(rating);
        }

        public void DeleteRating(string rating)
        {
            globalRatingsList.Remove(rating);
        }
    }
}
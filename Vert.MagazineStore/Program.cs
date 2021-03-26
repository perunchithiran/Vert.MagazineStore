using System;
using System.Collections.Generic;
using System.Linq;

namespace Vert.MagazineStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MagazineAPI magazineAPI = new MagazineAPI();

            var subscriberList = magazineAPI.GetSubscribers().Result.Data; //Getting the list of subscribers
            var categorieyList = magazineAPI.GetCategories().Result.Data; //Getting the list of categories

            if (categorieyList.Count > 0 && subscriberList.Count > 0)
            {
                List<Magazine> magazineList = new List<Magazine>();

                foreach (var category in categorieyList)
                {
                    var magazine = magazineAPI.GetMagazineByCategory(category).Result.Data;
                    if (magazine != null)
                    {
                        magazineList.AddRange(magazine);
                    }
                }

                #region Posting Answer

                AnswerRequest answerRequest = new AnswerRequest();
                answerRequest.subscribers = new List<string>();

                foreach (var subscriber in subscriberList)
                {
                    var categoryCount = magazineList.Where(r => subscriber.MagazineIds.Contains(Convert.ToInt32(r.Id))).ToList().Distinct().Count();

                    if (categorieyList.Count.Equals(categoryCount))
                    {
                        answerRequest.subscribers.Add(subscriber.Id);
                    }
                }

                var result = magazineAPI.PostAnswer(answerRequest).Result;

                #endregion
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Vertmarkets Inc. Magazine Store\r");
                Console.WriteLine("-------------------------------\n");
                Console.WriteLine($"{result}\n");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
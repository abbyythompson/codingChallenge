using System;
using System.Collections.Generic;

namespace CeloPracticalChallenge
{
    public class RandomUserGenerator
    {
        private List<String> _firstNames = new List<String>() { "Mary", "June", "Max", "Terrance", "Glenys", "Polly", "Harry", "Georgia", "Kade", "Lily", "Pandora", "Jack", "Sam", "Sarah", "Hazel", "Blair" };
        private List<String> _lastNames = new List<String>() { "James", "Norris", "McDonald", "Erickson", "Lee", "Retter", "Calder", "Poland", "Flynn", "Hunter", "Black", "Gray", "Musk", "Schgo" };
        private List<String> _titles = new List<String>() { "Ms", "Mrs", "Mr", "Miss", "Dr" };

        public User CreateRandomUser()
        {
            var rnd = new Random();

            string title = _titles[rnd.Next(_titles.Count)];
            string fName = _firstNames[rnd.Next(_firstNames.Count)];
            string lName = _lastNames[rnd.Next(_lastNames.Count)];

            return new User
            {
                Title = title,
                FirstName = fName,
                LastName = lName,
                Email = fName + lName + "@gmail.com",
                DoB = RandomDay(rnd),
                PhoneNum = rnd.Next(123456789, 999999999).ToString(),
                ThumbnailImgURL = "https://picsum.photos/200"
            };
        }

        private DateTime RandomDay(Random rnd)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}

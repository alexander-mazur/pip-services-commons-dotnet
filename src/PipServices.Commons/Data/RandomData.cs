//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace PipServices.Commons.Data
//{
//    public partial class RandomData
//    {
//        private static readonly Random random = new Random();

//        private static readonly string Digits = "01234956789";
//        private static readonly string Symbols = "_,.:-/.[].{},#-!,$=%.+^.&*-() ";
//        private static readonly string AlphaLower = "abcdefghijklmnopqrstuvwxyz";
//        private static readonly string AlphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//        private static readonly string Alpha = AlphaUpper + AlphaLower;
//        private static readonly string Chars = Alpha + Digits + Symbols;
//        private static readonly string[] NamePrefixes = new string[] { "Dr.", "Mr.", "Mrs" };
//        private static readonly string[] NameSuffixes = new string[] { "Jr.", "Sr.", "II", "III" };
//        private static readonly string[] FirstNames = new string[] {
//            "John", "Bill", "Andrew", "Nick", "Pamela", "Bela", "Sergio", "George", "Hurry", "Cecilia", "Vesta", "Terry", "Patrick"
//        };
//        private static readonly string[] LastNames = new string[] {
//            "Doe", "Smith", "Johns", "Gates", "Carmack", "Zontak", "Clinton", "Adams", "First", "Lopez", "Due", "White", "Black"
//        };
//        private static readonly string[] Colors = new string[] {
//            "Black", "White", "Red", "Blue", "Green", "Yellow", "Purple", "Grey", "Magenta", "Cian"
//        };
//        private static readonly string[] Stuffs = new string[] {
//            "Game", "Ball", "Home", "Board", "Car", "Plane", "Hotel", "Wine", "Pants", "Boots", "Table", "Chair"
//        };
//        private static readonly string[] Adjectives = new string[] {
//            "Large", "Small", "High", "Low", "Certain", "Fuzzy", "Modern", "Faster", "Slower"
//        };
//        private static readonly string[] Verbs = new string[] {
//            "Run", "Stay", "Breeze", "Fly", "Lay", "Write", "Draw", "Scream"
//        };
//        private static readonly string[] StreetTypes = new string[] {
//            "Lane", "Court", "Circle", "Drive", "Way", "Loop", "Blvd", "Street"
//        };
//        private static readonly string[] StreetPrefix = new string[] {
//            "North", "South", "East", "West", "Old", "New", "N.", "S.", "E.", "W."
//        };
//        private static readonly string[] StreetNames = new string[] {
//            "1st", "2nd", "3rd", "4th", "53rd", "6th", "8th", "Acacia", "Academy", "Adams", "Addison", "Airport", "Albany", "Alderwood", "Alton", "Amerige", "Amherst", "Anderson",
//            "Ann", "Annadale", "Applegate", "Arcadia", "Arch", "Argyle", "Arlington", "Armstrong", "Arnold", "Arrowhead", "Aspen", "Augusta", "Baker", "Bald Hill", "Bank", "Bay Meadows",
//            "Bay", "Bayberry", "Bayport", "Beach", "Beaver Ridge", "Bedford", "Beech", "Beechwood", "Belmont", "Berkshire", "Big Rock Cove", "Birch Hill", "Birchpond", "Birchwood",
//            "Bishop", "Blackburn", "Blue Spring", "Bohemia", "Border", "Boston", "Bow Ridge", "Bowman", "Bradford", "Brandywine", "Brewery", "Briarwood", "Brickell", "Brickyard",
//            "Bridge", "Bridgeton", "Bridle", "Broad", "Brookside", "Brown", "Buckingham", "Buttonwood", "Cambridge", "Campfire", "Canal", "Canterbury", "Cardinal", "Carpenter",
//            "Carriage", "Carson", "Catherine", "Cedar Swamp", "Cedar", "Cedarwood", "Cemetery", "Center", "Central", "Chapel", "Charles", "Cherry Hill", "Chestnut", "Church", "Circle",
//            "Clark", "Clay", "Cleveland", "Clinton", "Cobblestone", "Coffee", "College", "Colonial", "Columbia", "Cooper", "Corona", "Cottage", "Country Club", "Country", "County", "Court",
//            "Courtland", "Creek", "Creekside", "Crescent", "Cross", "Cypress", "Deerfield", "Del Monte", "Delaware", "Depot", "Devon", "Devonshire", "Division", "Dogwood", "Dunbar",
//            "Durham", "Eagle", "East", "Edgefield", "Edgemont", "Edgewater", "Edgewood", "El Dorado", "Elizabeth", "Elm", "Essex", "Euclid", "Evergreen", "Fairfield", "Fairground", "Fairview",
//            "Fairway", "Fawn", "Fifth", "Fordham", "Forest", "Foster", "Foxrun", "Franklin", "Fremont", "Front", "Fulton", "Galvin", "Garden", "Gartner", "Gates", "George", "Glen Creek",
//            "Glen Eagles", "Glen Ridge", "Glendale", "Glenlake", "Glenridge", "Glenwood", "Golden Star", "Goldfield", "Golf", "Gonzales", "Grand", "Grandrose", "Grant", "Green Hill",
//            "Green Lake", "Green", "Greenrose", "Greenview", "Gregory", "Griffin", "Grove", "Halifax", "Hamilton", "Hanover", "Harrison", "Hartford", "Harvard", "Harvey", "Hawthorne",
//            "Heather", "Henry Smith", "Heritage", "High Noon", "High Point", "High", "Highland", "Hill Field", "Hillcrest", "Hilldale", "Hillside", "Hilltop", "Holly", "Homestead",
//            "Homewood", "Honey Creek", "Howard", "Indian Spring", "Indian Summer", "Iroquois", "Jackson", "James", "Jefferson", "Jennings", "Jockey Hollow", "John", "Johnson", "Jones",
//            "Joy Ridge", "King", "Kingston", "Kirkland", "La Sierra", "Lafayette", "Lake Forest", "Lake", "Lakeshore", "Lakeview", "Lancaster", "Lane", "Laurel", "Leatherwood", "Lees Creek",
//            "Leeton Ridge", "Lexington", "Liberty", "Lilac", "Lincoln", "Linda", "Littleton", "Livingston", "Locust", "Longbranch", "Lookout", "Lower River", "Lyme", "Madison", "Maiden",
//            "Main", "Mammoth", "Manchester", "Manhattan", "Manor Station", "Maple", "Marconi", "Market", "Marsh", "Marshall", "Marvon", "Mayfair", "Mayfield", "Mayflower", "Meadow",
//            "Meadowbrook", "Mechanic", "Middle River", "Miles", "Mill Pond", "Miller", "Monroe", "Morris", "Mountainview", "Mulberry", "Myrtle", "Newbridge", "Newcastle", "Newport",
//            "Nichols", "Nicolls", "North", "Nut Swamp", "Oak Meadow", "Oak Valley", "Oak", "Oakland", "Oakwood", "Ocean", "Ohio", "Oklahoma", "Olive", "Orange", "Orchard", "Overlook",
//            "Pacific", "Paris Hill", "Park", "Parker", "Pawnee", "Peachtree", "Pearl", "Peg Shop", "Pendergast", "Peninsula", "Penn", "Pennington", "Pennsylvania", "Pheasant", "Philmont",
//            "Pierce", "Pin Oak", "Pine", "Pineknoll", "Piper", "Plumb Branch", "Poor House", "Prairie", "Primrose", "Prince", "Princess", "Princeton", "Proctor", "Prospect", "Pulaski",
//            "Pumpkin Hill", "Purple Finch", "Queen", "Race", "Ramblewood", "Redwood", "Ridge", "Ridgewood", "River", "Riverside", "Riverview", "Roberts", "Rock Creek", "Rock Maple",
//            "Rockaway", "Rockcrest", "Rockland", "Rockledge", "Rockville", "Rockwell", "Rocky River", "Roosevelt", "Rose", "Rosewood", "Ryan", "Saddle", "Sage", "San Carlos", "San Juan",
//            "San Pablo", "Santa Clara", "Saxon", "School", "Schoolhouse", "Second", "Shadow Brook", "Shady", "Sheffield", "Sherman", "Sherwood", "Shipley", "Shub Farm", "Sierra",
//            "Silver Spear", "Sleepy Hollow", "Smith Store", "Smoky Hollow", "Snake Hill", "Southampton", "Spring", "Spruce", "Squaw Creek", "St Louis", "St Margarets", "St Paul", "State",
//            "Stillwater", "Strawberry", "Studebaker", "Sugar", "Sulphur Springs", "Summerhouse", "Summit", "Sunbeam", "Sunnyslope", "Sunset", "Surrey", "Sutor", "Swanson", "Sycamore",
//            "Tailwater", "Talbot", "Tallwood", "Tanglewood", "Tarkiln Hill", "Taylor", "Thatcher", "Third", "Thomas", "Thompson", "Thorne", "Tower", "Trenton", "Trusel", "Tunnel",
//            "University", "Vale", "Valley Farms", "Valley View", "Valley", "Van Dyke", "Vermont", "Vernon", "Victoria", "Vine", "Virginia", "Wagon", "Wall", "Walnutwood", "Warren",
//            "Washington", "Water", "Wayne", "Westminster", "Westport", "White", "Whitemarsh", "Wild Rose", "William", "Williams", "Wilson", "Winchester", "Windfall", "Winding Way",
//            "Winding", "Windsor", "Wintergreen", "Wood", "Woodland", "Woodside", "Woodsman", "Wrangler", "York",
//        };
//        private static readonly string[] AllWords = FirstNames.Concat(LastNames).Concat(Colors).Concat(Stuffs).Concat(Adjectives)
//            .Concat(Verbs).ToArray();

//        public static char Pick(string values)
//        {
//            if (values == null || values.Length == 0)
//                return '\0';

//            return values[Integer(values.Length)];
//        }

//        public static string Pick(string[] values)
//        {
//            return Pick<string>(values);
//        }

//        public static T Pick<T>(T[] values)
//        {
//            if (values == null || values.Length == 0)
//                return default(T);

//            return values[Integer(values.Length)];
//        }

//        public static T Pick<T>(IList<T> values)
//        {
//            if (values == null || values.Count == 0)
//                return default(T);

//            return values[Integer(values.Count)];
//        }

//        public static T PickEnum<T>() where T : struct, IConvertible
//        {
//            if (!typeof(T).GetTypeInfo().IsEnum)
//            {
//                throw new ArgumentException("T must be an enumerated type");
//            }
//            Type etype = typeof(T);
//            T[] vals = etype.GetTypeInfo().GetEnumValues().Cast<T>().ToArray();
//            return Pick(vals);
//        }

//        public static List<int> Sequence(int max)
//        {
//            return Sequence(1, max);
//        }

//        public static List<int> Sequence(int min, int max)
//        {
//            int count = Integer(min, max);
//            List<int> retval = new List<int>();
//            for (int i = 0; i < count; i++)
//            {
//                retval.Add(i);
//            }
//            return retval;
//        }

//        public static string Distort(string value)
//        {
//            value = value.ToLower();

//            if (Integer(5) == 3)
//                value = value.Substring(0, 1).ToUpper() + value.Substring(1);

//            if (Integer(3) == 2)
//                value = value + Pick(Symbols);

//            return value;
//        }

//        public static bool Chance(float chances, float maxChances)
//        {
//            maxChances = Math.Max(maxChances, chances);
//            double start = (maxChances - chances) / 2;
//            double end = start + chances;
//            double hit = random.NextDouble() * maxChances;
//            return hit >= start && hit <= end;
//        }

//        public static bool Bool()
//        {
//            return random.Next(100) < 50;
//        }

//        public static int Integer(int maxValue)
//        {
//            return random.Next(maxValue);
//        }

//        public static int Integer(int minValue, int maxValue)
//        {
//            return random.Next(minValue, maxValue);
//        }

//        public static float Float(int maxValue)
//        {
//            return (float)random.NextDouble() * maxValue;
//        }

//        public static float Float(float minValue, float maxValue)
//        {
//            return (float)(minValue + random.NextDouble() * (maxValue - minValue));
//        }

//        public static double Double(double maxValue)
//        {
//            return random.NextDouble() * maxValue;
//        }

//        public static double Double(double minValue, double maxValue)
//        {
//            return minValue + random.NextDouble() * (maxValue - minValue);
//        }

//        public static DateTime Date(int minYear = 0, int maxYear = 0)
//        {
//            int currentYear = System.DateTime.Now.Year;
//            minYear = minYear == 0 ? currentYear - Integer(10) : minYear;
//            maxYear = maxYear == 0 ? currentYear : maxYear;

//            var year = Integer(minYear, maxYear);
//            var month = Integer(1, 13);
//            var day = Integer(1, 32);

//            if (month == 2)
//                day = Math.Min(28, day);
//            else if (month == 4 || month == 6 || month == 9 || month == 11)
//                day = Math.Min(30, day);

//            return new DateTime(year, month, day);
//        }

//        public static TimeSpan Time()
//        {
//            var hour = Integer(0, 24);
//            var min = Integer(0, 60);
//            var sec = Integer(0, 60);
//            var millis = Integer(0, 1000);

//            return new TimeSpan(hour, min, sec, millis);
//        }

//        public static DateTime DateTime(int minYear = 0, int maxYear = 0)
//        {
//            return Date(minYear, maxYear).Add(Time());
//        }

//        public static T Enum<T>()
//        {
//            var enumType = typeof(T);
//            var values = enumType.GetTypeInfo().GetEnumValues();
//            var index = Integer(values.Length);
//            return (T)values.GetValue(index);
//        }

//        public static int Update(int value, int range = 0)
//        {
//            range = range == 0 ? (int)(0.1 * value) : range;
//            var minValue = value - range;
//            var maxValue = value + range;
//            return Integer(minValue, maxValue);
//        }

//        public static float Update(float value, float range = 0)
//        {
//            range = range == 0 ? (float)(0.1 * value) : range;
//            var minValue = value - range;
//            var maxValue = value + range;
//            return Float(minValue, maxValue);
//        }

//        public static double Update(double value, double range = 0)
//        {
//            range = range == 0 ? 0.1 * value : range;
//            var minValue = value - range;
//            var maxValue = value + range;
//            return Double(minValue, maxValue);
//        }

//        public static DateTime Update(DateTime value, float range = 0)
//        {
//            range = range != 0 ? range : 10;
//            float days = Float(-range, range);
//            return value.AddDays(days);
//        }

//        public static char AlphaChar()
//        {
//            return Alpha[Integer(Alpha.Length)];
//        }

//        public static string String(int minLength, int maxLength, bool includeAlphaUpper = true, bool includeAlphaLower = true, bool includeDigits = true, bool includeSymbols = true)
//        {
//            int len = Integer(minLength, maxLength);
//            string chars = "";
//            if (includeAlphaUpper) { chars += AlphaUpper; }
//            if (includeAlphaLower) { chars += AlphaLower; }
//            if (includeDigits) { chars += Digits; }
//            if (includeSymbols) { chars += Symbols; }
//            int charLen = chars.Length;

//            string retval = "";

//            for (int i = 0; i < len; i++)
//            {
//                retval += chars[Integer(charLen)];
//            }

//            return retval;
//        }

//        public static string Color()
//        {
//            return Pick(Colors);
//        }

//        public static string Stuff()
//        {
//            return Pick(Stuffs);
//        }

//        public static string Adjective()
//        {
//            return Pick(Adjectives);
//        }

//        public static string Verb()
//        {
//            return Pick(Verbs);
//        }

//        public static string Phrase(int minSize, int maxSize = 0)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);

//            var phrase = Pick(AllWords);
//            while (phrase.Length < size)
//            {
//                phrase += " " + Pick(AllWords).ToLower();
//            }

//            return phrase;
//        }

//        public static string Name()
//        {
//            var name = "";

//            if (Integer(5) == 3)
//                name += Pick(NamePrefixes) + " ";

//            name += Pick(FirstNames) + " " + Pick(LastNames);

//            if (Integer(10) == 5)
//                name += " " + Pick(NameSuffixes);

//            return name;
//        }

//        public static string Word()
//        {
//            return Pick(AllWords);
//        }

//        public static string Words(int min, int max)
//        {
//            string retval = "";
//            int count = Integer(min, max);
//            for (int i = 0; i < count; i++)
//            {
//                retval += Pick(AllWords);
//            }
//            return retval;
//        }

//        public static string PhoneNumber()
//        {
//            return $"({Integer(111, 999).ToString("000")}) {Integer(111, 999).ToString("000")}-{Integer(0, 9999).ToString("0000")}";
//        }

//        public static string EmailAddress()
//        {
//            return $"{Words(2, 6)}@{Words(1, 3)}.com";
//        }

//        public static string Text(int minSize, int maxSize = 0)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);

//            var text = Pick(AllWords);
//            while (text.Length < size)
//            {
//                var next = Pick(AllWords);
//                if (Integer(6) < 4)
//                    next = " " + next.ToLower();
//                else if (Integer(5) < 2)
//                    next = Pick(":,-") + next.ToLower();
//                else if (Integer(5) < 3)
//                    next = Pick(":,-") + " " + next.ToLower();
//                else
//                    next = Pick(".!?") + " " + next;

//                text += next;
//            }

//            return text;
//        }

//    }
//}
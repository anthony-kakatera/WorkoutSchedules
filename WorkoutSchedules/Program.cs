using System.Security.Cryptography;

namespace WorkoutSchedules
{

    public class WorkoutSchedules {
        
        public static void Main(string[] args) {
            //string to hold schedule for casual members
            string oldmemberSchedule = "";
            //string to hold schedule for begginner members
            string beginnermemberSchedule = "";
            string filePath = ""; //C:\Users\akakatera\source\repos\WorkoutSchedules\sample.txt
            //getting file from user and setting to filePath
            getFileFromUser(filePath, oldmemberSchedule, beginnermemberSchedule);
        }

        private static void readFile(string filePath, string oldmemberSchedule, string beginnermemberSchedule, int exerciseCount)
        {
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(@filePath);

            //get all lines
            string[] lines = System.IO.File.ReadAllLines(@filePath);

            //loop through lines start from 1 cause the first lines contains only header titles
            for (int i = 1; i < exerciseCount+1; i++) {
                //go through each line and split at the comma,
                //using random to generate a random exercise each time
                string[]words = lines[RandomNumberGenerator.GetInt32(1, lines.Length)].Split(',');
                //split reps to get both upper and lower bunds
                string[] reps = words[1].Trim().Split('-');
                //generate random rep
                int randomRep = getRandomRep(reps[0], reps[1]);
                //generate schedule for old members
                oldmemberSchedule = oldmemberSchedule + generateScheduleForMembers(words[0], randomRep, false);
                //generate schedule for beginner members
                beginnermemberSchedule = beginnermemberSchedule + generateScheduleForMembers(words[0], randomRep, true);
            }
            outputSchedule(oldmemberSchedule, beginnermemberSchedule);
        }

        private static void outputSchedule(string oldmemberSchedule, string beginnermemberSchedule) {
            //Casual member schedule output headers
            Console.WriteLine("Exercise" + symetricalSpacer("Exercise") + "Reps");
            Console.WriteLine("--------" + symetricalSpacer("--------") + "-----");
            Console.WriteLine(oldmemberSchedule);
            //Beginner schedule
            Console.WriteLine("Exercise" + symetricalSpacer("Exercise") + "Reps");
            Console.WriteLine("--------" + symetricalSpacer("--------") + "-----");
            Console.WriteLine(beginnermemberSchedule);
        }

        public static int getRandomRep(string lowerBund, string upperBund) {
            //convert bunds to ints
            int lowerIntBund = Int32.Parse(lowerBund);
            int upperIntBund = Int32.Parse(upperBund);
            //generate random
            return RandomNumberGenerator.GetInt32(lowerIntBund, upperIntBund);
        }

        public static string symetricalSpacer(string exerciseName) {
            //create max length for each and every exercise
            int maxLength = 20;
            //subtract current name length from max length
            int remainder = maxLength - exerciseName.Length;
            //create space
            String spacer = "";
            //loop through remaining space and add to it the empty space
            for (int i = 0; i < remainder; i++) {
                spacer += " ";
            }
            return spacer;
        }

        public static string generateScheduleForMembers(string exerciseName, int randomRep, bool beginner)
        {
            //string to hold generated schedule for return 
            string builder = "";
            //check if this is a beginner or casual member
            if (beginner)
            {
                //building string and randomizing
                builder = builder + exerciseName + symetricalSpacer(exerciseName) + randomRep / 2 + "\n";
            }
            else {
                //building string and randomizing and divide by half for all beginner reps
                builder = builder + exerciseName + symetricalSpacer(exerciseName) + randomRep + "\n";
            }
            //return string
            return builder;
        }

        public static void getFileFromUser(string filePath, string oldmemberSchedule, string beginnermemberSchedule) {
            //Get new filename from user
            string @userFileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\", "data\\assignment_two_gym_exercises.csv");
            //verify if file exists
            if (File.Exists(@userFileName))
            {
                //get number of exercises to be generated from file
                int exerciseCount = getNumberOfExercises(@userFileName);
                //Read file and print out the generated schedule
                readFile(@userFileName, oldmemberSchedule, beginnermemberSchedule, exerciseCount);
            }
            else
            {
                Console.WriteLine("File doesn't exist !");
                //simple recursive call
                getFileFromUser(filePath, oldmemberSchedule, beginnermemberSchedule);
            }
        }

        private static int getNumberOfExercises(string userFileName)
        {
            //default value
            int defaultValue = 3;
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(@userFileName);
            //get all lines
            string[] lines = System.IO.File.ReadAllLines(@userFileName);
            //get number of exercises from user
            Console.WriteLine("Please provide a number for the exercises to be generated");
            string numberOfExercises = Console.ReadLine();
            //check if provided value is an actual number
            if (!int.TryParse(numberOfExercises, out _))
            {
                //Notify user of the wrong input
                Console.WriteLine($"The provided input isn't a number switching to default value {defaultValue}");
                return defaultValue;
            }
            else
            {
                //parse to int 
                int tempCount = Int32.Parse(numberOfExercises);
                //checke if temp is greater than or exercises available in the file
                if (tempCount > lines.Length - 1) {
                    //out put exceeded result
                    Console.WriteLine($"The provided number far exceeds the available exercises, switching to default value {defaultValue}");
                    return defaultValue;
                }
                return tempCount;
            }
        }
    }


}
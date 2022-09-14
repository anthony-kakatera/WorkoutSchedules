namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Test casual gym member output
            //Exercise name
            string exercise = "Burpees";
            //generate exercise
            var result = WorkoutSchedules.WorkoutSchedules.generateScheduleForMembers(exercise, 50, false);
            //test exercise
            Assert.That(result, Is.EqualTo(exercise + WorkoutSchedules.WorkoutSchedules.symetricalSpacer(exercise) + 50 + "\n"));
        }

        [Test]
        public void Test2()
        {
            //Test beginner member output by halving the expected random int which in ourcase is 50
            //Exercise name
            string exercise = "Burpees";
            //generate exercise
            var result = WorkoutSchedules.WorkoutSchedules.generateScheduleForMembers(exercise, 50, true);
            //test exercise
            Assert.That(result, Is.EqualTo(exercise + WorkoutSchedules.WorkoutSchedules.symetricalSpacer(exercise) + 50/2 + "\n"));
        }
    }
}
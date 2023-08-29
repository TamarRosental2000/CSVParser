
namespace CSVParser.Model
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInches { get; set; }
        public int WeightPounds { get; set; }
        public TeamModel Team { get; set; }
    }
}

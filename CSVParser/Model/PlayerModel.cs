
using System;
using System.Text.Json;

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
        public override bool Equals(object obj)
        {
            return this.EqualsPlayerModel(obj as PlayerModel);
        }
        public bool EqualsPlayerModel(PlayerModel other)
        {
            if (other == null)
                return false;

            return JsonSerializer.Serialize(this) == JsonSerializer.Serialize(other);
        }
    }
}

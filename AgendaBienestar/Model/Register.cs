namespace AgendaBienestar.Model
{
    class Register : IEquatable<Register>, IComparable<Register>
    {
        private string _comment = "Sin comentario.";

        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public string Comment
        {
            get => _comment;
            set => _comment = value.Trim();
        }
        public int ActivityLevel { get; set; }
        public int Energy { get; set; }

        public Register() { }

        public Register(Guid id)
        {
            Id = id;
        }

        public Register(DateTime date, string comment, int activityLevel, int energy)
        {
            Id = Guid.NewGuid();
            Date = date;
            Comment = comment;
            ActivityLevel = activityLevel;
            Energy = energy;
        }

        public Register(Guid id, DateTime date, string comment, int activityLevel, int energy)
        {
            Id = id;
            Date = date;
            Comment = comment;
            ActivityLevel = activityLevel;
            Energy = energy;
        }

        /// <summary>
        /// Two registers are equal if they have the same Id.
        /// </summary>
        /// <param name="other">Register to compare with.</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public bool Equals(Register? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Two registers are equal if they have the same Id.
        /// </summary>
        /// <param name="obj">Register to compare with.</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Register)obj);
        }

        public override int GetHashCode() => Id.GetHashCode();
        

        /// <summary>
        /// Two Registers are equal if they have the same id.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public static bool operator ==(Register? left, Register? right) => Equals(left, right);

        /// <summary>
        /// Two Registers are different if they haven't the same id.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they aren't equal, false otherwise.</returns>
        public static bool operator !=(Register? left, Register? right) => !Equals(left, right);

        /// <summary>
        /// First compare by the id, then by date, then by the activity level and then by the energy.
        /// </summary>
        /// <param name="other">Register to compare with.</param>
        /// <returns>int</returns>
        public int CompareTo(Register? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;

            var dateComparison = Date.CompareTo(other.Date);
            if (dateComparison != 0) return dateComparison;

            var activityLevelComparison = ActivityLevel.CompareTo(other.ActivityLevel);
            if (activityLevelComparison != 0) return activityLevelComparison;

            return Energy.CompareTo(other.Energy);
        }
    }
}

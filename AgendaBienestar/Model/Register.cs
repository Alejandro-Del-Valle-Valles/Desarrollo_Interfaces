namespace AgendaBienestar.Model
{
    class Register : IEquatable<Register>, IComparable<Register>
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int ActivityLevel { get; set; }
        public int Energy { get; set; }

        public Register(DateTime date, string comment, int activityLevel, int energy)
        {
            Date = date;
            Comment = comment;
            ActivityLevel = activityLevel;
            Energy = energy;
        }

        /// <summary>
        /// Two Registers are equal if they have the same data.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public bool Equals(Register? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Date.Equals(other.Date) && 
                   string.Equals(Comment, other.Comment, StringComparison.OrdinalIgnoreCase) 
                   && ActivityLevel == other.ActivityLevel 
                   && Energy == other.Energy;
        }

        /// <summary>
        /// Two Registers are equal if they have the same data.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Register)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Date);
            hashCode.Add(Comment, StringComparer.OrdinalIgnoreCase);
            hashCode.Add(ActivityLevel);
            hashCode.Add(Energy);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Two Registers are equal if they have the same data.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public static bool operator ==(Register? left, Register? right) => Equals(left, right);

        /// <summary>
        /// Two Registers are different if they haven't the same data.
        /// </summary>
        /// <param name="other">Register register</param>
        /// <returns>bool, true if they aren't equal, false otherwise.</returns>
        public static bool operator !=(Register? left, Register? right) => !Equals(left, right);

        /// <summary>
        /// First compare by the date, the by the activity level and then by the energy.
        /// </summary>
        /// <param name="other">Register to compare with.</param>
        /// <returns>int</returns>
        public int CompareTo(Register? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            var dateComparison = Date.CompareTo(other.Date);
            if (dateComparison != 0) return dateComparison;

            var activityLevelComparison = ActivityLevel.CompareTo(other.ActivityLevel);
            if (activityLevelComparison != 0) return activityLevelComparison;

            return Energy.CompareTo(other.Energy);
        }
    }
}

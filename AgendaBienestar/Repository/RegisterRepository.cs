using AgendaBienestar.Model;

namespace AgendaBienestar.Repository
{
    static class RegisterRepository
    {
        private static readonly HashSet<Register> Registers = new();

        /// <summary>
        /// Add a new Register to the memory if it doesn't exist yet.
        /// </summary>
        /// <param name="register">Register to add.</param>
        /// <returns>bool, true if it was inserted, false otherwise.</returns>
        public static bool AddRegister(Register register) => Registers.Add(register);

        /// <summary>
        /// Delete a Register from the memory if exists.
        /// </summary>
        /// <param name="register">Register to delete.</param>
        /// <returns>bool, true if it was deleted, false otherwise.</returns>
        public static bool DeleteRegister(Register register) => Registers.Remove(register);

        /// <summary>
        /// Update the data of a Register by deleting and adding it if exists.
        /// </summary>
        /// <param name="register">Register to update.</param>
        /// <returns>bool, true if it was updated, false otherwise.</returns>
        public static bool EditRegister(Register register)
        {
            bool isEdited = false;
            if (Registers.Contains(register))
            {
                Registers.Remove(register);
                isEdited = Registers.Add(register);
            }
            return isEdited;
        }

        /// <summary>
        /// Return the HasSet with all register. Can be empty.
        /// </summary>
        /// <returns>HasSet of Register</returns>
        public static HashSet<Register> GetAllRegisters() => Registers;
    }
}

using System;
using System.Linq;
using System.Net.Mail;

namespace ShipItApp
{
    public static class Validation
    {
        /// <summary>
        /// Checks if the provided state abbreviation is valid (two alphabetic characters).
        /// </summary>
        public static bool ValidateStateAbbreviation(string state)
        {
            return !string.IsNullOrWhiteSpace(state)
                   && state.Trim().Length == 2
                   && state.Trim().All(char.IsLetter);
        }

        /// <summary>
        /// Validates email format using System.Net.Mail.MailAddress.
        /// </summary>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new MailAddress(email.Trim());
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ensures password is at least 6 characters long.
        /// </summary>
        public static bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password)
                   && password.Length >= 6;
        }
    }
}

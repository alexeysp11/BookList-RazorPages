namespace Security.Models
{
    /// <summary>
    /// Class for hashing data. 
    /// </summary>
    public class Hashing
    {
        /// <summary>
        /// Allows to implement simple hash function. 
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>String that consists of 4 characters</returns>
        public string HashFunc(string input)
        {
            int coef = 32; 
            int iHash = 0; 

            // Get integer of a hash. 
            for (int i = 0; i < input.Length; i++)
            {
                iHash = iHash * coef + input[i]; 
            }

            // Create an array of bytes. 
            byte[] bHash = new byte[sizeof(int)]; 
            System.Buffer.BlockCopy(new int[] { iHash }, 0, bHash, 0, bHash.Length); 

            // Get a string of hash. 
            string sHash = System.Text.Encoding.ASCII.GetString(bHash);

            return sHash; 
        }
    }
}
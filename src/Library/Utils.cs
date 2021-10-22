using System.Linq;
namespace Bot
{
    public class Utils

    {
        /// <summary>
        /// Valida un número de cédula de identidad; el número puede tener o no puntos y el guión pero debe tener el dígito verificador
        /// </summary>
        /// <param name="id">El número de cédula</param>
        /// <returns>Retorna true si el número de cédula es válido y false en caso que sea invalido.</returns>
        public static bool IdIsValid(string id)
        {
            long tempOut;

            // Quitar puntos y guiones
            id = id.Replace(".", "");
            id = id.Replace("-", "");

            // Validar largo
            if (id.Length == 8 && long.TryParse(id, out tempOut))
            {
                var idAsCharArray = id.ToArray();
                var idAsIntArray = idAsCharArray.Select(c => int.Parse(c.ToString())).ToArray();
                var referenceArray = "2987634".ToArray().Select(c => int.Parse(c.ToString())).ToArray();
                var inputCheckDigit = idAsIntArray[7];

                // Calcular número verificador
                int checkSum = 0;
                for (int i = 0; i < referenceArray.Length; i++)
                {
                    checkSum = checkSum + (idAsIntArray[i] * referenceArray[i]);
                }

                int checkDigit = 10 - (checkSum % 10);

                if (checkDigit == 10)
                {
                    checkDigit = 0;
                }

                if (checkDigit != inputCheckDigit)
                {
                    /// Número verificador ingresado inválido
                    return false;
                }
            }
            else
            {
                // Formato de cédula de identidad inválido
                return false;
            }

            return true;
        }
    }
}
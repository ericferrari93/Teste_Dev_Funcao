using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.CustomAttribute
{
    public class DocumentoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                var CPF = value.ToString();

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCPF;
                string digito;
                int soma;
                int resto;

                CPF = CPF.Trim();
                CPF = CPF.Replace(".", "").Replace("-", "");

                if (CPF.Length != 11)
                    return false;

                switch (CPF)
                {
                    case "00000000000":
                        return false;
                    case "11111111111":
                        return false;
                    case "22222222222":
                        return false;
                    case "33333333333":
                        return false;
                    case "44444444444":
                        return false;
                    case "55555555555":
                        return false;
                    case "66666666666":
                        return false;
                    case "77777777777":
                        return false;
                    case "88888888888":
                        return false;
                    case "99999999999":
                        return false;
                }

                tempCPF = CPF.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCPF[i].ToString()) * multiplicador1[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();
                tempCPF = tempCPF + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCPF[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return CPF.EndsWith(digito);
            }
        }
    }
}

namespace Practica02
{
    /**
     * Código extraído del enunciado del ejercicio propuesto 1
     */
    public class ImcViewModel
    {
        public string CalcularEstado(double pesoKg, double alturaMetros)
        {
            if (alturaMetros <= 0 || pesoKg <= 0)
                return "Error";
            double imc = pesoKg / (alturaMetros * alturaMetros);
            if (imc < 18.5) return "Bajo Peso";
            if (imc < 25) return "Peso Normal";
            if (imc < 30) return "Sobrepeso";
            return "Obesidad";
        }
    }
}

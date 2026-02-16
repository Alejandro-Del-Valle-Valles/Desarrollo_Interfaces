namespace TaskMaster.Model
{
    /// <summary>
    /// Clase con la lógica de negocio para calcular presupuestos de proyectos.
    /// </summary>
    internal class CalculadoraPresupuesto
    {

        /// <summary>
        /// Calcula el precio final con el IVA añadido
        /// </summary>
        /// <param name="precio">float precio a añadir el IVA</param>
        /// <returns>float precio con IVA incluido</returns>
        public float CalcularIVA(float precio) => precio + (precio * 0.21f);

        /// <summary>
        /// Calcula el total de costes del proyecto
        /// </summary>
        /// <param name="costes">List de floats con los costes de cada parte del proyecto</param>
        /// <returns>float total de costes más el IVA</returns>
        public float SumarCostes(List<float> costes) => costes.Sum(CalcularIVA);
    }
}

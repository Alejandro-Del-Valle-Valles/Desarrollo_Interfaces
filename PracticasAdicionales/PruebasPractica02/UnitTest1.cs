using Practica02;

namespace PruebasPractica02
{
    public class UnitTest1
    {
        [Fact]
        public void CalcularEstado_DatosNegativos_DevuelveError()
        {
            var vm = new ImcViewModel();
            double peso = -5;
            double altura = -1.8;

            string resultado = vm.CalcularEstado(peso, altura);

            Assert.Equal("Error", resultado);
        }

        [Fact]
        public void CalcularEstado_DatosCero_NoFalla()
        {
            var vm = new ImcViewModel();
            double peso = 0;
            double altura = 0;

            var ex = Record.Exception(() => vm.CalcularEstado(peso, altura));
            Assert.Null(ex);
        }

        [Fact]
        public void CalcularEstado_DatosBajo_DevuelveBajo()
        {
            var vm = new ImcViewModel();
            double peso = 1;
            double altura = 1;

            string resultado = vm.CalcularEstado(peso, altura);

            Assert.Equal("Bajo Peso", resultado);
        }

        [Fact]
        public void CalcularEstado_DatosNormal_DevuelveNormal()
        {
            var vm = new ImcViewModel();
            double peso = 70;
            double altura = 1.70;

            string resultado = vm.CalcularEstado(peso, altura);

            Assert.Equal("Peso Normal", resultado);
        }

        [Fact]
        public void CalcularEstado_DatosSobrepeso_DevuelveSobrepeso()
        {
            var vm = new ImcViewModel();
            double peso = 85;
            double altura = 1.70;

            string resultado = vm.CalcularEstado(peso, altura);

            Assert.Equal("Sobrepeso", resultado);
        }

        [Fact]
        public void CalcularEstado_DatosObesidad_DevuelveObesidad()
        {
            var vm = new ImcViewModel();
            double peso = 100;
            double altura = 1.70;

            string resultado = vm.CalcularEstado(peso, altura);

            Assert.Equal("Obesidad", resultado);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila2.Classes
{
    class Produto
    {
        private string nome;
        private double valor;
        private double peso;
        private double valor_peso;

        public string Nome { get => nome; set => nome = value; }
        public double Valor { get => valor; set => valor = value; }
        public double Peso { get => peso; set => peso = value; }

        public double Valor_Peso
        {
            get
            {

                return valor_peso;
            }
        }

        public void atualizaPesoValor()
        {
            this.valor_peso = this.Valor / this.Peso;
        }
    }
}

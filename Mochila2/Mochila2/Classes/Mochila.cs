using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mochila2.Classes
{
    class MochilaAH
    {
        public MochilaAH()
        {
            this.produtos = new List<Produto>();
        }
        private double capacidade, capacidadeAux, capacidadeAnterior;
        private double pesoAtual = 0;
        private double valorAtual = 0;
        private List<Produto> produtos;        
        public double Capacidade { get => capacidade; set => capacidade = value; }
        public double CapacidadeAux { get => capacidadeAux; set => capacidadeAux = value; }
        public double CapacidadeAnterior { get => capacidadeAnterior; set => capacidadeAnterior = value; }

        internal List<Produto> Produtos { get => produtos; }  

        

        //Atualizo o peso atual da mochila;
        public void AtualizaInfoMochila()
        {
            pesoAtual = 0;
            valorAtual = 0;
            foreach (var item in produtos)
            {
                pesoAtual += item.Peso;
                valorAtual += (item.Valor * item.Peso);
            }
        }

        //Adiciona os produtos na mochila;
        public void AddMochila(Produto produto)
        {
            this.produtos.Add(produto);
            this.capacidadeAux -= produto.Peso;
        }

        //Atualiza o valor total da mochila e seu peso.
        public double getPesoMochila()
        {
            AtualizaInfoMochila();
            return pesoAtual;
        }
        public double getValorMochila()
        {
            AtualizaInfoMochila();
            return valorAtual;
        }

    }
}

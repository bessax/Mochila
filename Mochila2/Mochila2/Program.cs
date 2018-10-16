
using System;
using System.IO;
using System.Collections.Generic;
using Mochila2.Classes;

namespace Mochila2
{
    class Program
    {
        static List<Produto> produtos = new List<Produto>();
        static MochilaAH mochila = new MochilaAH();
        static MochilaAH mochilaRefinada = new MochilaAH();
        static bool refinamento = false;
        static void Main(string[] args)
        {
            char op = '0';            
            Console.Title = "[2ª Versão - Problema da Mochila]";
            
            while (op != '7')
            {
                
                Console.Clear();
                Console.WriteLine("1 - Carregar Lista de Itens/Produtos");
                Console.WriteLine("2 - Cadastrar Capacidade Mochila(KG)");
                Console.WriteLine("3 - Listar Produtos");
                Console.WriteLine("4 - Aplicar Refinamento ? [S] ou [N]");
                Console.WriteLine("5 - Adicionar Itens na Mochila");
                Console.WriteLine("6 - Listar Mochila");
                Console.WriteLine("7 - SAIR");
                Console.Write(">>> Digite Opção: ");
                op = Console.ReadLine()[0];
                switch (op)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine(" >>> Carregar Itens/Produtos <<<");
                        CarregarLista();
                        PressioneQualquerTecla();
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine(" >>> Cadastrar Capacidade da Mochila(KG) <<<");
                        CapacidadeMochila();
                        PressioneQualquerTecla();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine(" >>> Listar Produtos <<<");
                        ListarProdutos();
                        PressioneQualquerTecla();
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("Aplicar Refinamento ??? [S] ou [N] ");
                        string opAux = Console.ReadLine()[0].ToString().ToUpper();
                        if (opAux.Equals("S"))
                        {
                            refinamento = true;
                            Console.WriteLine("\n\n\n >>> Aplicando refinamento ... <<<");
                        }
                        else
                        {
                            refinamento = false;
                        }
                        PressioneQualquerTecla();
                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine(" >>> Adicionar Produtos na Mochila <<<");
                        AdicionarProdutosMochila();
                        PressioneQualquerTecla();
                        break;
                    case '6':
                        Console.Clear();
                        Console.WriteLine(" >>> Listar Itens da Mochila <<<\n\n");
                        Console.WriteLine("Capacidade da Mochila: " + mochila.Capacidade + "\n");
                        ListarMochila();
                        mochila.AtualizaInfoMochila();
                        Console.WriteLine("\n\nPeso total da Mochila (KG):{0:f2}", mochila.getPesoMochila());
                        Console.WriteLine("Valor total da Mochila (R$):{0:f2}", mochila.getValorMochila());
                        Console.WriteLine("\n\n Criado arquivo de saída em : {0}", Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\saida.txt");
                        PressioneQualquerTecla();
                        break;
                    case '7':
                        Console.WriteLine(" Encerrando a aplicação...");
                        PressioneQualquerTecla();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine(" >>> OPÇÃO NÃO IMPLEMENTADA <<<");
                        PressioneQualquerTecla();
                        break;
                }
            }
        }



        #region

        private static void CarregarLista()
        {
            string arquivo = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\lista.txt";

            if (File.Exists(arquivo))
            {
                using (StreamReader rd = File.OpenText(arquivo))
                {

                    while (!rd.EndOfStream)
                    {
                        Produto p = new Produto();
                        string[] arrayAux = rd.ReadLine().Split(";".ToCharArray());
                        p.Nome = arrayAux[0];
                        p.Peso = double.Parse(arrayAux[1]);
                        p.Valor = double.Parse(arrayAux[2]);
                        produtos.Add(p);
                    }

                }
                Console.WriteLine("*** Lista de Itens/Produtos carregada com sucesso!!! ***");
                
            }
            else
            {

                Console.WriteLine("*** Arquivo não existe no lugar especificado. ***");
                
            }


        }

        private static void ListarMochila()
        {
            if (mochila.Produtos.Count <= 0)
            {
                Console.WriteLine("Não há Produto/Item(s) na mochila.");
            }
            else
            {
                //Cria o arquivo no caminho que está o executável
                string saida = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\saida.txt";
                
                try
                {
                    // Se existe o arquivo deleta.
                    if (File.Exists(saida))
                    {
                        File.Delete(saida);
                    }

                    //Cria o arquivo de saída.
                    FileStream fs = new FileStream(saida, FileMode.Create);
                    fs.Close();
                }
                catch (Exception ex )
                {

                    Console.WriteLine("Erro: "+ ex.Message);
                }
               

                foreach (var item in mochila.Produtos)
                {
                    Console.WriteLine("Nome: " + item.Nome);
                    Console.WriteLine("Peso (KG):  {0:f2} ", item.Peso);
                    Console.WriteLine("Valor (R$): {0:f2} ", item.Valor);
                    Console.WriteLine("---------------------------------");
                   
                    using (StreamWriter sw = new StreamWriter(saida,true))
                    {
                        sw.WriteLine("Item: {0} ; Peso: {1:f2} ; Valor: {2:f2}", item.Nome,item.Peso,item.Valor);
                    }

                }
            }

        }

        private static void AdicionarProdutosMochila()
        {
            if (produtos.Count <= 0)
            {
                Console.WriteLine("Não há produtos/Itens cadastrados.");
            }
            if (mochila.Capacidade == 0)
            {
                Console.WriteLine("Não há mochila disponível." + "\n" + "Cadastre primeiro uma mochila para continuar.");
            }
            else
            {
                               
                // Ordena a Lista de maneira descrescente
                OrdenarProdutos();

                
                for (int i = 0; i < produtos.Count; i++)
                {
                    if (produtos[i].Peso <= mochila.CapacidadeAux)
                    {
                        if (!mochila.Produtos.Contains(produtos[i]))
                        {
                            mochila.AddMochila(produtos[i]);
                        }
                        
                    }
                }
                Console.WriteLine("Produto/Item(s) adicionados a mochila com sucesso.");

                if(refinamento == true)
                {
                   //Aplica o [Refinamento - Busca Local]
                    Refinamento();
                }
                
            }
        }

        /// <summary/>
        /// Este método de refinamento tem como objetivo, criar uma nova lista (mochilarefinada)
        /// rodando de novo o algoritmo para buscar uma solução melhor que a anterior, um número
        /// determinado de vezes(100) para não cair em loop infinito.
        /// </summary>
        private static void Refinamento()
        {
            bool acabou = false;
            // O contador foi criado para a solução ser refinada pelo menos 100 vezes
            // para não cair em loop infinito.
            int contador = 0;
            
            while ((!acabou)&&(contador != 100))
            {
                acabou = true;
                contador += 1;
                //foreach (var item in produtos)
                //{
                //    if (item.Peso <= mochilaRefinada.CapacidadeAux)
                //    {
                //        mochilaRefinada.AddMochila(item);
                //    }
                //}

                for (int i = 0; i < produtos.Count; i++)
                {
                    for (int j = 0; j < produtos.Count-1; j++)
                    {
                        if (produtos[j].Peso <= mochilaRefinada.CapacidadeAux)
                        {
                            if (!mochilaRefinada.Produtos.Contains(produtos[j]))
                            {
                                mochilaRefinada.AddMochila(produtos[j]);
                            }
                        }
                    }

                }
            

                if (mochilaRefinada.getValorMochila() > mochila.getValorMochila())
                {
                    mochila = mochilaRefinada;
                    acabou = true;
                }
                else
                {
                    acabou = false;
                    mochilaRefinada = null;
                    mochilaRefinada = new MochilaAH();
                    mochilaRefinada.Capacidade = mochila.CapacidadeAnterior;
                    
                }
            }
        }

        private static void PressioneQualquerTecla()
        {
            Console.WriteLine("...Pressione qualquer tecla para continuar ...");
            Console.ReadKey();
        }
        private static void ListarProdutos()
        {
            if (produtos.Count <= 0)
            {
                Console.WriteLine("Não há produtos/Itens cadastrados.");
            }
            else
            {
                foreach (var item in produtos)
                {
                    Console.WriteLine("Nome: " + item.Nome);
                    Console.WriteLine("Peso (KG): {0:f2} ", item.Peso);
                    Console.WriteLine("Valor (R$): {0:f2} ", item.Valor);
                    Console.WriteLine("---------------------------------");
                   
                }
            }
        }

        private static void CapacidadeMochila()
        {
            Console.Write("Informa a Capacidade da Mochila:");
            if (mochila.Capacidade ==0)
            {
                mochila.Capacidade = double.Parse(Console.ReadLine());
                mochila.CapacidadeAux = mochila.Capacidade;
                mochila.CapacidadeAnterior = mochila.Capacidade;
                mochilaRefinada.Capacidade = mochila.Capacidade;
                mochilaRefinada.CapacidadeAux = mochila.Capacidade;
                mochilaRefinada.CapacidadeAnterior = mochila.Capacidade;
            }
            else
            {
                double capAtual = mochila.Capacidade;                
                mochila.Capacidade = double.Parse(Console.ReadLine());
                mochila.CapacidadeAnterior = mochila.Capacidade;
                mochila.Capacidade -= capAtual;
                mochila.CapacidadeAux = mochila.Capacidade;
                mochilaRefinada.Capacidade = mochila.Capacidade;
                mochilaRefinada.CapacidadeAux = mochila.Capacidade;
               

            }
            
            
        }
        static public void CadastrarProdutos()
        {
            Produto prodAux = new Produto();
            double valor, peso;
            Console.Write("NOME DO PRODUTO : ");
            prodAux.Nome = Console.ReadLine();

            Console.Write("VALOR DO PRODUTO: ");
            valor = double.Parse(Console.ReadLine());
            if (valor == 0)
            {
                Console.WriteLine("Valor não pode ser zerado. Cadastre o produto novamente.");
                return;
            }
            else
            {
                prodAux.Valor = valor;
            }
            Console.Write("PESO DO PRODUTO : ");
            peso = double.Parse(Console.ReadLine());
            if (valor == 0)
            {
                Console.WriteLine("Peso não pode ser zerado. Cadastre o produto novamente.");
                return; 
            }
            else
            {
                prodAux.Peso = peso;
            }
            prodAux.atualizaPesoValor();
            produtos.Add(prodAux);
            

        }
        //Ordenar por Peso_Valor 
        static public void OrdenarProdutos()
        {
            Produto aux;
            for (int i = 0; i < produtos.Count; i++)
            {
                for (int y = 0; y < produtos.Count - 1; y++)
                {
                    if (produtos[y].Valor_Peso < produtos[y + 1].Valor_Peso)
                    {
                        aux = produtos[y + 1];
                        produtos[y + 1] = produtos[y];
                        produtos[y] = aux;
                    }
                }

            }
        }
        #endregion

    }
}

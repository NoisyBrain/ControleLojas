using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace code
{
    class Estoque
    {
        private string codEstoque;
        private string nomeEstoque;
        private string codLoja;

        public string CodLoja
        {
            get => codLoja;
            set => codLoja = value;
        }
        
        public string CodEstoque
        {
            get => codEstoque;
            set => codEstoque = value;
        }

        public string NomeEstoque
        {
            get => nomeEstoque;
            set => nomeEstoque = value;
        }

        public string GerarCodEstoque()
        {
            Random rand = new Random();
            int cod = rand.Next();
            string cdEstoque = NomeEstoque + Convert.ToString(cod);
            return cdEstoque;
        }

        public string SetNomeEstoque()
        {
            Console.WriteLine("Cadastre um nome para o estoque:");
            string nome = Console.ReadLine();
            return nome;
        }

    }
}
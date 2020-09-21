using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace code
{
    class Cliente
    {
        private string codCliente;
        private string nomeCliente;

        public string CodCliente
        {
            get => codCliente;
            set => codCliente = value;
        }

        public string NomeCliente
        {
            get => nomeCliente;
            set => nomeCliente = value;
        }

        public string GerarCodCliente()
        {
            Random rand = new Random();
            int cod = rand.Next();
            string codClienteString = NomeCliente + Convert.ToString(cod);
            return codClienteString;
        }
        public Cliente BuscarCliente()
        {
            string path = "C:/Users/Elaine/Documents/code/bancoDados/Clientes/";
            Console.WriteLine("Digite o cógido do cliente: ");
            string cod = Console.ReadLine();
            string jsonRecuperado = File.ReadAllText(path + $"{cod}");
            Cliente cl2 = JsonSerializer.Deserialize<Cliente>(jsonRecuperado);
            return cl2;
        }


    }
}
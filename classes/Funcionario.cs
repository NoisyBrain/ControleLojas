using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace code
{
    class Funcionario
    {
        private string codFuncionario;
        private string nomeFuncionario;
        private string cargo;

        public string CodFuncionario
        {
            get => codFuncionario;
            set => codFuncionario = value;
        }

        public string NomeFuncionario
        {
            get => nomeFuncionario;
            set => nomeFuncionario = value;
        }

        public string Cargo
        {
            get => cargo;
            set => cargo = value;
        }

        public string GerarCodFuncionario()
        {
            Random rand = new Random();
            int cod = rand.Next();
            string cdFuncionario = NomeFuncionario + Convert.ToString(cod);
            return cdFuncionario;
        }

    }
}
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using code.classesAux;

namespace code
{
    class Program
    {
        static void Main(string[] args)
        {
            Salva util = new Salva();
            string pathBase = @"C:\code";
            DirectoryInfo xi = new DirectoryInfo(pathBase);
            if(!xi.Exists)
            {
                 pathBase = util.DirectoryCreater("C:", "code");
            }
            string pathLojas = util.DirectoryCreater(pathBase, "Lojas");
            string pathCliente = util.DirectoryCreater(pathBase, "Clientes");
            string pathEstoque;
            string pathFuncionarios;
            string pathPedidos;


            bool test0 = true;
            while (test0 == true)
            {
                Console.WriteLine("\n-------------- Bem-vindo ao SAS --------------\nEscolha o número da ação que deseja realizar.");
                Console.WriteLine("\n1 - Login\n2 - Registrar\n0 - SAIR");
                string opcao = Console.ReadLine();
                bool test = true;
                switch (opcao)
                {
                    case "0":
                        test0 = false;
                        Console.WriteLine("Saindo do sistema ...");
                        break;
                    case "1":
                        while (test == true)
                        {
                            Console.WriteLine("\n-------------- LOGIN --------------");
                            Console.WriteLine("Seja bem-vindo ao ambiente de login.\nPor favor, digite o código da loja e a senha");
                            Console.WriteLine("Código da loja: ");
                            string usuario = Console.ReadLine(); 
                            if (usuario != "")
                            {                                
                                //Determinar se a loja existe
                                string path = $"{pathLojas}/{usuario}";
                                DirectoryInfo di = new DirectoryInfo(path);
                                if (di.Exists)
                                {
                                    //Recuperar os dados loja                                        
                                        FileInfo de = new FileInfo($"{path}/arquivoConfiguracao");
                                        if(de.Exists)
                                        {
                                            string jsonRecuperado = File.ReadAllText($"{path}/arquivoConfiguracao");
                                            Loja lj2 = JsonSerializer.Deserialize<Loja>(jsonRecuperado);

                                            bool test5 = true;
                                            while(test5 == true)
                                            {
                                                Console.WriteLine("Senha: ");
                                                string senha = Console.ReadLine();

                                                if(lj2.SenhaLoja == senha)
                                                {
                                                    string pathLoja = $"{pathLojas}/{lj2.CodLoja}";
                                                    pathPedidos = $"{pathLojas}/{lj2.CodLoja}/Pedidos";
                                                    pathEstoque = $"{pathLojas}/{lj2.CodLoja}/Estoque";
                                                    pathFuncionarios = $"{pathLojas}/{lj2.CodLoja}/Funcionarios";
                                                    
                                                    
                                                    
                                                    bool test6 = true;
                                                    while (test6 == true)
                                                    {
                                                        Console.WriteLine($"------------ Bem-Vindo ------------\n{lj2.NomeLoja}, O que gostaria de fazer?");
                                                        Console.WriteLine("\n1 - Cadastrar pedido\n2 - Registrar funcionário\n3 - Inserir produto\n4 - Relatórios\n0 - VOLTAR");
                                                        string op1 = Console.ReadLine();
                                                        Produto prod;
                                                        switch (op1)
                                                        {
                                                            case "0":
                                                                test6 = false;
                                                                break;
                                                            case "1":
                                                                Pedido pd = new Pedido();
                                                                pd.CodPedido = pd.GerarCodPedido();
                                                                pd.Date1 = pd.GetData();
                                                                bool test12 = true;
                                                                while(test12 == true)
                                                                {
                                                                    Console.WriteLine("------------ Cadastrar pedido ------------\n");
                                                                    
                                                                    Console.WriteLine("Digite o código do cliente");
                                                                    string Codusuario = Console.ReadLine();

                                                                    //recuperar dados do cliente
                                                                    string pathUsuario = $"{pathCliente}/{Codusuario}";
                                                                    bool exists = util.VerificarExistencia(pathUsuario);
                                                                    if(exists == true)
                                                                    {
                                                                        string json = File.ReadAllText(pathUsuario);
                                                                
                                                                        Cliente cl2 = JsonSerializer.Deserialize<Cliente>(json);

                                                                        pd.Cliente = Codusuario;

                                                                        Console.WriteLine("Digite o código do funcionario: ");
                                                                        pd.Funcionario = Console.ReadLine();

                                                                        bool test10 = true;
                                                                        bool test9 = true;
                                                                        while(test10 == true)
                                                                        {
                                                                            //Recuperar dados do produto
                                                                            bool test7 = true;
                                                                            while (test7 == true)
                                                                            {
                                                                                int x;
                                                                                Console.WriteLine($"\n{lj2.NomeLoja}, Gostaria de pesquisar o produto como?\n1 - Código do produto\n2 - Nome do produto\n0 - VOLTAR");
                                                                                string op3 = Console.ReadLine();
                                                                                switch (op3)
                                                                                {
                                                                                    case "0":
                                                                                        test7 = false;
                                                                                        test9 = false;
                                                                                        test10 = false;
                                                                                        break;
                                                                                    case "1":
                                                                                        Console.WriteLine($"------------ Pesquisar por código do produto ------------");
                                                                                        Console.WriteLine($"Digite o código do produto: \n");
                                                                                        string cdProduto = Console.ReadLine();
                                                                                        string arquivo = $"{pathEstoque}/{cdProduto}";

                                                                                        if (File.Exists(arquivo))
                                                                                        {
                                                                                            string jsonProduto = File.ReadAllText(arquivo);
                                                                                            Produto pdt2 = JsonSerializer.Deserialize<Produto>(jsonProduto);

                                                                                            PedidoItem itemP = new PedidoItem();
                                                                                            Console.WriteLine("\nDigite a quantidade de produto: ");
                                                                                            x = int.Parse(Console.ReadLine());
                                                                                            itemP.AdicionarProduto(pathEstoque, pdt2, x);
                                                                                            
                                                                                            pd.AdicionarProduto(itemP);

                                                                                            Console.WriteLine("\nDeseja adiciona outro produto? [S/n]");
                                                                                            string resp1 = Console.ReadLine();
                                                                                            if (resp1 == "S" || resp1 == "s")
                                                                                            {
                                                                                                test7 = true;

                                                                                            }else
                                                                                            {
                                                                                                test7 = false;
                                                                                                break;
                                                                                            }
                                                                                        }else
                                                                                        {
                                                                                            Console.WriteLine($"Não existe um arquivo com o código '{cdProduto}'");
                                                                                        }
                                                                                        test7 = false;
                                                                                        break;
                                                                                    case "2":
                                                                                        Console.WriteLine($"------------ Pesquisar por nome do produto ------------");
                                                                                        bool test8 = true;
                                                                                        while(test8 == true)
                                                                                        {   
                                                                                            Console.WriteLine("Digite o nome do produto: \n");
                                                                                            string name = Console.ReadLine();
                                                                                            lj2.BuscarPorNome(pathEstoque, name);
                                                                                            Console.WriteLine("Gostaria de realizar uma nova pesquisa? [S/n]");
                                                                                            string j = Console.ReadLine();
                                                                                            if (j == "S" || j == "s")
                                                                                            {
                                                                                                break;
                                                                                            }else
                                                                                            {
                                                                                                test8 = false;
                                                                                            }
                                                                                        }
                                                                                            break;
                                                                                    default:
                                                                                        Console.WriteLine("Comando inválido!!");
                                                                                        break;
                                                                                }
                                                                            }
                                                                        
                                                                            
                                                                            while(test9 == true)
                                                                            {    
                                                                                pd.ExibirPedido();
                                                                                Console.WriteLine("Finalizar compra? [S/n]");
                                                                                string op4 = Console.ReadLine();
                                                                                if (op4 == "S" || op4 == "s")
                                                                                {
                                                                                    
                                                                                    util.InfoSaver(pathPedidos, pd, pd.CodPedido);
                                                                                    test9 = false;
                                                                                    test10 = false;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.WriteLine("O que gostaria de fazer?\n1 - Descartar pedido\n2 - Remover item\n3 - Adicionar Produto");
                                                                                    string op5 = Console.ReadLine();
                                                                                    switch (op5)
                                                                                    {
                                                                                        case "1":
                                                                                        test9 = false;
                                                                                            break;
                                                                                        case "2":
                                                                                            Console.WriteLine("Digite o código do produto que gostaria de remover: ");

                                                                                            string cdItemPedido = Console.ReadLine();

                                                                                            pd.RemoveItem(cdItemPedido);
                                                                                            
                                                                                            break;
                                                                                        case "3":
                                                                                            test9 = false;
                                                                                            break;
                                                                                        default:
                                                                                            Console.WriteLine("Comando inválido");
                                                                                            break;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        test12 = false;
                                                                    }else
                                                                    {
                                                                        Console.WriteLine("Cliente inexistente!");
                                                                    }
                                                                }
                                                                break;
                                                            case "2":
                                                                Console.WriteLine("------------ Registrar funcionário ------------\n");

                                                                Funcionario fnc = new Funcionario();

                                                                Console.WriteLine("\nDigite o nome do funcionario: ");
                                                                fnc.NomeFuncionario = Console.ReadLine();

                                                                fnc.CodFuncionario = fnc.GerarCodFuncionario();

                                                                Console.WriteLine("\nDigite o cargo do funcionario: ");
                                                                fnc.Cargo = Console.ReadLine();

                                                                util.InfoSaver(pathFuncionarios, fnc, fnc.CodFuncionario);
                                                                break;
                                                            case "3":

                                                                Console.WriteLine("------------ Inserir produto ------------\n");
                                                                Produto pdt = new Produto();

                                                                pdt.NomeProduto = pdt.SetNomeProduto();

                                                                Console.WriteLine("Digite o nome do fabricante d produto: ");
                                                                pdt.NomeFabricante = Console.ReadLine();

                                                                pdt.CodProduto = pdt.GerarCodProduto();

                                                                Console.WriteLine("\nDigite a quantidade (unidades) do produto: ");
                                                                pdt.QntProduto = int.Parse(Console.ReadLine());

                                                                Console.WriteLine("\nDigite a quantidade (unidades) minima do produto: ");
                                                                pdt.QntMinProduto = int.Parse(Console.ReadLine());

                                                                Console.WriteLine("\nDigite o valor de compra do produto: ");
                                                                pdt.ValorCompra = double.Parse(Console.ReadLine());

                                                                Console.WriteLine("\nDigite o valor de venda do produto: ");
                                                                pdt.ValorVenda = double.Parse(Console.ReadLine());
                                                                
                                                                util.InfoSaver($"{pathLoja}/Estoque", pdt, pdt.CodProduto);
                                                                break;
                                                            
                                                            case "4"://Gerar relatório de venda
                                                                Console.WriteLine("------------ Relatórios ------------\n");
                                                                bool test13 = true;
                                                                while (test13 == true)
                                                                {
                                                                    Console.WriteLine("\n1 - Relatórios de vendas\n2 - Relatório estoque\n0 - VOLTAR");
                                                                    string op6 = Console.ReadLine();
                                                                    switch (op6)
                                                                    {
                                                                        case "0":
                                                                            test13 = false;
                                                                            break;
                                                                        case "1":
                                                                            Console.WriteLine("------------ Relatórios de vendas ------------\n");
                                                                            bool test14 = true;
                                                                            while(test14 == true)
                                                                            {
                                                                                Console.WriteLine("\n1 - Relatório por funcionário\n2 - Relatório por mês\n0 - VOLTAR");
                                                                                string op7 = Console.ReadLine();
                                                                                switch (op7)
                                                                                {
                                                                                    case "0":
                                                                                        test14 = false;
                                                                                        break;
                                                                                    case "1":
                                                                                        Console.WriteLine("Digite o Código do funcionario: ");
                                                                                        string cdfnc = Console.ReadLine();
                                                                                        lj2.GetFilesFuncionario(path, cdfnc);
                                                                                        break;
                                                                                    case "2":
                                                                                        Console.WriteLine("Digite o número do mês desejado [Ex.: 01 = janeiro]");
                                                                                        int num = int.Parse(Console.ReadLine());
                                                                                        lj2.GetFilesVendas(pathPedidos, num);
                                                                                        test14 = false;
                                                                                        break;
                                                                                    default:
                                                                                        Console.WriteLine("Comando Invalido");
                                                                                        break;
                                                                                }
                                                                            }
                                                                            break; 
                                                                        case "2": //Exibir o nome do produto as qnt atuais, minimas
                                                                            Console.WriteLine("------------ Relatórios de estoque ------------\n"); 
                                                                            lj2.GetFilesEstoque(pathEstoque);
                                                                            break;
                                                                        default:
                                                                            Console.WriteLine("Comando inválido!");
                                                                            break;
                                                                    }    
                                                                        
                                                                }
                                                                break;
                                                            default:
                                                                Console.WriteLine("Comando invalido!!");
                                                                break;
                                                        }
                                                    }
                                                    test5 = false;
                                                }else
                                                {
                                                    Console.WriteLine("Senha incorreta");
                                                }
                                            }
                                            test = false;
                                        }else
                                        {
                                            Console.WriteLine("Loja invalida!");
                                        }
                                    
                    
                                }else
                                {
                                    Console.WriteLine("\nO usuário não existe!");
                                    break;
                                }

                            }else
                            {
                                bool test2 = true;
                                Console.WriteLine("AVISO: Usuário o senha incorreto.\n");
                                while (test2)
                                {
                                    Console.WriteLine("Gostaria de tentar novamente? [S/n]");
                                    string op = Console.ReadLine();
                                    if(op == "S")
                                    {
                                        test2 = false;
                                        break;
                                    }else if(op == "n")
                                    {
                                        test = false;
                                        test2 = false;
                                    }else
                                    {
                                        Console.WriteLine("Comando inválido");
                                    }  
                                }  
                            }  
                        }
                        break;
                    case "2":
                        bool test3 = true;
                        while (test3 == true)
                        {
                            Console.WriteLine("-----------------------------------\n\nOlá, Gostaria de registrar-se como:\n1 - Loja\n2 - Cliente\n0 - VOLTAR");
                            string op = Console.ReadLine();
                            switch (op)
                            {
                                case "0":
                                    test3 = false;
                                    break;
                                    //REGISTRAR LOJA
                                case "1":
                                    Console.WriteLine("------------ Registrar loja ------------");
                                    Loja lj = new Loja();

                                    Console.WriteLine("\nDigite o nome da loja: ");
                                    lj.NomeLoja = Console.ReadLine();

                                    lj.CodLoja = lj.GerarCodLoja();
                                    lj.SenhaLoja = lj.GerarSenhaLoja();

                                    string NewPathLoja = util.DirectoryCreater(pathLojas, lj.CodLoja);
                                    util.DirectoryCreater(NewPathLoja, "Pedidos");
                                    util.DirectoryCreater(NewPathLoja, "Funcionarios");
                                    util.DirectoryCreater(NewPathLoja, "Estoque");
                                    
                                    //SALVAR LOJA
                                    Console.WriteLine("\n-------------------------\nDeseja salvar? [S/n] ");
                                    string op2 = Console.ReadLine();
                                    if (op2 == "S" || op2 == "s")
                                    {
                                        util.InfoSaver(NewPathLoja, lj, "/arquivoConfiguracao");
                                    }                                   
                                    
                                    test3 = false;

                                    break;
                                case "2":
                                    Console.WriteLine("------------ Registrar cliente ------------");
                                    bool test4 = true;
                                    while (test4 == true)
                                    {
                                        Cliente cl = new Cliente();
                                        Console.WriteLine("Digite o nome do cliente");
                                        cl.NomeCliente = Console.ReadLine();

                                        cl.CodCliente = cl.GerarCodCliente();

                                       

                                        Console.WriteLine($"\nDeseja salvar? [S/n]\n---> Código do cliente: {cl.CodCliente}\n---> Nome: {cl.NomeCliente}");
                                        string resp = Console.ReadLine();
                                        switch (resp)
                                        {
                                            case "S":
                                                util.InfoSaver(pathCliente, cl, $"/{cl.CodCliente}");
                                                
                                                Console.WriteLine("\nDeseja cadastrar outro cliente?[S/n]");
                                                string cont = Console.ReadLine();
                                                if (cont == "S")
                                                {
                                                    break;
                                                }else if(cont == "n")
                                                {
                                                    test4 = false;
                                                    break;
                                                }else
                                                {
                                                    Console.WriteLine("\nComando inválido");
                                                }
                                                break;
                                            case "n":
                                            test4 = false;
                                                break;
                                            default:
                                                Console.WriteLine("Comando inválido");
                                                break;
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine("\nComando inválido!!");
                                    break;                           
                            }                            
                        }
                        break;
                    default:
                        break;
                }   
            };

        }
    }
}

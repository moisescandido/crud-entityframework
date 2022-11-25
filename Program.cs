using _DbContext;
using ClientesModel;

ClienteContext db = new ClienteContext();
Cliente cliente = new Cliente();

bool ValidarCpf(string cpf)
{
    ulong intounao;

    if (cpf.Length != 11 || ulong.TryParse(cpf, out intounao) == false)
    {
        return false;
    }
    else
    {
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        char[] _cpf = cpf.ToCharArray();

        int digito1 = int.Parse(_cpf[9].ToString());
        int digito2 = int.Parse(_cpf[10].ToString());

        int soma1 = 0;
        int soma2 = 0;
        int resto1, resto2;

        // VERIFICAR DIGITO 1
        for (int i = 0; i <= 8; i++)
        {
            soma1 += int.Parse(_cpf[i].ToString()) * multiplicador1[i];
        }

        resto1 = (soma1 * 10) % 11;

        if (resto1 == 10)
        {
            resto1 = 0;
        }

        //VERIFICAR DIGITO 2
        for (int i = 0; i <= 9; i++)
        {
            soma2 += int.Parse(_cpf[i].ToString()) * multiplicador2[i];
        }

        resto2 = (soma2 * 10) % 11;

        if (resto1 == digito1 && resto2 == digito2)
        {
            return true;
        }
        else
            return false;
    }
}
bool ValidarTelefone(string telefone)
{
    ulong intounao;
    if (telefone.Length != 11 || ulong.TryParse(telefone, out intounao) == false)
    {
        return false;
    }
    else
    {
        return true;
    }

}
bool ValidarEndereco(string endereço)
{
    if (endereço.Length > 50)
    {
        return false;
    }
    else
    {
        return true;
    }
}
bool ValidarNome(string nome)
{
    if (nome.Length > 50 || nome.Length == 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}
void Create()
{
    Console.Write("Digite seu nome: ");
    cliente.Nome = Console.ReadLine();


    while (ValidarNome(cliente.Nome) == false)
    {
        Console.WriteLine("Número de caracteres excede valor permitido");
        Console.Write("Digite um nome valido: ");
        cliente.Telefone = Console.ReadLine();
    }

    Console.Write("Digite seu CPF (APENAS NÚMEROS): ");
    cliente.Cpf = Console.ReadLine();

    while (ValidarCpf(cliente.Cpf) == false)
    {
        Console.Write("Digite um CPF verdadeiro: ");
        cliente.Cpf = Console.ReadLine();
    }

    Console.Write("Digite seu endereço: ");
    cliente.Endereco = Console.ReadLine();

    while (ValidarEndereco(cliente.Endereco) == false)
    {
        Console.WriteLine("Número de caracteres excede valor permitido: ");
        Console.Write("Digite um endereço valido: ");
        cliente.Endereco = Console.ReadLine();
    }

    Console.Write("Digite seu telefone: ");
    cliente.Telefone = Console.ReadLine();

    while (ValidarTelefone(cliente.Telefone) == false)
    {
        Console.Write("Digite um telefone verdadeiro: ");
        cliente.Telefone = Console.ReadLine();
    }

    Console.WriteLine("Adicionando cliente...");

    db.Add(cliente);
    db.SaveChanges();

    Console.WriteLine("Cliente adicionado...");
}

void Read()
{
    Console.Write("Digite ID do cliente para procurar:");
    cliente.Id = int.Parse(Console.ReadLine());
    var cliente_read = db.Clientes.Find(cliente.Id);

    try
    {
        Console.WriteLine();
        Console.WriteLine("ID: {0}", cliente_read.Id);
        Console.WriteLine("Nome: {0}", cliente_read.Nome);
        Console.WriteLine("CPF: {0}", cliente_read.Cpf);
        Console.WriteLine("Endereço: {0}", cliente_read.Endereco);
        Console.WriteLine("Telefone: {0}", cliente_read.Telefone);
        Console.WriteLine();
    }
    catch
    {
        Console.WriteLine("CLIENTE NÃO ENCONTRADO!");
    }
    Console.Write("Deseja pesquisar mais um? (S/N): ");
    string resposta = Console.ReadLine();

    while (!(resposta == "S" || resposta == "N"))
    {
        Console.Write("S para SIM, N para NÃO: ");
        resposta = Console.ReadLine();
    }

    if (resposta == "S")
    {
        Read();
    }
    else
    {
        Menu();
    }
}

void Update()
{
    Console.Write("Digite ID do cliente para alterar: ");
    cliente.Id = int.Parse(Console.ReadLine());

    var cliente_read = db.Clientes.FirstOrDefault(x => x.Id == cliente.Id);

    string nomeAntigo = cliente_read.Nome;
    string cpfAntigo = cliente_read.Cpf;
    string enderecoAntigo = cliente_read.Endereco;
    string telefoneAntigo = cliente_read.Telefone;

    Console.WriteLine("Digite uma das opções para editar dados: ");
    Console.WriteLine("1) Nome");
    Console.WriteLine("2) CPF");
    Console.WriteLine("3) Endereço");
    Console.WriteLine("4) Telefone");
    Console.Write("Digite uma opção: ");
    ushort resposta = ushort.Parse(Console.ReadLine());

    if (resposta == 1)
    {
        Console.Write("Digite um novo nome para {0}: ", cliente_read.Nome);
        cliente_read.Nome = Console.ReadLine();

        while (!(ValidarNome(cliente_read.Nome) == true))
        {
            Console.Write("Nome inválido, digite novamente: ");
            cliente_read.Nome = Console.ReadLine();
        }
        db.Clientes.Update(cliente_read);
        db.SaveChanges();

        Console.WriteLine("Nome antigo : {0}", nomeAntigo);
        Console.WriteLine("Nome novo: {0}", cliente_read.Nome);

        Console.WriteLine();

        Console.Write("Deseja editar mais um? (S/N): ");
        string resposta2 = Console.ReadLine();

        while (!(resposta2 == "S" || resposta2 == "N"))
        {
            Console.Write("S para SIM, N para NÃO: ");
            resposta2 = Console.ReadLine();
        }

        if (resposta2 == "S")
        {
            Update();
        }
        else
        {
            Menu();
        }
    }

    if (resposta == 2)
    {
        Console.Write("Digite um novo CPF para {0}: ", cliente_read.Nome);
        cliente_read.Cpf = Console.ReadLine();
        while (!(ValidarCpf(cliente_read.Cpf) == true))
        {
            Console.WriteLine("CPF inválido, digite novamente: ");
            cliente_read.Cpf = Console.ReadLine();
        }
        db.Clientes.Update(cliente_read);
        db.SaveChanges();

        Console.WriteLine("CPF antigo: {0}", cpfAntigo);
        Console.WriteLine("CPF novo: {0}", cliente_read.Cpf);

        Console.WriteLine("CPF ALTERADO!");

        Console.Write("Deseja editar mais um? (S/N): ");
        string resposta2 = Console.ReadLine();

        while (!(resposta2 == "S" || resposta2 == "N"))
        {
            Console.Write("S para SIM, N para NÃO: ");
            resposta2 = Console.ReadLine();
        }

        if (resposta2 == "S")
        {
            Update();
        }
        else
        {
            Menu();
        }

    }

    if (resposta == 3)
    {
        Console.Write("Digite um novo endereço para {0}: ", cliente_read.Nome);
        cliente_read.Endereco = Console.ReadLine();
        while (!(ValidarEndereco(cliente_read.Endereco) == true))
        {
            Console.WriteLine("Endereço inválido, digite novamente: ");
            cliente_read.Endereco = Console.ReadLine();
        }
        db.Clientes.Update(cliente_read);
        db.SaveChanges();

        Console.WriteLine("CPF antigo: {0}", enderecoAntigo);
        Console.WriteLine("CPF novo: {0}", cliente_read.Endereco);

        Console.WriteLine("ENDEREÇO ALTERADO!");

        Console.Write("Deseja editar mais um? (S/N): ");
        string resposta2 = Console.ReadLine();

        while (!(resposta2 == "S" || resposta2 == "N"))
        {
            Console.Write("S para SIM, N para NÃO: ");
            resposta2 = Console.ReadLine();
        }

        if (resposta2 == "S")
        {
            Update();
        }
        else
        {
            Menu();
        }

    }
    if (resposta == 4)
    {
        Console.Write("Digite um novo telefone para {0}: ", cliente_read.Nome);
        cliente_read.Telefone = Console.ReadLine();

        while (!(ValidarTelefone(cliente_read.Telefone) == true))
        {
            Console.WriteLine("Endereço inválido, digite novamente: ");
            cliente_read.Telefone = Console.ReadLine();
        }
        db.Clientes.Update(cliente_read);
        db.SaveChanges();

        Console.WriteLine("CPF antigo: {0}", telefoneAntigo);
        Console.WriteLine("CPF novo: {0}", cliente_read.Telefone);

        Console.WriteLine("ENDEREÇO ALTERADO!");

        Console.Write("Deseja editar mais um? (S/N): ");
        string resposta2 = Console.ReadLine();

        while (!(resposta2 == "S" || resposta2 == "N"))
        {
            Console.Write("S para SIM, N para NÃO: ");
            resposta2 = Console.ReadLine();
        }

        if (resposta2 == "S")
        {
            Update();
        }
        else
        {
            Menu();
        }
    }
}

void Delete()
{
    Console.Write("Digite ID do cliente a ser excluido:");
    cliente.Id = int.Parse(Console.ReadLine());
    var cliente_read = db.Clientes.Find(cliente.Id);

    try
    {
        Console.Write("Deseja mesmo excluir cadastro de {0}? (S/N): ", cliente_read.Nome);
        string resposta = Console.ReadLine();

        while (!(resposta == "S" || resposta == "N"))
        {
            Console.Write("S para SIM, N para NÃO: ", cliente_read.Nome);
            resposta = Console.ReadLine();
        }

        if (resposta == "S")
        {
            Console.WriteLine("Excluindo...");
            db.Clientes.Remove(cliente_read);
            db.SaveChanges();
            Console.WriteLine("Cadastro de {0} foi excluido...", cliente_read.Nome);
        }
        else
        {
            Menu();
        }
    }
    catch
    {
        Console.WriteLine("CLIENTE NÃO ENCONTRADO!");
    }
}

void Menu()
{
    Console.Clear();
    Console.WriteLine("1) CRIAR");
    Console.WriteLine("2) LER");
    Console.WriteLine("3) EDITAR");
    Console.WriteLine("4) DELETAR");
    Console.WriteLine("0) SAIR");
    Console.Write("DIGITE UMA OPÇÃO: ");
    ushort opcao = ushort.Parse(Console.ReadLine());

    while (!(opcao != 1 || opcao != 2 || opcao != 3 || opcao != 4 || opcao != 0))
    {
        Console.Write("DIGITE UMA OPÇÃO CORRETA");
        opcao = ushort.Parse(Console.ReadLine());
    }

    if (opcao == 1)
    {
        Create();
    }
    if (opcao == 2)
    {
        Read();
    }
    if (opcao == 3)
    {
        Update();
    }
    if (opcao == 4)
    {
        Delete();
    }
    if (opcao == 0)
    {
        Environment.Exit(0);
    }
}

Menu();
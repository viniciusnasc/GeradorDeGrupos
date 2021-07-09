using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GeradorDeGrupos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao gerador de grupos aleatórios!");
            Console.Write("\nPrimeiro, digite o número de pessoas que participarão: ");
            string qtd_Pessoas = Console.ReadLine();
            bool validQtd_Pessoas = decimal.TryParse(qtd_Pessoas, out decimal qtdPessoas);

            while(validQtd_Pessoas == false)
            {
                Console.WriteLine("Só aceito números!\nDigite o número de pessoas que participarão: ");
                qtd_Pessoas = Console.ReadLine();
                validQtd_Pessoas = decimal.TryParse(qtd_Pessoas, out qtdPessoas);
            }

            List<string> nomes = new();
            Regex regex = new("[ ]{2,}");

            for (int i = 0; i < qtdPessoas; i++)
            {
                Console.Write($"Digite o {i+1}º nome: ");
                string incluirNome = Console.ReadLine().Trim();
                incluirNome = regex.Replace(incluirNome, " ");

                while(string.IsNullOrWhiteSpace(incluirNome))
                {
                    Console.WriteLine("Não aceito espaço em branco ou valor nulo!");
                    Console.WriteLine("Digite um nome valido!");
                    Console.Write($"Digite o {i + 1}º nome: ");
                    incluirNome = Console.ReadLine().Trim();
                    incluirNome = regex.Replace(incluirNome, " ");
                }

                string validadorNome = nomes.Find(x => x == incluirNome);
                while (validadorNome != null)
                {
                    Console.WriteLine("Nome já digitado, inclua outro nome!");
                    Console.WriteLine("Obs.: Caso existam nomes iguais, pode utilizar letra maiuscula para diferenciar");
                    Console.Write($"Digite o {i + 1}º nome: ");
                    incluirNome = Console.ReadLine().Trim();
                    incluirNome = regex.Replace(incluirNome, " ");
                    validadorNome = nomes.Find(x => x == incluirNome);
                }
                nomes.Add(incluirNome);
            }

            Console.WriteLine("\nQuantos serão os grupos a serem formados? ");
            string num_Grupos = Console.ReadLine();
            bool validNum_Grupos = int.TryParse(num_Grupos, out int numGrupos);

            while (validNum_Grupos == false)
            {
                Console.WriteLine("Só aceito números!\nDigite o número de grupos a serem formados: ");
                num_Grupos = Console.ReadLine();
                validNum_Grupos = int.TryParse(num_Grupos, out numGrupos);
            }

            // Arredondamento para cima
            int numIntegrantes = (int)Math.Ceiling(qtdPessoas / numGrupos);

            Console.Clear();

            // Nesse ponto é bagunçado a ordem de nomes
            Random rnd = new();
            List<string> listaNomes = new();

            for (int i = 0; i < qtdPessoas; i++)
            {
                int ind = rnd.Next((int)qtdPessoas);
                string proc = listaNomes.Find(x => x == nomes[ind]);
                
                while (proc != null)
                {
                    ind = rnd.Next((int)qtdPessoas);
                    proc = listaNomes.Find(x => x == nomes[ind]);
                }

                listaNomes.Add(nomes[ind]);
            }

            // Hora de formar os grupos
            for (int i = 0; i < numGrupos; i++)
            {
                Console.WriteLine($"Grupo {i+1}: ");

                for (int x = 0; x <= listaNomes.Count; x++)
                {
                    if(i + numGrupos * x < listaNomes.Count)
                    Console.WriteLine(listaNomes[i+numGrupos*x]);
                }
                Console.WriteLine();
            }
        }
    }
}

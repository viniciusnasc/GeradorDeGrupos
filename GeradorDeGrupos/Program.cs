using System;
using System.Collections.Generic;

namespace GeradorDeGrupos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao gerador de grupos aleatórios!");
            Console.Write("\nPrimeiro, digite o número de pessoas que participarão: ");
            decimal qtdPessoas = decimal.Parse(Console.ReadLine());

            List<string> nomes = new();

            for (int i = 0; i < qtdPessoas; i++)
            {
                Console.Write($"Digite o {i+1}º nome: ");
                string incluirNome = Console.ReadLine();
                string validadorNome = nomes.Find(x => x == incluirNome);
                while (validadorNome != null)
                {
                    Console.WriteLine("Nome já digitado, inclua outro nome!");
                    Console.Write($"Digite o {i + 1}º nome: ");
                    incluirNome = Console.ReadLine();
                    validadorNome = nomes.Find(x => x == incluirNome);
                }
                nomes.Add(incluirNome);
            }

            Console.WriteLine("\nQuantos serão os grupos a serem formados? ");
            int numGrupos = int.Parse(Console.ReadLine());

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

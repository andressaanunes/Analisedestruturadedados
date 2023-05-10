using System;

namespace TP04Q02
{
    class Celula
    {
        public Jogadores elemento;
        public Celula prox;

        public Celula()
        {
        }

        public Celula(Jogadores jogador)
        {
            this.elemento = jogador;
            this.prox = null;
        }
    }

    class Pilha
    {
        public Celula primeiro;
        public Celula ultimo;

        public Pilha()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }

        public void inserirFim(Jogadores jogador)
        {
            ultimo.prox = new Celula(jogador);
            ultimo = ultimo.prox;
        }
        public Jogadores removerFim()
        {
            if (primeiro == ultimo)
            {
                throw new Exception("Erro ao remover (vazia)!");
            }
            // Caminhar ate a penultima celula:
            Celula i;
            for (i = primeiro; i.prox != ultimo; i = i.prox) ;

            Jogadores resp = ultimo.elemento;
            ultimo = i;
            i = ultimo.prox = null;

            return resp;
        }

        public void mostrar()
        {
            for (Celula i = primeiro.prox; i != null; i = i.prox)
            {
                i.elemento.imprimir();
            }
        }
        public int tamanhoLista()
        {
            int tamanho = 0;
            for (Celula i = primeiro; i != ultimo; i = i.prox, tamanho++) ;

            return tamanho;
        }
    }

    class Jogadores
    {
        public String nome;
        public String foto;
        public DateTime nascimento;
        public int id;
        public int[] listas;

        public Jogadores()
        {
            this.nome = "";
            this.foto = "";
            this.nascimento = new DateTime(1111, 1, 1);
            this.id = 0;
            this.listas = new int[] { 123, 123, 456, 789 };
        }
        public void imprimir()
        {
            Console.Write(this.id + " ");
            Console.Write(this.nome + " ");
            Console.Write(this.nascimento.ToString("d/MM/yyyy") + " ");
            Console.Write(this.foto + " ");

            Console.Write("(");
            for (int i = 0; i < this.listas.Length; i++)
            {
                Console.Write(this.listas[i]);
                if (i < this.listas.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write(")");
            Console.WriteLine();
        }
        void ler(String linha)
        {
            String[] linhaSub = linha.Split(',');
            String[] data = linhaSub[3].Split('/');
            this.nome = linhaSub[1];
            this.foto = linhaSub[2];
            this.nascimento = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]));
            this.id = int.Parse(linhaSub[5]);

            this.listas = new int[linhaSub.Length - 6];
            for (int i = 6, j = 0; i < linhaSub.Length; i++, j++)
            {
                this.listas[j] = int.Parse(linhaSub[i].Replace("[", "").Replace("]", "").Replace("\"", ""));
            }
        }
        static void Main(string[] args)
        {
            String linha = Console.ReadLine();

            Jogadores jogador = new Jogadores();
            Pilha pilha = new Pilha();

            int i = 0, tamComandos = 0;

            while (linha != "FIM")
            {
                jogador = new Jogadores();
                jogador.ler(linha);
                pilha.inserirFim(jogador);
                linha = Console.ReadLine();
            }
            tamComandos = int.Parse(Console.ReadLine());

            linha = Console.ReadLine();

            String[] comando = { };

           for (i = 0; i < tamComandos; i++)
            {
                comando = linha.Split(',');
                comando = comando[0].Split(' ');
                jogador = new Jogadores();
                switch (comando[0])
                {
                    case "I":
                        jogador.ler(linha.Substring(2));
                        pilha.inserirFim(jogador);
                        break;
                    case "R":
                        pilha.removerFim();
                        break;
                    default:
                        break;
                }
                linha = Console.ReadLine();
            }
            pilha.mostrar();




        }
    }
}

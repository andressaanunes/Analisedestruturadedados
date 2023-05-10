using System;

namespace TP04Q01
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

    class Lista
    {
        public Celula primeiro;
        public Celula ultimo;

        public Lista()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }

        public void inserirInicio(Jogadores jogador)
        {
            Celula tmp = new Celula(jogador);
            tmp.prox = primeiro.prox;
            primeiro.prox = tmp;
            if (primeiro == ultimo)
            {
                ultimo = tmp;
            }
            tmp = null;
        }
        public void inserirFim(Jogadores jogador)
        {
            ultimo.prox = new Celula(jogador);
            ultimo = ultimo.prox;
        }

        public void inserir(Jogadores x, int pos)
        {

            int tamanho = tamanhoLista();

            if (pos < 0 || pos > tamanho)
            {
                throw new Exception("Erro ao inserir posicao (" + pos + " / tamanho = " + tamanho + ") invalida!");
            }
            else if (pos == 0)
            {
                inserirInicio(x);
            }
            else if (pos == tamanho)
            {
                inserirFim(x);
            }
            else
            {
                // Caminhar ate a posicao anterior a insercao
                Celula i = primeiro;
                for (int j = 0; j < pos; j++, i = i.prox) ;

                Celula tmp = new Celula(x);
                tmp.prox = i.prox;
                i.prox = tmp;
                tmp = i = null;
            }
        }

        public Jogadores remover(int pos)
        {
            Jogadores resp;
            int tamanho = tamanhoLista();

            if (primeiro == ultimo)
            {
                throw new Exception("Erro ao remover (vazia)!");

            }
            else if (pos < 0 || pos >= tamanho)
            {
                throw new Exception("Erro ao remover (posicao " + pos + " / " + tamanho + " invalida!");
            }
            else if (pos == 0)
            {
                resp = removerInicio();
            }
            else if (pos == tamanho - 1)
            {
                resp = removerFim();
            }
            else
            {
                // Caminhar ate a posicao anterior a insercao
                Celula i = primeiro;
                for (int j = 0; j < pos; j++, i = i.prox) ;

                Celula tmp = i.prox;
                resp = tmp.elemento;
                i.prox = tmp.prox;
                tmp.prox = null;
                i = tmp = null;
            }
            return resp;
        }





        public Jogadores removerInicio()
        {
            if (primeiro == ultimo)
            {
                throw new Exception("Erro ao remover (vazia)!");
            }
            Celula tmp = primeiro;
            primeiro = primeiro.prox;
            Jogadores resp = primeiro.elemento;
            tmp.prox = null;
            tmp = null;
            return resp;
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
            Lista lista = new Lista();

            int i = 0, tamComandos = 0;

            while (linha != "FIM")
            {
                jogador = new Jogadores();
                jogador.ler(linha);
                lista.inserirFim(jogador);
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
                    case "II":
                        jogador.ler(linha.Substring(2));
                        lista.inserirInicio(jogador);
                        break;
                    case "I*":
                        if (linha[4] != ' ')
                        {
                            jogador.ler(linha.Substring(6));
                        }
                        else
                        {
                            jogador.ler(linha.Substring(5));
                        }
                        lista.inserir(jogador, int.Parse(comando[1]));
                        break;
                    case "IF":
                        jogador.ler(linha.Substring(2));
                        lista.inserirFim(jogador);
                        break;
                    case "RI":
                        lista.removerInicio();
                        break;
                    case "R*":
                        lista.remover(int.Parse(comando[1]));
                        break;
                    case "RF":
                        lista.removerFim();
                        break;
                    default:
                        break;
                }
                linha = Console.ReadLine();
            }
            lista.mostrar();




        }
    }
}

using xadrez_console.tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {

                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(2, 5));
                tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(0, 3));

                tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(7, 2));
                tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(6, 5));
                tab.colocarPeca(new Rei(Cor.Branca, tab), new Posicao(7, 3));



                Tela.imprimirTabuleiro(tab);
              
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

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

                PosicaoXadrez pos = new PosicaoXadrez('C', 7);

                Console.WriteLine(pos);

                Console.WriteLine(pos.toPosicao());
              
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

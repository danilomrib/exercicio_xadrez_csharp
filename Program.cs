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
                PartidaDeXadrez partida = new PartidaDeXadrez();


                while (!partida.terminada)
                {
                    try
                    {

                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaodeOrigem(origem); // Aula 224, inclusão da ação de validação da origem da jogada

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis(); // vai guardar nesta matriz a lista de movimentos possíveis para a peça

                        Console.WriteLine();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis); // vai imprimir na tela um diferencial com os movimentos possiveis para a peça
                                               
                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaodeDestino(origem, destino); // Aula 225, inclusão da ação da validaçao do destino da jogada.

                        partida.realizaJogada(origem, destino); // Aula 223, alterado para realizaJogada.
                    }

                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
                Console.Clear(); 
                Tela.imprimirPartida(partida); // Aula 231: quando encerrar a partida, imprime o texto final.
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

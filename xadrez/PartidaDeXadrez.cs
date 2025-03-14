using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();
        }

        public void executaMovimento (Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('A', 1).toPosicao());
            tab.colocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('H', 1).toPosicao());
            tab.colocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('D', 1).toPosicao());

            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('A', 8).toPosicao());
            tab.colocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('H', 8).toPosicao());
            tab.colocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('D', 8).toPosicao());


            /*tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(2, 5));
            tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(0, 3));

            tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(7, 2));
            tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(6, 5));
            tab.colocarPeca(new Rei(Cor.Branca, tab), new Posicao(7, 3));*/

        }

    }
}

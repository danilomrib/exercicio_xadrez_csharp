using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino) // Método da aula 223, para realizar a mudança correta do jogador, conforme os turnos.
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaodeOrigem(Posicao pos) // Aula 224: Inclusão das mensagens de exceção para a validação da origem.
        {
            if (tab.peca(pos) == null) // caso não exista peça na posição escolhida.
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).Cor) // caso a peça escolhida não for a do jogador atual.
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) // caso não haja movimentos para a peça que foi escolhida.
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaodeDestino (Posicao origem, Posicao destino) // Aula 225: Inclusão das mensagens de exceção para a validação do destino.
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Movimento inválido!");
            }
        }

        public void mudaJogador() // Método da aula 223, para realizar a mudança correta do jogador, conforme os turnos.
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;

            } else
            {
                jogadorAtual = Cor.Branca;
            }

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

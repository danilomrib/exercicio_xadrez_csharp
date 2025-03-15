using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas; //Aula 226: Inclusão da coleção
        private HashSet<Peca> capturadas; //Aula 226: Inclusão da coleção

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) // Função que permite alimentar as peças capturadas.
            {
                capturadas.Add(pecaCapturada);
            }
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

        public void validarPosicaodeDestino(Posicao origem, Posicao destino) // Aula 225: Inclusão das mensagens de exceção para a validação do destino.
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

            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(pecasCapturadas(cor));

            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //Aula 226: inclusão de método novo para inclusão de peças
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca); //função que inclui as peças na coleção.
        }

        private void ColocarPecas()
        {
            colocarNovaPeca('A', 1, new Torre(Cor.Branca, tab)); //Nova peça sendo incluida.
            colocarNovaPeca('H', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('D', 1, new Rei(Cor.Branca, tab));

            colocarNovaPeca('A', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('H', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('D', 8, new Rei(Cor.Preta, tab));
        }

    }
}

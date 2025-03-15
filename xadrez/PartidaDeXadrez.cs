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
        public  bool  xeque { get; private set; }

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

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) // Função que permite alimentar as peças capturadas.
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) //Aula 228: Método que desfaz a ação da jogada
        {
            Peca p = tab.retirarPeca(destino); // define a peça do destino a ser devolvida
            p.decrementarQtdMovimentos(); // retira o movimento
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino); // retorna a peça ao tabuleiro
                capturadas.Remove(pecaCapturada); // retorna a peça da lista de capturados
            }

            tab.colocarPeca(p, origem); // retorna a peça de origem a sua posição.

        }

        public void realizaJogada(Posicao origem, Posicao destino) // Método da aula 223, para realizar a mudança correta do jogador, conforme os turnos.
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) // Aula 228: função includa para validar se está em xeque
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) // Aula 228: Valida se quem está xeque é o adversario
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }


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

        public bool estaEmXeque (Cor cor) // Método que avalia se o rei está em xeque
        {
            Peca R = rei(cor); // define que a regra vale apenas para o rei

            if (R == null) // condição que cobre a obrigatoriedade de ter um rei no tabuleiro no inicio da partida
            {
                throw new TabuleiroException("Não tem rei da cor" + cor + "no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) //Progura todos os movimentos de cada peça em comparação ao rei
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]){
                    return true;
                }
            }

            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) //Aula 226: inclusão de método novo para inclusão de peças
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca); //função que inclui as peças na coleção.
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
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

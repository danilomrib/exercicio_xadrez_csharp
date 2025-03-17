﻿using System.Data.Common;
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
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
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

            //Jogada Especial Roque Pequeno Aula 235
            
            if (p is Rei & destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            //Roque Grande
            
            if (p is Rei & destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
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


             //desfaz roque pequeno
            if (p is Rei & destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }
            
            //desfaz roque grande
            if (p is Rei & destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }

            

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

            if (testeXequeMate(adversaria(jogadorAtual))) // Aula 230: testa se a jogada realizada deixa o jogador adiversário em xeque mate
                {
                terminada = true; //aqui, caso sim, encerra a partida
            }
            else
            {


                turno++;
                mudaJogador();
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino))
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

        public bool estaEmXeque(Cor cor) // Método que avalia se o rei está em xeque
        {
            Peca R = rei(cor); // define que a regra vale apenas para o rei

            if (R == null) // condição que cobre a obrigatoriedade de ter um rei no tabuleiro no inicio da partida
            {
                throw new TabuleiroException("Não tem rei da cor" + cor + "no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) //Progura todos os movimentos de cada peça em comparação ao rei
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool testeXequeMate(Cor cor) // Aula 230: implementando o xequemate
        {
            if (!estaEmXeque(cor)) // apenas validando se a peça realmente está em xeque
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) // vai passar uma lista em cada peça validando possiveis jogadas
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(x.Posicao, destino);
                            bool testaXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada); // vai testar se o movimento possivel retira o rei do xeque
                            if (!testaXeque) // neste teste, caso retire o xeque e permite a jogada
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true; // caso xeque seja real, ele retorna true para confirmar a partida em xeque mate.
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

        /*     private void ColocarPecas()
             {
                 colocarNovaPeca('A', 1, new Torre(Cor.Branca, tab)); //Nova peça sendo incluida.
                 colocarNovaPeca('B', 1, new Cavalo(Cor.Branca, tab));
                 colocarNovaPeca('C', 1, new Bispo(Cor.Branca, tab));
                 colocarNovaPeca('E', 1, new Rei(Cor.Branca, tab, this));
                 colocarNovaPeca('D', 1, new Dama(Cor.Branca, tab));
                 colocarNovaPeca('F', 1, new Bispo(Cor.Branca, tab));
                 colocarNovaPeca('G', 1, new Cavalo(Cor.Branca, tab));
                 colocarNovaPeca('H', 1, new Torre(Cor.Branca, tab));
                 colocarNovaPeca('A', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('B', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('C', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('D', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('E', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('F', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('G', 2, new Peao(Cor.Branca, tab));
                 colocarNovaPeca('H', 2, new Peao(Cor.Branca, tab));



                 colocarNovaPeca('A', 8, new Torre(Cor.Preta, tab));
                 colocarNovaPeca('B', 8, new Cavalo(Cor.Preta, tab));
                 colocarNovaPeca('C', 8, new Bispo(Cor.Preta, tab));
                 colocarNovaPeca('D', 8, new Rei(Cor.Preta, tab, this));
                 colocarNovaPeca('E', 8, new Dama(Cor.Preta, tab));
                 colocarNovaPeca('F', 8, new Bispo(Cor.Preta, tab));
                 colocarNovaPeca('G', 8, new Cavalo(Cor.Preta, tab));
                 colocarNovaPeca('H', 8, new Torre(Cor.Preta, tab));
                 colocarNovaPeca('A', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('B', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('C', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('D', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('E', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('F', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('G', 7, new Peao(Cor.Preta, tab));
                 colocarNovaPeca('H', 7, new Peao(Cor.Preta, tab));


             }*/

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
        }

    }
}

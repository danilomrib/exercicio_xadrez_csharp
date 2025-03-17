using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Rei : Peca
    {
        private PartidaDeXadrez partida; // Aula 234 Inclusão do Roque

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;  // aqui definiu que a peça pode se mover quando a cor for diferente do atual e se ele estiver livre

        }

        private bool testeTorreParaRoque (Posicao pos) // Aula 234: Função para testar se é a torre 
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas]; //fez uma matriz temporária para defiinir os movimentos

            Posicao pos = new Posicao(0, 0);

            // definição de movimentos

            //acima

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna); // definção do movimento acima
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }  // se ele for uma posição valida (dentro dos limites do tabuleiro) e puder se mover (espaço livre ou com peça adversária) ele irá se mover.

            //nordeste

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //direita

            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //sudeste

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //abaixo

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //sudoeste

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //esquerda

            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //noroeste

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // #Jogada Especial Roque Aula 234

            if (QtdMovimentos == 0 && !partida.xeque)
            {
                //Roque pequeno
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tab.peca(p1) == null && Tab.peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                //Roque grande
                     Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                      if (testeTorreParaRoque(posT2))
                      {
                          Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                          Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                          Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                          if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                          {
                              mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                          }                
             
                }
            }
            return mat;
        }

        

        public override string ToString()
        {
            return "R";
        }
    }
}

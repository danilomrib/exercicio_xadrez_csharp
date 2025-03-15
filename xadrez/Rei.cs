using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;  // aqui definiu que a peça pode se mover quando a cor for diferente do atual e se ele estiver livre

        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas]; //fez uma matriz temporária para defiinir os movimentos

            Posicao pos = new Posicao(0,0);

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

            return mat;

        }

        public override string ToString()
        {
            return "R";
        }
    }
}

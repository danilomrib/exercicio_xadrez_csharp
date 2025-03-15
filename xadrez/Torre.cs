using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;  // aqui definiu que a peça pode se mover quando a cor for diferente do atual e se ele estiver livre

        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //definir os movimentos

            //acima

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos)) // definir a movimentação da torre, caso ela esteja no limite e possa se mover, irá realizar a ação.
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) // se estiver no limite do tabuleiro ou tiver uma peça na posição, ela irá parar
                {
                    break;
                }

                pos.Linha = pos.Linha - 1; // move a leitura para a casa adiante
            }

            //direita

            pos.definirValores(Posicao.Linha, Posicao.Coluna +1);
            while (Tab.posicaoValida(pos) && podeMover(pos)) 
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Coluna = pos.Coluna + 1; // move a leitura para a casa adiante (OBS: deve adaptar conforme a opção escolhida)
            }

            //abaixo

            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos)) 
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) 
                {
                    break;
                }

                pos.Linha = pos.Linha + 1; 
            }

            //esquerda

            pos.definirValores(Posicao.Linha, Posicao.Coluna -1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Coluna = pos.Coluna - 1;
            }



            return mat; // retorna a ação da peça.
        }

        public override string ToString()
        {
            return "T";
        }
    }
}

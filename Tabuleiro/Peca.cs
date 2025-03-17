namespace xadrez_console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.Posicao = null;
            this.Cor = cor;
            this.QtdMovimentos = 0;
            this.Tab = tab;
        }

        public void incrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }


        public void decrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }


        public bool existeMovimentosPossiveis() // Aula 224, validando os movimentos possiveis
        {
            bool[,] mat = movimentosPossiveis(); // criação da matriz temporária
            for (int i = 0; i < Tab.linhas; i++)
            {
                for (int j = 0; j < Tab.colunas; j++)
                {
                    if (mat[i, j]) // se aquela matriz for válida, irá permitir o movimento.
                        return true;
                }
            }
            return false;
        }

        public bool movimentoPossivel (Posicao pos) // Método que permite a avaliação da movimentação do destino
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna]; // retorna a ação de movimentação.
        }

        public abstract bool[,] movimentosPossiveis();

    }
}

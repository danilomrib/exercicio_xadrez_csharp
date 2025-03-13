using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QtdMovimentos { get; set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
        {
            Posicao = posicao;
            Cor = cor;
            QtdMovimentos = 0;
            Tab = tab;
        }
    }
}

using System;
namespace JogoDaVelha
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			do
			{
				var jogo = new JogoDaVelha();
				jogo.Iniciar();
				Console.WriteLine("Deseja jogar novamente? (s/n)");
			}
			while ((Console.ReadLine()?.Trim().ToLower() ?? "n").StartsWith("s"));
		}
	}
	public class JogoDaVelha
	{
		private bool fimDeJogo;
		private char[] posicoes;
		private char vez;
		private int quantidadePreenchida;
		public JogoDaVelha()
		{
			fimDeJogo = false;
			posicoes = new []{'1','2','3','4','5','6','7','8','9'};
			vez = 'X';
			quantidadePreenchida = 0;
		}
		public void Iniciar()
		{
			// Proteção extra: garantir que o tabuleiro sempre inicie limpo
			for (int i = 0; i < 9; i++)
				posicoes[i] = (char)('1' + i);
			fimDeJogo = false;
			quantidadePreenchida = 0;
			vez = 'X';
			RenderizarTabela(); // Mostra o tabuleiro inicial
			while (!fimDeJogo)
			{
				LerEscolhaDoUsuario();
				RenderizarTabela();
				VerficarFimDeJogo();
				if (!fimDeJogo) MudarVez();
			}
		}
		private void MudarVez() { vez = vez == 'X' ? 'O' : 'X'; }
		private void VerficarFimDeJogo()
		{
			if (quantidadePreenchida < 5) return;
			if (ExisteVitoriaHorizontal() || ExisteVitoriaVertical() || ExisteVitoriaDiagonal())
			{
				fimDeJogo = true;
				Console.WriteLine($"Fim de jogo!!! Vitória de {vez}");
				return;
			}
			if (quantidadePreenchida == 9)
			{
				fimDeJogo = true;
				Console.WriteLine("Fim de jogo!!! EMPATE");
			}
		}
		private bool ExisteVitoriaHorizontal()
		{
			return (posicoes[0] == posicoes[1] && posicoes[0] == posicoes[2]) ||
				   (posicoes[3] == posicoes[4] && posicoes[3] == posicoes[5]) ||
				   (posicoes[6] == posicoes[7] && posicoes[6] == posicoes[8]);
		}
		private bool ExisteVitoriaVertical()
		{
			return (posicoes[0] == posicoes[3] && posicoes[0] == posicoes[6]) ||
				   (posicoes[1] == posicoes[4] && posicoes[1] == posicoes[7]) ||
				   (posicoes[2] == posicoes[5] && posicoes[2] == posicoes[8]);
		}
		private bool ExisteVitoriaDiagonal()
		{
			return (posicoes[2] == posicoes[4] && posicoes[2] == posicoes[6]) ||
				   (posicoes[0] == posicoes[4] && posicoes[0] == posicoes[8]);
		}
		private void LerEscolhaDoUsuario()
		{
			Console.WriteLine($"Agora é a vez de {vez}, entre uma posição de 1 a 9 que esteja disponível na tabela");
			bool conversao = int.TryParse(Console.ReadLine(), out int posicaoEscolhida);
			while (!conversao || !ValidarEscolhaUsuario(posicaoEscolhida))
			{
				Console.WriteLine("O campo escolhido é inválido, por favor digite um número entre 1 e 9 que esteja disponível na tabela.");
				conversao = int.TryParse(Console.ReadLine(), out posicaoEscolhida);
			}
			PreencherEscolha(posicaoEscolhida);
		}
		private void PreencherEscolha(int posicaoEscolhida)
		{
			int indice = posicaoEscolhida - 1;
			posicoes[indice] = vez;
			quantidadePreenchida++;
		}
		private bool ValidarEscolhaUsuario(int posicaoEscolhida)
		{
			int indice = posicaoEscolhida - 1;
			return posicoes[indice] != 'O' && posicoes[indice] != 'X';
		}
		private void RenderizarTabela()
		{
			Console.Clear();
			Console.WriteLine(ObterTabela());
		}
		private string ObterTabela()
		{
			string[] display = new string[9];
			for (int i = 0; i < 9; i++)
			{
				display[i] = (posicoes[i] == 'X' || posicoes[i] == 'O') ? posicoes[i].ToString() : (i+1).ToString();
			}
			return $"__{display[0]}__|__{display[1]}__|__{display[2]}__\n" +
				   $"__{display[3]}__|__{display[4]}__|__{display[5]}__\n" +
				   $"  {display[6]}  |  {display[7]}  |  {display[8]}  \n\n";
		}
	}
}

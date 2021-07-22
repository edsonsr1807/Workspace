using ApiCatalagoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Repositorio
{
    public class JogoRepositorio : IJogoRepositorio

    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("3fa85f65-5712-4562-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("3fa85f65-5712-4562-b3fc-2c963f66afa6"), Nome = "Fifa 19", Produtora = "EA", Preco = 200 } },
            {Guid.Parse("4fa85f54-5217-4562-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("4fa85f54-5217-4562-b3fc-2c963f66afa6"), Nome = "Fifa 20", Produtora = "EA", Preco = 300 } },
            {Guid.Parse("5fa85f74-5717-4562-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("5fa85f74-5717-4562-b3fc-2c963f66afa6"), Nome = "Fifa 21", Produtora = "EA", Preco = 500 } },
            {Guid.Parse("6fa85f24-4717-4562-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("6fa85f24-4717-4562-b3fc-2c963f66afa6"), Nome = "Naruto", Produtora = "Konami", Preco = 100 } },
            {Guid.Parse("7fa80f64-5717-4560-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("7fa80f64-5717-4560-b3fc-2c963f66afa6"), Nome = "Ned For Speed", Produtora = "EA", Preco = 250 } },
            {Guid.Parse("8fa95f64-5727-4562-b3fc-2c963f66afa6") , new Jogo{ Id = Guid.Parse("8fa95f64-5727-4562-b3fc-2c963f66afa6"), Nome = "LOL", Produtora = "Riot Games", Preco = 0 } }
        };

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Sem conexão com banco
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (jogos.ContainsKey(id))
            {
                return null;
            }

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                {
                    retorno.Add(jogo);
                }
            }

            return Task.FromResult(retorno);
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
    }
}

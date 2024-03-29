﻿using ApiCatalagoJogos.Entities;
using ApiCatalagoJogos.Exceptions;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.Repositorio;
using ApiCatalagoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Services
{
    public class JogoService : IJogoService

    {
        private readonly IJogoRepositorio _jogoRepositorio;

        public JogoService(IJogoRepositorio jogoRepositorio) 
        {
            _jogoRepositorio = jogoRepositorio;
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);

            if (entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepositorio.Atualizar(entidadeJogo);

        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);

            if (entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.Preco = preco;

            await _jogoRepositorio.Atualizar(entidadeJogo);
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepositorio.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepositorio.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepositorio.Obter(id);

            if (jogo == null)
            {
                return null;
            }

            return new JogoViewModel {

                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepositorio.Obter(id);

            if (jogo == null)
            {
                throw new JogoJaCadastradoException();
            }

            await _jogoRepositorio.Remover(id);
        }


        public void Dispose()
        {
            _jogoRepositorio?.Dispose();
        }

    }

}

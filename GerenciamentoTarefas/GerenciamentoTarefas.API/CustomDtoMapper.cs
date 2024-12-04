using AutoMapper;
using GerenciamentoTarefas.Domain.DTOs;
using GerenciamentoTarefas.Domain.Models;

namespace GerenciamentoTarefas.API
{
    public class CustomDtoMapper : Profile
    {
        public CustomDtoMapper()
        {
            //Projeto
            CreateMap<ProjetoInputDto, Projeto>();

            //Tarefas
            CreateMap<TarefaInputDto, Tarefa>();
            CreateMap<TarefaUpdateDto, Tarefa>();

            //Comentario
            CreateMap<ComentarioInputDto, Comentario>();
        }
    }
}

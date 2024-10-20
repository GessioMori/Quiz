﻿using AutoMapper;
using Quiz.Models.DTO;
using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;
using Quiz.Shared.Interfaces.Services;

namespace Quiz.Services.Services
{
    public class QuestionaryService : IQuestionaryService
    {
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IMapper _mapper;

        public QuestionaryService(IQuestionaryRepository questionaryRepository,
            IMapper mapper)
        {
            this._questionaryRepository = questionaryRepository;
            this._mapper = mapper;
        }

        public async Task CreateQuestionaryAsync(QuestionaryDTO questionaryDTO)
        {
            Questionary questionary = this._mapper.Map<Questionary>(questionaryDTO);

            questionary.IsAvailable = true;
            questionary.CreatedAt = DateTime.UtcNow;

            await this._questionaryRepository.InsertQuestionaryAsync(questionary);
        }

        public async Task<List<Questionary>> GetAllQuestionariesAsync()
        {
            return await this._questionaryRepository.GetAllQuestionariesAsync();
        }

        public async Task CreateQuestionaryAnswerAsync(QuestionaryAnswerDTO questionaryAnswerDTO)
        {
            Questionary? questionary = await this._questionaryRepository
                .GetQuestionaryByIdAsync(questionaryAnswerDTO.QuestionaryId);

            if (questionary == null ||
                !new HashSet<Guid>(questionary.Questions.Select(q => q.Id))
                    .SetEquals(questionaryAnswerDTO.Answers.Select(a => a.QuestionId)))
            {
                throw new ArgumentException();
            }

            QuestionaryAnswer questionaryAnswer = this._mapper.Map<QuestionaryAnswer>(questionaryAnswerDTO);

            questionaryAnswer.CreatedAt = DateTime.UtcNow;

            await this._questionaryRepository.InsertQuestionaryAnswerAsync(questionaryAnswer);
        }

        public async Task<List<QuestionaryAnswer>> GetQuestionaryAnswersAsync()
        {
            return await this._questionaryRepository.GetQuestionaryAnswersAsync();
        }
    }
}

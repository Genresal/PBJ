﻿using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> GetAmountAsync(int amount)
        {
            var comments = await _commentRepository.GetAmountAsync(amount);

            if (comments.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.COMMENTS_EMPTY_MESSAGE);
            }

            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetAsync(int id)
        {
            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new NotFoundException(ExceptionMessages.COMMENT_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<bool> CreateAsync(CommentRequestModel commentRequestModel)
        {
            await _commentRepository.CreateAsync(_mapper.Map<Comment>(commentRequestModel));

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(int id, CommentRequestModel commentRequestModel)
        {
            var existingComment = await _commentRepository.GetAsync(id);

            if (existingComment == null)
            {
                throw new NotFoundException(ExceptionMessages.COMMENT_NOT_FOUND_MESSAGE);
            }

            existingComment = _mapper.Map<Comment>(commentRequestModel);

            existingComment.Id = id;

            await _commentRepository.UpdateAsync(existingComment);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingComment = await _commentRepository.GetAsync(id);

            if (existingComment == null)
            {
                throw new NotFoundException(ExceptionMessages.COMMENT_NOT_FOUND_MESSAGE);
            }

            await _commentRepository.DeleteAsync(existingComment);

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Commands.Comment.Update
{
    public class UpdateCommentCommandHandler : 
        IRequestHandler<UpdateCommentCommand, Infrastructure.Entities.Comment>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(ICommentRepository repository, IMapper mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }

        public async Task<Infrastructure.Entities.Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _commentRepository.GetComment(request.Id);
            if (entity.User.Id != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете изменить данный коментарий");
            return await _commentRepository.UpdateComment(_mapper.Map(request,entity));
        }
    }
}
